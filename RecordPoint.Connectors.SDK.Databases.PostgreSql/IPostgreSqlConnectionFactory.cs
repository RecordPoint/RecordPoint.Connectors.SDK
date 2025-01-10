using System.Data.SqlClient;

namespace RecordPoint.Connectors.SDK.Databases.PostgreSql
{
    /// <summary>
    /// 
    /// </summary>
    public interface IPostgreSqlConnectionFactory
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        SqlConnection GetConnection(string connectionString);
    }
}
