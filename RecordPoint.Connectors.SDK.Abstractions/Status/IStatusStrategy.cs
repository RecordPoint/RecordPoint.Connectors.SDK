using RecordPoint.Connectors.SDK.Connectors;

namespace RecordPoint.Connectors.SDK.Status
{
    public interface IStatusStrategy
    {
        public Task<List<string>> GetStatusText(ConnectorConfigurationModel connectorModel, CancellationToken cancellationToken);
    }
}
