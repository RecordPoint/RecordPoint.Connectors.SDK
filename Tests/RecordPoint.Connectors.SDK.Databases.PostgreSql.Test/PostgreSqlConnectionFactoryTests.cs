#nullable enable
using Microsoft.Data.SqlClient;
using Xunit;

namespace RecordPoint.Connectors.SDK.Databases.PostgreSql.Test
{
    public class PostgreSqlConnectionFactoryTests
    {
        [Fact]
        public void GetConnection_ReturnsSqlConnection_WithConnectionString()
        {
            var factory = new PostgreSqlConnectionFactory();
            const string connectionString = "Server=localhost;Database=testdb;User Id=svc;Password=pw";

            using var connection = factory.GetConnection(connectionString);

            Assert.NotNull(connection);
            Assert.IsType<SqlConnection>(connection);
            Assert.Equal(connectionString, connection.ConnectionString);
        }

        [Fact]
        public void GetConnection_ImplementsInterface()
        {
            var factory = new PostgreSqlConnectionFactory();

            using var connection = factory.GetConnection("Server=localhost;Database=db;");

            Assert.NotNull(connection);
        }
    }
}
