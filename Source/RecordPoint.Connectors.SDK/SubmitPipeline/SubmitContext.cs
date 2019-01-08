using System;
using System.Linq;
using System.Threading;
using System.Collections.Generic;
using RecordPoint.Connectors.SDK.Client;
using RecordPoint.Connectors.SDK.Client.Models;

namespace RecordPoint.Connectors.SDK.SubmitPipeline
{
    /// <summary>
    /// A SubmitContext captures all the state that relates to an individual submission from
    /// the content source to the Records365 vNext Connector API. This class can be subclassed
    /// to define more state that is specific to a particular connector or submission type.
    /// </summary>
    public class SubmitContext
    {
        /// <summary>
        /// String to be returned if the SubmitContext's LogPrefix method is called and no
        /// title is present on the SubmitContext
        /// </summary>
        protected const string NoTitleFound = "<no title found>";

        /// <summary>
        /// The ID of the Records365 vNext connector that this submission is being made through.
        /// </summary>
        public Guid ConnectorConfigId { get; set; }

        /// <summary>
        /// The ID of the Records365 vNext tenant that this submission is being made to.
        /// </summary>
        public Guid TenantId { get; set; }

        /// <summary>
        /// An ID used for correlating log messages for debugging purposes.
        /// </summary>
        public Guid CorrelationId { get; set; } = Guid.NewGuid();

        /// <summary>
        /// The settings used to create an ApiClient
        /// </summary>
        public ApiClientFactorySettings ApiClientFactorySettings { get; set; }

        /// <summary>
        /// The settings used for submission to the Records365 vNext Connector API.
        /// </summary>
        public AuthenticationHelperSettings AuthenticationHelperSettings { get; set; }

        /// <summary>
        /// The core metadata fields of the submission.
        /// </summary>
        public IList<SubmissionMetaDataModel> CoreMetaData { get; set; }

        /// <summary>
        /// The source metadata fields of the submission.
        /// </summary>
        public IList<SubmissionMetaDataModel> SourceMetaData { get; set; }

        /// <summary>
        /// Relationship info of the submission
        /// </summary>
        public IList<RelationshipDataModel> Relationships { get; set; }
        
        /// <summary>
        /// When submitting an aggregation, this value indicates whether the aggregation
        /// is a record folder or a box.
        /// </summary>
        public int? ItemTypeId { get; set; }

        /// <summary>
        /// Value that indicates if Records365 vNext has an items aggregation.
        /// If this SubmitContext has passed through the HttpSubmitItemPipelineElement, this value
        /// will be populated with a flag that indicates if the platform knows about the aggregation
        /// for the item. If this is false, the connector may take some action to submit the aggregation.
        /// If this is true, the platform already has the aggregation in the system and the connector
        /// doesn't need to submit the aggregation again.
        /// </summary>
        public bool? AggregationFoundDuringItemSubmission { get; set; }

        /// <summary>
        /// The result of the submission. This will be set by one of the HttpSubmit* pipeline elements.
        /// </summary>
        public SubmitResult SubmitResult { get; set; } = new SubmitResult();

        /// <summary>
        /// A cancellation token that can be used to cancel a submission while it is in the middle of the pipeline.
        /// </summary>
        public CancellationToken CancellationToken { get; set; }

        /// <summary>
        /// Defines any filtering to be done. If Filters are defined and the Connector makes use of the FilterPipelineElement,
        /// then the Core and Source metadata of this SubmitContext will be checked against the Filters, as followds:
        /// If the Excluded filter is defined and is matched, the submission will be skipped.
        /// If the Included filter is not defined, any submission which does not match the Excluded filter will continue in the pipeline.
        /// If the Included filter is defined, only submissions which match the Included filter will continue in the pipeline - All others will be skipped.
        /// </summary>
        public FiltersModel Filters { get; set; }

        /// <summary>
        /// A prefix that will be applied to all logs created using the SubmitPipelineElementBase Log* methods (e.g. LogMessage)
        /// Any messages output by Log* methods will be prefixed with this string
        /// </summary>
        /// <returns></returns>
        public virtual string LogPrefix()
        {
            return
                $"TenantId [{TenantId}] ConnectorConfigId [{ConnectorConfigId}] CorrelationId [{CorrelationId}] Title [{GetTitle()}] ";
        }

        /// <summary>
        /// Returns the title of the object the SubmitContext is related to. Typically this is sourced from the Core metadata on the 
        /// SubmitContext, but in some cases (e.g. on the BinarySubmitContext) it may be stored in a strongly typed field
        /// </summary>
        /// <returns></returns>
        protected virtual string GetTitle()
        {
            var fileName = CoreMetaData?.FirstOrDefault(metaInfo => metaInfo.Name == Fields.Title)?.Value;

            return !string.IsNullOrEmpty(fileName) ? fileName : NoTitleFound;
        }
    }
}
