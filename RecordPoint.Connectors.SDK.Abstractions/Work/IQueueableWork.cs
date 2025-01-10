namespace RecordPoint.Connectors.SDK.Work
{
    /// <summary>
    /// Definition of work that is submitted to a queue for execution.
    /// </summary>
    public interface IQueueableWork : IWork, IDisposable
    {
        /// <summary>
        /// Original work request
        /// </summary>
        WorkRequest WorkRequest { get; }

        /// <summary>
        /// Result of the unit of work
        /// </summary>
        WorkResult WorkResult { get; }

        /// <summary>
        /// Use this work item to run a work request
        /// </summary>
        /// <param name="workRequest">Work request that is compatible with this work item</param>
        /// <param name="cancellationToken">Cancellation token</param>
        Task RunWorkRequestAsync(WorkRequest workRequest, CancellationToken cancellationToken);
    }
}
