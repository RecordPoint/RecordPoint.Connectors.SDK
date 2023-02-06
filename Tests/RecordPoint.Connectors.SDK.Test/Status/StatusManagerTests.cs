using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RecordPoint.Connectors.SDK.Configuration;
using RecordPoint.Connectors.SDK.Connectors;
using RecordPoint.Connectors.SDK.Status;
using Xunit;
using RecordPoint.Connectors.SDK.Test.Mock.Databases;
using RecordPoint.Connectors.SDK.Databases;

namespace RecordPoint.Connectors.SDK.Test.Status
{
    /// <summary>
    /// SUT for Status Manager tests
    /// </summary>
    public class StatusManagerSUT : CommonSutBase
    {
        protected override IHostBuilder CreateSutBuilder() 
            => base
                .CreateSutBuilder()
                .UseStatusManager()
                .UseDatabaseConnectorConfigurationManager()
                .UseMockConnectorDatabase()
                .ConfigureServices(s => s
                    .AddSingleton<IR365ConfigurationClient, AppSettingsR365ConfigurationClient>()
                    .AddSingleton<IStatusStrategy, TestStrategyA>()
                    .AddSingleton<IStatusStrategy, TestStrategyB>()
                );

        public override async Task StopSUTAsync()
        {
            var databaseProvider = Services.GetRequiredService<IConnectorDatabaseProvider>();
            await databaseProvider.RemoveAsync(CancellationToken.None);
            await base.StopSUTAsync();
        }
    }


    /// <summary>
    /// Status Manager Tests
    /// </summary>
    public class StatusManagerTests : CommonTestBase<StatusManagerSUT>
    {

        [Fact]
        public async Task StatusManager_StatusModelResultNotNull_NoConfig()
        {
            await StartSutAsync();
            var statusManager = Services.GetRequiredService<IStatusManager>();
            var statusModel = statusManager.GetStatusModelAsync(CancellationToken.None);

            // statusModel should not be null even if no config available
            Assert.NotNull(statusModel.Result);
        }

        [Fact]
        public async Task StatusManager_StatusModel_Returns_NoStatus_When_No_Config()
        {
            await StartSutAsync();
            var statusManager = Services.GetRequiredService<IStatusManager>();
            var statusModels = await statusManager.GetStatusModelAsync(CancellationToken.None);

            Assert.Empty(statusModels);
        }

        [Fact]
        public async Task StatusManager_StatusModel_ContainsCorrectMsg_ValidConfig()
        {
            // Arrange
            await StartSutAsync();
            var connectorManager = Services.GetRequiredService<IConnectorConfigurationManager>();
            var fakeConnectorId = Guid.NewGuid().ToString();

            // set the r365 and connector models
            var testConnector = GetTestConnectorDataModel(fakeConnectorId);

            // Act
            await connectorManager.SetConnectorConfigurationAsync(testConnector, CancellationToken.None);


            // call StatusManager
            var statusManager = Services.GetRequiredService<IStatusManager>();
            var statusModels = await statusManager.GetStatusModelAsync(CancellationToken.None);

            // Assert
            Assert.NotNull(statusModels);
            Assert.Single(statusModels);

            var statusModel = statusModels.First();
            Assert.Contains("StrategyA1", statusModel.Status);
            Assert.Contains("StrategyA2", statusModel.Status);
            Assert.Contains("StrategyB1", statusModel.Status);
            Assert.Contains("StrategyB2", statusModel.Status);
        }


        #region HelperMethods

        private static ConnectorConfigurationModel GetTestConnectorDataModel(string connectorId) => new()
        {
            ConnectorId = connectorId,
            ConnectorTypeId = Guid.NewGuid().ToString(),
            TenantId = Guid.NewGuid().ToString(),
            DisplayName = "TestConnector",
            Status = "Enabled",
            Data = "ABC"
        };
        #endregion
    }

    #region InternalStrategyClasses    
    internal class TestStrategyA : IStatusStrategy
    {
        public Task<List<string>> GetStatusText(ConnectorConfigurationModel connectorModel, CancellationToken cancellationToken)
        {
            return Task.FromResult(new List<string> { "StrategyA1", "StrategyA2" });
        }
    }

    internal class TestStrategyB : IStatusStrategy
    {
        public Task<List<string>> GetStatusText(ConnectorConfigurationModel connectorModel, CancellationToken cancellationToken)
        {
            return Task.FromResult(new List<string> { "StrategyB1", "StrategyB2" });
        }
    }
    #endregion

}