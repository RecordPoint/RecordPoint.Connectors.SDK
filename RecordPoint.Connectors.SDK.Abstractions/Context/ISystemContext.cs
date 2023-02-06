namespace RecordPoint.Connectors.SDK.Context
{
    /// <summary>
    /// Defines a context for the entire connector system
    /// </summary>
    public interface ISystemContext
    {
        /// <summary>
        /// Name of the company that develops the system
        /// </summary>
        string GetCompanyName();

        /// <summary>
        /// Name of the system, typically the name of the connector
        /// </summary>
        string GetConnectorName();

        /// <summary>
        /// Abbreviated name of the system
        /// </summary>
        /// <returns></returns>
        string GetShortName();

        /// <summary>
        /// Get the root path for data
        /// </summary>
        /// <returns></returns>
        string GetDataRootPath();
    }
}