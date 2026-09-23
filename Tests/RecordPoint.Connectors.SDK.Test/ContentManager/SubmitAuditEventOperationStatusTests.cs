#nullable enable
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using RecordPoint.Connectors.SDK.Abstractions.ContentManager;
using RecordPoint.Connectors.SDK.Connectors;
using RecordPoint.Connectors.SDK.ContentManager;
using RecordPoint.Connectors.SDK.SubmitPipeline;
using RecordPoint.Connectors.SDK.Test.Mock.R365;
using RecordPoint.Connectors.SDK.Test.Mock.Work;
using RecordPoint.Connectors.SDK.Work;
using Xunit;

namespace RecordPoint.Connectors.SDK.Test.ContentManager
{
    /// <summary>
    /// Exercises the submit-result switch branches of <see cref="SubmitAuditEventOperation"/>
    /// (OK, Skipped, Deferred, TooManyRequests, ConnectorDisabled, ConnectorNotFound,
    /// null result and unexpected status).
    /// </summary>
    public class SubmitAuditEventOperationStatusTests : CommonTestBase<SubmitAuditEventOperationSut>
    {
        private async Task<SubmitAuditEventOperation> ArrangeAsync(
            Func<SubmitResult?>? resultFactory,
            bool registerCallback = true)
        {
            if (registerCallback)
            {
                SUT!.SelectAuditEventSubmissionCallbackActionMock(new Mock<IAuditEventSubmissionCallbackAction>());
            }
            else
            {
                // Register a factory that returns null so the "no callback registered" branch is exercised.
                SUT!.AuditEventSubmissionCallbackActionFactory = () => null!;
            }

            await StartSutAsync();

            var connector = ContentManagerSutBase.CreateConnector1();
            await SUT!.GetConnectorManager().SetConnectorAsync(connector, CancellationToken.None);

            if (resultFactory != null)
            {
                var mockClient = Services!.GetRequiredService<MockR365Client>();
                mockClient.AuditEventSubmitResultFactory = resultFactory;
            }

            return Services!.GetRequiredService<SubmitAuditEventOperation>();
        }

        [Fact]
        public async Task SubmitAuditEvent_Ok_Completes()
        {
            var operation = await ArrangeAsync(() => new SubmitResult { SubmitStatus = SubmitResult.Status.OK });
            var connector = ContentManagerSutBase.CreateConnector1();
            var workMessage = SUT!.CreateSubmitAuditEventManagedWorkStatusModel(connector);

            await operation.RunWorkRequestAsync(SUT.CreateSubmitAuditEventRequest(workMessage), CancellationToken.None);

            Assert.Equal(WorkResultType.Complete, operation.ResultType);
            Assert.Equal("Audit Event submitted", operation.ResultReason);
        }

        [Fact]
        public async Task SubmitAuditEvent_Skipped_Completes()
        {
            var operation = await ArrangeAsync(() => new SubmitResult { SubmitStatus = SubmitResult.Status.Skipped });
            var connector = ContentManagerSutBase.CreateConnector1();
            var workMessage = SUT!.CreateSubmitAuditEventManagedWorkStatusModel(connector);

            await operation.RunWorkRequestAsync(SUT.CreateSubmitAuditEventRequest(workMessage), CancellationToken.None);

            Assert.Equal(WorkResultType.Complete, operation.ResultType);
            Assert.Equal("Audit Event skipped", operation.ResultReason);
        }

        [Fact]
        public async Task SubmitAuditEvent_Deferred_CompletesAndRequeues()
        {
            var waitUntil = new DateTime(2030, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            var operation = await ArrangeAsync(() => new SubmitResult { SubmitStatus = SubmitResult.Status.Deferred, WaitUntilTime = waitUntil });
            var connector = ContentManagerSutBase.CreateConnector1();
            var workMessage = SUT!.CreateSubmitAuditEventManagedWorkStatusModel(connector);

            await operation.RunWorkRequestAsync(SUT.CreateSubmitAuditEventRequest(workMessage), CancellationToken.None);

            Assert.Equal(WorkResultType.Complete, operation.ResultType);
            Assert.Equal("Audit Event was deferred by Records365", operation.ResultReason);

            var workQueueClient = Services!.GetRequiredService<MockWorkQueueClient>();
            Assert.Contains(workQueueClient.SubmittedRequests, r => r.WorkType == SubmitAuditEventOperation.WORK_TYPE);
        }

        [Fact]
        public async Task SubmitAuditEvent_Deferred_WithoutSuggestedTime_UsesDefaultDeferral()
        {
            var operation = await ArrangeAsync(() => new SubmitResult { SubmitStatus = SubmitResult.Status.Deferred, WaitUntilTime = null });
            var connector = ContentManagerSutBase.CreateConnector1();
            var workMessage = SUT!.CreateSubmitAuditEventManagedWorkStatusModel(connector);

            await operation.RunWorkRequestAsync(SUT.CreateSubmitAuditEventRequest(workMessage), CancellationToken.None);

            Assert.Equal(WorkResultType.Complete, operation.ResultType);
            Assert.Equal("Audit Event was deferred by Records365", operation.ResultReason);
        }

        [Fact]
        public async Task SubmitAuditEvent_TooManyRequests_CompletesAndRequeues()
        {
            var operation = await ArrangeAsync(() => new SubmitResult { SubmitStatus = SubmitResult.Status.TooManyRequests, WaitUntilTime = null });
            var connector = ContentManagerSutBase.CreateConnector1();
            var workMessage = SUT!.CreateSubmitAuditEventManagedWorkStatusModel(connector);

            await operation.RunWorkRequestAsync(SUT.CreateSubmitAuditEventRequest(workMessage), CancellationToken.None);

            Assert.Equal(WorkResultType.Complete, operation.ResultType);
            Assert.Equal("Backoff requested by Records365", operation.ResultReason);

            var workQueueClient = Services!.GetRequiredService<MockWorkQueueClient>();
            Assert.Contains(workQueueClient.SubmittedRequests, r => r.WorkType == SubmitAuditEventOperation.WORK_TYPE);
        }

        [Fact]
        public async Task SubmitAuditEvent_ConnectorDisabledStatus_Abandons()
        {
            var operation = await ArrangeAsync(() => new SubmitResult { SubmitStatus = SubmitResult.Status.ConnectorDisabled });
            var connector = ContentManagerSutBase.CreateConnector1();
            var workMessage = SUT!.CreateSubmitAuditEventManagedWorkStatusModel(connector);

            await operation.RunWorkRequestAsync(SUT.CreateSubmitAuditEventRequest(workMessage), CancellationToken.None);

            Assert.Equal(WorkResultType.Abandoned, operation.ResultType);
            Assert.Equal("Connector was disabled", operation.ResultReason);
        }

        [Fact]
        public async Task SubmitAuditEvent_ConnectorNotFoundStatus_Abandons()
        {
            var operation = await ArrangeAsync(() => new SubmitResult { SubmitStatus = SubmitResult.Status.ConnectorNotFound });
            var connector = ContentManagerSutBase.CreateConnector1();
            var workMessage = SUT!.CreateSubmitAuditEventManagedWorkStatusModel(connector);

            await operation.RunWorkRequestAsync(SUT.CreateSubmitAuditEventRequest(workMessage), CancellationToken.None);

            Assert.Equal(WorkResultType.Abandoned, operation.ResultType);
            Assert.Equal("The connector was not found in Records365", operation.ResultReason);
        }

        [Fact]
        public async Task SubmitAuditEvent_NullResult_Fails()
        {
            var operation = await ArrangeAsync(() => null);
            var connector = ContentManagerSutBase.CreateConnector1();
            var workMessage = SUT!.CreateSubmitAuditEventManagedWorkStatusModel(connector);

            await operation.RunWorkRequestAsync(SUT.CreateSubmitAuditEventRequest(workMessage), CancellationToken.None);

            Assert.Equal(WorkResultType.Failed, operation.ResultType);
            Assert.Equal("Unexpected no submit result", operation.ResultReason);
        }

        [Fact]
        public async Task SubmitAuditEvent_UnexpectedStatus_Fails()
        {
            var operation = await ArrangeAsync(() => new SubmitResult { SubmitStatus = (SubmitResult.Status)999 });
            var connector = ContentManagerSutBase.CreateConnector1();
            var workMessage = SUT!.CreateSubmitAuditEventManagedWorkStatusModel(connector);

            await operation.RunWorkRequestAsync(SUT.CreateSubmitAuditEventRequest(workMessage), CancellationToken.None);

            Assert.Equal(WorkResultType.Failed, operation.ResultType);
            Assert.StartsWith("Unexpected submit result", operation.ResultReason);
        }

        [Fact]
        public async Task SubmitAuditEvent_Ok_WithoutRegisteredCallback_StillCompletes()
        {
            // No callback registered: InvokeSubmissionCallbackAsync should bail out early.
            var operation = await ArrangeAsync(
                () => new SubmitResult { SubmitStatus = SubmitResult.Status.OK },
                registerCallback: false);

            var connector = ContentManagerSutBase.CreateConnector1();
            var workMessage = SUT!.CreateSubmitAuditEventManagedWorkStatusModel(connector);

            await operation.RunWorkRequestAsync(SUT.CreateSubmitAuditEventRequest(workMessage), CancellationToken.None);

            Assert.Equal(WorkResultType.Complete, operation.ResultType);
            Assert.Equal("Audit Event submitted", operation.ResultReason);
        }
    }
}
