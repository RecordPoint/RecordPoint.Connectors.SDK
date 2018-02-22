using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordPoint.Connectors.SDK.Client
{
    /// <summary>
    /// Records365vNext NotificationType. 
    /// Possible values include: 'ItemDestroyed', 'Ping', 'ConnectorConfigAdded', 'ConnectorConfigUpdated', 'ConnectorConfigDeleted' and 'Unknown'
    /// </summary>
    public enum NotificationType
    {
        Unknown,
        ItemDestroyed,
        Ping,
        ConnectorConfigAdded,
        ConnectorConfigUpdated,
        ConnectorConfigDeleted
    }
}
