using RecordPoint.Connectors.SDK.Client.Models;

namespace RecordPoint.Connectors.SDK.SubmitPipeline
{
    /// <summary>
    /// Ensures that a value is provided for all fields required by the Connector API.
    /// If a null or empty value is provided for a required field, this pipeline element
    /// substitutes the value "Unspecified".
    /// Doesn't substitute for ConnectorId, ExternalId or ParentExternalId.
    /// </summary>
    public abstract class UnspecifiedFieldValuePipelineElementBase : SubmitPipelineElementBase
    {
        /// <summary>
        /// Constructs a new UnspecifiedFieldValuePipelineElementBase with an optional next submit
        /// pipeline element.
        /// </summary>
        /// <param name="next"></param>
        protected UnspecifiedFieldValuePipelineElementBase(ISubmission next) : base(next)
        {
        }

        /// <summary>
        /// Implement in a derived class to specify the names of any string fields that are required.
        /// The pipeline element will substitute any blank or missing values with "Unspecified" for these
        /// fields.
        /// </summary>
        /// <returns></returns>
        protected abstract IEnumerable<string> GetRequiredStringFields();

        private static readonly string UnspecifiedFieldValue = "Unspecified";

        /// <summary>
        /// Checks core metadata for any required fields that are blank or missing
        /// and provides the value "Unspecified" for those fields.
        /// </summary>
        /// <param name="submitContext"></param>
        /// <returns></returns>
        public override async Task Submit(SubmitContext submitContext)
        {
            var requiredFields = GetRequiredStringFields();

            foreach (var requiredField in requiredFields)
            {
                var metaData = submitContext.CoreMetaData.FirstOrDefault(x => x.Name == requiredField);
                if (metaData != null)
                {
                    if (string.IsNullOrEmpty(metaData.Value))
                    {
                        metaData.Value = UnspecifiedFieldValue;
                        LogVerbose(submitContext, nameof(Submit), $"setting required field [{metaData.Name}] to [{UnspecifiedFieldValue}]");
                    }
                }
                else
                {
                    submitContext.CoreMetaData.Add(new SubmissionMetaDataModel { Name = requiredField, Type = nameof(String), Value = UnspecifiedFieldValue });
                }
            }

            await InvokeNext(submitContext).ConfigureAwait(false);
        }
    }
}
