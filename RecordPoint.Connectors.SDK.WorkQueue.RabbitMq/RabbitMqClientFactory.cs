using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using System.Diagnostics.CodeAnalysis;

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
        /// Creates or reuses an open RabbitMQ connection.
        /// </summary>
        /// <remarks>
        /// Excluded from code coverage: this is a thin wrapper over
        /// ConnectionFactory.CreateConnection() which requires a live RabbitMQ broker and
        /// cannot be exercised without an integration environment.
        /// </remarks>
        /// <returns>An open RabbitMQ connection.</returns>
        [ExcludeFromCodeCoverage]
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

                _rabbitMqConnection = connectionFactory.CreateConnectionAsync().GetAwaiter().GetResult();
                return _rabbitMqConnection;
            }
        }
    }
}
