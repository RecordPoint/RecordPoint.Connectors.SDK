﻿namespace RecordPoint.Connectors.SDK.Databases.LocalDb
{
    /// <summary>
    /// The local db connector database options.
    /// </summary>
    public class LocalDbConnectorDatabaseOptions
    {

        /// <summary>
        /// Default database name
        /// </summary>
        public const string DEFAULT_DATABASE_NAME = "ConnectorConfig";

        /// <summary>
        /// Configuration section name
        /// </summary>
        public const string SECTION_NAME = "ConnectorLocalDbDatabase";

        /// <summary>
        /// Name of the database
        /// </summary>
        public string DatabaseName { get; set; } = DEFAULT_DATABASE_NAME;

    }
}
