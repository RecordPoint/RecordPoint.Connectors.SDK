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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="queueableWorkFactories"></param>
        public QueueableWorkManager(ILogger<QueueableWorkManager> logger, IEnumerable<IQueueableWorkFactory> queueableWorkFactories)
        {
            _logger = logger;
            _queueableWorkFactories = queueableWorkFactories.ToImmutableDictionary(a => a.WorkType);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="workType"></param>
        /// <param name="queueableWorkFactory"></param>
        /// <returns></returns>
        public bool TryGetQueueableWorkFactory(string workType, out IQueueableWorkFactory queueableWorkFactory) => _queueableWorkFactories.TryGetValue(workType, out queueableWorkFactory);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="workRequest"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<WorkResult> HandleWorkRequestAsync(WorkRequest workRequest, CancellationToken cancellationToken)
        {
            var workType = workRequest.WorkType;
            try
            {
                if (TryGetQueueableWorkFactory(workType, out var queueableWorkFactory))
                {
                    using var queueableWorkOperation = queueableWorkFactory.CreateWorkOperation();
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
