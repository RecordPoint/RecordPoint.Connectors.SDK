using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RecordPoint.Connectors.SDK.WorkQueue.RabbitMq
{
    /// <summary>
    /// The rabbit mq process model.
    /// </summary>
    public class RabbitMqProcessModel
    {
        /// <summary>
        /// Gets or sets the rabbit mq eventing basic consumer.
        /// </summary>
        public required AsyncEventingBasicConsumer RabbitMqEventingBasicConsumer { get; set; }
        /// <summary>
        /// Gets or sets the rabbit mq model.
        /// </summary>
        public required IChannel RabbitMqModel { get; set; }
        /// <summary>
        /// Gets or sets the consumer tag.
        /// </summary>
        public string? ConsumerTag { get; set; }
    }
}
