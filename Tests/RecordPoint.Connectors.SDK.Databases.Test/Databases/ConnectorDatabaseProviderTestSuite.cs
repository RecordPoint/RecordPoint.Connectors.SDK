using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using Xunit;
using System.IO;
using System.Linq;
using System.Threading;
using RecordPoint.Connectors.SDK.Databases;
using RecordPoint.Connectors.SDK.Observability.Null;
using RecordPoint.Connectors.SDK.Time;

namespace RecordPoint.Connectors.SDK.Test.ConnectorDatabase
{
    /// <summary>
    /// Connector database, provider test suite
    /// </summary>
    /// <remarks>
    /// These tests form a test suite for database provider itself. It differs from ConnecterDatabaseTestBase which is a base class
    /// for tests that *use* database services
    /// </remarks>
    public abstract class ConnectorDatabaseProviderTestSuite : IDisposable
    {

        private IHost _sut;
        private IServiceProvider _services;
        private string _testDataPath;
        private bool disposedValue;

        private static string CreateTemporaryDirectory()
        {
            string tempDirectory = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());
            Directory.CreateDirectory(tempDirectory);
            Directory.CreateDirectory(Path.Join(tempDirectory, "Data"));
            return tempDirectory;
        }

        /// <summary>
        /// Override to get the SUT System name
        /// </summary>
        /// <returns>Tests system name</returns>
        protected virtual string GetSUTSystemName()
        {
            return GetType().Name;
        }

        protected virtual string GetTestDataPath()
        {
            return _testDataPath;
        }

        /// <summary>
        /// Create the "Base" SUT Builder that gets customised for specific components
        /// </summary>
        /// <returns>App Host Builder</returns>
        protected IHostBuilder CreateBaseSUTBuilder()
        {
            _testDataPath = CreateTemporaryDirectory();
            var host = Host
                .CreateDefaultBuilder()
                .UseSystemTime()
                .UseNullTelemetryTracking();
            return host;
        }

        /// <summary>
        /// Customize the SUT Builder as needed for a specific database
        /// </summary>
        /// <returns>App Host Builder</returns>
        protected abstract IHostBuilder CustomizeSUTBuilder(IHostBuilder hostBuilder);

        /// <summary>
        /// Create the SUT Builder 
        /// </summary>
        /// <returns>App Host Builder</returns>
        protected IHostBuilder CreateSUTBuilder()
        {
            return CustomizeSUTBuilder(CreateBaseSUTBuilder());
        }

        /// <summary>
        /// Start the SUT
        /// </summary>
        /// <param name="hostBuilder">SUT Host builder</param>
        /// <returns>Task</returns>
        private async Task StartSUT(IHostBuilder hostBuilder)
        {
            _sut = hostBuilder.Build();
            _services = _sut.Services;
            await _sut.StartAsync();
        }

        private async Task EnsureDatabaseRemoved()
        {
            var databaseProvider = _services.GetRequiredService<IConnectorDatabaseProvider>();
            if (databaseProvider.Exists())
            {
                await databaseProvider.RemoveAsync(CancellationToken.None);
            }
        }

        /// <summary>
        /// Stop the SUT
        /// </summary>
        /// <returns></returns>
        private async Task StopSUT()
        {
            await _sut.StopAsync();
            if (Directory.Exists(_testDataPath))
            {
                Directory.Delete(_testDataPath, true);
            }
        }


        [Fact]
        public async Task PrepareDatabase_HappyPath()
        {
            try
            {
                await StartSUT(CreateSUTBuilder());
                await EnsureDatabaseRemoved();
                var databaseProvider = _services.GetRequiredService<IConnectorDatabaseProvider>();
                await databaseProvider.PrepareAsync(CancellationToken.None);
                Assert.True(databaseProvider.Exists());
            }
            finally
            {
                await StopSUT();
            }
        }

        [Fact]
        public async Task RemoveDatabase_DoesntExist()
        {
            try
            {
                await StartSUT(CreateSUTBuilder());
                await EnsureDatabaseRemoved();
                var databaseProvider = _services.GetRequiredService<IConnectorDatabaseProvider>();
                await databaseProvider.PrepareAsync(CancellationToken.None);
                await databaseProvider.RemoveAsync(CancellationToken.None);
                Assert.False(databaseProvider.Exists());
            }
            finally
            {
                await StopSUT();
            }
        }

        [Fact]
        public async Task ConnectorData_IsMapped()
        {
            try
            {
                await StartSUT(CreateSUTBuilder());
                await EnsureDatabaseRemoved();
                var databaseProvider = _services.GetRequiredService<IConnectorDatabaseProvider>();
                await databaseProvider.PrepareAsync(CancellationToken.None);
                using var dbContext = databaseProvider.CreateDbContext();
                // Querying without error passes the test
                dbContext.Connectors.FirstOrDefault();

                Assert.True(true);
            }
            finally
            {
                await StopSUT();
            }
        }

        [Fact]
        public async Task Channels_IsMapped()
        {
            try
            {
                await StartSUT(CreateSUTBuilder());
                await EnsureDatabaseRemoved();
                var databaseProvider = _services.GetRequiredService<IConnectorDatabaseProvider>();
                await databaseProvider.PrepareAsync(CancellationToken.None);
                using var dbContext = databaseProvider.CreateDbContext();
                // Querying without error passes the test
                dbContext.Channels.FirstOrDefault();

                Assert.True(true);
            }
            finally
            {
                await StopSUT();
            }
        }

        [Fact]
        public async Task ManagedWorkStatuses_IsMapped()
        {
            try
            {
                await StartSUT(CreateSUTBuilder());
                await EnsureDatabaseRemoved();
                var databaseProvider = _services.GetRequiredService<IConnectorDatabaseProvider>();
                await databaseProvider.PrepareAsync(CancellationToken.None);
                using var dbContext = databaseProvider.CreateDbContext();
                // Querying without error passes the test
                dbContext.ManagedWorkStatuses.FirstOrDefault();

                Assert.True(true);
            }
            finally
            {
                await StopSUT();
            }
        }

        #region IDisposable
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _sut.Dispose();
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        } 
        #endregion
    }
}
