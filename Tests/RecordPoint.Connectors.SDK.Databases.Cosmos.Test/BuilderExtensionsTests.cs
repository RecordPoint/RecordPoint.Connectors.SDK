#nullable enable
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Moq;
using RecordPoint.Connectors.SDK.Caching;
using RecordPoint.Connectors.SDK.Caching.Semaphore;
using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.Configuration;
using RecordPoint.Connectors.SDK.Content;
using RecordPoint.Connectors.SDK.Databases.Cosmos.Manager;
using RecordPoint.Connectors.SDK.Databases.Cosmos.SemaphoreLock;
using RecordPoint.Connectors.SDK.Databases.Cosmos.SemephoreLock;
using RecordPoint.Connectors.SDK.Databases.SemephoreLock;
using RecordPoint.Connectors.SDK.Observability;
using RecordPoint.Connectors.SDK.Observability.Null;
using RecordPoint.Connectors.SDK.Test.Mock.Context;
using RecordPoint.Connectors.SDK.Toggles;
using RecordPoint.Connectors.SDK.Toggles.Null;
using Xunit;

namespace RecordPoint.Connectors.SDK.Databases.Cosmos.Test
{
    public class CosmosDbConnectorDatabaseBuilderExtensionsTests
    {
        private const string EmulatorConnectionString =
            "AccountEndpoint=https://localhost:8081/;AccountKey=C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==;";

        private static IHost BuildHost(Dictionary<string, string?> config)
        {
            Environment.SetEnvironmentVariable("DOTNET_hostBuilder__reloadConfigOnChange", "false");
            return Host.CreateDefaultBuilder()
                .UseMockSystemContext(nameof(CosmosDbConnectorDatabaseBuilderExtensionsTests))
                .UseNullToggleProvider()
                .UseNullTelemetryTracking()
                .ConfigureAppConfiguration(b => b.AddInMemoryCollection(config))
                .UseCosmosDbConnectorDatabase()
                .Build();
        }

        [Fact]
        public void UseCosmosDbConnectorDatabase_RegistersProvider_AndDirectAccess_WhenDirectReadsEnabled()
        {
            var config = new Dictionary<string, string?>
            {
                [$"{CosmosDbConnectorDatabaseOptions.SECTION_NAME}:ConnectionString"] = EmulatorConnectionString,
                [$"{CosmosDbConnectorDatabaseOptions.SECTION_NAME}:UseDirectReads"] = "true"
            };

            using var host = BuildHost(config);

            var provider = host.Services.GetService<IConnectorDatabaseProvider>();
            var directAccess = host.Services.GetService<IDirectChannelAccess>();
            var concrete = host.Services.GetService<CosmosDbConnectorDatabaseProvider>();

            Assert.NotNull(provider);
            Assert.NotNull(concrete);
            Assert.NotNull(directAccess);
            Assert.IsType<CosmosDirectChannelAccess>(directAccess);
        }

        [Fact]
        public void UseCosmosDbConnectorDatabase_DoesNotRegisterDirectAccess_WhenDirectReadsDisabled()
        {
            var config = new Dictionary<string, string?>
            {
                [$"{CosmosDbConnectorDatabaseOptions.SECTION_NAME}:ConnectionString"] = EmulatorConnectionString,
                [$"{CosmosDbConnectorDatabaseOptions.SECTION_NAME}:UseDirectReads"] = "false"
            };

            using var host = BuildHost(config);

            Assert.NotNull(host.Services.GetService<IConnectorDatabaseProvider>());
            Assert.Null(host.Services.GetService<IDirectChannelAccess>());
        }

        [Fact]
        public void UseCosmosDbConnectorDatabase_NoDirectAccess_WhenSectionMissing()
        {
            using var host = BuildHost(new Dictionary<string, string?>());

            Assert.Null(host.Services.GetService<IDirectChannelAccess>());
        }

        private static ServiceProvider BuildStorageServiceProvider()
        {
            var services = new ServiceCollection();
            var toggle = new Mock<IToggleProvider>();
            toggle.Setup(t => t.GetToggleBool(It.IsAny<string>(), It.IsAny<bool>())).Returns(false);
            services.AddSingleton(toggle.Object);
            services.AddSingleton(new Mock<ITelemetryTracker>().Object);
            return services.BuildServiceProvider();
        }

        [Fact]
        public void InitialiseCosmosStorage_WithConnectionString_ReturnsManager()
        {
            var sp = BuildStorageServiceProvider();
            var options = new CosmosDbConnectorDatabaseOptions
            {
                ConnectionString = EmulatorConnectionString,
                UseCamelCaseNamingPolicy = true
            };

            var manager = sp.InitialiseCosmosStorage<SemaphoreLockCosmosDbItem>(
                options, new AzureAuthenticationOptions(), "db", "container");

            Assert.NotNull(manager);
        }

        [Fact]
        public void InitialiseCosmosStorage_WithConnectionString_DefaultNamingPolicy_AndTls_ReturnsManager()
        {
            var sp = BuildStorageServiceProvider();
            var options = new CosmosDbConnectorDatabaseOptions
            {
                ConnectionString = EmulatorConnectionString,
                UseCamelCaseNamingPolicy = false,
                UseGateWayConnectionMode = true,
                TlsVersion = "Tls12"
            };

            var manager = sp.InitialiseCosmosStorage<SemaphoreLockCosmosDbItem>(
                options, new AzureAuthenticationOptions(), "db", "container");

            Assert.NotNull(manager);
        }

        [Fact]
        public void InitialiseCosmosStorage_WithInvalidTlsVersion_FallsBackToNone_ReturnsManager()
        {
            var sp = BuildStorageServiceProvider();
            var options = new CosmosDbConnectorDatabaseOptions
            {
                ConnectionString = EmulatorConnectionString,
                TlsVersion = "not-a-real-tls-version"
            };

            var manager = sp.InitialiseCosmosStorage<SemaphoreLockCosmosDbItem>(
                options, new AzureAuthenticationOptions(), "db", "container");

            Assert.NotNull(manager);
        }

        [Fact]
        public void InitialiseCosmosStorage_WithoutConnectionString_UsesCredentialEndpoint_ReturnsManager()
        {
            var sp = BuildStorageServiceProvider();
            var options = new CosmosDbConnectorDatabaseOptions
            {
                ConnectionString = string.Empty,
                CosmosDbAccountName = "myacct"
            };
            var auth = new AzureAuthenticationOptions
            {
                TenantId = "tenant",
                ClientId = "client",
                ClientSecret = "secret"
            };

            var manager = sp.InitialiseCosmosStorage<SemaphoreLockCosmosDbItem>(options, auth, "db", "container");

            Assert.NotNull(manager);
        }
    }

    public class CosmosSemaphoreLockBuilderExtensionsTests
    {
        private const string EmulatorConnectionString =
            "AccountEndpoint=https://localhost:8081/;AccountKey=C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==;";

        private static IHostBuilder BaseBuilder()
        {
            Environment.SetEnvironmentVariable("DOTNET_hostBuilder__reloadConfigOnChange", "false");
            var config = new Dictionary<string, string?>
            {
                [$"{CosmosDbConnectorDatabaseOptions.SECTION_NAME}:ConnectionString"] = EmulatorConnectionString,
                [$"{CosmosDbConnectorDatabaseOptions.SECTION_NAME}:DatabaseName"] = "connector-db"
            };
            return Host.CreateDefaultBuilder()
                .UseMockSystemContext(nameof(CosmosSemaphoreLockBuilderExtensionsTests))
                .UseNullToggleProvider()
                .UseNullTelemetryTracking()
                .ConfigureAppConfiguration(b => b.AddInMemoryCollection(config));
        }

        [Fact]
        public void UseCosmosSemaphoreLock_RegistersManagerAndLockManager()
        {
            using var host = BaseBuilder().UseCosmosSemaphoreLock().Build();

            var dbManager = host.Services.GetService<ICosmosDbManager<SemaphoreLockCosmosDbItem>>();
            var lockManager = host.Services.GetService<ISemaphoreLockManager>();

            Assert.NotNull(dbManager);
            Assert.NotNull(lockManager);
            Assert.IsType<CosmosSemaphoreLockManager>(lockManager);
        }

        [Fact]
        public void UseCosmosSemaphoreLock_Generic_AlsoRegistersScopedKeyAction()
        {
            using var host = BaseBuilder().UseCosmosSemaphoreLock<TestScopedKeyAction>().Build();

            using var scope = host.Services.CreateScope();
            var action = scope.ServiceProvider.GetService<ISemaphoreLockScopedKeyAction>();
            var lockManager = host.Services.GetService<ISemaphoreLockManager>();

            Assert.NotNull(action);
            Assert.IsType<TestScopedKeyAction>(action);
            Assert.NotNull(lockManager);
        }

        private sealed class TestScopedKeyAction : ISemaphoreLockScopedKeyAction
        {
            public Task<string> ExecuteAsync(ConnectorConfigModel connectorConfigModel, string workType, object? context, CancellationToken cancellationToken)
                => Task.FromResult("scoped-key");
        }
    }
}
