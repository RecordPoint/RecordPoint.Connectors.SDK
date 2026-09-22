#nullable enable
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using RecordPoint.Connectors.SDK.Configuration;
using Xunit;

namespace RecordPoint.Connectors.SDK.WorkQueue.AzureServiceBus.Test
{
    public class ServiceBusClientFactoryTests
    {
        private const string FakeConnectionString =
            "Endpoint=sb://connstr-namespace.servicebus.windows.net/;SharedAccessKeyName=key;SharedAccessKey=YWJjZGVmZ2hpamtsbW5vcA==";

        private static IConfiguration BuildConfiguration(IEnumerable<KeyValuePair<string, string?>>? values = null)
            => new ConfigurationBuilder()
                .AddInMemoryCollection(values ?? new Dictionary<string, string?>())
                .Build();

        [Fact]
        public void CreateServiceBusClient_UsesConnectionString_WhenProvided()
        {
            var options = Options.Create(new AzureServiceBusOptions
            {
                ServiceBusConnectionString = FakeConnectionString
            });
            var factory = new ServiceBusClientFactory(BuildConfiguration(), options);

            var client = factory.CreateServiceBusClient();

            Assert.NotNull(client);
            Assert.Equal("connstr-namespace.servicebus.windows.net", client.FullyQualifiedNamespace);
        }

        [Fact]
        public void CreateServiceBusClient_UsesNamespaceCredential_WhenNoConnectionString()
        {
            var options = Options.Create(new AzureServiceBusOptions
            {
                ServiceBusName = "namespace-fqdn"
            });
            var configuration = BuildConfiguration(new Dictionary<string, string?>
            {
                [$"{AzureAuthenticationOptions.SECTION_NAME}:TenantId"] = "tenant",
                [$"{AzureAuthenticationOptions.SECTION_NAME}:ClientId"] = "client",
                [$"{AzureAuthenticationOptions.SECTION_NAME}:ClientSecret"] = "secret"
            });
            var factory = new ServiceBusClientFactory(configuration, options);

            var client = factory.CreateServiceBusClient();

            Assert.NotNull(client);
            Assert.Equal("namespace-fqdn.servicebus.windows.net", client.FullyQualifiedNamespace);
        }

        [Fact]
        public void CreateServiceBusAdministrationClient_UsesConnectionString_WhenProvided()
        {
            var options = Options.Create(new AzureServiceBusOptions
            {
                ServiceBusConnectionString = FakeConnectionString
            });
            var factory = new ServiceBusClientFactory(BuildConfiguration(), options);

            var adminClient = factory.CreateServiceBusAdministrationClient();

            Assert.NotNull(adminClient);
        }

        [Fact]
        public void CreateServiceBusAdministrationClient_UsesNamespaceCredential_WhenNoConnectionString()
        {
            var options = Options.Create(new AzureServiceBusOptions
            {
                ServiceBusName = "admin-namespace"
            });
            var configuration = BuildConfiguration(new Dictionary<string, string?>
            {
                [$"{AzureAuthenticationOptions.SECTION_NAME}:TenantId"] = "tenant",
                [$"{AzureAuthenticationOptions.SECTION_NAME}:ClientId"] = "client",
                [$"{AzureAuthenticationOptions.SECTION_NAME}:ClientSecret"] = "secret"
            });
            var factory = new ServiceBusClientFactory(configuration, options);

            var adminClient = factory.CreateServiceBusAdministrationClient();

            Assert.NotNull(adminClient);
        }
    }
}
