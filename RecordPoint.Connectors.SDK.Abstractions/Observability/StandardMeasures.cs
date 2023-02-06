namespace RecordPoint.Connectors.SDK.Observability
{

    /// <summary>
    /// Set of standard observability measures
    /// </summary>
    public static class StandardMeasures
    {

        /// <summary>
        /// How many seconds it took from when a request was made, till it was completed.
        /// Includes time spent in queues, getting retried, etc.
        /// </summary>
        public const string OUTCOME_SECONDS = "OutcomeSeconds";

        /// <summary>
        /// How many seconds something was actually worked on
        /// </summary>
        public const string WORK_SECONDS = "WorkSeconds";

        /// <summary>
        /// How many seconds it took to complete a major task, normally within a unit of work
        /// </summary>
        public const string ACTION_EXECUTION_SECONDS = "ActionExecutionSeconds";

        /// <summary>
        /// How many seconds it took for an operation to submit new work operations
        /// </summary>
        public const string SUBMIT_SECONDS = "SubmitSeconds";

        /// <summary>
        /// How many records were processed?
        /// </summary>
        public const string RECORD_COUNT = "RecordCount";

        /// <summary>
        /// How many aggregations were processed?
        /// </summary>
        public const string AGGREGATION_COUNT = "AggregationCount";

        /// <summary>
        /// How many audit events were processed?
        /// </summary>
        public const string AUDIT_COUNT = "AuditCount";

        /// <summary>
        /// How many binaries were processed?
        /// </summary>
        public const string BINARY_COUNT = "BinaryCount";

    }
}
