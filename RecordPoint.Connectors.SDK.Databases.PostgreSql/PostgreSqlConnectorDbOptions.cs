namespace RecordPoint.Connectors.SDK.Databases.PostgreSql
{
    public class PostgreSqlConnectorDbOptions
    {

        /// <summary>
        /// Configuration section name
        /// </summary>
        public const string SECTION_NAME = "PostgreSqlConnectorDatabase";

        /// <summary>
        /// Connection string for the database to be used
        /// </summary>
        public string ConnectionString { get; set; } = string.Empty;

        public string AdminUsername { get; set; } = string.Empty;

        public string AdminPassword { get; set; } = string.Empty;

    }
}
