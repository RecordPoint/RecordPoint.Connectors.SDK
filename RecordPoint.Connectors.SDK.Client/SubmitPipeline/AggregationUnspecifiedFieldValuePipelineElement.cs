namespace RecordPoint.Connectors.SDK.SubmitPipeline
{
    /// <summary>
    /// A submit pipeline element that ensures that required fields for aggregations have a value of "Unspecified".
    /// </summary>
    public class AggregationUnspecifiedFieldValuePipelineElement : UnspecifiedFieldValuePipelineElementBase
    {
        /// <summary>
        /// Constructs a new AggregationUnspecifiedFieldValuePipelineElement with an optional next submit
        /// pipeline element.
        /// </summary>
        /// <param name="next"></param>
        public AggregationUnspecifiedFieldValuePipelineElement(ISubmission next) : base(next)
        {
        }

        private static readonly List<string> _requiredFields = new List<string>
        {
            Fields.Title,
            Fields.SourceLastModifiedBy,
            Fields.SourceCreatedBy
        };

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected override IEnumerable<string> GetRequiredStringFields()
        {
            return _requiredFields;
        }
    }
}
