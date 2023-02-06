using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.Content;
using RecordPoint.Connectors.SDK.ContentManager;

namespace ReferenceConnector.ContentRegistration
{
    public class ContentRegistrationAction : IContentRegistrationAction
    {
        public Task<ContentResult> BeginAsync(ConnectorConfigModel connectorConfiguration, Channel channel, IDictionary<string, string> context, CancellationToken cancellationToken)
        {
            return Task.FromResult(new ContentResult
            {
                Cursor = DateTimeOffset.Now.Ticks.ToString(),
                ResultType = ContentResultType.Incomplete,
                Reason = "There are more records to register"
            });
        }

        public Task<ContentResult> ContinueAsync(ConnectorConfigModel connectorConfiguration, Channel channel, string cursor, IDictionary<string, string> context, CancellationToken cancellationToken)
        {
            return Task.FromResult(new ContentResult
            {
                Cursor = DateTimeOffset.Now.Ticks.ToString(),
                ResultType = ContentResultType.Complete,
                Reason = "All records have been registered"
            });
        }

        public Task StopAsync(ConnectorConfigModel connectorConfiguration, Channel channel, string cursor, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
