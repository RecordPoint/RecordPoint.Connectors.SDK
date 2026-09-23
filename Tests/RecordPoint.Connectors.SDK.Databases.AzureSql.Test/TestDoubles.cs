#nullable enable
using System.Collections.Generic;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Moq;
using RecordPoint.Connectors.SDK.Context;
using RecordPoint.Connectors.SDK.Databases;
using RecordPoint.Connectors.SDK.Observability;

namespace RecordPoint.Connectors.SDK.Databases.AzureSql.Test
{
    /// <summary>
    /// Test subclass that exposes the protected members of
    /// <see cref="AzureSqlConnectorDbProvider"/> so they can be exercised directly
    /// without a live SQL Server instance.
    /// </summary>
    internal sealed class TestableAzureSqlConnectorDbProvider : AzureSqlConnectorDbProvider
    {
        public TestableAzureSqlConnectorDbProvider(
            ISystemContext systemContext,
            IOptions<AzureSqlConnectorDbOptions> databaseOptions,
            ITelemetryTracker telemetryTracker,
            IAzureSqlConnectionFactory connectionFactory)
            : base(systemContext, databaseOptions, telemetryTracker, connectionFactory)
        {
        }

        public string CallGetDatabaseName() => GetDatabaseName();

        public string CallGetAdminConnectionString() => GetAdminConnectionString();

        public ConnectorDbContext CallCreateDbAdminContext() => CreateDbAdminContext();

        public DbContextOptionsBuilder<ConnectorDbContext> CallGetAdminContextOptionsBuilder()
            => GetAdminContextOptionsBuilder();

        public DbContextOptionsBuilder<ConnectorDbContext> CallGetContextOptionsBuilder()
            => GetContextOptionsBuilder();

        public string CallGetSqlDatabaseScript(string scriptName, Dictionary<string, string> parameters)
            => GetSqlDatabaseScript(scriptName, parameters);

        public void CallCheckDatabaseExists(CancellationToken cancellationToken)
            => CheckDatabaseExists(cancellationToken);
    }

    /// <summary>
    /// Test subclass that exposes the protected GetSchema override of
    /// <see cref="AzureSqlConnectorDbContext"/>.
    /// </summary>
    internal sealed class TestableAzureSqlConnectorDbContext : AzureSqlConnectorDbContext
    {
        public TestableAzureSqlConnectorDbContext(DbContextOptions<ConnectorDbContext> options, string schemaName)
            : base(options, schemaName)
        {
        }

        public string CallGetSchema() => base.GetSchema();
    }

    /// <summary>
    /// Helpers for constructing providers under test.
    /// </summary>
    internal static class TestFactory
    {
        // A connection string that never resolves so that offline CanConnect() returns
        // false quickly (invalid TLD -> DNS failure, plus a 1s connect timeout backstop).
        public const string UnreachableConnectionString =
            "Server=unreachable-host.invalid;Database=ConnectorTestDb;User Id=svc;Password=pw;Connect Timeout=1;Encrypt=False;TrustServerCertificate=True";

        public static IOptions<AzureSqlConnectorDbOptions> Options(
            string? connectionString = UnreachableConnectionString,
            string adminUsername = "",
            string adminPassword = "")
        {
            return Microsoft.Extensions.Options.Options.Create(new AzureSqlConnectorDbOptions
            {
                ConnectionString = connectionString!,
                AdminUsername = adminUsername,
                AdminPassword = adminPassword,
            });
        }

        public static Mock<ISystemContext> SystemContext(string connectorName = "TestConnector")
        {
            var mock = new Mock<ISystemContext>();
            mock.Setup(x => x.GetConnectorName()).Returns(connectorName);
            return mock;
        }

        public static TestableAzureSqlConnectorDbProvider CreateProvider(
            IOptions<AzureSqlConnectorDbOptions>? options = null,
            ISystemContext? systemContext = null,
            ITelemetryTracker? telemetryTracker = null,
            IAzureSqlConnectionFactory? connectionFactory = null)
        {
            return new TestableAzureSqlConnectorDbProvider(
                systemContext ?? SystemContext().Object,
                options ?? Options(),
                telemetryTracker ?? new Mock<ITelemetryTracker>().Object,
                connectionFactory ?? new AzureSqlConnectionFactory());
        }
    }
}
