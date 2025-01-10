using System.Data.SqlClient;

namespace RecordPoint.Connectors.SDK.Databases.PostgreSql
{
    /// <summary>
    /// The postgre sql connection factory.
    /// </summary>
    public class PostgreSqlConnectionFactory : IPostgreSqlConnectionFactory
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
