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

        /// <summary>
        /// When set, overrides the result returned by SubmitRecord. May return null to
        /// simulate a missing submit result.
        /// </summary>
        public Func<SubmitResult> RecordSubmitResultFactory { get; set; }

        /// <summary>
        /// When set, overrides the result returned by SubmitBinary.
        /// </summary>
        public Func<SubmitResult> BinarySubmitResultFactory { get; set; }

        /// <summary>
        /// When set, overrides the result returned by SubmitAggregation.
        /// </summary>
        public Func<SubmitResult> AggregationSubmitResultFactory { get; set; }

        /// <summary>
        /// When set, overrides the result returned by SubmitAuditEvent. May return null to
        /// simulate a missing submit result.
        /// </summary>
        public Func<SubmitResult> AuditEventSubmitResultFactory { get; set; }

        /// <summary>
        /// Records disposal callbacks that were invoked, so tests can inspect them.
        /// </summary>
        public BlockingCollection<ItemNotificationDisposalCallbackModel> DisposalCallbacks { get; private set; } = new();


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
            var result = AggregationSubmitResultFactory != null
                ? AggregationSubmitResultFactory()
                : new SubmitResult
                {
                    WaitUntilTime = _dateTimeProvider.UtcNow,
                    SubmitStatus = aggregation != null ? SubmitResult.Status.OK : SubmitResult.Status.Skipped
                };
            if (result != null)
            {
                AggregationSubmitResults.Add(result, cancellationToken);
            }

            return Task.FromResult(result);
        }

        public Task<SubmitResult> SubmitBinary(ConnectorConfigModel connectorConfig, BinaryMetaInfo binaryMetaInfo, Stream binaryStream, CancellationToken cancellationToken)
        {
            var result = BinarySubmitResultFactory != null
                ? BinarySubmitResultFactory()
                : new SubmitResult
                {
                    WaitUntilTime = _dateTimeProvider.UtcNow,
                    SubmitStatus = SubmitResult.Status.OK
                };
            if (result != null)
            {
                BinarySubmitResults.Add(result, cancellationToken);
            }

            return Task.FromResult(result);
        }

        public Task<SubmitResult> SubmitRecord(ConnectorConfigModel connectorConfig, Record record, CancellationToken cancellationToken)
        {
            var result = RecordSubmitResultFactory != null
                ? RecordSubmitResultFactory()
                : new SubmitResult
                {
                    WaitUntilTime = _dateTimeProvider.UtcNow,
                    SubmitStatus = record != null ? SubmitResult.Status.OK : SubmitResult.Status.Skipped
                };
            if (result != null)
            {
                RecordSubmitResults.Add(result, cancellationToken);
            }

            return Task.FromResult(result);
        }

        public Task<SubmitResult> SubmitAuditEvent(ConnectorConfigModel connectorConfig, AuditEvent auditEvent, CancellationToken cancellationToken)
        {
            var result = AuditEventSubmitResultFactory != null
                ? AuditEventSubmitResultFactory()
                : new SubmitResult
                {
                    WaitUntilTime = _dateTimeProvider.UtcNow,
                    SubmitStatus = auditEvent != null ? SubmitResult.Status.OK : SubmitResult.Status.Skipped
                };
            if (result != null)
            {
                AuditEventSubmitResults.Add(result, cancellationToken);
            }

            return Task.FromResult(result);
        }

        public Task DisposalCallback(ItemNotificationDisposalCallbackModel callbackNotification, ConnectorConfigModel connectorConfig, CancellationToken cancellationToken)
        {
            DisposalCallbacks.Add(callbackNotification, cancellationToken);
            return Task.CompletedTask;
        }
    }
}
