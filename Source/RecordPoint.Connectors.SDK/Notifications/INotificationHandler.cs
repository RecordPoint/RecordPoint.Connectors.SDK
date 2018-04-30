using RecordPoint.Connectors.SDK.Client.Models;
using System.Threading;
using System.Threading.Tasks;

namespace RecordPoint.Connectors.SDK.Notifications
{
    public interface INotificationHandler
    {
        Task HandleNotification(ConnectorConfigModel connectorConfigModel, ConnectorNotificationModel notification, CancellationToken ct);
    }
}
