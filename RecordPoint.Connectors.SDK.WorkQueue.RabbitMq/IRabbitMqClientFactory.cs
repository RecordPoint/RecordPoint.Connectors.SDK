using RabbitMQ.Client;

namespace RecordPoint.Connectors.SDK.WorkQueue.RabbitMq
{
    /// <summary>
    /// 
    /// </summary>
    public interface IRabbitMqClientFactory
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IConnection CreateRabbitMqConnection();
    }
}