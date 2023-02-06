using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RecordPoint.Connectors.SDK.Connectors;
using Xunit;
using System.Threading.Tasks;
using System.Threading;
using System.Text.Json;
using RecordPoint.Connectors.SDK.Test;
using RecordPoint.Connectors.SDK.Test.Mock.Databases;

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
            await StartSutAsync();
            var configurationClient = Services.GetRequiredService<IConnectorConfigurationManager>();
            var connectorConfigurationJson = GetEmbeddedResourceText(CONNECTOR_RESOURCE, typeof(ConnectorConfigurationTests));
            var testConfiguration = JsonSerializer.Deserialize<ConnectorConfigurationModel>(connectorConfigurationJson);
            testConfiguration.ReportLocation = @"C:\Data";

            await configurationClient.SetConnectorConfigurationAsync(testConfiguration, CancellationToken.None);

            var savedConfiguration = await configurationClient.GetConnectorConfigurationAsync(testConfiguration.ConnectorId, CancellationToken.None);

            Assert.NotNull(savedConfiguration);
            Assert.Equal(testConfiguration.ReportLocation, savedConfiguration.ReportLocation);
        }


        [Fact]
        public async Task ConnectorConfiguration_OverwriteExistingConfiguration()
        {
            await StartSutAsync();
            var configurationClient = Services.GetRequiredService<IConnectorConfigurationManager>();

            var testConfiguration = new ConnectorConfigurationModel()
            {
                ReportLocation = @"C:\Data"
            };

            await configurationClient.SetConnectorConfigurationAsync(testConfiguration, CancellationToken.None);

            var updatedConfiguration = new ConnectorConfigurationModel()
            {
                ReportLocation = @"C:\Updated"
            };

            await configurationClient.SetConnectorConfigurationAsync(updatedConfiguration, CancellationToken.None);

            var savedConfiguration = await configurationClient.GetConnectorConfigurationAsync(testConfiguration.ConnectorId, CancellationToken.None);

            Assert.NotNull(savedConfiguration);
            Assert.Equal(updatedConfiguration.ReportLocation, savedConfiguration.ReportLocation);
        }
    }
}