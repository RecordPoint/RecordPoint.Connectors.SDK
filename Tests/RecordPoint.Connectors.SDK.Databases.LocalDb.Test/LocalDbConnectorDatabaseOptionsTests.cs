#nullable enable
using RecordPoint.Connectors.SDK.Databases.LocalDb;
using Xunit;

namespace RecordPoint.Connectors.SDK.Databases.LocalDb.Test
{
    /// <summary>
    /// Unit tests for <see cref="LocalDbConnectorDatabaseOptions"/>.
    /// </summary>
    public sealed class LocalDbConnectorDatabaseOptionsTests
    {
        [Fact]
        public void Constants_HaveExpectedValues()
        {
            Assert.Equal("ConnectorConfig", LocalDbConnectorDatabaseOptions.DEFAULT_DATABASE_NAME);
            Assert.Equal("ConnectorLocalDbDatabase", LocalDbConnectorDatabaseOptions.SECTION_NAME);
        }

        [Fact]
        public void DatabaseName_DefaultsToDefaultDatabaseName()
        {
            var options = new LocalDbConnectorDatabaseOptions();
            Assert.Equal(LocalDbConnectorDatabaseOptions.DEFAULT_DATABASE_NAME, options.DatabaseName);
        }

        [Fact]
        public void DatabaseName_CanBeOverridden()
        {
            var options = new LocalDbConnectorDatabaseOptions { DatabaseName = "Overridden" };
            Assert.Equal("Overridden", options.DatabaseName);
        }
    }
}
