using Microsoft.Data.SqlClient;

namespace RecordPoint.Connectors.SDK.Databases.AzureSql
{
    /// <summary>
    /// The azure sql connection factory.
    /// </summary>
    public class AzureSqlConnectionFactory : IAzureSqlConnectionFactory
    {
        /// <summary>
        /// Get the connection.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        /// <returns>A SqlConnection</returns>
        public SqlConnection GetConnection(string connectionString)
        {
            return new SqlConnection(connectionString);
        }
    }
}
