using Newtonsoft.Json;
using RabbitMQ.Client;
using RecordPoint.Connectors.SDK.Work;
using RecordPoint.Connectors.SDK.Work.Models;
using System.Text;

namespace RecordPoint.Connectors.SDK.WorkQueue.RabbitMq.Extensions
{
    /// <summary>
    /// ServiceBus Received Message Extentions
    /// </summary>
    public static class RabbitMqReceivedMessageExtentions
    {
        /// <summary>
        /// DeadLetterReasonKey key used for deadletter reason
        /// </summary>
        public const string DeadLetterReasonKey = "x-first-death-reason";

        /// <summary>
        /// Deadletter Extentions Model 
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static DeadLetterModel ToDeadLetterModel(this BasicGetResult source)
        {
            var messageBody = Encoding.UTF8.GetString(source.Body.ToArray());
            var workRequest = JsonConvert.DeserializeObject<WorkRequest>(messageBody);

            string deadLetterReason = string.Empty;
            if (source.BasicProperties.Headers.ContainsKey(DeadLetterReasonKey))
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
