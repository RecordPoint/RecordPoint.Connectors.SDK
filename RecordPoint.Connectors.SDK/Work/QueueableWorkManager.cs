using Microsoft.Extensions.DependencyInjection;
using RecordPoint.Connectors.SDK.Observability;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RecordPoint.Connectors.SDK.Work;

/// <summary>
/// Standard implementation of a work manager
/// </summary>
public class QueueableWorkManager(ITelemetryTracker telemetryTracker, IServiceProvider services) : IQueueableWorkManager
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="workRequest"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<WorkResult> HandleWorkRequestAsync(WorkRequest workRequest, CancellationToken cancellationToken)
    {
        try
        {
            using var servicesScope = services.CreateScope();
            var queueableWorkOperations = servicesScope.ServiceProvider.GetServices<IQueueableWork>();
            var queueableWorkOperation = queueableWorkOperations.FirstOrDefault(service => service.WorkType == workRequest.WorkType);
            if (queueableWorkOperation == null)
                return WorkResult.Failed($"No work operation registered for {workRequest.WorkType} work");

            await queueableWorkOperation.RunWorkRequestAsync(workRequest, cancellationToken);
            return queueableWorkOperation.GetWorkResult();
        }
        catch (Exception ex)
        {
            var workRequestException = new UnknownWorkRequestException(ex);
            telemetryTracker.TrackException(workRequestException);
        }

        return WorkResult.Failed("Unknown exception thrown attempting to handle a Work Request");
    }
}
