using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RecordPoint.Connectors.Client.Models;

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
        protected UnspecifiedFieldValuePipelineElementBase(ISubmission next) : base(next)
        {
        }

        protected abstract IEnumerable<string> GetRequiredStringFields();

        private static readonly string UnspecifiedFieldValue = "Unspecified";
        
        public override async Task Submit(SubmitContext submitContext)
        {
            var requiredFields = GetRequiredStringFields();
            
            foreach (var requiredField in requiredFields)
            {
                var metaData = submitContext.CoreMetaData.SingleOrDefault(x => x.Name == requiredField);
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
