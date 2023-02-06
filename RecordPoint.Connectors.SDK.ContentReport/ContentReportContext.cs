namespace RecordPoint.Connectors.SDK.ContentReport
{

    /// <summary>
    /// Provides overall operating context to the content report components.
    /// Allows consistent logging scopes to be created.
    /// 
    /// TODO Standardise! Base Classes! Namespaces!
    /// </summary>
    public class ContentReportContext : IContentReportContext
    {

        public const string CONTENT_REPORT_SERVICE_NAME = "Content Report";

        private readonly string _serviceId;

        public ContentReportContext()
        {
            _serviceId = Guid.NewGuid().ToString();
        }

        public string GetServiceId()
        {
            return _serviceId;
        }

        public string GetServiceName()
        {
            return CONTENT_REPORT_SERVICE_NAME;
        }
    }
}
