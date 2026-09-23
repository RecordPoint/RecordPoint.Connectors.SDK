#nullable enable
using Xunit;

namespace RecordPoint.Connectors.SDK.Databases.PostgreSql.Test
{
    public class PostgreSqlConnectorDbOptionsTests
    {
        [Fact]
        public void SectionName_HasExpectedValue()
        {
            Assert.Equal("PostgreSqlConnectorDatabase", PostgreSqlConnectorDbOptions.SECTION_NAME);
        }

        [Fact]
        public void Defaults_AreEmptyStrings()
        {
            var options = new PostgreSqlConnectorDbOptions();

            Assert.Equal(string.Empty, options.ConnectionString);
            Assert.Equal(string.Empty, options.AdminUsername);
            Assert.Equal(string.Empty, options.AdminPassword);
        }

        [Fact]
        public void Properties_AreSettable()
        {
            var options = new PostgreSqlConnectorDbOptions
            {
                ConnectionString = "Server=localhost;Database=db;",
                AdminUsername = "admin",
                AdminPassword = "secret"
            };

            Assert.Equal("Server=localhost;Database=db;", options.ConnectionString);
            Assert.Equal("admin", options.AdminUsername);
            Assert.Equal("secret", options.AdminPassword);
        }
    }
}
