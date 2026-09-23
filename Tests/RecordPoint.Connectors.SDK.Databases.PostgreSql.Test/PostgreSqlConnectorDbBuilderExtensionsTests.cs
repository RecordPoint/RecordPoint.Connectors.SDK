#nullable enable
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using RecordPoint.Connectors.SDK.Databases;
using RecordPoint.Connectors.SDK.Observability.Null;
using RecordPoint.Connectors.SDK.Test.Mock.Context;
using RecordPoint.Connectors.SDK.Time;
using System.Collections.Generic;
using Xunit;

namespace RecordPoint.Connectors.SDK.Databases.PostgreSql.Test
{
    public class PostgreSqlConnectorDbBuilderExtensionsTests
    {
        private static IHost BuildHost()
        {
            var hostBuilder = Host
                .CreateDefaultBuilder()
                .ConfigureAppConfiguration(builder =>
                    builder.AddInMemoryCollection(new Dictionary<string, string?>
                    {
                        ["PostgreSqlConnectorDatabase:ConnectionString"] = "Server=localhost;Database=mydb;User Id=svc;Password=pw",
                        ["PostgreSqlConnectorDatabase:AdminUsername"] = "admin",
                        ["PostgreSqlConnectorDatabase:AdminPassword"] = "adminpw"
                    }))
                .UseSystemTime()
                .UseMockSystemContext(nameof(PostgreSqlConnectorDbBuilderExtensionsTests))
                .UseNullTelemetryTracking()
                .UsePostgreSqlConnectorDatabase();

            return hostBuilder.Build();
        }

        [Fact]
        public void UsePostgreSqlConnectorDatabase_RegistersConnectorDatabaseProvider()
        {
            using var host = BuildHost();

            var provider = host.Services.GetRequiredService<IConnectorDatabaseProvider>();

            Assert.IsType<PostgreSqlConnectorDbProvider>(provider);
        }

        [Fact]
        public void UsePostgreSqlConnectorDatabase_RegistersConnectionFactory()
        {
            using var host = BuildHost();

            var factory = host.Services.GetRequiredService<IPostgreSqlConnectionFactory>();

            Assert.IsType<PostgreSqlConnectionFactory>(factory);
        }

        [Fact]
        public void UsePostgreSqlConnectorDatabase_RegistersDatabaseClient()
        {
            using var host = BuildHost();

            var client = host.Services.GetRequiredService<IConnectorDatabaseClient>();

            Assert.NotNull(client);
        }

        [Fact]
        public void UsePostgreSqlConnectorDatabase_BindsOptionsFromConfiguration()
        {
            using var host = BuildHost();

            var options = host.Services.GetRequiredService<IOptions<PostgreSqlConnectorDbOptions>>();

            Assert.Equal("Server=localhost;Database=mydb;User Id=svc;Password=pw", options.Value.ConnectionString);
            Assert.Equal("admin", options.Value.AdminUsername);
            Assert.Equal("adminpw", options.Value.AdminPassword);
        }

        [Fact]
        public void UsePostgreSqlConnectorDatabase_RegistersHostedDatabaseService()
        {
            using var host = BuildHost();

            var hostedServices = host.Services.GetServices<IHostedService>();

            Assert.Contains(hostedServices, s => s.GetType().Name.StartsWith("DatabaseService"));
        }
    }
}
