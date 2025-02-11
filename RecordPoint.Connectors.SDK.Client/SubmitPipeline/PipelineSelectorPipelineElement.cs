﻿namespace RecordPoint.Connectors.SDK.SubmitPipeline
{
    /// <summary>
    /// A submit pipeline element that branches the pipeline based on whether the submission is
    /// for an item or an aggregation. The pipeline element decides which branch to take based on the 
    /// ItemTypeId field in the core metadata. A value of "1" indicates that the item is an aggregation,
    /// and the aggregation branch is used. Other values use the item branch.
    /// </summary>
    public class PipelineSelectorPipelineElement : SubmitPipelineElementBase
    {
        private readonly ISubmission _submitAggregation = null;

        /// <summary>
        /// Constructs a new PipelineSelectorPipelineElement.
        /// </summary>
        /// <param name="submitRecord"></param>
        /// <param name="submitAggregation"></param>
        public PipelineSelectorPipelineElement(ISubmission submitRecord, ISubmission submitAggregation)
            : base(submitRecord)
        {
            _submitAggregation = submitAggregation;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="submitContext"></param>
        /// <returns></returns>
        public override async Task Submit(SubmitContext submitContext)
        {
            var rawItemType = submitContext.CoreMetaData?.Where(x => x.Name == Fields.ItemTypeId)?.FirstOrDefault()?.Value;
            bool itemTypeValid = int.TryParse(rawItemType, out int itemTypeId);

            if (itemTypeValid)
            {
                if (itemTypeId == 1)
                {
                    // submit using the folder submission pipeline
                    if (_submitAggregation != null)
                    {
                        await _submitAggregation.Submit(submitContext).ConfigureAwait(false);
                    }
                    else
                    {
                        SkipNext(submitContext, "No aggregation pipeline was provided!");
                        return;
                    }
                }
                else
                {
                    // submit using the default submission pipeline
                    await InvokeNext(submitContext).ConfigureAwait(false);
                }
            }
            else
            {
                SkipNext(submitContext, $"Item type {rawItemType} is invalid");
                return;
            }
        }
    }
}
