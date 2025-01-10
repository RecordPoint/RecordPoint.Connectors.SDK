namespace RecordPoint.Connectors.SDK.Work
{
    /// <summary>
    /// Defines the client for a work queue
    /// </summary>
    public interface IWorkQueueClient
    {
        /// <summary>
        /// Submit work
        /// </summary>
        /// <param name="workRequest">Work request to submit</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>Task</returns>
        Task SubmitWorkAsync(WorkRequest workRequest, CancellationToken cancellationToken);
    }
}