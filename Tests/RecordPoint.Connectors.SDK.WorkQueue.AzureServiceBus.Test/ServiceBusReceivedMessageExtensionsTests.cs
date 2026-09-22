#nullable enable
using System.Collections.Generic;
using Azure.Messaging.ServiceBus;
using RecordPoint.Connectors.SDK.WorkQueue.AzureServiceBus.Extensions;
using Xunit;

namespace RecordPoint.Connectors.SDK.WorkQueue.AzureServiceBus.Test
{
    public class ServiceBusReceivedMessageExtensionsTests
    {
        [Fact]
        public void ToDeadLetterModel_MapsAllFields()
        {
            var enqueued = new DateTimeOffset(2026, 3, 4, 5, 6, 7, TimeSpan.Zero);
            var message = ServiceBusModelFactory.ServiceBusReceivedMessage(
                messageId: "message-1",
                sequenceNumber: 42L,
                enqueuedTime: enqueued,
                properties: new Dictionary<string, object> { { "DeadLetterReason", "boom" } });

            var model = message.ToDeadLetterModel();

            Assert.Equal("message-1", model.MessageId);
            Assert.Equal("boom", model.DeadLetterReason);
            Assert.Equal(enqueued, model.EnqueuedTime);
            Assert.Equal("42", model.SequenceNumber);
        }

        [Fact]
        public void ToDeadLetterModel_HandlesMissingDeadLetterReason()
        {
            var message = ServiceBusModelFactory.ServiceBusReceivedMessage(
                messageId: "message-2",
                sequenceNumber: 7L);

            var model = message.ToDeadLetterModel();

            Assert.Equal("message-2", model.MessageId);
            Assert.Null(model.DeadLetterReason);
            Assert.Equal("7", model.SequenceNumber);
        }
    }
}
