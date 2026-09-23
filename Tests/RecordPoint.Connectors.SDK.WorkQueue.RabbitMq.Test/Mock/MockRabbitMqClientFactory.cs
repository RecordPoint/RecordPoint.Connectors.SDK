using Moq;
using RabbitMQ.Client;

namespace RecordPoint.Connectors.SDK.WorkQueue.RabbitMq.Test.Mock
{
    /// <summary>
    /// A factory that creates a RabbitMqClient
    /// </summary>
    public class MockRabbitMqClientFactory : IRabbitMqClientFactory
    {
        private readonly IConnection _rabbitMqConnection;
        private readonly Mock<IConnection> _rabbitMqConnectionMock;
        private readonly Mock<IChannel> _rabbitMqChannelMock;

        public Mock<IConnection> RabbitMqConnectionMock => _rabbitMqConnectionMock;
        public Mock<IChannel> RabbitMqChannelMock => _rabbitMqChannelMock;

        /// <summary>
        /// Constructor for the Factory
        /// </summary>
        public MockRabbitMqClientFactory()
        {
            _rabbitMqConnectionMock = new Mock<IConnection>();
            _rabbitMqChannelMock = new Mock<IChannel>();
            _rabbitMqConnectionMock
                .Setup(a => a.CreateChannelAsync(It.IsAny<CreateChannelOptions?>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(_rabbitMqChannelMock.Object);
            _rabbitMqConnection = _rabbitMqConnectionMock.Object;
        }

        /// <summary>
        /// Creates an instance of a RabbitMqClient
        /// </summary>
        /// <returns></returns>
        public IConnection CreateRabbitMqConnection()
        {
            return _rabbitMqConnection;
        }
    }
}
