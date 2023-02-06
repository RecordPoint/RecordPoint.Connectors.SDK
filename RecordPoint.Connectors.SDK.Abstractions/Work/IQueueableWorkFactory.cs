namespace RecordPoint.Connectors.SDK.Work
{
    /// <summary>
    /// Definition of a factory that can create queueable work
    /// </summary>
    public interface IQueueableWorkFactory
    {
        /// <summary>
        /// The type of work the items can work on
        /// </summary>
        string WorkType { get; }

        /// <summary>
        /// Create the work queue item
        /// </summary>
        /// <returns>Work queue item</returns>
        IQueueableWork CreateWorkOperation();
    }
}
