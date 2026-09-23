#nullable enable
using Microsoft.Extensions.Options;
using Moq;
using RecordPoint.Connectors.SDK.Context;
using RecordPoint.Connectors.SDK.Databases;
using RecordPoint.Connectors.SDK.Observability;
using RecordPoint.Connectors.SDK.Observability.Null;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace RecordPoint.Connectors.SDK.Databases.PostgreSql.Test
{
    public class PostgreSqlDbProviderTests
    {
        private const string BaseConnectionString = "Server=localhost;Database=mydb;User Id=svc;Password=svcpw";

        // Points at an RFC 2606 reserved ".invalid" host that can never resolve, with a
        // one second connect timeout. This lets the offline/failure branches run quickly and
        // deterministically without a live PostgreSQL server, and deliberately avoids "localhost"
        // (a dev machine may run Postgres on :5432, which would make these tests flaky).
        private const string UnreachableConnectionString =
            "Server=connector-db.invalid;Database=connectordb;User Id=svc;Password=svcpw;Connect Timeout=1";

        private static TestablePostgreSqlConnectorDbProvider CreateProvider(PostgreSqlConnectorDbOptions options)
        {
            var systemContext = new Mock<ISystemContext>();
            return new TestablePostgreSqlConnectorDbProvider(
                systemContext.Object,
                Options.Create(options),
                new NullTelemetryTracker(),
                new PostgreSqlConnectionFactory());
        }

        [Fact]
        public void Constructor_WhenConnectionStringIsNull_Throws()
        {
            var options = Options.Create(new PostgreSqlConnectorDbOptions { ConnectionString = null! });

            var ex = Assert.Throws<RequiredValueNullException>(() =>
                new TestablePostgreSqlConnectorDbProvider(
                    new Mock<ISystemContext>().Object,
                    options,
                    new NullTelemetryTracker(),
                    new PostgreSqlConnectionFactory()));

            Assert.Equal("ConnectionString", ex.ParamName);
        }

        [Fact]
        public void Constructor_WhenConnectionStringIsEmpty_DoesNotThrow()
        {
            var provider = CreateProvider(new PostgreSqlConnectorDbOptions { ConnectionString = string.Empty });

            Assert.NotNull(provider);
        }

        [Fact]
        public void GetSqlDatabaseScript_SubstitutesSchemaNameParameter()
        {
            var provider = CreateProvider(new PostgreSqlConnectorDbOptions { ConnectionString = BaseConnectionString });

            var script = provider.PublicGetSqlDatabaseScript(
                "PostgreSqlSchemaCreate.sql",
                new Dictionary<string, string> { ["SchemaName"] = "connector" });

            Assert.Contains("connector", script);
            Assert.DoesNotContain("{SchemaName}", script);
        }

        [Fact]
        public void GetContextOptionsBuilder_IsConfiguredForNpgsql()
        {
            var provider = CreateProvider(new PostgreSqlConnectorDbOptions { ConnectionString = "Host=localhost;Database=mydb;Username=svc;Password=pw" });

            var builder = provider.PublicGetContextOptionsBuilder();

            Assert.True(builder.IsConfigured);
            using var context = new PostgreSqlConnectorDbContext(builder.Options, "connector");
            Assert.Contains("Npgsql", context.Database.ProviderName);
        }

        [Fact]
        public void GetAdminContextOptionsBuilder_IsConfiguredForNpgsql()
        {
            var provider = CreateProvider(new PostgreSqlConnectorDbOptions
            {
                ConnectionString = BaseConnectionString,
                AdminUsername = "admin",
                AdminPassword = "adminpw"
            });

            var builder = provider.PublicGetAdminContextOptionsBuilder();

            Assert.True(builder.IsConfigured);
            using var context = new PostgreSqlConnectorDbContext(builder.Options, "connector");
            Assert.Contains("Npgsql", context.Database.ProviderName);
        }

        [Fact]
        public void Exists_WhenHostIsUnresolvable_ReturnsFalse()
        {
            var provider = CreateProvider(new PostgreSqlConnectorDbOptions { ConnectionString = UnreachableConnectionString });

            // CanConnect() against an unresolvable host must return false (not throw) so the
            // caller can decide how to react.
            Assert.False(provider.Exists());
        }

        [Fact]
        public void CheckDatabaseExists_WhenDatabaseCannotBeConnected_ThrowsInvalidOperationException()
        {
            var provider = CreateProvider(new PostgreSqlConnectorDbOptions { ConnectionString = UnreachableConnectionString });

            var ex = Assert.Throws<InvalidOperationException>(
                () => provider.PublicCheckDatabaseExists(CancellationToken.None));

            Assert.Contains("connectordb", ex.Message);
            Assert.Contains("cannot be connected with or does not exist", ex.Message);
        }

        [Fact]
        public async Task PrepareAsync_WhenCancellationAlreadyRequested_ReturnsEarlyWithoutThrowing()
        {
            // Even with an unreachable database, a pre-cancelled token must short-circuit
            // PrepareAsync before it attempts any connectivity check, so no exception is thrown.
            var provider = CreateProvider(new PostgreSqlConnectorDbOptions { ConnectionString = UnreachableConnectionString });
            using var cts = new CancellationTokenSource();
            cts.Cancel();

            var exception = await Record.ExceptionAsync(() => provider.PrepareAsync(cts.Token));

            Assert.Null(exception);
        }

        [Fact]
        public async Task PrepareAsync_WhenHostIsUnresolvable_ThrowsInvalidOperationException()
        {
            var provider = CreateProvider(new PostgreSqlConnectorDbOptions { ConnectionString = UnreachableConnectionString });

            var ex = await Assert.ThrowsAsync<InvalidOperationException>(
                () => provider.PrepareAsync(CancellationToken.None));

            Assert.Contains("connectordb", ex.Message);
            Assert.Contains("cannot be connected with or does not exist", ex.Message);
        }
    }
}
