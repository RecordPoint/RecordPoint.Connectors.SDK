namespace RecordPoint.Connectors.SDK.Databases.AzureSql
{
    public class AzureSqlConnectorDbOptions
    {

        /// <summary>
        /// Configuration section name
        /// </summary>
        public const string SECTION_NAME = "AzureSqlConnectorDatabase";

        /// <summary>
        /// Connection string for the database to be used
        /// </summary>
        public string ConnectionString { get; set; } = string.Empty;

        public string AdminUsername { get; set; } = string.Empty;

        public string AdminPassword { get; set; } = string.Empty;

    }
}
