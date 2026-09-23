using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RecordPoint.Connectors.SDK.Connectors;
using RecordPoint.Connectors.SDK.Test;
using RecordPoint.Connectors.SDK.Test.Mock.Databases;
using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace RecordPoint.Connectors.SDK.Databases.Test.Configuration
{
    /// <summary>
    /// SUT for Connector Configuration tests
    /// </summary>
    public class ConnectorConfigurationSUT : CommonSutBase
    {
        protected override IHostBuilder CreateSutBuilder()
            => base
                .CreateSutBuilder()
                .UseDatabaseConnectorConfigurationManager()
                .UseMockConnectorDatabase();

        public override async Task StopSUTAsync()
        {
            var databaseProvider = Services.GetRequiredService<IConnectorDatabaseProvider>();
            await databaseProvider.RemoveAsync(CancellationToken.None);
            await base.StopSUTAsync();
        }
    }

    /// <summary>
    /// Connector Configuration Tests
    /// </summary>
    public class ConnectorConfigurationTests : CommonTestBase<ConnectorConfigurationSUT>
    {
        public const string CONNECTOR_RESOURCE = "RecordPoint.Connectors.SDK.Databases.Test.Configuration.connector_configuration.json";


        [Fact]
        public async Task ConnectorConfiguration_GetNonExistingConfiguration()
        {
            await StartSutAsync();
            var configurationClient = Services.GetRequiredService<IConnectorConfigurationManager>();
            var existingConfiguration = await configurationClient.GetConnectorConfigurationAsync(string.Empty, CancellationToken.None);

            Assert.Null(existingConfiguration);
        }

        [Fact]
        public async Task ConnectorConfiguration_SetNonExistingConfiguration()
        {
            const string connectorDisplayName = "Test Connector";

            await StartSutAsync();
            var configurationClient = Services.GetRequiredService<IConnectorConfigurationManager>();
            var connectorConfigurationJson = GetEmbeddedResourceText(CONNECTOR_RESOURCE, typeof(ConnectorConfigurationTests));
            var testConfiguration = JsonSerializer.Deserialize<ConnectorConfigurationModel>(connectorConfigurationJson);
            testConfiguration.DisplayName = connectorDisplayName;

            await configurationClient.SetConnectorConfigurationAsync(testConfiguration, CancellationToken.None);

            var savedConfiguration = await configurationClient.GetConnectorConfigurationAsync(testConfiguration.ConnectorId, CancellationToken.None);

            Assert.NotNull(savedConfiguration);
            Assert.Equal(testConfiguration.DisplayName, savedConfiguration.DisplayName);
        }


        [Fact]
        public async Task ConnectorConfiguration_OverwriteExistingConfiguration()
        {
            const string connectorDisplayName1 = "Test Connector";
            const string connectorDisplayName2 = "Test Connector Modified";

            await StartSutAsync();
            var configurationClient = Services.GetRequiredService<IConnectorConfigurationManager>();

            var testConfiguration = new ConnectorConfigurationModel()
            {
                DisplayName = connectorDisplayName1
            };

            await configurationClient.SetConnectorConfigurationAsync(testConfiguration, CancellationToken.None);

            var updatedConfiguration = new ConnectorConfigurationModel()
            {
                DisplayName = connectorDisplayName2
            };

            await configurationClient.SetConnectorConfigurationAsync(updatedConfiguration, CancellationToken.None);

            var savedConfiguration = await configurationClient.GetConnectorConfigurationAsync(testConfiguration.ConnectorId, CancellationToken.None);

            Assert.NotNull(savedConfiguration);
            Assert.Equal(updatedConfiguration.DisplayName, savedConfiguration.DisplayName);
        }

        [Fact]
        public async Task ConnectorConfiguration_OverwriteExistingConfiguration_DoesNotModifyChannelDiscoveryState()
        {
            var connectorId = Guid.NewGuid().ToString();
            const string connectorDisplayName1 = "Test Connector";
            const string connectorDisplayName2 = "Test Connector Modified";
            var channelDiscoveryEnqueuedDate = DateTimeOffset.Now;

            await StartSutAsync();
            var configurationClient = Services.GetRequiredService<IConnectorConfigurationManager>();

            var testConfiguration = new ConnectorConfigurationModel()
            {
                ConnectorId = connectorId,
                DisplayName = connectorDisplayName1
            };

            await configurationClient.SetConnectorConfigurationAsync(testConfiguration, CancellationToken.None);
            await configurationClient.PatchConnectorConfigurationAsync(connectorId, connector => connector.ChannelDiscoveryEnqueuedDate = channelDiscoveryEnqueuedDate, CancellationToken.None);

            var updatedConfiguration = new ConnectorConfigurationModel()
            {
                ConnectorId = connectorId,
                DisplayName = connectorDisplayName2
            };

            await configurationClient.SetConnectorConfigurationAsync(updatedConfiguration, CancellationToken.None);

            var savedConfiguration = await configurationClient.GetConnectorConfigurationAsync(testConfiguration.ConnectorId, CancellationToken.None);

            Assert.NotNull(savedConfiguration);
            Assert.Equal(updatedConfiguration.DisplayName, savedConfiguration.DisplayName);
            Assert.Equal(channelDiscoveryEnqueuedDate, savedConfiguration.ChannelDiscoveryEnqueuedDate);
        }
    }
}