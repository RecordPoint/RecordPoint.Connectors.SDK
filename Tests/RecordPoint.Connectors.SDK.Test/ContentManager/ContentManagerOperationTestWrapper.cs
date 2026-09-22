using Microsoft.Extensions.Options;
using RecordPoint.Connectors.SDK.Connectors;
using RecordPoint.Connectors.SDK.Content;
using RecordPoint.Connectors.SDK.ContentManager;
using RecordPoint.Connectors.SDK.Observability;
using RecordPoint.Connectors.SDK.Providers;
using RecordPoint.Connectors.SDK.Work;

namespace RecordPoint.Connectors.SDK.Test.ContentManager;

public class ContentManagerOperationTestWrapper(
        IContentManagerActionProvider contentManagerActionProvider,
        IConnectorConfigurationManager connectorConfigManager,
        IChannelManager channelManager,
        IManagedWorkStatusManager managedWorkStatusManager,
        IManagedWorkFactory managedWorkFactory,
        IOptions<ContentManagerOptions> options,
        IObservabilityScope observabilityScope,
        ITelemetryTracker telemetryTracker,
        IDateTimeProvider dateTimeProvider,
        IServiceProvider serviceProvider) : ContentManagerOperation(contentManagerActionProvider, connectorConfigManager, channelManager, managedWorkStatusManager, managedWorkFactory, options, observabilityScope, telemetryTracker, dateTimeProvider, serviceProvider)
{
    public async Task InvokeInnerStartAsync(CancellationToken cancellationToken)
    {
        await InnerStartAsync(cancellationToken);
    }

    public async Task InvokeInnerRunAsync(CancellationToken cancellationToken)
    {
        await InnerRunAsync(cancellationToken);
    }

    public void InvokeResetResult()
    {
        ResetResult();
    }
}
