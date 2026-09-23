#nullable enable
using RecordPoint.Connectors.SDK.Databases.Sqlite;
using Xunit;

namespace RecordPoint.Connectors.SDK.Databases.Sqlite.Test
{
    public class SqliteConnectorDatabaseOptionsTests
    {
        [Fact]
        public void DatabaseName_DefaultsToDefaultDatabaseName()
        {
            var options = new SqliteConnectorDatabaseOptions();

            Assert.Equal(SqliteConnectorDatabaseOptions.DEFAULT_DATABASE_NAME, options.DatabaseName);
            Assert.Equal("ConnectorConfig", options.DatabaseName);
        }

        [Fact]
        public void DatabaseName_CanBeOverridden()
        {
            var options = new SqliteConnectorDatabaseOptions
            {
                DatabaseName = "MyCustomDb"
            };

            Assert.Equal("MyCustomDb", options.DatabaseName);
        }

        [Fact]
        public void SectionName_HasExpectedValue()
        {
            Assert.Equal("ConnectorSqliteDatabase", SqliteConnectorDatabaseOptions.SECTION_NAME);
        }
    }
}
