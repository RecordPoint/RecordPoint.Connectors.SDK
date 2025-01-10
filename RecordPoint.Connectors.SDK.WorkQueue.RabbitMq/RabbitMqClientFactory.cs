using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;

namespace RecordPoint.Connectors.SDK.WorkQueue.RabbitMq
{
    /// <summary>
    /// A factory that creates a RabbitMqClient
    /// </summary>
    public class RabbitMqClientFactory : IRabbitMqClientFactory
    {
        private readonly IConfiguration _configuration;
        private readonly IOptions<RabbitMqOptions> _rabbitMqOptions;
        private IConnection? _rabbitMqConnection = null;

        /// <summary>
        /// Constructor for the Factory
        /// </summary>
        public RabbitMqClientFactory(
            IConfiguration configuration,
            IOptions<RabbitMqOptions> rabbitMqOptions)
        {
            _configuration = configuration;
            _rabbitMqOptions = rabbitMqOptions;
        }

        /// <summary>
        /// Creates an instance of a RabbitMqClient
        /// </summary>
        /// <returns></returns>
        public IConnection CreateRabbitMqConnection()
        {
            if (_rabbitMqConnection != null && _rabbitMqConnection.IsOpen)
            {
                return _rabbitMqConnection;
            }
            else
            {
                var connectionFactory = new ConnectionFactory
                {
                    HostName = _rabbitMqOptions.Value.HostName,
                    UserName = _rabbitMqOptions.Value.HostUserName,
                    Password = _rabbitMqOptions.Value.HostPassword
                };

                _rabbitMqConnection = connectionFactory.CreateConnection();
                return _rabbitMqConnection;
            }
        }
    }
}
