using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.Content;
using RecordPoint.Connectors.SDK.ContentManager;

namespace ReferenceConnector.RecordDisposal
{
    public class RecordDisposalAction : IRecordDisposalAction
    {
        public Task<RecordDisposalResult> ExecuteAsync(ConnectorConfigModel connectorConfiguration, Record record, CancellationToken cancellationToken)
        {
            var result = new RecordDisposalResult { 
                ResultType = RecordDisposalResultType.Complete,
                Reason = "Record Successfully Deleted"
            };

            return Task.FromResult(result);
        }
    }
}
