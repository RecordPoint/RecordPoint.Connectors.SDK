#nullable enable
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using RecordPoint.Connectors.SDK.Observability;
using RecordPoint.Connectors.SDK.Work;
using Xunit;

namespace RecordPoint.Connectors.SDK.Test.Work
{
    public class QueueableWorkManagerTests
    {
        private const string WORK_TYPE = "TestWork";

        private static WorkRequest Request() => new()
        {
            WorkId = "work-1",
            WorkType = WORK_TYPE,
            ConnectorConfigId = "connector-1"
        };

        [Fact]
        public async Task HandleWorkRequestAsync_NoMatchingOperation_ReturnsFailed()
        {
            var telemetry = new Mock<ITelemetryTracker>();
            var services = new ServiceCollection().BuildServiceProvider();

            var manager = new QueueableWorkManager(telemetry.Object, services);

            var result = await manager.HandleWorkRequestAsync(Request(), CancellationToken.None);

            Assert.Equal(WorkResultType.Failed, result.ResultType);
            Assert.Contains(WORK_TYPE, result.Reason);
        }

        [Fact]
        public async Task HandleWorkRequestAsync_MatchingOperation_ReturnsOperationResult()
        {
            var telemetry = new Mock<ITelemetryTracker>();

            var work = new Mock<IQueueableWork>();
            work.SetupGet(x => x.WorkType).Returns(WORK_TYPE);
            work.Setup(x => x.RunWorkRequestAsync(It.IsAny<WorkRequest>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);
            work.Setup(x => x.GetWorkResult()).Returns(WorkResult.Complete("done"));

            var services = new ServiceCollection()
                .AddScoped<IQueueableWork>(_ => work.Object)
                .BuildServiceProvider();

            var manager = new QueueableWorkManager(telemetry.Object, services);

            var result = await manager.HandleWorkRequestAsync(Request(), CancellationToken.None);

            Assert.Equal(WorkResultType.Complete, result.ResultType);
            Assert.Equal("done", result.Reason);
        }

        [Fact]
        public async Task HandleWorkRequestAsync_OperationThrows_TracksExceptionAndReturnsFailed()
        {
            var telemetry = new Mock<ITelemetryTracker>();

            var work = new Mock<IQueueableWork>();
            work.SetupGet(x => x.WorkType).Returns(WORK_TYPE);
            work.Setup(x => x.RunWorkRequestAsync(It.IsAny<WorkRequest>(), It.IsAny<CancellationToken>()))
                .ThrowsAsync(new InvalidOperationException("boom"));

            var services = new ServiceCollection()
                .AddScoped<IQueueableWork>(_ => work.Object)
                .BuildServiceProvider();

            var manager = new QueueableWorkManager(telemetry.Object, services);

            var result = await manager.HandleWorkRequestAsync(Request(), CancellationToken.None);

            Assert.Equal(WorkResultType.Failed, result.ResultType);
            Assert.Equal("Unknown exception thrown attempting to handle a Work Request", result.Reason);
            telemetry.Verify(x => x.TrackException(It.IsAny<Exception>(), It.IsAny<Dimensions>(), It.IsAny<Measures>()), Times.Once);
        }
    }
}
