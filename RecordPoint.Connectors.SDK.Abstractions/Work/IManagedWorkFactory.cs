using RecordPoint.Connectors.SDK.Client.Models;

namespace RecordPoint.Connectors.SDK.Work;

/// <summary>
/// Defines the public interface used to create and load jobs
/// </summary>
public interface IManagedWorkFactory
{
    /// <summary>
    /// Create a new job
    /// </summary>
    /// <param name="connectorConfig">The connector configuration</param>
    /// <param name="workStatusId">Work Status Id</param>
    /// <param name="workType">Work type</param>
    /// <param name="configurationType">String that identified how the configuration is formatted</param>
    /// <param name="configuration">Work configuration</param>
    /// <returns>Managed work status manager</returns>
    IManagedWorkManager CreateWork(ConnectorConfigModel connectorConfig, string workStatusId, string workType, string configurationType, string configuration);

    /// <summary>
    /// Load work from a work state object
    /// </summary>
    /// <param name="workRequest">Work Request that defines the work</param>
    /// <param name="workStatus">Work State that defines the work</param>
    /// <returns>Managed work status manager</returns>
    IManagedWorkManager LoadWork(WorkRequest workRequest, ManagedWorkStatusModel workStatus);

}
