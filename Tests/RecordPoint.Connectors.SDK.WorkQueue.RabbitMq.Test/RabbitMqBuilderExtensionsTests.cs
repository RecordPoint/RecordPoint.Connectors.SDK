#nullable enable
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using RecordPoint.Connectors.SDK.Work;
using Xunit;

namespace RecordPoint.Connectors.SDK.WorkQueue.RabbitMq.Test
{
    public class RabbitMqBuilderExtensionsTests
    {
        private static IServiceCollection CaptureServices(Func<IHostBuilder, IHostBuilder> configure)
        {
            IServiceCollection? captured = null;
            var builder = Host.CreateDefaultBuilder();
            configure(builder);
            builder.ConfigureServices((_, services) => captured = services);
            builder.Build();
            Assert.NotNull(captured);
            return captured!;
        }

        [Fact]
        public void UseRabbitMqWorkQueue_RegistersExpectedServices()
        {
            var services = CaptureServices(b => b.UseRabbitMqWorkQueue());

            Assert.Contains(services, d => d.ServiceType == typeof(IRabbitMqClientFactory)
                && d.ImplementationType == typeof(RabbitMqClientFactory)
                && d.Lifetime == ServiceLifetime.Singleton);
            Assert.Contains(services, d => d.ServiceType == typeof(IWorkQueueClient)
                && d.ImplementationType == typeof(RabbitMqWorkClient)
                && d.Lifetime == ServiceLifetime.Singleton);
            Assert.Contains(services, d => d.ServiceType == typeof(IHostedService)
                && d.ImplementationType == typeof(RabbitMqWorkServer));
            Assert.Contains(services, d => d.ServiceType == typeof(IConfigureOptions<RabbitMqOptions>));
        }

        [Fact]
        public void UseRabbitMqWorkClient_RegistersClientButNotHostedService()
        {
            var services = CaptureServices(b => b.UseRabbitMqWorkClient());

            Assert.Contains(services, d => d.ServiceType == typeof(IRabbitMqClientFactory)
                && d.ImplementationType == typeof(RabbitMqClientFactory));
            Assert.Contains(services, d => d.ServiceType == typeof(IWorkQueueClient)
                && d.ImplementationType == typeof(RabbitMqWorkClient));
            Assert.DoesNotContain(services, d => d.ServiceType == typeof(IHostedService)
                && d.ImplementationType == typeof(RabbitMqWorkServer));
        }

        [Fact]
        public void UseRabbitMqDeadLetterQueueService_RegistersDlqService()
        {
            var services = CaptureServices(b => b.UseRabbitMqDeadLetterQueueService());

            Assert.Contains(services, d => d.ServiceType == typeof(IDeadLetterQueueService)
                && d.ImplementationType == typeof(RabbitMqDeadLetterQueueService)
                && d.Lifetime == ServiceLifetime.Transient);
        }

        [Fact]
        public void UseRabbitMqWorkQueue_ReturnsSameBuilderInstance()
        {
            var builder = Host.CreateDefaultBuilder();
            var result = builder.UseRabbitMqWorkQueue();
            Assert.Same(builder, result);
        }
    }
}
