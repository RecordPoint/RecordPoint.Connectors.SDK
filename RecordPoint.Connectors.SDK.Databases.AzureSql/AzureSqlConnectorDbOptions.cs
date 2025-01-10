namespace RecordPoint.Connectors.SDK.Databases.AzureSql
{
    /// <summary>
    /// The azure sql connector db options.
    /// </summary>
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

        /// <summary>
        /// Gets or sets the admin username.
        /// </summary>
        public string AdminUsername { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the admin password.
        /// </summary>
        public string AdminPassword { get; set; } = string.Empty;

    }
}
