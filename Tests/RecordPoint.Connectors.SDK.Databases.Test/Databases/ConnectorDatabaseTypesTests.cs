#nullable enable
using RecordPoint.Connectors.SDK.Databases;
using Xunit;

namespace RecordPoint.Connectors.SDK.Databases.Test.Databases
{
    public class ConnectorDatabaseTypesTests
    {
        [Fact]
        public void ConnectorDatabaseException_SetsMessage()
        {
            var ex = new ConnectorDatabaseException("something failed");
            Assert.Equal("something failed", ex.Message);
        }

        [Fact]
        public void ConnectorDatabaseOptions_HasDefaultDatabaseName()
        {
            var options = new ConnectorDatabaseOptions();
            Assert.Equal("ConnectorConfig", options.DatabaseName);
        }

        [Fact]
        public void ConnectorDatabaseOptions_DatabaseName_IsSettable()
        {
            var options = new ConnectorDatabaseOptions { DatabaseName = "Custom" };
            Assert.Equal("Custom", options.DatabaseName);
        }
    }
}
