using System.Collections.Generic;

namespace RecordPoint.Connectors.SDK.SubmitPipeline
{
    public class ItemUnspecifiedFieldValuePipelineElement : UnspecifiedFieldValuePipelineElementBase
    {
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

        protected override IEnumerable<string> GetRequiredStringFields()
        {
            return _requiredFields;
        }
    }
}
