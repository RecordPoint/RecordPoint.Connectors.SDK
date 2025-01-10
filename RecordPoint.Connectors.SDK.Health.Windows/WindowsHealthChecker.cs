using Microsoft.Extensions.Options;
using System.Management;

namespace RecordPoint.Connectors.SDK.Health.Windows
{
#pragma warning disable CA1416 // Validate platform compatibility

    /// <summary>
    /// The windows health checker.
    /// </summary>
    public class WindowsHealthChecker : IHealthCheckStrategy
    {

        /// <summary>
        /// The STATUS HEALTH CHECK TYPE.
        /// </summary>
        public const string STATUS_HEALTH_CHECK_TYPE = "Status";

        /// <summary>
        /// Gets the health check type.
        /// </summary>
        public string HealthCheckType => STATUS_HEALTH_CHECK_TYPE;

        /// <summary>
        /// The health check options.
        /// </summary>
        private readonly IOptions<HealthCheckOptions> _healthCheckOptions;

        /// <summary>
        /// Initializes a new instance of the <see cref="WindowsHealthChecker"/> class.
        /// </summary>
        /// <param name="healthCheckOptions">The health check options.</param>
        public WindowsHealthChecker(IOptions<HealthCheckOptions> healthCheckOptions)
        {
            _healthCheckOptions = healthCheckOptions;
        }

        /// <summary>
        /// Health the check asynchronously.
        /// </summary>
        /// <param name="stoppingToken">The stopping token.</param>
        /// <returns><![CDATA[Task<HealthCheckResult>]]></returns>
        public Task<HealthCheckResult> HealthCheckAsync(CancellationToken stoppingToken)
        {
            var healthCheckResult = new HealthCheckResult();

            healthCheckResult.Measures.AddRange(GetSystemMemoryInfo());
            healthCheckResult.Measures.AddRange(GetDiskSpaceInfo());

            return Task.FromResult(healthCheckResult);
        }

        /// <summary>
        /// Get window's memory info
        /// </summary>
        /// <returns></returns>
        private static List<HealthCheckMeasure> GetSystemMemoryInfo()
        {
            var winQuery = new ObjectQuery("SELECT * FROM CIM_OperatingSystem");

            var searcher = new ManagementObjectSearcher(winQuery);
            var systemMemoryInfo = new SystemMemory();
            var systemMemoryHealthCheckItems = new List<HealthCheckMeasure>();

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

        /// <summary>
        /// Get memory gb.
        /// </summary>
        /// <param name="managementBaseObject">The management base object.</param>
        /// <param name="key">The key.</param>
        /// <returns>A long</returns>
        private static long GetMemoryGb(ManagementBaseObject? managementBaseObject, string key)
        {
            if (managementBaseObject == null) return 0;
            var value = managementBaseObject[key].ToString();
            if (value == null) return 0;
            return long.Parse(value) / 1024 / 1024;
        }

        /// <summary>
        /// Get disk space info.
        /// </summary>
        /// <returns><![CDATA[List<HealthCheckMeasure>]]></returns>
        private List<HealthCheckMeasure> GetDiskSpaceInfo()
        {

            DriveInfo[] allDrives = DriveInfo.GetDrives();
            var systemDiskHealthCheckItems = new List<HealthCheckMeasure>();

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

        /// <summary>
        /// Checks if is root path or system disk space warning.
        /// </summary>
        /// <param name="volumeLabel">The volume label.</param>
        /// <param name="volumeSize">The volume size.</param>
        /// <exception cref="RequiredValueNullException"></exception>
        /// <returns>A bool</returns>
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
