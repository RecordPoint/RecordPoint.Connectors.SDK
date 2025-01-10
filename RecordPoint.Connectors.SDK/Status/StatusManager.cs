using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RecordPoint.Connectors.SDK.Status
{
    /// <summary>
    /// The status manager.
    /// </summary>
    public class StatusManager : IStatusManager
    {
        /// <summary>
        /// The strategies.
        /// </summary>
        private readonly IEnumerable<IStatusStrategy> _strategies;
        /// <summary>
        /// The connector manager.
        /// </summary>
        private readonly Connectors.IConnectorConfigurationManager _connectorManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="StatusManager"/> class.
        /// </summary>
        /// <param name="strategies">The strategies.</param>
        /// <param name="connectorManager">The connector manager.</param>
        public StatusManager(IEnumerable<IStatusStrategy> strategies,
            Connectors.IConnectorConfigurationManager connectorManager)
        {
            _strategies = strategies;
            _connectorManager = connectorManager;

        }

        /// <summary>
        /// Get status model asynchronously.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns><![CDATA[Task<List<StatusModel>>]]></returns>
        public async Task<List<StatusModel>> GetStatusModelAsync(CancellationToken cancellationToken)
        {
            var model = new List<StatusModel>();
            var connectorConfigurations = await _connectorManager.GetAllConnectorConfigurationsAsync(cancellationToken);

            if (connectorConfigurations.Any())
            {
                foreach (var connectorConfiguration in connectorConfigurations)
                {
                    var connectorStatus = new StatusModel
                    {
                        ConnectorId = connectorConfiguration.ConnectorId,
                        Status = new()
                    };
                    model.Add(connectorStatus);
                    foreach (var strategy in _strategies)
                    {
                        connectorStatus.Status = connectorStatus.Status.Concat(await strategy.GetStatusText(connectorConfiguration, cancellationToken)).ToList();
                    }
                }
                return model;
            }
            else
            {
                return model;
            }

        }
    }
}
