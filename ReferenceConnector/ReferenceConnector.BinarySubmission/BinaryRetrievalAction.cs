using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.Content;
using RecordPoint.Connectors.SDK.ContentManager;
using ReferenceConnector.Common;

namespace ReferenceConnector.BinarySubmission
{
    public class BinaryRetrievalAction : IBinaryRetrievalAction
    {
        private readonly IRandomHelper _randomHelper;

        public BinaryRetrievalAction(IRandomHelper randomHelper)
        {
            _randomHelper = randomHelper;
        }

        public Task<BinaryRetrievalResult> ExecuteAsync(ConnectorConfigModel connectorConfiguration, BinaryMetaInfo binaryMetaInfo, CancellationToken cancellationToken)
        {
            const int rndWordCount = 100;

            var ms = new MemoryStream();
            var tw = new StreamWriter(ms);
            var content = _randomHelper.GenerateRandomWords(rndWordCount);
            tw.Write(content);

            var outcome = new BinaryRetrievalResult
            {
                ResultType = BinaryRetrievalResultType.Complete,
                Stream = ms,
                Reason = "Successfully decoded content from meta info"
            };
            return Task.FromResult(outcome);
        }
    }
}
