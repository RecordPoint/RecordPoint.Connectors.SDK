namespace RecordPoint.Connectors.SDK.Work
{
    /// <summary>
    /// Definition of generic work
    /// </summary>
    public interface IWork
    {
        /// <summary>
        /// Unique name that defines the Work Item
        /// </summary>
        string WorkType { get; }

        /// <summary>
        /// Unique Work ID that identifies the unit of work
        /// </summary>
        string Id { get; }

        /// <summary>
        /// Time the work item was started
        /// </summary>
        DateTimeOffset StartDateTime { get; }

        /// <summary>
        /// Has a result been recorded
        /// </summary>
        bool HasResult { get; }

        /// <summary>
        /// Work Result Type
        /// </summary>
        WorkResultType ResultType { get; }

        /// <summary>
        /// Optional message that describes why a particular result was reached
        /// </summary>
        string ResultReason { get; }

        /// <summary>
        /// Optional message to provide details on why a particular result was reached
        /// </summary>
        string ResultReasonDetails { get; }

        /// <summary>
        /// The time that we got a result
        /// </summary>
        DateTimeOffset FinishDateTime { get; }

        /// <summary>
        /// Optional exception that caused the work to fail or be abandoned
        /// </summary>
        Exception Exception { get; }

        /// <summary>
        /// Time it took to do the work
        /// </summary>
        TimeSpan WorkDuration { get; }
    }

    /// <summary>
    /// Definition of unmanaged work that can be executed within the context of the executing process.
    /// </summary>
    public interface IWork<TParameter> : IWork
    {
        /// <summary>
        /// Generic parameter
        /// </summary>
        TParameter Parameter { get; }

        /// <summary>
        /// Asynchronously executes the work item
        /// </summary>
        /// <param name="parameter">Work item parameter</param>
        /// /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns>Task</returns>
        Task RunAsync(TParameter parameter, CancellationToken cancellationToken);
    }
}
