using System;
using System.Linq;
using System.Threading;
using System.Collections.Generic;
using RecordPoint.Connectors.SDK.Client;
using RecordPoint.Connectors.SDK.Client.Models;

namespace RecordPoint.Connectors.SDK.SubmitPipeline
{
    public class SubmitContext
    {
        public Guid ConnectorConfigId { get; set; }

        public Guid TenantId { get; set; }

        public Guid CorrelationId { get; set; }

        public ApiClientFactorySettings ApiClientFactorySettings { get; set; }

        public IList<SubmissionMetaDataModel> CoreMetaData { get; set; }

        public IList<SubmissionMetaDataModel> SourceMetaData { get; set; }

        public SubmitResult SubmitResult { get; set; }

        public CancellationToken CancellationToken { get; set; }

        public virtual string LogPrefix()
        {
            return
                $"TenantId [{TenantId}] ConnectorConfigId [{ConnectorConfigId}] CorrelationId [{CorrelationId}] Title [{CoreMetaData?.FirstOrDefault(metaInfo => metaInfo.Name == Fields.Title)?.Value ?? "<no title field in metadata found>"}] ";
        }

    }
}
