namespace RecordPoint.Connectors.SDK.Databases.Sqlite
{
    public class SqliteConnectorDatabaseOptions
    {

        /// <summary>
        /// Default database name
        /// </summary>
        public const string DEFAULT_DATABASE_NAME = "ConnectorConfig";

        /// <summary>
        /// Configuration section name
        /// </summary>
        public const string SECTION_NAME = "ConnectorSqliteDatabase";

        /// <summary>
        /// Name of the database
        /// </summary>
        public string DatabaseName { get; set; } = DEFAULT_DATABASE_NAME;

    }
}
