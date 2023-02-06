using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading;
using System.Threading.Tasks;

namespace RecordPoint.Connectors.SDK.Work
{
    /// <summary>
    /// Standard implementation of a work manager
    /// </summary>
    public class QueueableWorkManager : IQueueableWorkManager
    {
        private readonly ILogger _logger;
        private readonly ImmutableDictionary<string, IQueueableWorkFactory> _queueableWorkFactories;

        public QueueableWorkManager(ILogger<QueueableWorkManager> logger, IEnumerable<IQueueableWorkFactory> queueableWorkFactories)
        {
            _logger = logger;
            _queueableWorkFactories = queueableWorkFactories.ToImmutableDictionary(a => a.WorkType);
        }

        public bool TryGetQueueableWorkFactory(string workType, out IQueueableWorkFactory queueableWorkFactory) => _queueableWorkFactories.TryGetValue(workType, out queueableWorkFactory);

        public async Task<WorkResult> HandleWorkRequestAsync(WorkRequest workRequest, CancellationToken cancellationToken)
        {
            var workType = workRequest.WorkType;
            try
            {
                if (TryGetQueueableWorkFactory(workType, out var queueableWorkFactory))
                {
                    var queueableWorkOperation = queueableWorkFactory.CreateWorkOperation();
                    await queueableWorkOperation.RunWorkRequestAsync(workRequest, cancellationToken);
                    return queueableWorkOperation.WorkResult;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unknown exception thrown attempting to handle a Work Request");
            }

            return WorkResult.Failed($"No work operation registered for {workType} work");
        }

    }
}
