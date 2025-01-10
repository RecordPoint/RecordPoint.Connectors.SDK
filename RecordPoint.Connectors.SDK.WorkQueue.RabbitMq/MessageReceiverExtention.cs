using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RecordPoint.Connectors.SDK.Work;
using RecordPoint.Connectors.SDK.Work.Models;

namespace RecordPoint.Connectors.SDK.WorkQueue.RabbitMq
{
    /// <summary>
    /// ServiceBus Received Message Extensions
    /// </summary>
    public static class RabbitMqReceivedMessageExtensions
    {
        /// <summary>
        /// DeadLetterReasonKey key used for deadletter reason
        /// </summary>
        public const string DeadLetterReasonKey = "x-first-death-reason";

        /// <summary>
        /// Deadletter extensions model
        /// </summary>
        public static DeadLetterModel ToDeadLetterModel(this BasicGetResult source)
        {
            var messageBody = Encoding.UTF8.GetString(source.Body.ToArray());
            var workRequest = JsonConvert.DeserializeObject<WorkRequest>(messageBody);

            string deadLetterReason = string.Empty;
            if (source.BasicProperties.Headers != null &&
                source.BasicProperties.Headers.ContainsKey(DeadLetterReasonKey))
            {
                deadLetterReason = Encoding.UTF8.GetString((byte[])source.BasicProperties.Headers[DeadLetterReasonKey]);
            }

            return new()
            {
                MessageId = source.DeliveryTag.ToString(),
                DeadLetterReason = deadLetterReason,
                EnqueuedTime = workRequest!.SubmitDateTime,
                SequenceNumber = source.DeliveryTag.ToString(),
            };
        }
    }
}
