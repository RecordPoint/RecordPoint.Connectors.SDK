namespace RecordPoint.Connectors.SDK.Work
{
    /// <summary>
    /// Configuration for the generic dead-letter controller surface.
    /// </summary>
    public class DeadLetterControllerOptions
    {
        /// <summary>
        /// The maximum replay batch size allowed by the controller.
        /// </summary>
        public int MaxReplayBatchSize { get; set; } = 1000;

        /// <summary>
        /// Whether delete endpoints should be exposed.
        /// </summary>
        public bool EnableDeleteOperations { get; set; } = true;

        /// <summary>
        /// Optional allow-list of queue names the controller may access.
        /// When empty, all queue names are allowed.
        /// </summary>
        public ISet<string> AllowedQueueNames { get; set; } = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
    }
}
