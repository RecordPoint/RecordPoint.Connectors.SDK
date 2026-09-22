#nullable enable
using Microsoft.Extensions.DependencyInjection;
using Moq;
using RabbitMQ.Client;
using RecordPoint.Connectors.SDK.Test;
using RecordPoint.Connectors.SDK.Work;
using Xunit;

namespace RecordPoint.Connectors.SDK.WorkQueue.RabbitMq.Test
{
    public class RabbitMqWorkClientExtraTests : CommonTestBase<RabbitMqWorkClientSut>
    {
        private const string ExchangeDelayHeader = "x-delay";
        private const string QueueName = "content-manager";

        [Fact]
        public async Task SubmitWork_WithCancelledToken_Throws()
        {
            await StartSutAsync();
            var client = Services?.GetRequiredService<IWorkQueueClient>();
            Assert.NotNull(client);

            using var cts = new CancellationTokenSource();
            cts.Cancel();

            await Assert.ThrowsAnyAsync<OperationCanceledException>(
                () => client!.SubmitWorkAsync(new WorkRequest { WorkType = QueueName }, cts.Token));
        }

        [Fact]
        public async Task SubmitWork_WithExistingHeaders_AddsDelayHeaderWithoutReplacing()
        {
            await StartSutAsync();

            IDictionary<string, object?>? publishedHeaders = null;
            SUT!.MockRabbitMqClientFactory.RabbitMqChannelMock.Setup(a => a.BasicPublishAsync(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<bool>(),
                It.IsAny<BasicProperties>(),
                It.IsAny<ReadOnlyMemory<byte>>(),
                It.IsAny<CancellationToken>())).Callback<string, string, bool, BasicProperties, ReadOnlyMemory<byte>, CancellationToken>((_, _, _, props, _, _) =>
                {
                    publishedHeaders = props.Headers;
                });

            var client = Services?.GetRequiredService<IWorkQueueClient>();
            Assert.NotNull(client);

            await client!.SubmitWorkAsync(
                new WorkRequest { WorkType = QueueName, WaitTill = DateTimeOffset.UtcNow.AddSeconds(30) },
                CancellationToken.None);

            Assert.NotNull(publishedHeaders);
            Assert.True(publishedHeaders!.ContainsKey(ExchangeDelayHeader));
        }

        [Fact]
        public async Task SubmitWork_SameWorkType_ReusesSingleModel()
        {
            await StartSutAsync();
            var client = Services?.GetRequiredService<IWorkQueueClient>();
            Assert.NotNull(client);

            await client!.SubmitWorkAsync(new WorkRequest { WorkType = QueueName }, CancellationToken.None);
            await client.SubmitWorkAsync(new WorkRequest { WorkType = QueueName }, CancellationToken.None);

            SUT!.MockRabbitMqClientFactory.RabbitMqConnectionMock.Verify(c => c.CreateChannelAsync(It.IsAny<CreateChannelOptions?>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task SubmitWork_DifferentWorkTypes_CreatesModelPerType()
        {
            await StartSutAsync();
            var client = Services?.GetRequiredService<IWorkQueueClient>();
            Assert.NotNull(client);

            await client!.SubmitWorkAsync(new WorkRequest { WorkType = "type-a" }, CancellationToken.None);
            await client.SubmitWorkAsync(new WorkRequest { WorkType = "type-b" }, CancellationToken.None);

            SUT!.MockRabbitMqClientFactory.RabbitMqConnectionMock.Verify(c => c.CreateChannelAsync(It.IsAny<CreateChannelOptions?>(), It.IsAny<CancellationToken>()), Times.Exactly(2));
        }

        [Fact]
        public async Task DisposeAsync_DisposesSendersAndConnection()
        {
            await StartSutAsync();
            var client = Services?.GetRequiredService<IWorkQueueClient>() as IAsyncDisposable;
            Assert.NotNull(client);

            // create a sender so there is something to dispose
            await ((IWorkQueueClient)client!).SubmitWorkAsync(new WorkRequest { WorkType = QueueName }, CancellationToken.None);

            await client.DisposeAsync();

            SUT!.MockRabbitMqClientFactory.RabbitMqChannelMock.Verify(m => m.Dispose(), Times.AtLeastOnce);
            SUT.MockRabbitMqClientFactory.RabbitMqConnectionMock.Verify(c => c.Dispose(), Times.Once);
        }
    }
}
