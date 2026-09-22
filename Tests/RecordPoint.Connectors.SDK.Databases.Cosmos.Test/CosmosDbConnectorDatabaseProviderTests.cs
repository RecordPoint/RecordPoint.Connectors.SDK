#nullable enable
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Moq;
using RecordPoint.Connectors.SDK.Configuration;
using RecordPoint.Connectors.SDK.Context;
using RecordPoint.Connectors.SDK.Observability;
using RecordPoint.Connectors.SDK.Toggles;
using Xunit;

namespace RecordPoint.Connectors.SDK.Databases.Cosmos.Test
{
    public class CosmosDbConnectorDatabaseProviderTests
    {
        // Well-known Cosmos DB Emulator connection string (public, not a real secret).
        private const string EmulatorConnectionString =
            "AccountEndpoint=https://localhost:8081/;AccountKey=C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==;";

        private readonly Mock<ISystemContext> _systemContext = new();
        private readonly Mock<ITelemetryTracker> _telemetry = new();
        private readonly Mock<IToggleProvider> _toggleProvider = new();

        private static IConfiguration BuildConfig(Dictionary<string, string?>? values = null)
            => new ConfigurationBuilder()
                .AddInMemoryCollection(values ?? new Dictionary<string, string?>())
                .Build();

        private CosmosDbConnectorDatabaseProvider CreateSut(
            CosmosDbConnectorDatabaseOptions options, IConfiguration? configuration = null)
        {
            return new CosmosDbConnectorDatabaseProvider(
                _systemContext.Object,
                configuration ?? BuildConfig(),
                _telemetry.Object,
                _toggleProvider.Object,
                Options.Create(options));
        }

        [Fact]
        public void GetConnectionString_ReturnsOptionsValue()
        {
            var sut = CreateSut(new CosmosDbConnectorDatabaseOptions { ConnectionString = EmulatorConnectionString });

            Assert.Equal(EmulatorConnectionString, sut.GetConnectionString());
        }

        [Fact]
        public void Exists_AlwaysReturnsTrue()
        {
            var sut = CreateSut(new CosmosDbConnectorDatabaseOptions { ConnectionString = EmulatorConnectionString });

            Assert.True(sut.Exists());
        }

        [Fact]
        public void CreateDbContext_WithConnectionString_DirectMode_BuildsContext()
        {
            var sut = CreateSut(new CosmosDbConnectorDatabaseOptions
            {
                ConnectionString = EmulatorConnectionString,
                DatabaseName = "connector-db",
                UseGateWayConnectionMode = false
            });

            using var ctx = sut.CreateDbContext();

            Assert.NotNull(ctx);
            Assert.IsType<CosmosDbConnectorDbContext>(ctx);
        }

        [Fact]
        public void CreateDbContext_WithConnectionString_GatewayModeForced_BuildsContext()
        {
            var sut = CreateSut(new CosmosDbConnectorDatabaseOptions
            {
                ConnectionString = EmulatorConnectionString,
                UseGateWayConnectionMode = true
            });

            using var ctx = sut.CreateDbContext();

            Assert.NotNull(ctx);
        }

        [Fact]
        public void CreateDbContext_WithDedicatedGatewayConnectionString_BuildsContext()
        {
            var gatewayCs =
                "AccountEndpoint=https://myacct.sqlx.cosmos.azure.com:8081/;AccountKey=C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==;";
            var sut = CreateSut(new CosmosDbConnectorDatabaseOptions { ConnectionString = gatewayCs });

            using var ctx = sut.CreateDbContext();

            Assert.NotNull(ctx);
        }

        [Fact]
        public void CreateDbContext_WithoutConnectionString_UsesCredentialPath()
        {
            var config = BuildConfig(new Dictionary<string, string?>
            {
                [$"{AzureAuthenticationOptions.SECTION_NAME}:TenantId"] = "tenant",
                [$"{AzureAuthenticationOptions.SECTION_NAME}:ClientId"] = "client",
                [$"{AzureAuthenticationOptions.SECTION_NAME}:ClientSecret"] = "secret"
            });
            _toggleProvider.Setup(t => t.GetToggleBool(It.IsAny<string>(), It.IsAny<bool>())).Returns(false);

            var sut = CreateSut(new CosmosDbConnectorDatabaseOptions
            {
                ConnectionString = string.Empty,
                CosmosDbAccountName = "myacct"
            }, config);

            using var ctx = sut.CreateDbContext();

            Assert.NotNull(ctx);
        }

        [Fact]
        public void CreateDbContext_WithoutConnectionString_MissingAuthSection_Throws()
        {
            var sut = CreateSut(new CosmosDbConnectorDatabaseOptions
            {
                ConnectionString = string.Empty,
                CosmosDbAccountName = "myacct"
            }, BuildConfig());

            Assert.ThrowsAny<Exception>(() => sut.CreateDbContext());
        }

        [Fact]
        public async Task PrepareAsync_WhenNotCancelled_ChecksDatabaseAndTracksSuccessTrace()
        {
            var sut = CreateSut(new CosmosDbConnectorDatabaseOptions { ConnectionString = EmulatorConnectionString });

            await sut.PrepareAsync(CancellationToken.None);

            _telemetry.Verify(t => t.TrackTrace(
                It.Is<string>(s => s.Contains("connection successful")),
                SeverityLevel.Information,
                It.IsAny<Dimensions?>()), Times.Once);
        }

        [Fact]
        public async Task PrepareAsync_WhenCancelled_DoesNothing()
        {
            var sut = CreateSut(new CosmosDbConnectorDatabaseOptions { ConnectionString = EmulatorConnectionString });

            await sut.PrepareAsync(new CancellationToken(canceled: true));

            _telemetry.Verify(t => t.TrackTrace(It.IsAny<string>(), It.IsAny<SeverityLevel>(), It.IsAny<Dimensions?>()), Times.Never);
        }

        [Fact]
        public void Dispose_IsIdempotent()
        {
            var sut = CreateSut(new CosmosDbConnectorDatabaseOptions { ConnectionString = EmulatorConnectionString });

            sut.Dispose();
            var ex = Record.Exception(() => sut.Dispose());

            Assert.Null(ex);
        }

        [Fact]
        public void GetExternalSystemName_DelegatesToSystemContext()
        {
            _systemContext.Setup(s => s.GetConnectorName()).Returns("connector-x");
            var sut = CreateSut(new CosmosDbConnectorDatabaseOptions { ConnectionString = EmulatorConnectionString });

            Assert.Equal("connector-x", sut.GetExternalSystemName());
        }

        [Fact]
        public async Task CleanupAsync_And_RemoveAsync_CompleteWithoutError()
        {
            var sut = CreateSut(new CosmosDbConnectorDatabaseOptions { ConnectionString = EmulatorConnectionString });

            var cleanup = await Record.ExceptionAsync(() => sut.CleanupAsync(CancellationToken.None));
            var remove = await Record.ExceptionAsync(() => sut.RemoveAsync(CancellationToken.None));

            Assert.Null(cleanup);
            Assert.Null(remove);
        }
    }
}
