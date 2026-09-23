using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RecordPoint.Connectors.SDK.Caching.Semaphore;
using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.Connectors;
using RecordPoint.Connectors.SDK.Content;
using RecordPoint.Connectors.SDK.ContentManager;
using RecordPoint.Connectors.SDK.Test.Common;
using RecordPoint.Connectors.SDK.Test.Content;
using RecordPoint.Connectors.SDK.Work;
using System.Text.Json;
using Xunit;
using Record = RecordPoint.Connectors.SDK.Content.Record;

namespace RecordPoint.Connectors.SDK.Test.ContentManager;

/// <summary>
/// Tests for the work work items
/// </summary>
public class ContentRegistrationOperationTests : CommonTestBase<ContentRegistrationOperationSut>
{
    private const string SEMAPHORE_DEFERRAL_REASON = "Semaphore Lock enabled, deferring Content Registration.";

    public class ContentRegistrationAction : IContentRegistrationAction
    {
        public List<Aggregation> Aggregations { get; set; } = new();
        public List<Record> Records { get; set; } = new();
        public ContentResultType ResultType { get; set; }
        public string Reason { get; set; } = string.Empty;
        public Exception Exception { get; set; }
        public string Cursor { get; set; } = string.Empty;
        public SemaphoreLockType SemaphoreLockType { get; set; } = SemaphoreLockType.Global;
        public int? NextDelay { get; set; } = null;

        public string ReportedContinueCursor { get; set; }
        public int BeginCalledCount { get; set; } = 0;
        public int ContinueCalledCount { get; set; } = 0;
        public int StopCalledCount { get; set; } = 0;

        public bool ThrowExecption { get; set; } = false;

        public Task<ContentResult> BeginAsync(ConnectorConfigModel connectorConfiguration, Channel channel, IDictionary<string, string> context, CancellationToken cancellationToken)
        {
            BeginCalledCount++;

            if (ThrowExecption) throw new TestException();

            return Task.FromResult(CreateContentResult());
        }

        public Task<ContentResult> ContinueAsync(ConnectorConfigModel connectorConfiguration, Channel channel, string cursor, IDictionary<string, string> context, CancellationToken cancellationToken)
        {
            ReportedContinueCursor = cursor;
            ContinueCalledCount++;

            if (ThrowExecption) throw new TestException();

            return Task.FromResult(CreateContentResult());
        }

        public Task StopAsync(ConnectorConfigModel connectorConfiguration, Channel channel, string cursor, CancellationToken cancellationToken)
        {
            StopCalledCount++;
            return Task.CompletedTask;
        }

        private ContentResult CreateContentResult() => new()
        {
            Aggregations = Aggregations,
            Records = Records,
            ResultType = ResultType,
            Reason = Reason,
            Exception = Exception,
            Cursor = Cursor,
            SemaphoreLockType = SemaphoreLockType,
            NextDelay = NextDelay
        };
    }

    [Fact]
    public async Task IfRequestedConnectorMissing_WorkIsAbandoned()
    {
        var cancellationToken = CancellationToken.None;

        await StartSutAsync();

        var connector = ContentManagerSutBase.CreateConnector1();
        var channel = ContentManagerSutBase.CreateChannel1();

        var workMessage = SUT.CreateContentRegistrationManagedWorkStatusModel(connector, channel);

        using var servicesScope = Services.CreateScope();
        var operation = servicesScope.ServiceProvider.GetRequiredService<ContentRegistrationOperation>();
        await operation.RunWorkRequestAsync(SUT.CreateContentRegistrationRequest(workMessage), cancellationToken);

        var workQueueClient = ContentManagerSutBase.GetWorkQueueClient(Services);
        //Operation result should be abandoned
        Assert.Equal(WorkResultType.Abandoned, operation.ResultType);
        //Operation result reason should be 'Connector not found'
        Assert.Equal("Connector not found", operation.ResultReason);
        //Content Registration work should not be requeued
        Assert.DoesNotContain(workQueueClient.SubmittedRequests, a => a.WorkType == ContentRegistrationOperation.WORK_TYPE);
    }

    [Fact]
    public async Task IfChannelMissing_OperationIsAbandonded()
    {
        var cancellationToken = CancellationToken.None;

        await StartSutAsync();

        var connector = ContentManagerSutBase.CreateConnector1();
        var channel = ContentManagerSutBase.CreateChannel1();

        await SUT.GetConnectorManager().SetConnectorAsync(connector, cancellationToken);

        var workMessage = SUT.CreateContentRegistrationManagedWorkStatusModel(connector, channel);

        using var servicesScope = Services.CreateScope();
        var operation = servicesScope.ServiceProvider.GetRequiredService<ContentRegistrationOperation>();
        await operation.RunWorkRequestAsync(SUT.CreateContentRegistrationRequest(workMessage), cancellationToken);

        var workQueueClient = ContentManagerSutBase.GetWorkQueueClient(Services);
        //Operation result should be abandoned
        Assert.Equal(WorkResultType.Abandoned, operation.ResultType);
        //Operation result reason should be 'Connector not found'
        Assert.Equal("Channel not found", operation.ResultReason);
        //Content Registration work should not be requeued
        Assert.DoesNotContain(workQueueClient.SubmittedRequests, a => a.WorkType == ContentRegistrationOperation.WORK_TYPE);
    }

    [Fact]
    public async Task OnFirstSyncWithConnectorDisabled_ContinuesWorkWithoutScanning()
    {
        var cancellationToken = CancellationToken.None;

        var testCursor1 = "TestCursor1";
        var scanner = new ContentRegistrationAction
        {
            ResultType = ContentResultType.Complete,
            Cursor = testCursor1
        };
        SUT.SelectContentRegistrationAction(scanner);
        await StartSutAsync();

        var connector = ContentManagerSutBase.CreateConnector1();
        ContentManagerSutBase.DisableConnector(connector, DateTimeOffset.Now);
        var channel = ContentManagerSutBase.CreateChannel1();

        await SUT.GetConnectorManager().SetConnectorAsync(connector, cancellationToken);
        await SUT.GetChannelManager().UpsertChannelAsync(channel, cancellationToken);

        var workMessage = SUT.CreateContentRegistrationManagedWorkStatusModel(connector, channel);

        using var servicesScope = Services.CreateScope();
        var operation = servicesScope.ServiceProvider.GetRequiredService<ContentRegistrationOperation>();
        await operation.RunWorkRequestAsync(SUT.CreateContentRegistrationRequest(workMessage), cancellationToken);

        var workQueueClient = ContentManagerSutBase.GetWorkQueueClient(Services);
        //Operation result should be complete
        Assert.Equal(WorkResultType.Complete, operation.ResultType);
        //Operation result reason should be 'Connector disabled'
        Assert.Equal("Connector disabled", operation.ResultReason);
        //Action methods should not have been called
        Assert.Equal(0, scanner.BeginCalledCount);
        Assert.Equal(0, scanner.ContinueCalledCount);
        //Content Registration work should be requeued
        Assert.Contains(workQueueClient.SubmittedRequests, a => a.WorkType == ContentRegistrationOperation.WORK_TYPE);
    }

    [Fact]
    public async Task OnFirstSyncWithConnectorDisabled_ExceedsDisabledThreshold_AbandonsWork()
    {
        var cancellationToken = CancellationToken.None;

        var testCursor1 = "TestCursor1";
        var scanner = new ContentRegistrationAction
        {
            ResultType = ContentResultType.Complete,
            Cursor = testCursor1
        };
        SUT.SelectContentRegistrationAction(scanner);

        var myConfig = new Dictionary<string, string>
        {
            { "ContentManager:MaxDisabledConnectorAge", "1209600" },
        };

        var config = new ConfigurationBuilder()
            .AddInMemoryCollection(myConfig)
            .Build();

        await StartSutAsync(config);

        var connector = ContentManagerSutBase.CreateConnector1();
        var channel = ContentManagerSutBase.CreateChannel1();
        ContentManagerSutBase.DisableConnector(connector, DateTimeOffset.Now.AddDays(-30));

        await SUT.GetConnectorManager().SetConnectorAsync(connector, cancellationToken);
        await SUT.GetChannelManager().UpsertChannelAsync(channel, cancellationToken);

        var workMessage = SUT.CreateContentRegistrationManagedWorkStatusModel(connector, channel);

        using var servicesScope = Services.CreateScope();
        var operation = servicesScope.ServiceProvider.GetRequiredService<ContentRegistrationOperation>();
        await operation.RunWorkRequestAsync(SUT.CreateContentRegistrationRequest(workMessage), cancellationToken);

        var workQueueClient = ContentManagerSutBase.GetWorkQueueClient(Services);
        //Operation result should be abandoned
        Assert.Equal(WorkResultType.Abandoned, operation.ResultType);
        //Operation result reason should be 'Connector disabled'
        Assert.Equal("Connector disabled", operation.ResultReason);
        //Action methods should not have been called
        Assert.Equal(0, scanner.BeginCalledCount);
        Assert.Equal(0, scanner.ContinueCalledCount);
        //Content Registration work should be NOT requeued
        Assert.DoesNotContain(workQueueClient.SubmittedRequests, a => a.WorkType == ContentRegistrationOperation.WORK_TYPE);
    }

    [Fact]
    public async Task OnFirstSync_BeginSyncIsCalled_NoQueuedWorkWhenComplete()
    {
        var cancellationToken = CancellationToken.None;

        var scanner = new ContentRegistrationAction
        {
            ResultType = ContentResultType.Complete
        };
        SUT.SelectContentRegistrationAction(scanner);

        await StartSutAsync();

        var connector = ContentManagerSutBase.CreateConnector1();
        var channel = ContentManagerSutBase.CreateChannel1();

        await SUT.GetConnectorManager().SetConnectorAsync(connector, cancellationToken);
        await SUT.GetChannelManager().UpsertChannelAsync(channel, cancellationToken);

        var workMessage = SUT.CreateContentRegistrationManagedWorkStatusModel(connector, channel);

        using var servicesScope = Services.CreateScope();
        var operation = servicesScope.ServiceProvider.GetRequiredService<ContentRegistrationOperation>();
        await operation.RunWorkRequestAsync(SUT.CreateContentRegistrationRequest(workMessage), cancellationToken);

        //Operation result should be complete
        Assert.Equal(WorkResultType.Complete, operation.ResultType);
        //Begin method should have been called once
        Assert.Equal(1, scanner.BeginCalledCount);
        //Continue method should not have been called
        Assert.Equal(0, scanner.ContinueCalledCount);

        var workQueueClient = ContentManagerSutBase.GetWorkQueueClient(Services);
        //Content Registration work should not be continued when result is Complete
        Assert.Empty(workQueueClient.SubmittedRequests);
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task OnFirstSync_ContentDiscovered_EnqueuesSubmissions(bool submitRecordAndBinariesSynchronously)
    {
        var cancellationToken = CancellationToken.None;

        var scanner = new ContentRegistrationAction
        {
            ResultType = ContentResultType.Complete,
            Aggregations = [ContentManagerSutBase.CreateAggregationContent(1)],
            Records = [ContentManagerSutBase.CreateRecordContent(1)]
        };
        SUT.SelectContentRegistrationAction(scanner);

        var myConfig = new Dictionary<string, string>
        {
            { "ContentManager:RecordSubmission:SubmitRecordAndBinariesSynchronously", submitRecordAndBinariesSynchronously.ToString() },
        };

        var config = new ConfigurationBuilder()
            .AddInMemoryCollection(myConfig)
            .Build();

        await StartSutAsync(config);

        var connector = ContentManagerSutBase.CreateConnector1();
        var channel = ContentManagerSutBase.CreateChannel1();

        await SUT.GetConnectorManager().SetConnectorAsync(connector, cancellationToken);
        await SUT.GetChannelManager().UpsertChannelAsync(channel, cancellationToken);

        var workMessage = SUT.CreateContentRegistrationManagedWorkStatusModel(connector, channel);

        using var servicesScope = Services.CreateScope();
        var operation = servicesScope.ServiceProvider.GetRequiredService<ContentRegistrationOperation>();
        await operation.RunWorkRequestAsync(SUT.CreateContentRegistrationRequest(workMessage), cancellationToken);

        //Operation result should be complete
        Assert.Equal(WorkResultType.Complete, operation.ResultType);
        //Begin method should have been called once
        Assert.Equal(1, scanner.BeginCalledCount);
        //Continue method should not have been called
        Assert.Equal(0, scanner.ContinueCalledCount);

        var workQueueClient = ContentManagerSutBase.GetWorkQueueClient(Services);
        //Submission work should be enqueued for the discovered aggregation and record
        Assert.Contains(workQueueClient.SubmittedRequests, a => a.WorkType == SubmitRecordOperation.WORK_TYPE && JsonSerializer.Deserialize<Record>(a.Body).ExternalId == "Record_1");
        Assert.Contains(workQueueClient.SubmittedRequests, a => a.WorkType == SubmitAggregationOperation.WORK_TYPE && JsonSerializer.Deserialize<Aggregation>(a.Body).ExternalId == "Aggregation_1");

        if (submitRecordAndBinariesSynchronously)
        {
            //Binary Submission should not be enqueued when submitting records & binaries synchronously
            Assert.DoesNotContain(workQueueClient.SubmittedRequests, a => a.WorkType == SubmitBinaryOperation.WORK_TYPE);
        }
        else
        {
            Assert.Contains(workQueueClient.SubmittedRequests, a => a.WorkType == SubmitBinaryOperation.WORK_TYPE && JsonSerializer.Deserialize<BinaryMetaInfo>(a.Body).ExternalId == "Binary_1");
        }
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task OnSecondSync_ContentDiscovered_EnqueuesSubmissions(bool submitRecordAndBinariesSynchronously)
    {
        var cancellationToken = CancellationToken.None;

        var scanner = new ContentRegistrationAction
        {
            ResultType = ContentResultType.Incomplete,
            Cursor = "TestCursor1",
            Aggregations = [ContentManagerSutBase.CreateAggregationContent(1)],
            Records = [ContentManagerSutBase.CreateRecordContent(1)]
        };
        SUT.SelectContentRegistrationAction(scanner);

        var myConfig = new Dictionary<string, string>
        {
            { "ContentManager:RecordSubmission:SubmitRecordAndBinariesSynchronously", submitRecordAndBinariesSynchronously.ToString() },
        };

        var config = new ConfigurationBuilder()
            .AddInMemoryCollection(myConfig)
            .Build();

        await StartSutAsync(config);

        var connector = ContentManagerSutBase.CreateConnector1();
        var channel = ContentManagerSutBase.CreateChannel1();

        await SUT.GetConnectorManager().SetConnectorAsync(connector, cancellationToken);
        await SUT.GetChannelManager().UpsertChannelAsync(channel, cancellationToken);

        var workQueueClient = ContentManagerSutBase.GetWorkQueueClient(Services);

        using (var servicesScope = Services.CreateScope())
        {
            var operation = servicesScope.ServiceProvider.GetRequiredService<ContentRegistrationOperation>();
            var workMessage = SUT.CreateContentRegistrationManagedWorkStatusModel(connector, channel);
            await operation.RunWorkRequestAsync(SUT.CreateContentRegistrationRequest(workMessage), cancellationToken);

            //Operation result should be complete
            Assert.Equal(WorkResultType.Complete, operation.ResultType);
            //Begin method should have been called once
            Assert.Equal(1, scanner.BeginCalledCount);
            //Continue method should not have been called
            Assert.Equal(0, scanner.ContinueCalledCount);

            //Submission work should be enqueued for the discovered aggregation and record
            Assert.Contains(workQueueClient.SubmittedRequests, a => a.WorkType == SubmitRecordOperation.WORK_TYPE && JsonSerializer.Deserialize<Record>(a.Body).ExternalId == "Record_1");
            Assert.Contains(workQueueClient.SubmittedRequests, a => a.WorkType == SubmitAggregationOperation.WORK_TYPE && JsonSerializer.Deserialize<Aggregation>(a.Body).ExternalId == "Aggregation_1");

            if (submitRecordAndBinariesSynchronously)
            {
                //Binary Submission should not be enqueued when submitting records & binaries synchronously
                Assert.DoesNotContain(workQueueClient.SubmittedRequests, a => a.WorkType == SubmitBinaryOperation.WORK_TYPE);
            }
            else
            {
                Assert.Contains(workQueueClient.SubmittedRequests, a => a.WorkType == SubmitBinaryOperation.WORK_TYPE && JsonSerializer.Deserialize<BinaryMetaInfo>(a.Body).ExternalId == "Binary_1");
            }

            //Content Registration should be requeued
            Assert.Contains(workQueueClient.SubmittedRequests, a => a.WorkType == ContentRegistrationOperation.WORK_TYPE);
        }

        scanner.ResultType = ContentResultType.Complete;
        scanner.Aggregations = [ContentManagerSutBase.CreateAggregationContent(2)];
        scanner.Records = [ContentManagerSutBase.CreateRecordContent(2, 2)];

        using (var servicesScope = Services.CreateScope())
        {
            var workRequest = workQueueClient.SubmittedRequests.First(a => a.WorkType == ContentRegistrationOperation.WORK_TYPE);
            var operation = servicesScope.ServiceProvider.GetRequiredService<ContentRegistrationOperation>();
            await operation.RunWorkRequestAsync(workRequest, cancellationToken);

            //Operation result should be complete
            Assert.Equal(WorkResultType.Complete, operation.ResultType);
            //Begin method should have been called once
            Assert.Equal(1, scanner.BeginCalledCount);
            //Continue method should not have been called
            Assert.Equal(1, scanner.ContinueCalledCount);

            //Submission work should be enqueued for the discovered aggregation and record
            Assert.Contains(workQueueClient.SubmittedRequests, a => a.WorkType == SubmitRecordOperation.WORK_TYPE && JsonSerializer.Deserialize<Record>(a.Body).ExternalId == "Record_2");
            Assert.Contains(workQueueClient.SubmittedRequests, a => a.WorkType == SubmitAggregationOperation.WORK_TYPE && JsonSerializer.Deserialize<Aggregation>(a.Body).ExternalId == "Aggregation_2");

            if (submitRecordAndBinariesSynchronously)
            {
                //Binary Submission should not be enqueued when submitting records & binaries synchronously
                Assert.DoesNotContain(workQueueClient.SubmittedRequests, a => a.WorkType == SubmitBinaryOperation.WORK_TYPE);
            }
            else
            {
                Assert.Contains(workQueueClient.SubmittedRequests, a => a.WorkType == SubmitBinaryOperation.WORK_TYPE && JsonSerializer.Deserialize<BinaryMetaInfo>(a.Body).ExternalId == "Binary_2");
            }
        }
    }

    [Fact]
    public async Task OnSecondSyncRequest_ContinuesSyncWithCursorIsCalled()
    {
        var cancellationToken = CancellationToken.None;

        var testCursor1 = "TestCursor1";
        var testCursor2 = "TestCursor2";
        var scanner = new ContentRegistrationAction
        {
            ResultType = ContentResultType.Incomplete,
            Cursor = testCursor1
        };
        SUT.SelectContentRegistrationAction(scanner);

        await StartSutAsync();

        var connector = ContentManagerSutBase.CreateConnector1();
        var channel = ContentManagerSutBase.CreateChannel1();

        await SUT.GetConnectorManager().SetConnectorAsync(connector, cancellationToken);
        await SUT.GetChannelManager().UpsertChannelAsync(channel, cancellationToken);

        var workQueueClient = ContentManagerSutBase.GetWorkQueueClient(Services);

        using (var servicesScope = Services.CreateScope())
        {
            var workMessage = SUT.CreateContentRegistrationManagedWorkStatusModel(connector, channel);
            var operation = servicesScope.ServiceProvider.GetRequiredService<ContentRegistrationOperation>();
            await operation.RunWorkRequestAsync(SUT.CreateContentRegistrationRequest(workMessage), cancellationToken);

            //Operation result should be complete
            Assert.Equal(WorkResultType.Complete, operation.ResultType);
            //Operation State should contain the cursor from the action
            Assert.Equal(testCursor1, operation.State.Cursor);
            //Begin method should have been called once
            Assert.Equal(1, scanner.BeginCalledCount);
            //Continue method should not have been called
            Assert.Equal(0, scanner.ContinueCalledCount);
            //Content Registration work should be requeued
            Assert.Contains(workQueueClient.SubmittedRequests, a => a.WorkType == ContentRegistrationOperation.WORK_TYPE);
        }

        //Set the action return state
        scanner.Cursor = testCursor2;

        using (var servicesScope = Services.CreateScope())
        {
            var enqueuedWork = workQueueClient.SubmittedRequests.First(a => a.WorkType == ContentRegistrationOperation.WORK_TYPE);
            var operation = servicesScope.ServiceProvider.GetRequiredService<ContentRegistrationOperation>();
            await operation.RunWorkRequestAsync(enqueuedWork, cancellationToken);

            //Operation result should be complete
            Assert.Equal(WorkResultType.Complete, operation.ResultType);
            //Operation State should contain the cursor from the action
            Assert.Equal(testCursor2, operation.State.Cursor);
            //Begin method should have been called once
            Assert.Equal(1, scanner.ContinueCalledCount);
        }
    }

    [Fact]
    public async Task OnSecondSyncWithConnectorDisabled_ContinuesWorkWithoutScanning()
    {
        var cancellationToken = CancellationToken.None;

        var testCursor1 = "TestCursor1";
        var testCursor2 = "TestCursor2";
        var scanner = new ContentRegistrationAction
        {
            ResultType = ContentResultType.Incomplete,
            Cursor = testCursor1
        };
        SUT.SelectContentRegistrationAction(scanner);

        await StartSutAsync();

        var connector = ContentManagerSutBase.CreateConnector1();
        var channel = ContentManagerSutBase.CreateChannel1();

        await SUT.GetConnectorManager().SetConnectorAsync(connector, cancellationToken);
        await SUT.GetChannelManager().UpsertChannelAsync(channel, cancellationToken);

        var workQueueClient = ContentManagerSutBase.GetWorkQueueClient(Services);

        using (var servicesScope = Services.CreateScope())
        {
            var workMessage = SUT.CreateContentRegistrationManagedWorkStatusModel(connector, channel);
            var operation = servicesScope.ServiceProvider.GetRequiredService<ContentRegistrationOperation>();
            await operation.RunWorkRequestAsync(SUT.CreateContentRegistrationRequest(workMessage), cancellationToken);

            //Operation result should be complete
            Assert.Equal(WorkResultType.Complete, operation.ResultType);
            //Operation State should contain the cursor from the action
            Assert.Equal(testCursor1, operation.State.Cursor);
            //Begin method should have been called once
            Assert.Equal(1, scanner.BeginCalledCount);
            //Continue method should not have been called
            Assert.Equal(0, scanner.ContinueCalledCount);
            //Content Registration work should be requeued
            Assert.Contains(workQueueClient.SubmittedRequests, a => a.WorkType == ContentRegistrationOperation.WORK_TYPE);
        }

        scanner.Cursor = testCursor2;
        ContentManagerSutBase.DisableConnector(connector, DateTimeOffset.Now);
        await SUT.GetConnectorManager().SetConnectorAsync(connector, cancellationToken);

        using (var servicesScope = Services.CreateScope())
        {
            var enqueuedWork = workQueueClient.SubmittedRequests.First(a => a.WorkType == ContentRegistrationOperation.WORK_TYPE);
            var operation = servicesScope.ServiceProvider.GetRequiredService<ContentRegistrationOperation>();
            await operation.RunWorkRequestAsync(enqueuedWork, cancellationToken);

            //Operation result should be complete
            Assert.Equal(WorkResultType.Complete, operation.ResultType);
            //Operation result reason should be 'Connector disabled'
            Assert.Equal("Connector disabled", operation.ResultReason);
            //Continue method should not have been called
            Assert.Equal(0, scanner.ContinueCalledCount);
            //Content Registration work should be requeued
            Assert.Contains(workQueueClient.SubmittedRequests, a => a.WorkType == ContentRegistrationOperation.WORK_TYPE);
        }
    }

    [Fact]
    public async Task IfBeginScanException_WorkCompletedWithExistingCursor_WhenRetryIsEnabled()
    {
        var cancellationToken = CancellationToken.None;

        var testCursor1 = "TestCursor1";
        var scanner = new ContentRegistrationAction
        {
            Cursor = testCursor1,
            ThrowExecption = true
        };
        SUT.SelectContentRegistrationAction(scanner);

        await StartSutAsync();

        var connector = ContentManagerSutBase.CreateConnector1();
        var channel = ContentManagerSutBase.CreateChannel1();

        await SUT.GetConnectorManager().SetConnectorAsync(connector, cancellationToken);
        await SUT.GetChannelManager().UpsertChannelAsync(channel, cancellationToken);

        var workQueueClient = ContentManagerSutBase.GetWorkQueueClient(Services);

        using (var servicesScope = Services.CreateScope())
        {
            var workMessage = SUT.CreateContentRegistrationManagedWorkStatusModel(connector, channel);
            var operation = servicesScope.ServiceProvider.GetRequiredService<ContentRegistrationOperation>();
            await operation.RunWorkRequestAsync(SUT.CreateContentRegistrationRequest(workMessage), cancellationToken);

            //Operation result should be complete
            Assert.Equal(WorkResultType.Complete, operation.ResultType);
            //Begin method should have been called once
            Assert.Equal(1, scanner.BeginCalledCount);
            //Continue method should not have been called
            Assert.Equal(0, scanner.ContinueCalledCount);
            //Content Registration work should be requeued without a cursor (as the Begin method must be retried)
            Assert.Contains(workQueueClient.SubmittedRequests, a => a.WorkType == ContentRegistrationOperation.WORK_TYPE && a.FaultedCount == 1 && string.IsNullOrEmpty(JsonSerializer.Deserialize<ContentRegistrationState>(a.Body).Cursor));
        }
    }

    [Fact]
    public async Task IfBeginScanException_WorkFailedWithExistingCursor_WhenRetryIsDisabled()
    {
        var cancellationToken = CancellationToken.None;

        var testCursor1 = "TestCursor1";
        var scanner = new ContentRegistrationAction
        {
            Cursor = testCursor1,
            ThrowExecption = true
        };
        SUT.SelectContentRegistrationAction(scanner);

        var myConfig = new Dictionary<string, string>
        {
            { "Connector:RetryOnFailure", "False" },
        };

        var config = new ConfigurationBuilder()
            .AddInMemoryCollection(myConfig)
            .Build();

        await StartSutAsync(config);

        var connector = ContentManagerSutBase.CreateConnector1();
        var channel = ContentManagerSutBase.CreateChannel1();

        await SUT.GetConnectorManager().SetConnectorAsync(connector, cancellationToken);
        await SUT.GetChannelManager().UpsertChannelAsync(channel, cancellationToken);

        var workQueueClient = ContentManagerSutBase.GetWorkQueueClient(Services);

        using var servicesScope = Services.CreateScope();
        var workMessage = SUT.CreateContentRegistrationManagedWorkStatusModel(connector, channel);
        var operation = servicesScope.ServiceProvider.GetRequiredService<ContentRegistrationOperation>();
        await operation.RunWorkRequestAsync(SUT.CreateContentRegistrationRequest(workMessage), cancellationToken);

        //Operation result should be complete
        Assert.Equal(WorkResultType.Failed, operation.ResultType);
        //Begin method should have been called once
        Assert.Equal(1, scanner.BeginCalledCount);
        //Continue method should not have been called
        Assert.Equal(0, scanner.ContinueCalledCount);
        //Content Registration work should NOT be requeued
        Assert.DoesNotContain(workQueueClient.SubmittedRequests, a => a.WorkType == ContentRegistrationOperation.WORK_TYPE);

    }

    [Fact]
    public async Task IfContinueScanException_WorkCompletedWithExistingCursor_WhenRetryIsEnabled()
    {
        var cancellationToken = CancellationToken.None;

        var testCursor1 = "TestCursor1";
        var testCursor2 = "TestCursor2";
        var scanner = new ContentRegistrationAction
        {
            ResultType = ContentResultType.Incomplete,
            Cursor = testCursor1,
        };
        SUT.SelectContentRegistrationAction(scanner);

        await StartSutAsync();

        var connector = ContentManagerSutBase.CreateConnector1();
        var channel = ContentManagerSutBase.CreateChannel1();

        await SUT.GetConnectorManager().SetConnectorAsync(connector, cancellationToken);
        await SUT.GetChannelManager().UpsertChannelAsync(channel, cancellationToken);

        var workQueueClient = ContentManagerSutBase.GetWorkQueueClient(Services);

        using (var servicesScope = Services.CreateScope())
        {
            var workMessage = SUT.CreateContentRegistrationManagedWorkStatusModel(connector, channel);
            var operation = servicesScope.ServiceProvider.GetRequiredService<ContentRegistrationOperation>();
            await operation.RunWorkRequestAsync(SUT.CreateContentRegistrationRequest(workMessage), cancellationToken);

            //Operation result should be complete
            Assert.Equal(WorkResultType.Complete, operation.ResultType);
            //Operation State should contain the cursor from the action
            Assert.Equal(testCursor1, operation.State.Cursor);
            //Begin method should have been called once
            Assert.Equal(1, scanner.BeginCalledCount);
            //Continue method should not have been called
            Assert.Equal(0, scanner.ContinueCalledCount);
            //Content Registration work should be requeued
            Assert.Contains(workQueueClient.SubmittedRequests, a => a.WorkType == ContentRegistrationOperation.WORK_TYPE);
        }

        //Set the action return state
        scanner.Cursor = testCursor2;
        scanner.ThrowExecption = true;

        using (var servicesScope = Services.CreateScope())
        {
            var enqueuedWork = workQueueClient.SubmittedRequests.First(a => a.WorkType == ContentRegistrationOperation.WORK_TYPE);
            var operation = servicesScope.ServiceProvider.GetRequiredService<ContentRegistrationOperation>();
            await operation.RunWorkRequestAsync(enqueuedWork, cancellationToken);

            //Operation result should be Complete
            Assert.Equal(WorkResultType.Complete, operation.ResultType);
            //Operation State should contain the original cursor from the action
            Assert.Equal(testCursor1, operation.State.Cursor);
            //Begin method should have been called once
            Assert.Equal(1, scanner.ContinueCalledCount);
        }

        //The work should have been requeued for retry (workQueueClient includes the original queued work)
        Assert.Equal(2, workQueueClient.SubmittedRequests.Count);
        var retryWorkRequest = workQueueClient.SubmittedRequests.Last();
        //The queued retry faulted count should be 1
        Assert.Equal(1, retryWorkRequest.FaultedCount);
    }

    [Fact]
    public async Task IfContinueScanException_WorkFailedWithExistingCursor_WhenRetryIsDisabled()
    {
        var cancellationToken = CancellationToken.None;

        var testCursor1 = "TestCursor1";
        var testCursor2 = "TestCursor2";
        var scanner = new ContentRegistrationAction
        {
            ResultType = ContentResultType.Incomplete,
            Cursor = testCursor1
        };
        SUT.SelectContentRegistrationAction(scanner);

        var myConfig = new Dictionary<string, string>
        {
            { "Connector:RetryOnFailure", "False" },
        };

        var config = new ConfigurationBuilder()
            .AddInMemoryCollection(myConfig)
            .Build();

        await StartSutAsync(config);

        var connector = ContentManagerSutBase.CreateConnector1();
        var channel = ContentManagerSutBase.CreateChannel1();

        await SUT.GetConnectorManager().SetConnectorAsync(connector, cancellationToken);
        await SUT.GetChannelManager().UpsertChannelAsync(channel, cancellationToken);

        var workQueueClient = ContentManagerSutBase.GetWorkQueueClient(Services);

        using (var servicesScope = Services.CreateScope())
        {
            var workMessage = SUT.CreateContentRegistrationManagedWorkStatusModel(connector, channel);
            var operation = servicesScope.ServiceProvider.GetRequiredService<ContentRegistrationOperation>();
            await operation.RunWorkRequestAsync(SUT.CreateContentRegistrationRequest(workMessage), cancellationToken);

            //Operation result should be complete
            Assert.Equal(WorkResultType.Complete, operation.ResultType);
            //Operation State should contain the cursor from the action
            Assert.Equal(testCursor1, operation.State.Cursor);
            //Begin method should have been called once
            Assert.Equal(1, scanner.BeginCalledCount);
            //Continue method should not have been called
            Assert.Equal(0, scanner.ContinueCalledCount);
            //Content Registration work should be requeued
            Assert.Contains(workQueueClient.SubmittedRequests, a => a.WorkType == ContentRegistrationOperation.WORK_TYPE);
        }

        //Set the action return state
        scanner.Cursor = testCursor2;
        scanner.ThrowExecption = true;

        using (var servicesScope = Services.CreateScope())
        {
            var enqueuedWork = workQueueClient.SubmittedRequests.First(a => a.WorkType == ContentRegistrationOperation.WORK_TYPE);
            var operation = servicesScope.ServiceProvider.GetRequiredService<ContentRegistrationOperation>();
            await operation.RunWorkRequestAsync(enqueuedWork, cancellationToken);

            //Operation result should be Failed
            Assert.Equal(WorkResultType.Failed, operation.ResultType);
            //Operation State should contain the original cursor from the action
            Assert.Equal(testCursor1, operation.State.Cursor);
            //Begin method should have been called once
            Assert.Equal(1, scanner.ContinueCalledCount);
        }

        //The work should NOT be requeued for retry (workQueueClient includes the original queued work)
        Assert.Single(workQueueClient.SubmittedRequests);

    }

    [Fact]
    public async Task IfGlobalSemaphoreLockActive_IsDeferred()
    {
        var cancellationToken = CancellationToken.None;

        const int lockDuration = 300;

        await StartSutAsync();

        var connector = ContentManagerSutBase.CreateConnector1();
        var channel = ContentManagerSutBase.CreateChannel1();
        await SUT.GetConnectorManager().SetConnectorAsync(connector, cancellationToken);
        await SUT.GetChannelManager().UpsertChannelAsync(channel, cancellationToken);

        var semaphoreLockManager = Services.GetRequiredService<ISemaphoreLockManager>();
        semaphoreLockManager.ConnectorConfiguration = connector;
        await semaphoreLockManager.SetSemaphoreAsync(SemaphoreLockType.Global, ContentRegistrationOperation.WORK_TYPE, null, lockDuration, cancellationToken);

        var workMessage = SUT.CreateContentRegistrationManagedWorkStatusModel(connector, channel);

        using var servicesScope = Services.CreateScope();
        var operation = servicesScope.ServiceProvider.GetRequiredService<ContentRegistrationOperation>();
        await operation.RunWorkRequestAsync(SUT.CreateContentRegistrationRequest(workMessage), cancellationToken);

        var workQueueClient = ContentManagerSutBase.GetWorkQueueClient(servicesScope.ServiceProvider);

        //Work Result should be deferred
        Assert.Equal(WorkResultType.Deferred, operation.ResultType);
        Assert.Equal(SEMAPHORE_DEFERRAL_REASON, operation.ResultReason);
        Assert.Equal(DateTimeOffset.Now.AddSeconds(lockDuration), operation.WaitTill.Value, TimeSpan.FromSeconds(5));

        //Note: Work is requeued based on the wait time by the WorkQueue Provider (e.g. AzureServiceBusWorkServer, or RabbitMqWorkServer)
    }

    [Fact]
    public async Task IfScopedSemaphoreLockActive_IsDeferred()
    {
        var cancellationToken = CancellationToken.None;

        const int lockDuration = 300;

        await StartSutAsync();

        var connector = ContentManagerSutBase.CreateConnector1();
        var channel = ContentManagerSutBase.CreateChannel1();
        await SUT.GetConnectorManager().SetConnectorAsync(connector, cancellationToken);
        await SUT.GetChannelManager().UpsertChannelAsync(channel, cancellationToken);

        var semaphoreLockManager = Services.GetRequiredService<ISemaphoreLockManager>();
        semaphoreLockManager.ConnectorConfiguration = connector;
        await semaphoreLockManager.SetSemaphoreAsync(SemaphoreLockType.Scoped, ContentRegistrationOperation.WORK_TYPE, null, lockDuration, cancellationToken);

        var workMessage = SUT.CreateContentRegistrationManagedWorkStatusModel(connector, channel);

        using var servicesScope = Services.CreateScope();
        var operation = servicesScope.ServiceProvider.GetRequiredService<ContentRegistrationOperation>();
        await operation.RunWorkRequestAsync(SUT.CreateContentRegistrationRequest(workMessage), cancellationToken);

        var workQueueClient = ContentManagerSutBase.GetWorkQueueClient(servicesScope.ServiceProvider);

        //Work Result should be deferred
        Assert.Equal(WorkResultType.Deferred, operation.ResultType);
        Assert.Equal(SEMAPHORE_DEFERRAL_REASON, operation.ResultReason);
        Assert.Equal(DateTimeOffset.Now.AddSeconds(lockDuration), operation.WaitTill.Value, TimeSpan.FromSeconds(5));

        //Note: Work is requeued based on the wait time by the WorkQueue Provider (e.g. AzureServiceBusWorkServer, or RabbitMqWorkServer)
    }

    [Fact]
    public async Task IfDifferentScopedSemaphoreLockActive_IsNotDeferred()
    {
        var cancellationToken = CancellationToken.None;

        const int lockDuration = 300;

        await StartSutAsync();

        var connector = ContentManagerSutBase.CreateConnector1();
        var channel = ContentManagerSutBase.CreateChannel1();
        await SUT.GetConnectorManager().SetConnectorAsync(connector, cancellationToken);
        await SUT.GetChannelManager().UpsertChannelAsync(channel, cancellationToken);

        var semaphoreLockManager = Services.GetRequiredService<ISemaphoreLockManager>();
        semaphoreLockManager.ConnectorConfiguration = connector;
        await semaphoreLockManager.SetSemaphoreAsync(SemaphoreLockType.Scoped, ContentRegistrationOperation.WORK_TYPE, null, lockDuration, cancellationToken);

        //Set the Key to a different value so the lock is not detected
        SUT.SemaphoreLockScopedKeyAction.Key = "KEY_456";

        var workMessage = SUT.CreateContentRegistrationManagedWorkStatusModel(connector, channel);

        using var servicesScope = Services.CreateScope();
        var operation = servicesScope.ServiceProvider.GetRequiredService<ContentRegistrationOperation>();
        await operation.RunWorkRequestAsync(SUT.CreateContentRegistrationRequest(workMessage), cancellationToken);

        var workQueueClient = ContentManagerSutBase.GetWorkQueueClient(servicesScope.ServiceProvider);

        //Work Result should NOT be deferred
        Assert.Equal(WorkResultType.Complete, operation.ResultType);

    }

    [Theory]
    [InlineData(false, false)]
    [InlineData(true, false)]
    [InlineData(false, true)]
    public async Task IfIncomplete_ImmediateReExecution_BackOffIsZero(bool hasRecords, bool hasAggregations)
    {
        var cancellationToken = CancellationToken.None;

        var scanner = new ContentRegistrationAction
        {
            ResultType = ContentResultType.Incomplete,
            Cursor = "TestCursor1",
            Records = hasRecords ? [ContentManagerSutBase.CreateRecordContent(1)] : [],
            Aggregations = hasAggregations ? [ContentManagerSutBase.CreateAggregationContent(1)] : []
        };
        SUT.SelectContentRegistrationAction(scanner);

        var myConfig = new Dictionary<string, string>
        {
            { "ContentManager:ContentRegistration:ImmediateReExecution", "True" },
        };

        var config = new ConfigurationBuilder()
            .AddInMemoryCollection(myConfig)
            .Build();

        await StartSutAsync(config);

        var connector = ContentManagerSutBase.CreateConnector1();
        var channel = ContentManagerSutBase.CreateChannel1();

        await SUT.GetConnectorManager().SetConnectorAsync(connector, cancellationToken);
        await SUT.GetChannelManager().UpsertChannelAsync(channel, cancellationToken);

        var workQueueClient = ContentManagerSutBase.GetWorkQueueClient(Services);

        using var servicesScope = Services.CreateScope();
        var workMessage = SUT.CreateContentRegistrationManagedWorkStatusModel(connector, channel);
        var operation = servicesScope.ServiceProvider.GetRequiredService<ContentRegistrationOperation>();
        await operation.RunWorkRequestAsync(SUT.CreateContentRegistrationRequest(workMessage), cancellationToken);

        //Operation result should be complete
        Assert.Equal(WorkResultType.Complete, operation.ResultType);
        //Incomplete result should always trigger immediate re-execution regardless of content
        Assert.Equal(0, operation.State.LastBackOffDelaySeconds);
        //Content Registration work should be requeued
        Assert.Contains(workQueueClient.SubmittedRequests, a => a.WorkType == ContentRegistrationOperation.WORK_TYPE);
    }
}
