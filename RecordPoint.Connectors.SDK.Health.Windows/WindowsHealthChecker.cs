using Microsoft.Extensions.Options;
using System.Management;

namespace RecordPoint.Connectors.SDK.Health.Windows
{
#pragma warning disable CA1416 // Validate platform compatibility
    /// <summary>
    /// Health Checker that just adds onprem windows health information to the health check
    /// </summary>
    public class WindowsHealthChecker : IHealthCheckStrategy
    {

        public const string STATUS_HEALTH_CHECK_TYPE = "Status";

        public string HealthCheckType => STATUS_HEALTH_CHECK_TYPE;

        private readonly IOptions<HealthCheckOptions> _healthCheckOptions;

        public WindowsHealthChecker(IOptions<HealthCheckOptions> healthCheckOptions)
        {
            _healthCheckOptions = healthCheckOptions;
        }

        public Task<List<HealthCheckItem>> HealthCheckAsync(CancellationToken stoppingToken)
        {
            var healthCheckItems = new List<HealthCheckItem>();

            healthCheckItems.AddRange(GetSystemMemoryInfo());
            healthCheckItems.AddRange(GetDiskSpaceInfo());

            return Task.FromResult(healthCheckItems);
        }

        /// <summary>
        /// Get window's memory info
        /// </summary>
        /// <returns></returns>
        private static List<HealthCheckItem> GetSystemMemoryInfo()
        {
            var winQuery = new ObjectQuery("SELECT * FROM CIM_OperatingSystem");

            var searcher = new ManagementObjectSearcher(winQuery);
            var systemMemoryInfo = new SystemMemory();
            var systemMemoryHealthCheckItems = new List<HealthCheckItem>();

            foreach (var item in searcher.Get())
            {
                systemMemoryInfo.FreePhyiscalMemory = GetMemoryGb(item, "FreePhysicalMemory");
                systemMemoryInfo.TotalPhysicalMemorySize = GetMemoryGb(item, "TotalVisibleMemorySize");
                systemMemoryInfo.FreeVirtualMemory = GetMemoryGb(item, "FreeVirtualMemory");
                systemMemoryInfo.TotalVirtualMemorySize = GetMemoryGb(item, "TotalVirtualMemorySize");

                systemMemoryHealthCheckItems.Add(new HealthCheckMeasure()
                {
                    Name = nameof(SystemMemory.TotalPhysicalMemorySize),
                    Value = systemMemoryInfo.TotalPhysicalMemorySize
                });

                systemMemoryHealthCheckItems.Add(new HealthCheckMeasure()
                {
                    Name = nameof(SystemMemory.FreePhyiscalMemory),
                    Value = systemMemoryInfo.FreePhyiscalMemory
                });

                systemMemoryHealthCheckItems.Add(new HealthCheckMeasure()
                {
                    Name = nameof(SystemMemory.TotalVirtualMemorySize),
                    Value = systemMemoryInfo.TotalVirtualMemorySize
                });

                systemMemoryHealthCheckItems.Add(new HealthCheckMeasure()
                {
                    Name = nameof(SystemMemory.FreeVirtualMemory),
                    Value = systemMemoryInfo.FreeVirtualMemory
                });
            }

            return systemMemoryHealthCheckItems;
        }

        private static long GetMemoryGb(ManagementBaseObject? managementBaseObject, string key)
        {
            if (managementBaseObject == null) return 0;
            var value = managementBaseObject[key].ToString();
            if (value == null) return 0;
            return long.Parse(value) / 1024 / 1024;
        }

        private List<HealthCheckItem> GetDiskSpaceInfo()
        {

            DriveInfo[] allDrives = DriveInfo.GetDrives();
            var systemDiskHealthCheckItems = new List<HealthCheckItem>();

            foreach (DriveInfo d in allDrives)
            {
                if (d.IsReady)
                {
                    var availableVolume = d.AvailableFreeSpace / 1024 / 1024 / 1024;
                    var volumeName = d.Name?.Replace(@"\", "");
                    systemDiskHealthCheckItems.Add(new HealthCheckMeasure()
                    {
                        Name = $"{volumeName}.AvailableSpace",
                        Value = availableVolume,  // In Gb 
                        HealthLevel = IsRootPathOrSystemDiskSpaceWarning(d.Name ?? string.Empty, availableVolume) ? HealthLevel.Warning : HealthLevel.Normal
                    });

                }
            }

            return systemDiskHealthCheckItems;
        }

        private bool IsRootPathOrSystemDiskSpaceWarning(string volumeLabel, double volumeSize)
        {
            var assembly = System.Reflection.Assembly.GetEntryAssembly();
            if (assembly == null)
            {
                throw new RequiredValueNullException(nameof(assembly));
            }
            var root = Path.GetPathRoot(assembly.Location);
            if (root == null)
            {
                throw new RequiredValueNullException(nameof(root));
            }
            var osRoot = Path.GetPathRoot(Environment.SystemDirectory);
            if (osRoot == null)
            {
                throw new RequiredValueNullException(nameof(osRoot));
            }
            if (volumeSize > _healthCheckOptions.Value.MinimumDiskSpaceInGB)
            {
                return false;
            }

            // Both os disk and application's disk need to have minimum disk space
            return root.Contains(volumeLabel) || osRoot.Contains(volumeLabel);
        }
    }
#pragma warning restore CA1416 // Validate platform compatibility
}
