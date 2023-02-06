using RecordPoint.Connectors.SDK.SubmitPipeline;

namespace RecordPoint.Connectors.SDK.R365
{

    /// <summary>
    /// Contains the various submission pipelines
    /// </summary>
    public interface IR365Pipelines
    {

        /// <summary>
        /// Record submission pipeline
        /// </summary>
        ISubmission RecordPipeline { get; }

        /// <summary>
        /// Binary submission pipeline
        /// </summary>
        ISubmission BinaryPipeline { get; }

        /// <summary>
        /// Aggregation submission pipeline
        /// </summary>
        ISubmission AggregationPipeline { get; }

        /// <summary>
        /// Autdit event submission pipeline
        /// </summary>
        ISubmission AuditEventPipeline { get; }

    }
}
