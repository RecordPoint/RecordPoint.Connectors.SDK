namespace RecordPoint.Connectors.SDK.Work
{
    /// <summary>
    /// Definition for manager responsible for executing work
    /// </summary>
    public interface IQueueableWorkManager
    {
        /// <summary>
        /// Try to get the work item factory for a given type of work
        /// </summary>
        /// <param name="workType">Type of work to get type for</param>
        /// <param name="queueableWorkFactory">The queueable work factory</param>
        /// <returns>True if the work type has been registered, otherwise false</returns>
        public bool TryGetQueueableWorkFactory(string workType, out IQueueableWorkFactory queueableWorkFactory);

        /// <summary>
        /// Handle a work request
        /// </summary>
        /// <param name="workRequest">Work request to handle</param>
        /// <returns>Task</returns>
        public Task<WorkResult> HandleWorkRequestAsync(WorkRequest workRequest, CancellationToken cancellationToken);

    }
}
