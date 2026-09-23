#nullable enable
using System;
using System.IO;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Moq;
using RecordPoint.Connectors.SDK.Context;
using RecordPoint.Connectors.SDK.Databases;
using RecordPoint.Connectors.SDK.Databases.LocalDb;
using RecordPoint.Connectors.SDK.Observability.Null;
using RecordPoint.Connectors.SDK.Time;
using Xunit;

namespace RecordPoint.Connectors.SDK.Databases.LocalDb.Test
{
    /// <summary>
    /// Unit tests for <see cref="LocalDbConnectorDatabaseBuilderExtensions"/> covering the
    /// dependency-injection registrations performed by <c>UseLocalDbConnectorDatabase</c>.
    /// </summary>
    public sealed class LocalDbConnectorDatabaseBuilderExtensionsTests
    {
        private static IHost BuildHost()
        {
            var systemContext = new Mock<ISystemContext>();
            systemContext.Setup(x => x.GetDataRootPath())
                .Returns(Path.Combine(Path.GetTempPath(), "localdb_di_" + Guid.NewGuid().ToString("N")));

            return new HostBuilder()
                .ConfigureServices(services =>
                {
                    services.AddOptions();
                    services.AddSingleton(systemContext.Object);
                })
                .UseNullTelemetryTracking()
                .UseSystemTime()
                .UseLocalDbConnectorDatabase()
                .Build();
        }

        [Fact]
        public void UseLocalDbConnectorDatabase_ReturnsSameHostBuilder()
        {
            var builder = new HostBuilder();
            var result = builder.UseLocalDbConnectorDatabase();
            Assert.Same(builder, result);
        }

        [Fact]
        public void UseLocalDbConnectorDatabase_RegistersConcreteProvider()
        {
            using var host = BuildHost();
            var provider = host.Services.GetService<LocalDbConnectorDatabaseProvider>();
            Assert.NotNull(provider);
        }

        [Fact]
        public void UseLocalDbConnectorDatabase_RegistersTelemetryWrappedProvider()
        {
            using var host = BuildHost();

            var provider = host.Services.GetService<IConnectorDatabaseProvider>();

            Assert.NotNull(provider);
            // The registration wraps the real provider with telemetry, so the resolved
            // IConnectorDatabaseProvider must not be the bare concrete provider.
            Assert.IsNotType<LocalDbConnectorDatabaseProvider>(provider);
            Assert.Contains("Telemetry", provider!.GetType().Name, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public void UseLocalDbConnectorDatabase_RegistersDatabaseClient()
        {
            using var host = BuildHost();

            var client = host.Services.GetService<IConnectorDatabaseClient>();

            Assert.NotNull(client);
            Assert.IsType<ConnectorDatabaseClient>(client);
        }

        [Fact]
        public void UseLocalDbConnectorDatabase_RegistersDatabaseHostedService()
        {
            using var host = BuildHost();

            var hostedServices = host.Services.GetServices<IHostedService>();

            Assert.Contains(hostedServices, s => s.GetType().Name.Contains("DatabaseService"));
        }
    }
}
