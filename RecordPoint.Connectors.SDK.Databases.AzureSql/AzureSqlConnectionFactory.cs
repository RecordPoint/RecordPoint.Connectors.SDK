using Microsoft.Data.SqlClient;

namespace RecordPoint.Connectors.SDK.Databases.AzureSql
{
    public class AzureSqlConnectionFactory : IAzureSqlConnectionFactory
    {
        public SqlConnection GetConnection(string connectionString)
        {
            return new SqlConnection(connectionString);
        }
    }
}
