#nullable enable
using System.Text;
using Moq;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RecordPoint.Connectors.SDK.Work;
using Xunit;

namespace RecordPoint.Connectors.SDK.WorkQueue.RabbitMq.Test
{
    public class RabbitMqReceivedMessageExtensionsTests
    {
        private static BasicGetResult CreateResult(WorkRequest workRequest, IDictionary<string, object?>? headers)
        {
            var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(workRequest));
            var props = new BasicProperties
            {
                Headers = headers
            };
            return new BasicGetResult(
                deliveryTag: 42,
                redelivered: false,
                exchange: "exchange",
                routingKey: "routingKey",
                messageCount: 1,
                basicProperties: props,
                body: body);
        }

        [Fact]
        public void ToDeadLetterModel_WithDeadLetterReasonHeader_PopulatesReason()
        {
            var submitted = DateTimeOffset.UtcNow;
            var workRequest = new WorkRequest { WorkType = "work", SubmitDateTime = submitted };
            var headers = new Dictionary<string, object?>
            {
                { RabbitMqReceivedMessageExtensions.DeadLetterReasonKey, Encoding.UTF8.GetBytes("rejected") }
            };

            var model = CreateResult(workRequest, headers).ToDeadLetterModel();

            Assert.Equal("42", model.MessageId);
            Assert.Equal("42", model.SequenceNumber);
            Assert.Equal("rejected", model.DeadLetterReason);
            Assert.Equal(submitted, model.EnqueuedTime);
        }

        [Fact]
        public void ToDeadLetterModel_WithNullHeaders_ReasonIsEmpty()
        {
            var workRequest = new WorkRequest { WorkType = "work" };

            var model = CreateResult(workRequest, null).ToDeadLetterModel();

            Assert.Equal(string.Empty, model.DeadLetterReason);
            Assert.Equal("42", model.MessageId);
        }

        [Fact]
        public void ToDeadLetterModel_WithHeadersButNoReasonKey_ReasonIsEmpty()
        {
            var workRequest = new WorkRequest { WorkType = "work" };
            var headers = new Dictionary<string, object?> { { "other-header", Encoding.UTF8.GetBytes("value") } };

            var model = CreateResult(workRequest, headers).ToDeadLetterModel();

            Assert.Equal(string.Empty, model.DeadLetterReason);
        }
    }
}
