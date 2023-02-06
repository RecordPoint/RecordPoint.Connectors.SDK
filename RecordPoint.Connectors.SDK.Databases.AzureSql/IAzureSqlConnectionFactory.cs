using Microsoft.Data.SqlClient;

namespace RecordPoint.Connectors.SDK.Databases.AzureSql
{
    public interface IAzureSqlConnectionFactory
    {
        SqlConnection GetConnection(string connectionString);
    }
}
