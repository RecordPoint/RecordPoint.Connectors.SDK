using Microsoft.Extensions.Hosting;
using RecordPoint.Connectors.SDK.Test.Mock.Context;
using RecordPoint.Connectors.SDK.Observability.Null;
using RecordPoint.Connectors.SDK.Time;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using RecordPoint.Connectors.SDK.Toggles.Null;
using Microsoft.Extensions.Configuration;

namespace RecordPoint.Connectors.SDK.Test
{

    /// <summary>
    /// Base class for the common tests SUT's
    /// </summary>
    public abstract class CommonSutBase : IDisposable
    {
        #region SUT Name

        /// <summary>
        /// Get the SUT Name. Returns the test class by default
        /// </summary>
        /// <returns>Test system name</returns>
        /// <remarks>
        /// The SUT Name is used to create resources, like databases, that are specific to the test.
        /// </remarks>
        public virtual string GetSUTName() => GetType().Name;

        #endregion

        #region Temp Data Directory

        private string _tempDataDirectory;

        protected void CreateTempDataDirectory()
        {
            string tempDirectory = Path.Combine(Path.GetTempPath(), $"{GetSUTName()}_{Path.GetRandomFileName()}");
            Directory.CreateDirectory(tempDirectory);
            _tempDataDirectory = tempDirectory;
        }

        protected void DeleteTempDataDirectory()
        {
            if (!string.IsNullOrEmpty(_tempDataDirectory))
            {
                Directory.Delete(_tempDataDirectory, true);
            }
        }

        protected string GetTempDataDirectory()
        {
            return _tempDataDirectory;
        }

        #endregion

        #region Configure

        /// <summary>
        /// Override where you configure the SUT, selecting any required test resources
        /// </summary>
        protected virtual void Configure()
        {

        }

        #endregion

        #region Host

        /// <summary>
        /// Create the SUT Builder 
        /// </summary>
        /// <returns>App Host Builder</returns>
        protected virtual IHostBuilder CreateSutBuilder()
        {
            //Prevent exceeding file system watchers when running tests on devops
            //The configured user limit (128) on the number of inotify instances has been reached, or the per-process limit on the number of open file descriptors has been reached.
            Environment.SetEnvironmentVariable("DOTNET_hostBuilder__reloadConfigOnChange", "false");

            return Microsoft.Extensions.Hosting.Host
                .CreateDefaultBuilder()
                .UseSystemTime()
                .UseMockSystemContext(GetSUTName())
                .UseNullToggleProvider()
                .UseNullTelemetryTracking();
        }

        /// <summary>
        /// Set the SUT Running
        /// </summary>
        /// <returns>Task</returns>
        public virtual Task StartSUTAsync(IConfiguration configuration = null)
        {
            Configure();
            CreateTempDataDirectory();

            var hostBuilder = CreateSutBuilder();
            if (configuration != null)
            {
                hostBuilder.ConfigureAppConfiguration(builder => builder.AddConfiguration(configuration));
            }
            Host = hostBuilder.Build();
            return Host.StartAsync();
        }

        /// <summary>
        /// Stop the SUT
        /// </summary>
        /// <returns>Task</returns>
        public async virtual Task StopSUTAsync()
        {
            await Host.StopAsync(CancellationToken.None);
            DeleteTempDataDirectory();
        }

        // Accessors

        public IHost Host { get; set; }

        public IServiceProvider Services => Host.Services;

        #endregion

        #region IDisposable
        private bool disposedValue;
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    Host?.Dispose();
                    DeleteTempDataDirectory();
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        #endregion

    }
}
