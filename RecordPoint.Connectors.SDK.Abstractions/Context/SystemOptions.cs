namespace RecordPoint.Connectors.SDK.Context
{

    /// <summary>
    /// On premise system options
    /// </summary>
    public class SystemOptions
    {

        /// <summary>
        /// Configuration section name
        /// </summary>
        public const string SECTION_NAME = "System";

        /// <summary>
        /// Company name
        /// </summary>
        public string CompanyName { get; set; } = "Undefined";

        /// <summary>
        /// System name
        /// </summary>
        public string ConnectorName { get; set; } = "Undefined";

        /// <summary>
        /// Short name
        /// </summary>
        public string ShortName { get; set; } = "Undefined";

        // TODO: Remove once we supporting clustering and dependant services removed

        /// <summary>
        /// Root path for the connectors data.
        /// </summary>
        /// <remarks>
        /// The system context has a fall-back default if the data path is not defined
        /// </remarks>
        public string DataPathRoot { get; set; } = string.Empty;

    }
}
