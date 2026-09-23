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
        /// Constructs a new unspecified-field pipeline element.
        /// </summary>
        /// <param name="next">The next pipeline element to invoke.</param>
        protected UnspecifiedFieldValuePipelineElementBase(ISubmission next) : base(next)
        {
        }

        /// <summary>
        /// Gets the required string fields that should be defaulted when missing.
        /// </summary>
        /// <returns>The required field names.</returns>
        protected abstract IEnumerable<string> GetRequiredStringFields();

        private static readonly string UnspecifiedFieldValue = "Unspecified";

        /// <summary>
        /// Replaces missing required metadata values with <c>Unspecified</c>.
        /// </summary>
        /// <param name="submitContext">The current submission context.</param>
        /// <returns>A task that completes when submission processing finishes.</returns>
        public override async Task Submit(SubmitContext submitContext)
        {
            var requiredFields = GetRequiredStringFields();

            foreach (var requiredField in requiredFields)
            {
                var metaData = submitContext.CoreMetaData.FirstOrDefault(x => x.Name == requiredField);
                if (metaData != null)
                {
                    // The Connector API's [Required] validation trims strings, so a
                    // whitespace-only value is rejected the same as an empty one
                    if (string.IsNullOrWhiteSpace(metaData.Value))
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
