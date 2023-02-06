using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace RecordPoint.Connectors.SDK.WorkQueue.RabbitMq
{
    public class RabbitMqProcessModel
    {
        public EventingBasicConsumer RabbitMqEventingBasicConsumer { get; set; }
        public IModel RabbitMqModel { get; set; }
    }
}
