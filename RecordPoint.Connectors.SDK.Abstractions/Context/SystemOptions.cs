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
        private const string COMPANY_NAME = "RecordPoint";
        private const string UNDEFINED = "Undefined";

        /// <summary>
        /// Company name
        /// </summary>
        public string CompanyName { get; set; } = COMPANY_NAME;

        /// <summary>
        /// System name
        /// </summary>
        public string ConnectorName { get; set; } = UNDEFINED;

        /// <summary>
        /// Short name
        /// </summary>
        public string ShortName { get; set; } = UNDEFINED;

        /// <summary>
        /// Service name
        /// </summary>
        public string ServiceName { get; set; } = UNDEFINED;

        /// <summary>
        /// Root path for the connectors data.
        /// </summary>
        /// <remarks>
        /// The system context has a fall-back default if the data path is not defined
        /// </remarks>
        public string DataPathRoot { get; set; } = string.Empty;

    }
}
