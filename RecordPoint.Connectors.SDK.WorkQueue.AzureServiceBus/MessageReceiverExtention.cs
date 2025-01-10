using Azure.Messaging.ServiceBus;
using RecordPoint.Connectors.SDK.Work.Models;

namespace RecordPoint.Connectors.SDK.WorkQueue.AzureServiceBus.Extensions
{
    /// <summary>
    /// ServiceBus Received Message Extentions
    /// </summary>
    public static class ServiceBusReceivedMessageExtentions
    {
        /// <summary>
        /// Deadletter Extentions Model 
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static DeadLetterModel ToDeadLetterModel(this ServiceBusReceivedMessage source) => new()
        {
            MessageId = source.MessageId,
            DeadLetterReason = source.DeadLetterReason,
            EnqueuedTime = source.EnqueuedTime,
            SequenceNumber = source.SequenceNumber.ToString(),
        };
    }
}
