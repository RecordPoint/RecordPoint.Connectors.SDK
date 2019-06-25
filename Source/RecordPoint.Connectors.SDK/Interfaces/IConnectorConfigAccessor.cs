using RecordPoint.Connectors.SDK.Client.Models;
using System;
using System.Threading.Tasks;

namespace RecordPoint.Connectors.SDK.Interfaces
{
    /// <summary>
    /// An abstraction over access of connector configuration objects.
    /// </summary>
    public interface IConnectorConfigAccessor
    {
        /// <summary>
        /// Gets a ConnectorConfigModel by tenant ID and connector config ID. 
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="connectorConfigId"></param>
        /// <returns></returns>
        Task<ConnectorConfigModel> GetConnectorConfig(Guid tenantId, Guid connectorConfigId);
    }
}
