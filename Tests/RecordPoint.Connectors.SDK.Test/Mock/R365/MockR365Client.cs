using RecordPoint.Connectors.SDK.Client;
using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.Content;
using RecordPoint.Connectors.SDK.Providers;
using RecordPoint.Connectors.SDK.R365;
using RecordPoint.Connectors.SDK.SubmitPipeline;
using System.Collections.Concurrent;

namespace RecordPoint.Connectors.SDK.Test.Mock.R365
{
    public class MockR365Client : IR365Client
    {
        private readonly IDateTimeProvider _dateTimeProvider;

        public BlockingCollection<SubmitResult> RecordSubmitResults { get; private set; }
        public BlockingCollection<SubmitResult> BinarySubmitResults { get; private set; }
        public BlockingCollection<SubmitResult> AggregationSubmitResults { get; private set; }
        public BlockingCollection<SubmitResult> AuditEventSubmitResults { get; private set; }


        public MockR365Client(IDateTimeProvider dateTimeProvider)
        {
            _dateTimeProvider = dateTimeProvider;
            RecordSubmitResults = new BlockingCollection<SubmitResult>();
            BinarySubmitResults = new BlockingCollection<SubmitResult>();
            AggregationSubmitResults = new BlockingCollection<SubmitResult>();
            AuditEventSubmitResults = new BlockingCollection<SubmitResult>();
        }

        public Task AcknowledgeNotificationAsync(ConnectorNotificationModel notification, ProcessingResult result, string message, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task<IList<ConnectorNotificationModel>> GetAllPendingNotifications(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public bool IsConfigured()
        {
            return true;
        }

        public Task<SubmitResult> SubmitAggregation(ConnectorConfigModel connectorConfig, Aggregation aggregation, CancellationToken cancellationToken)
        {
            var result = new SubmitResult
            {
                WaitUntilTime = _dateTimeProvider.UtcNow,
                SubmitStatus = aggregation != null ? SubmitResult.Status.OK : SubmitResult.Status.Skipped
            };
            AggregationSubmitResults.Add(result, cancellationToken);

            return Task.FromResult(result);
        }

        public Task<SubmitResult> SubmitBinary(ConnectorConfigModel connectorConfig, BinaryMetaInfo binaryMetaInfo, Stream binaryStream, CancellationToken cancellationToken)
        {
            var result = new SubmitResult
            {
                WaitUntilTime = _dateTimeProvider.UtcNow,
                SubmitStatus = SubmitResult.Status.OK
            };
            BinarySubmitResults.Add(result, cancellationToken);

            return Task.FromResult(result);
        }

        public Task<SubmitResult> SubmitRecord(ConnectorConfigModel connectorConfig, Record record, CancellationToken cancellationToken)
        {
            var result = new SubmitResult
            {
                WaitUntilTime = _dateTimeProvider.UtcNow,
                SubmitStatus = record != null ? SubmitResult.Status.OK : SubmitResult.Status.Skipped
            };
            RecordSubmitResults.Add(result, cancellationToken);

            return Task.FromResult(result);
        }

        public Task<SubmitResult> SubmitAuditEvent(ConnectorConfigModel connectorConfig, AuditEvent auditEvent, CancellationToken cancellationToken)
        {
            var result = new SubmitResult
            {
                WaitUntilTime = _dateTimeProvider.UtcNow,
                SubmitStatus = auditEvent != null ? SubmitResult.Status.OK : SubmitResult.Status.Skipped
            };
            RecordSubmitResults.Add(result, cancellationToken);

            return Task.FromResult(result);
        }
    }
}
