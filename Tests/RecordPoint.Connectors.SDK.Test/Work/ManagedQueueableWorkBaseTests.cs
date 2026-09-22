using Microsoft.Extensions.Hosting;
using RecordPoint.Connectors.SDK.Context;
using RecordPoint.Connectors.SDK.Observability;
using RecordPoint.Connectors.SDK.Providers;
using RecordPoint.Connectors.SDK.Test.Mock.Work;
using RecordPoint.Connectors.SDK.Work;

namespace RecordPoint.Connectors.SDK.Test.Work;


public class WorkItemSut : CommonSutBase
{
    protected override IHostBuilder CreateSutBuilder()
    {
        return base.CreateSutBuilder()
            .UseWorkManager()
            .UseWorkStateManager<DatabaseManagedWorkStatusManager>()
            .ConfigureServices(svcs =>
                svcs.AddMockWorkQueue()
                    .AddQueueableWorkOperation<NullManagedQueueableWork>()
            );
    }
}

/// <summary>
/// Work work item that does nothing
/// </summary>
class NullManagedQueueableWork(
    IServiceProvider serviceProvider,
    IManagedWorkFactory workFactory,
    ISystemContext systemContext,
    IObservabilityScope observabilityScope,
    ITelemetryTracker telemetryTracker,
    IDateTimeProvider dateTimeProvider) : ManagedQueueableWorkBase<object, object>(serviceProvider, workFactory, systemContext, observabilityScope, telemetryTracker, dateTimeProvider)
{
    public override string ServiceName => nameof(NullManagedQueueableWork);

    public override string WorkType => nameof(NullManagedQueueableWork);

    protected override Task InnerRunAsync(CancellationToken cancellationToken)
    {
        return CompleteAsync("Test", cancellationToken);
    }

    protected override object DeserializeConfiguration(string configurationType, string configurationText)
    {
        return null;
    }

    protected override object DeserializeState(string stateType, string stateText)
    {
        return null;
    }

    protected override (string, string) SerializeConfiguration(object state)
    {
        return ("", "");
    }

    protected override (string, string) SerializeState(object state)
    {
        return ("", "");
    }
}
