using RabbitMQ.Client;

namespace RecordPoint.Connectors.SDK.WorkQueue.RabbitMq
{
    public interface IRabbitMqClientFactory
    {
        IConnection CreateRabbitMqConnection();
    }
}