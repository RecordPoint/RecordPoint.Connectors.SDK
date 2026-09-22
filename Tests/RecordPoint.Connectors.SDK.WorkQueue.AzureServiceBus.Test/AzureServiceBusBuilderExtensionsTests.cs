#nullable enable
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using RecordPoint.Connectors.SDK.WebHost.Services;
using RecordPoint.Connectors.SDK.Work;
using Xunit;

namespace RecordPoint.Connectors.SDK.WorkQueue.AzureServiceBus.Test
{
    public class AzureServiceBusBuilderExtensionsTests
    {
        /// <summary>
        /// Minimal <see cref="IHostBuilder"/> that runs the <c>ConfigureServices</c>
        /// callbacks immediately against a shared <see cref="IServiceCollection"/> so
        /// the registrations performed by the extensions can be inspected. Avoids a
        /// dependency on the full Microsoft.Extensions.Hosting package.
        /// </summary>
        private sealed class FakeHostBuilder : IHostBuilder
        {
            public IServiceCollection Services { get; } = new ServiceCollection();
            private readonly HostBuilderContext _context;

            public FakeHostBuilder()
            {
                var configuration = new ConfigurationBuilder().AddInMemoryCollection().Build();
                _context = new HostBuilderContext(new Dictionary<object, object>())
                {
                    Configuration = configuration
                };
            }

            public IDictionary<object, object> Properties => _context.Properties;

            public IHostBuilder ConfigureServices(Action<HostBuilderContext, IServiceCollection> configureDelegate)
            {
                configureDelegate(_context, Services);
                return this;
            }

            public IHostBuilder ConfigureAppConfiguration(Action<HostBuilderContext, IConfigurationBuilder> configureDelegate) => this;
            public IHostBuilder ConfigureContainer<TContainerBuilder>(Action<HostBuilderContext, TContainerBuilder> configureDelegate) => this;
            public IHostBuilder ConfigureHostConfiguration(Action<IConfigurationBuilder> configureDelegate) => this;
            public IHostBuilder UseServiceProviderFactory<TContainerBuilder>(IServiceProviderFactory<TContainerBuilder> factory) where TContainerBuilder : notnull => this;
            public IHostBuilder UseServiceProviderFactory<TContainerBuilder>(Func<HostBuilderContext, IServiceProviderFactory<TContainerBuilder>> factory) where TContainerBuilder : notnull => this;
            public IHost Build() => throw new NotSupportedException();
        }

        private static bool HasService(IServiceCollection services, Type serviceType, Type implementationType)
            => services.Any(d => d.ServiceType == serviceType && d.ImplementationType == implementationType);

        [Fact]
        public void UseASBWorkQueue_RegistersExpectedServices()
        {
            var builder = new FakeHostBuilder();
            builder.UseASBWorkQueue();

            Assert.True(HasService(builder.Services, typeof(IServiceBusClientFactory), typeof(ServiceBusClientFactory)));
            Assert.True(HasService(builder.Services, typeof(IWorkQueueClient), typeof(AzureServiceBusWorkClient)));
            Assert.True(HasService(builder.Services, typeof(IHostedService), typeof(AzureServiceBusWorkServer)));
            Assert.Contains(builder.Services, d => d.ServiceType == typeof(IList<Type>));
        }

        [Fact]
        public void UseASBWorkQueue_RegistersProvidedOperationTypes()
        {
            var operationTypes = new List<Type> { typeof(string) };
            var builder = new FakeHostBuilder();
            builder.UseASBWorkQueue(operationTypes);

            var descriptor = builder.Services.First(d => d.ServiceType == typeof(IList<Type>));
            Assert.Same(operationTypes, descriptor.ImplementationInstance);
        }

        [Fact]
        public void UseASBWorkClient_RegistersClientButNotHostedService()
        {
            var builder = new FakeHostBuilder();
            builder.UseASBWorkClient();

            Assert.True(HasService(builder.Services, typeof(IServiceBusClientFactory), typeof(ServiceBusClientFactory)));
            Assert.True(HasService(builder.Services, typeof(IWorkQueueClient), typeof(AzureServiceBusWorkClient)));
            Assert.DoesNotContain(builder.Services, d => d.ServiceType == typeof(IHostedService) && d.ImplementationType == typeof(AzureServiceBusWorkServer));
        }

        [Fact]
        public void UseASBDeadLetterQueueService_Default_RegistersServices()
        {
            var builder = new FakeHostBuilder();
            builder.UseASBDeadLetterQueueService();

            Assert.True(HasService(builder.Services, typeof(IDeadLetterQueueService), typeof(AzureServiceBusDeadLetterQueueService)));
            Assert.True(HasService(builder.Services, typeof(IServiceBusClientFactory), typeof(ServiceBusClientFactory)));
            Assert.Contains(builder.Services, d => d.ServiceType == typeof(IOptions<DeadLetterControllerOptions>));
        }

        [Fact]
        public void UseASBDeadLetterQueueService_WithConfigureOptions_AppliesOptions()
        {
            var builder = new FakeHostBuilder();
            builder.UseASBDeadLetterQueueService(options =>
            {
                options.MaxReplayBatchSize = 42;
                options.EnableDeleteOperations = false;
                options.AllowedQueueNames.Add("queue-a");
            });

            var provider = builder.Services.BuildServiceProvider();
            var resolved = provider.GetRequiredService<IOptions<DeadLetterControllerOptions>>().Value;

            Assert.Equal(42, resolved.MaxReplayBatchSize);
            Assert.False(resolved.EnableDeleteOperations);
            Assert.Contains("queue-a", resolved.AllowedQueueNames);
        }
    }
}
