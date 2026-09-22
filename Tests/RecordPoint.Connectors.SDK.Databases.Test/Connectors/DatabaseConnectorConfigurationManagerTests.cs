using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Moq;
using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.Connectors;
using RecordPoint.Connectors.SDK.Context;
using RecordPoint.Connectors.SDK.Databases;
using RecordPoint.Connectors.SDK.Test;
using RecordPoint.Connectors.SDK.Test.Mock.Databases;
using RecordPoint.Connectors.SDK.Toggles;
using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace RecordPoint.Connectors.SDK.Databases.Test.Connectors
{
    /// <summary>
    /// SUT for Connector Extension Tests
    /// </summary>
    public class DatabaseConnectorConfigurationManagerSut : CommonSutBase
    {
        private Mock<ISystemContext> _systemContext;

        public Mock<IToggleProvider> ToggleProvider { get; private set; }
        public ConnectorOptions ConnectorOptions { get; set; } = new();

        protected override IHostBuilder CreateSutBuilder()
        {
            ToggleProvider = new Mock<IToggleProvider>();
            ToggleProvider.Setup(x => x.GetToggleBool(It.IsAny<string>(), It.IsAny<string>(), true)).Returns(true);

            _systemContext = new Mock<ISystemContext>();

            return base
                .CreateSutBuilder()
                .UseDatabaseConnectorConfigurationManager()
                .UseMockConnectorDatabase()
                .ConfigureServices(svcs =>
                    svcs.AddSingleton(_systemContext.Object)
                        .AddSingleton(Options.Create(ConnectorOptions))
                        .AddSingleton(ToggleProvider.Object)
                    );
        }

        public override async Task StopSUTAsync()
        {
            var databaseProvider = Services.GetRequiredService<IConnectorDatabaseProvider>();
            await databaseProvider.RemoveAsync(CancellationToken.None);
            await base.StopSUTAsync();
        }
    }

    /// <summary>
    /// Connector Extension Tests
    /// </summary>
    public class DatabaseConnectorConfigurationManagerTests : CommonTestBase<DatabaseConnectorConfigurationManagerSut>
    {
        private Mock<IToggleProvider> ToggleProvider => SUT.ToggleProvider;

        #region ConnectorData

        [Fact]
        public async Task MissingConnectorData_ReturnsNull()
        {
            await StartSutAsync();
            var connectorManager = Services.GetRequiredService<IConnectorConfigurationManager>();
            var connectorData = await connectorManager.GetConnectorAsync("Test", CancellationToken.None);
            Assert.Null(connectorData);
        }

        [Fact]
        public async Task SetNewConnectorData_ChangesConnectorData()
        {
            await StartSutAsync();
            var connectorManager = Services.GetRequiredService<IConnectorConfigurationManager>();
            var connectorDataModel = new ConnectorConfigurationModel
            {
                ConnectorId = nameof(SetNewConnectorData_ChangesConnectorData),
                ConnectorTypeId = "",
                TenantId = "",
                DisplayName = "",
                Status = "",
                Data = "ABC"
            };

            var cancellationToken = CancellationToken.None;

            await connectorManager.SetConnectorConfigurationAsync(connectorDataModel, cancellationToken);
            var savedConnector = await connectorManager.GetConnectorConfigurationAsync(connectorDataModel.ConnectorId, cancellationToken);
            Assert.Equal(connectorDataModel.ConnectorId, savedConnector.ConnectorId);
            Assert.Equal(connectorDataModel.ConnectorTypeId, savedConnector.ConnectorTypeId);
            Assert.Equal(connectorDataModel.TenantId, savedConnector.TenantId);
            Assert.Equal(connectorDataModel.DisplayName, savedConnector.DisplayName);
            Assert.Equal(connectorDataModel.Status, savedConnector.Status);
            Assert.Equal(connectorDataModel.Data, savedConnector.Data);
        }

        [Fact]
        public async Task ConnectorConfigurationExists_ReturnsTrue_WhenPresent()
        {
            await StartSutAsync();
            var connectorManager = Services.GetRequiredService<IConnectorConfigurationManager>();
            var connectorId = nameof(ConnectorConfigurationExists_ReturnsTrue_WhenPresent);
            await connectorManager.SetConnectorConfigurationAsync(
                CreateConnectorConfigurationModel(connectorId, "Enabled"), CancellationToken.None);

            var exists = await connectorManager.ConnectorConfigurationExistsAsync(connectorId, CancellationToken.None);

            Assert.True(exists);
        }

        [Fact]
        public async Task ConnectorConfigurationExists_ReturnsFalse_WhenMissing()
        {
            await StartSutAsync();
            var connectorManager = Services.GetRequiredService<IConnectorConfigurationManager>();

            var exists = await connectorManager.ConnectorConfigurationExistsAsync(
                Guid.NewGuid().ToString(), CancellationToken.None);

            Assert.False(exists);
        }

        [Fact]
        public async Task GetConnectorConfiguration_UsesCache_WhenTtlPositive()
        {
            SUT.ConnectorOptions = new ConnectorOptions
            {
                ConnectorConfigurationCacheTtl = 60
            };
            await StartSutAsync();

            var connectorManager = Services.GetRequiredService<IConnectorConfigurationManager>();
            var connectorId = nameof(GetConnectorConfiguration_UsesCache_WhenTtlPositive);
            var model = CreateConnectorConfigurationModel(connectorId, "Enabled");
            await connectorManager.SetConnectorConfigurationAsync(model, CancellationToken.None);

            // First read is a cache miss: it loads from the database and populates the cache.
            var first = await connectorManager.GetConnectorConfigurationAsync(connectorId, CancellationToken.None);

            // Delete the backing row directly via the DbContext, bypassing the manager so the
            // cache is NOT invalidated. If the second read hit the database it would now return null.
            var databaseClient = Services.GetRequiredService<IConnectorDatabaseClient>();
            using (var dbContext = databaseClient.CreateDbContext())
            {
                var row = await dbContext.Connectors
                    .FirstOrDefaultAsync(a => a.ConnectorId == connectorId, CancellationToken.None);
                Assert.NotNull(row);
                dbContext.Connectors.Remove(row);
                await dbContext.SaveChangesAsync(CancellationToken.None);
            }

            // Confirm the row really is gone from the database.
            using (var verifyContext = databaseClient.CreateDbContext())
            {
                var deleted = await verifyContext.Connectors
                    .FirstOrDefaultAsync(a => a.ConnectorId == connectorId, CancellationToken.None);
                Assert.Null(deleted);
            }

            // Second read must STILL return the original value from the cache, proving the cache
            // path was taken rather than the (now-empty) database.
            var second = await connectorManager.GetConnectorConfigurationAsync(connectorId, CancellationToken.None);

            Assert.NotNull(first);
            Assert.NotNull(second);
            Assert.Equal(connectorId, first.ConnectorId);
            Assert.Equal(connectorId, second.ConnectorId);
            Assert.Equal(first.Data, second.Data);
            Assert.Equal(model.Status, second.Status);
        }

        [Fact]
        public async Task DeleteExistingConnectorData_RemovesConnectorData()
        {
            await StartSutAsync();
            var connectorManager = Services.GetRequiredService<IConnectorConfigurationManager>();
            var connectorDataModel = new ConnectorConfigurationModel
            {
                ConnectorId = nameof(DeleteExistingConnectorData_RemovesConnectorData),
                ConnectorTypeId = "",
                TenantId = "",
                DisplayName = "",
                Status = "",
                Data = "ABC"
            };

            var cancellationToken = CancellationToken.None;

            await connectorManager.SetConnectorConfigurationAsync(connectorDataModel, cancellationToken);
            await connectorManager.DeleteConnectorConfigurationAsync(connectorDataModel.ConnectorId, cancellationToken);

            var retrievedConnectorData = await connectorManager.GetConnectorConfigurationAsync(connectorDataModel.ConnectorId, cancellationToken);
            Assert.Null(retrievedConnectorData);
        }

        [Fact]
        public async Task DeleteMissingConnectorData_DoesNothing()
        {
            await StartSutAsync();
            var connectorManager = Services.GetRequiredService<IConnectorConfigurationManager>();
            var connectorId = nameof(DeleteMissingConnectorData_DoesNothing);

            // Deleting a connector that does not exist should complete without throwing.
            Assert.Null(await Record.ExceptionAsync(
                () => connectorManager.DeleteConnectorConfigurationAsync(connectorId, CancellationToken.None)));
        }

        [Fact]
        public async Task SetNewConnectorNotificationModel_ChangesConnectorData()
        {
            await StartSutAsync();

            var connectorManager = Services.GetRequiredService<IConnectorConfigurationManager>();
            var connectorId = Guid.Empty.ToString();
            var connectorConfigModel = new ConnectorConfigModel("", "", false, connectorId, "", "", "", "", DateTime.UtcNow.AddDays(-2), DateTime.UtcNow.AddDays(-1), "", "", "");
            var connectorNotificationModel = new ConnectorNotificationModel("", "", DateTime.UtcNow, "", connectorId, null, connectorConfigModel);

            var cancellationToken = CancellationToken.None;
            await connectorManager.SetConnectorAsync(connectorNotificationModel.ConnectorConfig, cancellationToken);
            var savedConnector = await connectorManager.GetConnectorConfigurationAsync(connectorId, cancellationToken);
            if (savedConnector == null)
            {
                throw new RequiredValueNullException(nameof(savedConnector));
            }
            Assert.NotNull(savedConnector);
            Assert.Equal(connectorId, savedConnector.ConnectorId);
        }

        #endregion

        #region Connector Status

        [Fact]
        public async Task IsConnectorMissing_ConnectorStatusReportsMissing()
        {
            await StartSutAsync();

            var cancellationToken = CancellationToken.None;
            var connectorId = nameof(IsConnectorMissing_ConnectorStatusReportsMissing);
            var connectorManager = Services.GetRequiredService<IConnectorConfigurationManager>();
            var connectorStatus = await connectorManager.GetConnectorStatusAsync(connectorId, cancellationToken);

            Assert.NotNull(connectorStatus);
            Assert.Equal(connectorId, connectorStatus.ConnectorId);
            Assert.False(connectorStatus.Enabled);
            Assert.Equal(DatabaseConnectorConfigurationManager.CONNECTOR_FEATURE, connectorStatus.FeatureName);
            Assert.Equal(DatabaseConnectorConfigurationManager.CONNECTOR_NOT_FOUND_REASON, connectorStatus.EnabledReason);
        }

        protected static ConnectorConfigurationModel CreateConnectorConfigurationModel(string connectorId, string status)
        {
            var connectorDisplayName = $"{connectorId} Name";
            var connectorTypeId = $"{connectorId}_Type";
            var tenantId = $"{connectorId}_Tenant";

            var connectorConfigModel = new ConnectorConfigModel
            {
                Id = connectorId,
                ConnectorTypeId = connectorTypeId,
                TenantId = tenantId
            };

            var connectorDataModel = new ConnectorConfigurationModel
            {
                ConnectorId = connectorId,
                ConnectorTypeId = connectorTypeId,
                TenantId = tenantId,
                DisplayName = connectorDisplayName,
                Status = status,
                Data = JsonSerializer.Serialize(connectorConfigModel)
            };
            return connectorDataModel;
        }

        [Fact]
        public async Task IfConnectorDisabled_ConnectorStatusIsDisabled()
        {
            await StartSutAsync();

            var cancellationToken = CancellationToken.None;
            var connectorManager = Services.GetRequiredService<IConnectorConfigurationManager>();

            var connectorId = nameof(IfConnectorDisabled_ConnectorStatusIsDisabled);
            var connectorConfigurationModel = CreateConnectorConfigurationModel(connectorId, "Disabled");
            await connectorManager.SetConnectorConfigurationAsync(connectorConfigurationModel, cancellationToken);

            var connectorStatus = await connectorManager.GetConnectorStatusAsync(connectorId, cancellationToken);

            Assert.NotNull(connectorStatus);
            Assert.Equal(connectorId, connectorStatus.ConnectorId);
            Assert.False(connectorStatus.Enabled);
            Assert.Equal(DatabaseConnectorConfigurationManager.CONNECTOR_DISABLED_REASON, connectorStatus.EnabledReason);
        }

        [Fact]
        public async Task IfConnectorDisabled_DisabledTime_Property_IsPopulated()
        {
            await StartSutAsync();

            var cancellationToken = CancellationToken.None;
            var connectorManager = Services.GetRequiredService<IConnectorConfigurationManager>();

            var connectorId = nameof(IfConnectorDisabled_DisabledTime_Property_IsPopulated);
            var connectorConfigurationModel = CreateConnectorConfigurationModel(connectorId, "Disabled");
            await connectorManager.SetConnectorConfigurationAsync(connectorConfigurationModel, cancellationToken);

            var persistedConnector = await connectorManager.GetConnectorAsync(connectorId, cancellationToken);
            var disabledTime = persistedConnector.GetPropertyOrDefault("DisabledTime");

            Assert.NotNull(persistedConnector);
            Assert.Equal(connectorId, persistedConnector.Id);
            Assert.False(string.IsNullOrEmpty(disabledTime));
        }

        [Fact]
        public async Task IfDisabledConnector_IsUpdated_DisabledTime_Property_IsUnchanged()
        {
            await StartSutAsync();

            var cancellationToken = CancellationToken.None;
            var connectorManager = Services.GetRequiredService<IConnectorConfigurationManager>();

            var connectorId = nameof(IfDisabledConnector_IsUpdated_DisabledTime_Property_IsUnchanged);
            var connectorConfigurationModel = CreateConnectorConfigurationModel(connectorId, "Disabled");
            await connectorManager.SetConnectorConfigurationAsync(connectorConfigurationModel, cancellationToken);

            var persistedConnector1 = await connectorManager.GetConnectorAsync(connectorId, cancellationToken);
            var disabledTime1 = persistedConnector1.GetPropertyOrDefault("DisabledTime");

            connectorConfigurationModel = CreateConnectorConfigurationModel(connectorId, "Disabled");
            await connectorManager.SetConnectorConfigurationAsync(connectorConfigurationModel, cancellationToken);

            var persistedConnector2 = await connectorManager.GetConnectorAsync(connectorId, cancellationToken);
            var disabledTime2 = persistedConnector2.GetPropertyOrDefault("DisabledTime");

            Assert.NotNull(persistedConnector1);
            Assert.NotNull(persistedConnector2);
            Assert.Equal(connectorId, persistedConnector1.Id);
            Assert.Equal(connectorId, persistedConnector2.Id);
            Assert.False(string.IsNullOrEmpty(disabledTime1));
            Assert.False(string.IsNullOrEmpty(disabledTime2));
            Assert.Equal(disabledTime1, disabledTime2);
        }

        [Fact]
        public async Task IfDisabledConnector_IsReenabled_DisabledTime_Property_IsRemoved()
        {
            await StartSutAsync();

            var cancellationToken = CancellationToken.None;
            var connectorManager = Services.GetRequiredService<IConnectorConfigurationManager>();

            var connectorId = nameof(IfDisabledConnector_IsReenabled_DisabledTime_Property_IsRemoved);
            var connectorConfigurationModel = CreateConnectorConfigurationModel(connectorId, "Disabled");
            await connectorManager.SetConnectorConfigurationAsync(connectorConfigurationModel, cancellationToken);

            connectorConfigurationModel = CreateConnectorConfigurationModel(connectorId, "Enabled");
            await connectorManager.SetConnectorConfigurationAsync(connectorConfigurationModel, cancellationToken);

            var persistedConnector = await connectorManager.GetConnectorAsync(connectorId, cancellationToken);
            var disabledTime = persistedConnector.GetPropertyOrDefault("DisabledTime");

            Assert.NotNull(persistedConnector);
            Assert.Equal(connectorId, persistedConnector.Id);
            Assert.True(string.IsNullOrEmpty(disabledTime));
        }

        [Fact]
        public async Task IfConnectorEnabled_ConnectorStatusIsEnabled()
        {
            await StartSutAsync();

            var cancellationToken = CancellationToken.None;
            var connectorId = nameof(IfConnectorEnabled_ConnectorStatusIsEnabled);
            var connectorConfigurationModel = CreateConnectorConfigurationModel(connectorId, "Enabled");
            var connectorManager = Services.GetRequiredService<IConnectorConfigurationManager>();
            await connectorManager.SetConnectorConfigurationAsync(connectorConfigurationModel, cancellationToken);

            var connectorStatus = await connectorManager.GetConnectorStatusAsync(connectorId, cancellationToken);

            Assert.NotNull(connectorStatus);
            Assert.Equal(connectorId, connectorStatus.ConnectorId);
            Assert.True(connectorStatus.Enabled);
            Assert.Equal(DatabaseConnectorConfigurationManager.CONNECTOR_ENABLED_REASON, connectorStatus.EnabledReason);
        }

        [Fact]
        public async Task IfConnectorToggleDisabled_ConnectorStatusIsFeatureDisabled()
        {
            await StartSutAsync();

            var cancellationToken = CancellationToken.None;
            var connectorId = nameof(IfConnectorToggleDisabled_ConnectorStatusIsFeatureDisabled);
            var connectorConfigurationModel = CreateConnectorConfigurationModel(connectorId, "Enabled");
            var connectorManager = Services.GetRequiredService<IConnectorConfigurationManager>();
            await connectorManager.SetConnectorConfigurationAsync(connectorConfigurationModel, cancellationToken);

            // The GetConnectorEnabled toggle uses a default of true; force it off.
            ToggleProvider.Setup(x => x.GetToggleBool(It.IsAny<string>(), It.IsAny<string>(), true)).Returns(false);

            var connectorStatus = await connectorManager.GetConnectorStatusAsync(connectorId, cancellationToken);

            Assert.NotNull(connectorStatus);
            Assert.Equal(connectorId, connectorStatus.ConnectorId);
            Assert.False(connectorStatus.Enabled);
            Assert.Equal(DatabaseConnectorConfigurationManager.CONNECTOR_FEATURE_DISABLED_REASON, connectorStatus.EnabledReason);
        }

        #endregion

        #region Submission Status

        [Fact]
        public async Task IfConnectorEnabled_SubmissionIsEnabled()
        {
            await StartSutAsync();

            var cancellationToken = CancellationToken.None;
            var connectorId = nameof(IfConnectorEnabled_SubmissionIsEnabled);
            var connectorConfigurationModel = CreateConnectorConfigurationModel(connectorId, "Enabled");
            var connectorManager = Services.GetRequiredService<IConnectorConfigurationManager>();
            await connectorManager.SetConnectorConfigurationAsync(connectorConfigurationModel, cancellationToken);

            var status = await connectorManager.GetSubmissionStatusAsync(connectorId, cancellationToken);

            Assert.NotNull(status);
            Assert.Equal(connectorId, status.ConnectorId);
            Assert.True(status.Enabled);
            Assert.Equal(DatabaseConnectorConfigurationManager.SUBMISSION_FEATURE, status.FeatureName);
            Assert.Equal(DatabaseConnectorConfigurationManager.SUBMISSION_ENABLED_REASON, status.EnabledReason);
        }

        [Fact]
        public async Task IfSubmissionAppsettingFalse_SubmissionIsDisabled()
        {
            SUT.ConnectorOptions = new ConnectorOptions()
            {
                SubmissionEnabled = false
            };
            await StartSutAsync();

            var cancellationToken = CancellationToken.None;
            var connectorId = nameof(IfSubmissionAppsettingFalse_SubmissionIsDisabled);
            var connectorConfigurationModel = CreateConnectorConfigurationModel(connectorId, "Enabled");
            var connectorManager = Services.GetRequiredService<IConnectorConfigurationManager>();
            await connectorManager.SetConnectorConfigurationAsync(connectorConfigurationModel, cancellationToken);

            var status = await connectorManager.GetSubmissionStatusAsync(connectorId, cancellationToken);

            Assert.NotNull(status);
            Assert.Equal(connectorId, status.ConnectorId);
            Assert.Equal(DatabaseConnectorConfigurationManager.SUBMISSION_FEATURE, status.FeatureName);
            Assert.False(status.Enabled);
            Assert.Equal(DatabaseConnectorConfigurationManager.SUBMISSION_APPSETTING_OFF_REASON, status.EnabledReason);
        }

        [Fact]
        public async Task IfSubmissionAppsettingTrueAndConnectorDisabled_SubmissionIsDisabled()
        {
            SUT.ConnectorOptions = new ConnectorOptions()
            {
                SubmissionEnabled = true
            };
            await StartSutAsync();

            var cancellationToken = CancellationToken.None;
            var connectorId = nameof(IfSubmissionAppsettingTrueAndConnectorDisabled_SubmissionIsDisabled);
            var connectorConfigurationModel = CreateConnectorConfigurationModel(connectorId, "Disabled");
            var connectorManager = Services.GetRequiredService<IConnectorConfigurationManager>();
            await connectorManager.SetConnectorConfigurationAsync(connectorConfigurationModel, cancellationToken);

            var status = await connectorManager.GetSubmissionStatusAsync(connectorId, cancellationToken);

            Assert.NotNull(status);
            Assert.Equal(connectorId, status.ConnectorId);
            Assert.Equal(DatabaseConnectorConfigurationManager.SUBMISSION_FEATURE, status.FeatureName);
            Assert.False(status.Enabled);
            Assert.Equal(DatabaseConnectorConfigurationManager.CONNECTOR_DISABLED_REASON, status.EnabledReason);
        }

        #endregion

        #region Binary Submission Status

        [Fact]
        public async Task IfConnectorEnabled_BinarySubmissionIsEnabled()
        {
            await StartSutAsync();

            var cancellationToken = CancellationToken.None;
            var connectorId = nameof(IfConnectorEnabled_BinarySubmissionIsEnabled);
            var connectorConfigurationModel = CreateConnectorConfigurationModel(connectorId, "Enabled");
            var connectorManager = Services.GetRequiredService<IConnectorConfigurationManager>();
            await connectorManager.SetConnectorConfigurationAsync(connectorConfigurationModel, cancellationToken);

            var status = await connectorManager.GetBinarySubmissionStatusAsync(connectorId, cancellationToken);

            Assert.NotNull(status);
            Assert.Equal(connectorId, status.ConnectorId);
            Assert.True(status.Enabled);
            Assert.Equal(DatabaseConnectorConfigurationManager.BINARY_SUBMISSION_FEATURE, status.FeatureName);
            Assert.Equal(DatabaseConnectorConfigurationManager.BINARY_SUBMISSION_ENABLED_REASON, status.EnabledReason);
        }

        [Fact]
        public async Task IfBinarySubmissionAppsettingFalse_BinarySubmissionIsDisabled()
        {
            SUT.ConnectorOptions = new ConnectorOptions()
            {
                BinariesEnabled = false
            };
            await StartSutAsync();

            var cancellationToken = CancellationToken.None;
            var connectorId = nameof(IfBinarySubmissionAppsettingFalse_BinarySubmissionIsDisabled);
            var connectorConfigurationModel = CreateConnectorConfigurationModel(connectorId, "Enabled");
            var connectorManager = Services.GetRequiredService<IConnectorConfigurationManager>();
            await connectorManager.SetConnectorConfigurationAsync(connectorConfigurationModel, cancellationToken);

            var status = await connectorManager.GetBinarySubmissionStatusAsync(connectorId, cancellationToken);

            Assert.NotNull(status);
            Assert.Equal(connectorId, status.ConnectorId);
            Assert.Equal(DatabaseConnectorConfigurationManager.BINARY_SUBMISSION_FEATURE, status.FeatureName);
            Assert.False(status.Enabled);
            Assert.Equal(DatabaseConnectorConfigurationManager.BINARY_APPSETTING_OFF_REASON, status.EnabledReason);
        }

        // Check that the submission app setting overrides the binary submission app setting
        [Fact]
        public async Task IfSubmissionAppsettingFalse_BinarySubmissionIsDisabled()
        {
            SUT.ConnectorOptions = new ConnectorOptions()
            {
                SubmissionEnabled = false
            };
            await StartSutAsync();

            var cancellationToken = CancellationToken.None;
            var connectorId = nameof(IfSubmissionAppsettingFalse_BinarySubmissionIsDisabled);
            var connectorConfigurationModel = CreateConnectorConfigurationModel(connectorId, "Enabled");
            var connectorManager = Services.GetRequiredService<IConnectorConfigurationManager>();
            await connectorManager.SetConnectorConfigurationAsync(connectorConfigurationModel, cancellationToken);

            var status = await connectorManager.GetBinarySubmissionStatusAsync(connectorId, cancellationToken);

            Assert.NotNull(status);
            Assert.Equal(connectorId, status.ConnectorId);
            Assert.Equal(DatabaseConnectorConfigurationManager.BINARY_SUBMISSION_FEATURE, status.FeatureName);
            Assert.False(status.Enabled);
            Assert.Equal(DatabaseConnectorConfigurationManager.SUBMISSION_APPSETTING_OFF_REASON, status.EnabledReason);
        }


        [Fact]
        public async Task IfBinarySubmissionAppsettingTrueAndConnectorDisabled_BinarySubmissionIsDisabled()
        {
            SUT.ConnectorOptions = new ConnectorOptions()
            {
                BinariesEnabled = true
            };
            await StartSutAsync();

            var cancellationToken = CancellationToken.None;
            var connectorId = nameof(IfBinarySubmissionAppsettingTrueAndConnectorDisabled_BinarySubmissionIsDisabled);
            var connectorConfigurationModel = CreateConnectorConfigurationModel(connectorId, "Disabled");
            var connectorManager = Services.GetRequiredService<IConnectorConfigurationManager>();
            await connectorManager.SetConnectorConfigurationAsync(connectorConfigurationModel, cancellationToken);

            var status = await connectorManager.GetBinarySubmissionStatusAsync(connectorId, cancellationToken);

            Assert.NotNull(status);
            Assert.Equal(connectorId, status.ConnectorId);
            Assert.Equal(DatabaseConnectorConfigurationManager.BINARY_SUBMISSION_FEATURE, status.FeatureName);
            Assert.False(status.Enabled);
            Assert.Equal(DatabaseConnectorConfigurationManager.CONNECTOR_DISABLED_REASON, status.EnabledReason);
        }

        [Fact]
        public async Task IfBinarySubmissionAppsettingTrueAndConnectorEnabledAndKillswitchOn_BinarySubmissionIsDisabled()
        {
            SUT.ConnectorOptions = new ConnectorOptions()
            {
                BinariesEnabled = true
            };
            await StartSutAsync();

            var cancellationToken = CancellationToken.None;
            var connectorId = nameof(IfBinarySubmissionAppsettingTrueAndConnectorEnabledAndKillswitchOn_BinarySubmissionIsDisabled);
            var connectorConfigurationModel = CreateConnectorConfigurationModel(connectorId, "Enabled");

            ToggleProvider.Setup(x => x.GetToggleBool(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>())).Returns(true);
            var connectorManager = Services.GetRequiredService<IConnectorConfigurationManager>();
            await connectorManager.SetConnectorConfigurationAsync(connectorConfigurationModel, cancellationToken);

            var status = await connectorManager.GetBinarySubmissionStatusAsync(connectorId, cancellationToken);

            Assert.NotNull(status);
            Assert.Equal(connectorId, status.ConnectorId);
            Assert.Equal(DatabaseConnectorConfigurationManager.BINARY_SUBMISSION_FEATURE, status.FeatureName);
            Assert.False(status.Enabled);
            Assert.Equal(DatabaseConnectorConfigurationManager.SUBMISSION_KILLSWITCH_ON_REASON, status.EnabledReason);
        }

        #endregion

    }
}
