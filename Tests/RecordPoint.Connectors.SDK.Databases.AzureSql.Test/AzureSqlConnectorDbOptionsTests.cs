#nullable enable
using Xunit;

namespace RecordPoint.Connectors.SDK.Databases.AzureSql.Test
{
    public class AzureSqlConnectorDbOptionsTests
    {
        [Fact]
        public void SectionName_HasExpectedValue()
        {
            Assert.Equal("AzureSqlConnectorDatabase", AzureSqlConnectorDbOptions.SECTION_NAME);
        }

        [Fact]
        public void Defaults_AreEmptyStrings()
        {
            var options = new AzureSqlConnectorDbOptions();

            Assert.Equal(string.Empty, options.ConnectionString);
            Assert.Equal(string.Empty, options.AdminUsername);
            Assert.Equal(string.Empty, options.AdminPassword);
        }

        [Fact]
        public void Properties_RoundTrip()
        {
            var options = new AzureSqlConnectorDbOptions
            {
                ConnectionString = "Server=abc;Database=def;",
                AdminUsername = "admin",
                AdminPassword = "secret",
            };

            Assert.Equal("Server=abc;Database=def;", options.ConnectionString);
            Assert.Equal("admin", options.AdminUsername);
            Assert.Equal("secret", options.AdminPassword);
        }
    }
}
