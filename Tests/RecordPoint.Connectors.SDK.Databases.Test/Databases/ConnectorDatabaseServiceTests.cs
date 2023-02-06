using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RecordPoint.Connectors.SDK.Connectors;
using RecordPoint.Connectors.SDK.Databases;
using RecordPoint.Connectors.SDK.Test.Mock.Databases;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace RecordPoint.Connectors.SDK.Test.ConnectorDatabase
{
    /// <summary>
    /// SUT for Connector Database service Tests
    /// </summary>
    public class ConnectorDatabaseServiceSUT : CommonSutBase
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
    /// Unit tests for the connector database service
    /// </summary>
    public class ConnectorDatabaseServiceTests : CommonTestBase<ConnectorDatabaseServiceSUT>
    {

        /// <summary>
        /// Test that the database context was setup right
        /// </summary>
        [Fact]
        public async Task PrepareDatabase_HappyPath()
        {
            await StartSutAsync();
            var databaseClient = Services.GetRequiredService<IConnectorDatabaseClient>();
            using var context1 = databaseClient.CreateDbContext();

            // Just quickly add a record and read it back to see if it looks like its working

            var testData = new ConnectorConfigurationModel()
            {
                ConnectorId = "Test1",
                ConnectorTypeId = "Test2",
                DisplayName = "Test3",
                Status = "Test4",
                TenantId = "Test5",
                Data = "ABC"
            };
            context1.Connectors.Add(testData);
            context1.SaveChanges();

            using var context2 = databaseClient.CreateDbContext();
            var readData = context2.Connectors.Where(c => c.ConnectorId == testData.ConnectorId).Single();
            Assert.Equal(testData.DisplayName, readData.DisplayName);
        }


    }
}
