namespace RecordPoint.Connectors.SDK.ContentReport
{

    /// <summary>
    /// Provides a context which the components of content report can use to get
    /// cross-cutting information.
    /// 
    /// TODO Replace "Environment" across the board with "Context"
    /// TODO Make a new top level namespace
    /// </summary>
    public interface IContentReportContext
    {

        /// <summary>
        /// Get the name of the service as used in standard logging
        /// </summary>
        /// <returns>Service name</returns>
        string GetServiceName();

        /// <summary>
        /// Get the service correlation ID as used in standard loggging
        /// </summary>
        /// <returns>Service Id</returns>
        string GetServiceId();

    }
}
