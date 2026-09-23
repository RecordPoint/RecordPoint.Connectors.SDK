#nullable enable
using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Options;
using Moq;
using Newtonsoft.Json;
using RecordPoint.Connectors.SDK.Providers;
using RecordPoint.Connectors.SDK.Work;
using RecordPoint.Connectors.SDK.WorkQueue.AzureServiceBus.Test.Mock;
using System.Text;
using Xunit;

namespace RecordPoint.Connectors.SDK.WorkQueue.AzureServiceBus.Test
{
    public class AzureServiceBusWorkClientTests
    {
        private static AzureServiceBusWorkClient CreateClient(
            MockServiceBusClientFactory factory,
            AzureServiceBusOptions options,
            DateTime? utcNow = null)
        {
            var dateTimeProvider = new Mock<IDateTimeProvider>();
            dateTimeProvider.Setup(p => p.UtcNow).Returns(utcNow ?? DateTime.UtcNow);
            return new AzureServiceBusWorkClient(factory, Options.Create(options), dateTimeProvider.Object);
        }

        [Fact]
        public async Task SubmitWorkAsync_SendsMessage_WithoutWaitTill()
        {
            var factory = new MockServiceBusClientFactory();
            ServiceBusMessage? captured = null;
            factory.SenderMock
                .Setup(s => s.SendMessageAsync(It.IsAny<ServiceBusMessage>(), It.IsAny<CancellationToken>()))
                .Callback<ServiceBusMessage, CancellationToken>((m, _) => captured = m)
                .Returns(Task.CompletedTask);

            var now = new DateTime(2026, 1, 2, 3, 4, 5, DateTimeKind.Utc);
            var client = CreateClient(factory, new AzureServiceBusOptions(), now);

            var workRequest = new WorkRequest { WorkType = "Content Registration" };
            await client.SubmitWorkAsync(workRequest, CancellationToken.None);

            Assert.NotNull(captured);
            var body = Encoding.UTF8.GetString(captured!.Body.ToArray());
            var deserialised = JsonConvert.DeserializeObject<WorkRequest>(body);
            Assert.Equal("Content Registration", deserialised!.WorkType);
            Assert.Equal(now, deserialised.SubmitDateTime);
            // No prefix => queue name lowercased with dashes
            factory.ClientMock.Verify(c => c.CreateSender("content-registration"), Times.Once);
            factory.SenderMock.Verify(s => s.SendMessageAsync(It.IsAny<ServiceBusMessage>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task SubmitWorkAsync_SetsScheduledEnqueueTime_WhenWaitTillProvided()
        {
            var factory = new MockServiceBusClientFactory();
            ServiceBusMessage? captured = null;
            factory.SenderMock
                .Setup(s => s.SendMessageAsync(It.IsAny<ServiceBusMessage>(), It.IsAny<CancellationToken>()))
                .Callback<ServiceBusMessage, CancellationToken>((m, _) => captured = m)
                .Returns(Task.CompletedTask);

            var client = CreateClient(factory, new AzureServiceBusOptions());

            var waitTill = DateTimeOffset.UtcNow.AddMinutes(5);
            var workRequest = new WorkRequest { WorkType = "content sync", WaitTill = waitTill };
            await client.SubmitWorkAsync(workRequest, CancellationToken.None);

            Assert.NotNull(captured);
            Assert.Equal(waitTill, captured!.ScheduledEnqueueTime);
        }

        [Fact]
        public async Task SubmitWorkAsync_AppliesQueuePrefix()
        {
            var factory = new MockServiceBusClientFactory();
            factory.SenderMock
                .Setup(s => s.SendMessageAsync(It.IsAny<ServiceBusMessage>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            var client = CreateClient(factory, new AzureServiceBusOptions { QueuePrefix = "test" });

            await client.SubmitWorkAsync(new WorkRequest { WorkType = "Content Registration" }, CancellationToken.None);

            factory.ClientMock.Verify(c => c.CreateSender("test-content-registration"), Times.Once);
        }

        [Fact]
        public async Task SubmitWorkAsync_ReusesSenderForSameQueue()
        {
            var factory = new MockServiceBusClientFactory();
            factory.SenderMock
                .Setup(s => s.SendMessageAsync(It.IsAny<ServiceBusMessage>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            var client = CreateClient(factory, new AzureServiceBusOptions());

            await client.SubmitWorkAsync(new WorkRequest { WorkType = "content registration" }, CancellationToken.None);
            await client.SubmitWorkAsync(new WorkRequest { WorkType = "content registration" }, CancellationToken.None);

            factory.ClientMock.Verify(c => c.CreateSender("content-registration"), Times.Once);
            factory.SenderMock.Verify(s => s.SendMessageAsync(It.IsAny<ServiceBusMessage>(), It.IsAny<CancellationToken>()), Times.Exactly(2));
        }

        [Fact]
        public async Task SubmitWorkAsync_Throws_WhenCancelled()
        {
            var factory = new MockServiceBusClientFactory();
            var client = CreateClient(factory, new AzureServiceBusOptions());

            using var cts = new CancellationTokenSource();
            await cts.CancelAsync();

            await Assert.ThrowsAsync<OperationCanceledException>(
                () => client.SubmitWorkAsync(new WorkRequest { WorkType = "content registration" }, cts.Token));

            factory.SenderMock.Verify(s => s.SendMessageAsync(It.IsAny<ServiceBusMessage>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task DisposeAsync_DisposesSendersAndClient()
        {
            var factory = new MockServiceBusClientFactory();
            factory.SenderMock
                .Setup(s => s.SendMessageAsync(It.IsAny<ServiceBusMessage>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);

            var client = CreateClient(factory, new AzureServiceBusOptions());
            await client.SubmitWorkAsync(new WorkRequest { WorkType = "content registration" }, CancellationToken.None);

            await client.DisposeAsync();

            factory.SenderMock.Verify(s => s.DisposeAsync(), Times.Once);
            factory.ClientMock.Verify(c => c.DisposeAsync(), Times.Once);
        }
    }
}
