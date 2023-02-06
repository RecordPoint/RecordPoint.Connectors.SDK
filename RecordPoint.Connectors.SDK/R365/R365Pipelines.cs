using RecordPoint.Connectors.SDK.SubmitPipeline;

namespace RecordPoint.Connectors.SDK.R365
{
    public class R365Pipelines : IR365Pipelines
    {

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
