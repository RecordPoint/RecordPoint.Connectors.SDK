using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.Content;
using RecordPoint.Connectors.SDK.SubmitPipeline;

namespace RecordPoint.Connectors.SDK.ContentReport
{
    public class ContentReportFilter : IContentReportFilter
    {
        private readonly ISubmission _filterSubmission;

        public ContentReportFilter(ISubmission submission)
        {
            _filterSubmission = submission;
        }

        /// <summary>
        /// Get filtered records for report
        /// </summary>
        /// <param name="config"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<IList<Record>?> GetRecords(ConnectorConfigModel config, IList<Record> input)
        {
            if (input == null || !input.Any())
                return input;

            var result = new List<Record>();
            foreach (var item in input)
            {
                var context = CreateSubmitContextForReport(config, item);
                await _filterSubmission.Submit(context).ConfigureAwait(false);
                var outCome = context.SubmitResult;
                if (outCome == null) continue;
                switch (outCome.SubmitStatus)
                {
                    case SubmitResult.Status.OK:
                        result.Add(item);
                        break;
                    default:
                        // Add Log
                        break;
                }
            }

            return result;
        }

        private SubmitContext CreateSubmitContextForReport(ConnectorConfigModel config, Record record)
        {
            var coreFields = new List<SubmissionMetaDataModel>
            {
                new SubmissionMetaDataModel(Fields.ExternalId, type: nameof(String), value: record.ExternalId),
                new SubmissionMetaDataModel(Fields.Title, type: nameof(String), value: record.Title),
                new SubmissionMetaDataModel(Fields.SourceLastModifiedBy, type: nameof(String), value: record.SourceLastModifiedBy),
                new SubmissionMetaDataModel(Fields.SourceLastModifiedDate, type: nameof(DateTimeOffset), value: record.SourceLastModifiedDate.ToString("o")),
                new SubmissionMetaDataModel(Fields.SourceCreatedBy, type: nameof(String), value: record.SourceCreatedBy),
                new SubmissionMetaDataModel(Fields.Author, type: nameof(String), value: record.Author),
                new SubmissionMetaDataModel(Fields.ParentExternalId, type: nameof(String), value: record.ParentExternalId),
                new SubmissionMetaDataModel(Fields.ContentVersion, type: nameof(String), value: record.ContentVersion),
                new SubmissionMetaDataModel(Fields.Location, type: nameof(String), value: record.Location),
                new SubmissionMetaDataModel(Fields.MediaType, type: nameof(String), value: "Electronic"),
                new SubmissionMetaDataModel(Fields.MimeType, type: nameof(String), value: record.MimeType)
            };


            // TODO. Add capacity for source fields. Possibly copied from the record.
            var sourceFields = new List<SubmissionMetaDataModel>() {
                new SubmissionMetaDataModel("Name", type: nameof(String), value: record.Title)
            };

            var submitContext = new SubmitContext
            {
                CoreMetaData = coreFields,
                SourceMetaData = sourceFields,
                Filters = config.Filters
            };

            return submitContext;
        }
    }
}
