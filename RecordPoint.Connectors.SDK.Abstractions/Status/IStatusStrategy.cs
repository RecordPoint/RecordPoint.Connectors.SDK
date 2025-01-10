using RecordPoint.Connectors.SDK.Connectors;

namespace RecordPoint.Connectors.SDK.Status
{
    /// <summary>
    /// 
    /// </summary>
    public interface IStatusStrategy
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectorModel"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public Task<List<string>> GetStatusText(ConnectorConfigurationModel connectorModel, CancellationToken cancellationToken);
    }
}
