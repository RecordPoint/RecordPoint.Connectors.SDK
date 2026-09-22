#nullable enable
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RecordPoint.Connectors.SDK.Context;
using RecordPoint.Connectors.SDK.Databases;
using RecordPoint.Connectors.SDK.Observability;
using System.Collections.Generic;
using System.Threading;

namespace RecordPoint.Connectors.SDK.Databases.PostgreSql.Test
{
    /// <summary>
    /// Test-only subclass exposing the protected members of
    /// <see cref="PostgreSqlConnectorDbProvider"/> (and its base
    /// <see cref="PostgreSqlDbProvider{TDbContext}"/>) so they can be asserted
    /// without a live database.
    /// </summary>
    internal sealed class TestablePostgreSqlConnectorDbProvider : PostgreSqlConnectorDbProvider
    {
        public TestablePostgreSqlConnectorDbProvider(
            ISystemContext systemContext,
            IOptions<PostgreSqlConnectorDbOptions> databaseOptions,
            ITelemetryTracker telemetryTracker,
            IPostgreSqlConnectionFactory connectionFactory)
            : base(systemContext, databaseOptions, telemetryTracker, connectionFactory)
        {
        }

        public string PublicGetDatabaseName() => GetDatabaseName();

        public string PublicGetAdminConnectionString() => GetAdminConnectionString();

        public ConnectorDbContext PublicCreateDbAdminContext() => CreateDbAdminContext();

        public DbContextOptionsBuilder<ConnectorDbContext> PublicGetContextOptionsBuilder()
            => GetContextOptionsBuilder();

        public DbContextOptionsBuilder<ConnectorDbContext> PublicGetAdminContextOptionsBuilder()
            => GetAdminContextOptionsBuilder();

        public string PublicGetSqlDatabaseScript(string scriptName, Dictionary<string, string> parameters)
            => GetSqlDatabaseScript(scriptName, parameters);

        public void PublicCheckDatabaseExists(CancellationToken cancellationToken)
            => CheckDatabaseExists(cancellationToken);
    }
}
