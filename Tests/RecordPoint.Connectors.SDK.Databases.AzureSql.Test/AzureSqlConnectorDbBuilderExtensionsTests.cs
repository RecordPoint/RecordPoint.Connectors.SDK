#nullable enable
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using RecordPoint.Connectors.SDK.Databases;
using RecordPoint.Connectors.SDK.Observability.Null;
using RecordPoint.Connectors.SDK.Test.Mock.Context;
using RecordPoint.Connectors.SDK.Time;
using Xunit;

namespace RecordPoint.Connectors.SDK.Databases.AzureSql.Test
{
    public class AzureSqlConnectorDbBuilderExtensionsTests
    {
        private const string ConnectionString = "Server=host.invalid;Database=Db;Encrypt=False";

        private static IHost BuildHost()
        {
            // Build (but do not start) a host so the DatabaseService hosted service does not
            // attempt a real connection. Building resolves the registration lambda.
            return Host.CreateDefaultBuilder()
                .UseSystemTime()
                .UseMockSystemContext("BuilderTest")
                .UseNullTelemetryTracking()
                .ConfigureAppConfiguration(builder => builder.AddInMemoryCollection(
                    new Dictionary<string, string?>
                    {
                        [$"{AzureSqlConnectorDbOptions.SECTION_NAME}:ConnectionString"] = ConnectionString,
                        [$"{AzureSqlConnectorDbOptions.SECTION_NAME}:AdminUsername"] = "admin",
                        [$"{AzureSqlConnectorDbOptions.SECTION_NAME}:AdminPassword"] = "pw",
                    }))
                .UseAzureSqlConnectorDatabase()
                .Build();
        }

        [Fact]
        public void UseAzureSqlConnectorDatabase_RegistersConnectionFactory()
        {
            using var host = BuildHost();

            var factory = host.Services.GetService<IAzureSqlConnectionFactory>();

            Assert.IsType<AzureSqlConnectionFactory>(factory);
        }

        [Fact]
        public void UseAzureSqlConnectorDatabase_RegistersDatabaseProvider()
        {
            using var host = BuildHost();

            var provider = host.Services.GetService<IConnectorDatabaseProvider>();

            Assert.IsType<AzureSqlConnectorDbProvider>(provider);
        }

        [Fact]
        public void UseAzureSqlConnectorDatabase_RegistersDatabaseClient()
        {
            using var host = BuildHost();

            var client = host.Services.GetService<IConnectorDatabaseClient>();

            Assert.NotNull(client);
        }

        [Fact]
        public void UseAzureSqlConnectorDatabase_BindsOptionsFromConfiguration()
        {
            using var host = BuildHost();

            var options = host.Services.GetRequiredService<IOptions<AzureSqlConnectorDbOptions>>();

            Assert.Equal(ConnectionString, options.Value.ConnectionString);
            Assert.Equal("admin", options.Value.AdminUsername);
            Assert.Equal("pw", options.Value.AdminPassword);
        }

        [Fact]
        public void UseAzureSqlConnectorDatabase_ProviderExposesConfiguredConnectionString()
        {
            using var host = BuildHost();

            var provider = host.Services.GetRequiredService<IConnectorDatabaseProvider>();

            Assert.Equal(ConnectionString, provider.GetConnectionString());
        }

        [Fact]
        public void UseAzureSqlConnectorDatabase_RegistersDatabaseHostedService()
        {
            using var host = BuildHost();

            Assert.Contains(
                host.Services.GetServices<IHostedService>(),
                s => s is DatabaseService<ConnectorDbContext, IConnectorDatabaseProvider>);
        }
    }
}
