using System.Data.SqlClient;

namespace RecordPoint.Connectors.SDK.Databases.PostgreSql
{
    public interface IPostgreSqlConnectionFactory
    {
        SqlConnection GetConnection(string connectionString);
    }
}
