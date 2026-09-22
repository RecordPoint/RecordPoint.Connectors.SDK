#nullable enable
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Moq;
using Xunit;

namespace RecordPoint.Connectors.SDK.WorkQueue.RabbitMq.Test
{
    public class RabbitMqClientFactoryTests
    {
        [Fact]
        public void Constructor_WithDependencies_CreatesInstance()
        {
            var configuration = new Mock<IConfiguration>().Object;
            var options = Options.Create(new RabbitMqOptions
            {
                HostName = "host",
                HostUserName = "user",
                HostPassword = "pass"
            });

            var factory = new RabbitMqClientFactory(configuration, options);

            Assert.NotNull(factory);
            Assert.IsAssignableFrom<IRabbitMqClientFactory>(factory);
        }
    }
}
