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
        private readonly Mock<IConnectionFactory> _connectionFactoryMock;
        private readonly Mock<IConnection> _rabbitMqConnectionMock;
        private readonly Mock<IModel> _rabbitMqModelMock;
        private readonly Mock<IBasicProperties> _rabbitMqBasicPropertiesMock;

        public Mock<IConnectionFactory> ConnectionFactoryMock => _connectionFactoryMock;
        public Mock<IConnection> RabbitMqConnectionMock => _rabbitMqConnectionMock;
        public Mock<IModel> RabbitMqModelMock => _rabbitMqModelMock;
        public Mock<IBasicProperties> RabbitMqBasicPropertiesMock => _rabbitMqBasicPropertiesMock;

        /// <summary>
        /// Constructor for the Factory
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="rabbitMqOptions"></param>
        public MockRabbitMqClientFactory()
        {
            _connectionFactoryMock = new Mock<IConnectionFactory>();
            _rabbitMqConnectionMock = new Mock<IConnection>();
            _rabbitMqModelMock = new Mock<IModel>();
            _rabbitMqBasicPropertiesMock = new Mock<IBasicProperties>();
            _connectionFactoryMock.Setup(m => m.CreateConnection()).Returns(_rabbitMqConnectionMock.Object);
            _rabbitMqConnectionMock.Setup(a => a.CreateModel()).Returns(_rabbitMqModelMock.Object);
            _rabbitMqModelMock.Setup(a => a.CreateBasicProperties()).Returns(_rabbitMqBasicPropertiesMock.Object);
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
