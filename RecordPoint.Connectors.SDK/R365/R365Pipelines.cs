using RecordPoint.Connectors.SDK.SubmitPipeline;

namespace RecordPoint.Connectors.SDK.R365
{
    /// <summary>
    /// The r365 pipelines.
    /// </summary>
    public class R365Pipelines : IR365Pipelines
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="R365Pipelines"/> class.
        /// </summary>
        /// <param name="recordPipeline">The record pipeline.</param>
        /// <param name="binaryPipeline">The binary pipeline.</param>
        /// <param name="aggregationPipeline">The aggregation pipeline.</param>
        /// <param name="auditEventPipeline">The audit event pipeline.</param>
        public R365Pipelines(ISubmission recordPipeline, ISubmission binaryPipeline, ISubmission aggregationPipeline, ISubmission auditEventPipeline)
        {
            RecordPipeline = recordPipeline;
            BinaryPipeline = binaryPipeline;
            AggregationPipeline = aggregationPipeline;
            AuditEventPipeline = auditEventPipeline;
        }

        /// <summary>
        /// Record submission pipeline
        /// </summary>
        public ISubmission RecordPipeline { get; private set; }

        /// <summary>
        /// Binary submission pipeline
        /// </summary>
        public ISubmission BinaryPipeline { get; private set; }

        /// <summary>
        /// Aggregation submission pipeline
        /// </summary>
        public ISubmission AggregationPipeline { get; private set; }

        /// <summary>
        /// Audit event submission pipeline
        /// </summary>
        public ISubmission AuditEventPipeline { get; private set; }


    }
}
