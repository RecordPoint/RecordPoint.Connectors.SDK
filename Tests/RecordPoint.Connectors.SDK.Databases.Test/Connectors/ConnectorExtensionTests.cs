using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.Connectors;
using RecordPoint.Connectors.SDK.Test;
using RecordPoint.Connectors.SDK.Test.Mock.Databases;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace RecordPoint.Connectors.SDK.Databases.Test.Connectors
{
    /// <summary>
    /// SUT for Connector Extension Tests
    /// </summary>
    public class ConnectorExtensionSUT : CommonSutBase
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
    /// Connector Extension Tests
    /// </summary>
    public class ConnectorExtensionTests : CommonTestBase<ConnectorExtensionSUT>
    {

        public const string CONNECTOR_RESOURCE = "RecordPoint.Connectors.SDK.Databases.Test.Connectors.connector_model.json";

        [Fact]
        public async Task SetConnectorConfiguration_SavesConnectorConfig()
        {
            await StartSutAsync();
            var connectorClient = Services.GetRequiredService<IConnectorConfigurationManager>();
            var connectorConfigurationJson = GetEmbeddedResourceText(CONNECTOR_RESOURCE, typeof(ConnectorExtensionTests));
            var originalConnectorConfiguration = JsonSerializer.Deserialize<ConnectorConfigModel>(connectorConfigurationJson);

            // Save back to json to ensure formatting is identical
            var originalJson = JsonSerializer.Serialize(originalConnectorConfiguration);
            await connectorClient.SetConnectorAsync(originalConnectorConfiguration, CancellationToken.None);

            var savedConnectorConfiguration = await connectorClient.GetConnectorAsync(originalConnectorConfiguration.Id, CancellationToken.None);
            var savedConnectorJson = JsonSerializer.Serialize(savedConnectorConfiguration);

            Assert.Equal(originalJson, savedConnectorJson);
        }

        /// <summary>
        /// Make sure that saving sets the data columns as well as the json
        /// </summary>
        [Fact]
        public async Task SetConnectorConfiguration_SetsDataColumns()
        {
            await StartSutAsync();
            var connectorClient = Services.GetRequiredService<IConnectorConfigurationManager>();
            var connectorConfigurationJson = GetEmbeddedResourceText(CONNECTOR_RESOURCE, typeof(ConnectorExtensionTests));
            var originalConnectorConfigurationuration = JsonSerializer.Deserialize<ConnectorConfigModel>(connectorConfigurationJson);
            await connectorClient.SetConnectorAsync(originalConnectorConfigurationuration, CancellationToken.None);

            var savedConnectorConfigurationuration = await connectorClient.GetConnectorAsync(originalConnectorConfigurationuration.Id, CancellationToken.None);
            Assert.Equal(originalConnectorConfigurationuration.Id, savedConnectorConfigurationuration.Id);
            Assert.Equal(originalConnectorConfigurationuration.ConnectorTypeId, savedConnectorConfigurationuration.ConnectorTypeId);
            Assert.Equal(originalConnectorConfigurationuration.DisplayName, savedConnectorConfigurationuration.DisplayName);
            Assert.Equal(originalConnectorConfigurationuration.Status, savedConnectorConfigurationuration.Status);
            Assert.Equal(originalConnectorConfigurationuration.TenantId, savedConnectorConfigurationuration.TenantId);
        }

        /// <summary>
        /// Can we list saved connectors
        /// </summary>
        [Fact]
        public async Task ConnectorConfiguration_CanBeListed()
        {
            await StartSutAsync();
            var connectorClient = Services.GetRequiredService<IConnectorConfigurationManager>();
            var connectorConfigurationJson = GetEmbeddedResourceText(CONNECTOR_RESOURCE, typeof(ConnectorExtensionTests));
            var originalConnectorConfiguration = JsonSerializer.Deserialize<ConnectorConfigModel>(connectorConfigurationJson);
            var originalJson = JsonSerializer.Serialize(originalConnectorConfiguration);
            await connectorClient.SetConnectorAsync(originalConnectorConfiguration, CancellationToken.None);

            var savedConnectors = await connectorClient.ListConnectorsAsync(CancellationToken.None);
            Assert.Single(savedConnectors);

            var savedConnectorJson = JsonSerializer.Serialize(savedConnectors[0]);
            Assert.Equal(originalJson, savedConnectorJson);
        }


    }
}
