namespace RecordPoint.Connectors.SDK.Work
{
    /// <summary>
    /// Defines the public interface used to create and load jobs
    /// </summary>
    public interface IManagedWorkFactory
    {
        /// <summary>
        /// Create a new job
        /// </summary>
        /// <param name="workStatusId">Work Status Id</param>
        /// <param name="workType">Work type</param>
        /// <param name="connectorId">Id of the connector the job is for</param>
        /// <param name="configurationType">String that identified how the configuration is formatted</param>
        /// <param name="configuration">Work configuration</param>
        /// <returns>Managed work status manager</returns>
        IManagedWorkManager CreateWork(string workStatusId, string workType, string connectorId, string configurationType, string configuration);

        /// <summary>
        /// Load work from a work state object
        /// </summary>
        /// <param name="workStatus">Work State that defines the work</param>
        /// <returns>Managed work status manager</returns>
        IManagedWorkManager LoadWork(ManagedWorkStatusModel workStatus);

    }
}
