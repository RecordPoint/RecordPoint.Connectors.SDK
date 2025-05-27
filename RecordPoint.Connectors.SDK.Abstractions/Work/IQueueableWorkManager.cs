namespace RecordPoint.Connectors.SDK.Work
{
    /// <summary>
    /// Definition for manager responsible for executing work
    /// </summary>
    public interface IQueueableWorkManager
    {
        /// <summary>
        /// Handle a work request
        /// </summary>
        /// <param name="workRequest">Work request to handle</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Task</returns>
        public Task<WorkResult> HandleWorkRequestAsync(WorkRequest workRequest, CancellationToken cancellationToken);
    }
}
