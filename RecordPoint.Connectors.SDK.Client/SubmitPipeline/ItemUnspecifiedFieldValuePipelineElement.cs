namespace RecordPoint.Connectors.SDK.SubmitPipeline
{
    /// <summary>
    /// A submit pipeline element that ensures that required fields for items have a value of "Unspecified".
    /// </summary>
    public class ItemUnspecifiedFieldValuePipelineElement : UnspecifiedFieldValuePipelineElementBase
    {
        /// <summary>
        /// Constructs a new ItemUnspecifiedFieldValuePipelineElement with an optional next submit
        /// pipeline element.
        /// </summary>
        /// <param name="next"></param>
        public ItemUnspecifiedFieldValuePipelineElement(ISubmission next) : base(next)
        {
        }

        static readonly List<string> _requiredFields = new List<string>
        {
            // Don't check ConnectorId, ExternalId or ParentExternalId, as substituting 
            // a value for these fields might have unintended consquences
            Fields.Title,
            Fields.Author,
            Fields.SourceLastModifiedBy,
            Fields.SourceCreatedBy,
            Fields.ContentVersion,
            Fields.Location,
            Fields.MediaType
        };

        /// <summary>
        /// Specifies the fields that are required for items.
        /// </summary>
        /// <returns></returns>
        protected override IEnumerable<string> GetRequiredStringFields()
        {
            return _requiredFields;
        }
    }
}
