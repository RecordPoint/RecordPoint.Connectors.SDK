using System.Data.SqlClient;

namespace RecordPoint.Connectors.SDK.Databases.PostgreSql
{
    public class PostgreSqlConnectionFactory : IPostgreSqlConnectionFactory
    {
        public SqlConnection GetConnection(string connectionString)
        {
            return new SqlConnection(connectionString);
        }
    }
}
