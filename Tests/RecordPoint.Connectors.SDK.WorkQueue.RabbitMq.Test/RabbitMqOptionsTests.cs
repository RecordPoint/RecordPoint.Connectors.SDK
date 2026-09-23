#nullable enable
using Xunit;

namespace RecordPoint.Connectors.SDK.WorkQueue.RabbitMq.Test
{
    public class RabbitMqOptionsTests
    {
        [Fact]
        public void Defaults_AreExpected()
        {
            var options = new RabbitMqOptions();

            Assert.Equal(string.Empty, options.QueuePrefix);
            Assert.Equal(25000, options.ServiceShutdownDelay);
            Assert.Equal(60000, options.KillswitchCheckInterval);
            Assert.Equal(string.Empty, options.HostName);
            Assert.Equal(string.Empty, options.HostUserName);
            Assert.Equal(string.Empty, options.HostPassword);
            Assert.Null(options.MaxDegreeOfParallelism);
            Assert.Equal("RabbitMqSettings", RabbitMqOptions.SECTION_NAME);
        }

        [Fact]
        public void Setters_RoundTripValues()
        {
            var options = new RabbitMqOptions
            {
                QueuePrefix = "prefix",
                ServiceShutdownDelay = 10,
                KillswitchCheckInterval = 20,
                HostName = "host",
                HostUserName = "user",
                HostPassword = "pass",
                MaxDegreeOfParallelism = 5
            };

            Assert.Equal("prefix", options.QueuePrefix);
            Assert.Equal(10, options.ServiceShutdownDelay);
            Assert.Equal(20, options.KillswitchCheckInterval);
            Assert.Equal("host", options.HostName);
            Assert.Equal("user", options.HostUserName);
            Assert.Equal("pass", options.HostPassword);
            Assert.Equal((ushort)5, options.MaxDegreeOfParallelism);
        }
    }
}
