namespace RecordPoint.Connectors.SDK.ContentManager
{
    /// <summary>
    /// Configuration for Content Submission Operations
    /// </summary>
    public class ContentSubmissionConfiguration
    {
        /// <summary>
        /// Connector config we are performing the operation on. Content modules can use this to identify the target content 
        /// </summary>
        public string? ConnectorConfigurationId { get; set; }

        /// <summary>
        /// Tenant Id
        /// </summary>
        public string? TenantId { get; set; }

        /// <summary>
        /// Tenant domain name
        /// </summary>
        public string? TenantDomainName { get; set; }
    }
}