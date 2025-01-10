using Microsoft.Data.SqlClient;

namespace RecordPoint.Connectors.SDK.Databases.AzureSql
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAzureSqlConnectionFactory
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        SqlConnection GetConnection(string connectionString);
    }
}
