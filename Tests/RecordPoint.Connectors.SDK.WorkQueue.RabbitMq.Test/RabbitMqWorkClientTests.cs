using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Moq;
using RabbitMQ.Client;
using RecordPoint.Connectors.SDK.Test;
using RecordPoint.Connectors.SDK.Work;
using RecordPoint.Connectors.SDK.WorkQueue.RabbitMq.Test.Mock;
using Xunit;

namespace RecordPoint.Connectors.SDK.WorkQueue.RabbitMq.Test
{
    public class RabbitMqWorkClientSut : CommonSutBase
    {
        public MockRabbitMqClientFactory MockRabbitMqClientFactory { get; set; } = new MockRabbitMqClientFactory();

        protected override IHostBuilder CreateSutBuilder()
        {
            var builder = base.CreateSutBuilder();

            builder.ConfigureServices((hostContext, services) =>
                services
                .Configure<RabbitMqOptions>(options => options.QueuePrefix = "test")
                .AddSingleton<IRabbitMqClientFactory>(provider => MockRabbitMqClientFactory)
                .AddSingleton<IWorkQueueClient, RabbitMqWorkClient>()
                );
            return builder;
        }
    }

    public class RabbitMqWorkClientTests : CommonTestBase<RabbitMqWorkClientSut>
    {
        private const string ExchangeDelayHeader = "x-delay";
        private const string QueueName = "content-manager";

        [Fact]
        public async Task SubmitWork_WithoutWaitTill()
        {
            await StartSutAsync();

            var basicPublishCalled = 0;

            var workRequest = new WorkRequest { SubmitDateTime = DateTime.UtcNow, WorkType = QueueName };

            SUT?.MockRabbitMqClientFactory.RabbitMqModelMock.Setup(a => a.BasicPublish(It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<bool>(),
                It.IsAny<IBasicProperties>(),
                It.IsAny<ReadOnlyMemory<byte>>()))
                  .Callback<string, string, bool, IBasicProperties, ReadOnlyMemory<byte>>((exchangeName, routingKey, mandatory, props, body) =>
                {
                    basicPublishCalled++;
                });

            var workQueueClient = Services?.GetRequiredService<IWorkQueueClient>();
            Assert.NotNull(workQueueClient);

            await workQueueClient.SubmitWorkAsync(workRequest, CancellationToken.None);
            Assert.Equal(1, basicPublishCalled);
        }

        [Fact]
        public async Task SubmitWork_WithWaitTill()
        {
            await StartSutAsync();

            var currentTime = DateTimeOffset.Now;
            var waitTill = currentTime.AddSeconds(60);
            var delayMilliSeconds = (waitTill - currentTime).TotalMilliseconds;

            var basicPublishCalled = 0;
            var actualMilliseconds = 0.0;

            var workRequest = new WorkRequest { WaitTill = waitTill, WorkType = QueueName };

            SUT?.MockRabbitMqClientFactory.RabbitMqBasicPropertiesMock.Setup(o => o.Headers)
                                                                     .Returns(new Dictionary<string, object> { });

            SUT?.MockRabbitMqClientFactory.RabbitMqModelMock.Setup(a => a.BasicPublish(It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<bool>(),
                It.IsAny<IBasicProperties>(),
                It.IsAny<ReadOnlyMemory<byte>>())).Callback<string, string, bool, IBasicProperties, ReadOnlyMemory<byte>>((exchangeName, routingKey, mandatory, props, body) =>
                {
                    basicPublishCalled++;
                    actualMilliseconds = Convert.ToDouble(props.Headers[ExchangeDelayHeader]);
                });

            var workQueueClient = Services?.GetRequiredService<IWorkQueueClient>();
            Assert.NotNull(workQueueClient);
            await workQueueClient.SubmitWorkAsync(workRequest, CancellationToken.None);

            Assert.Equal(1, basicPublishCalled);
            Assert.True(actualMilliseconds > 0);
            Assert.True(delayMilliSeconds > actualMilliseconds);
        }
    }
}
