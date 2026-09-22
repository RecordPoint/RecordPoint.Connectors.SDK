#nullable enable
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RecordPoint.Connectors.SDK.Toggles;
using RecordPoint.Connectors.SDK.Toggles.LaunchDarkly;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace RecordPoint.Connectors.SDK.Toggles.LaunchDarkly.Test
{
    public class LaunchDarklyBuilderExtensionsTests
    {
        [Fact]
        public void UseLaunchDarklyToggles_ReturnsSameHostBuilderInstance()
        {
            var hostBuilder = Host.CreateDefaultBuilder();

            var result = hostBuilder.UseLaunchDarklyToggles();

            Assert.Same(hostBuilder, result);
        }

        [Fact]
        public void UseLaunchDarklyToggles_RegistersToggleProviderAsSingleton()
        {
            IServiceCollection? captured = null;

            var host = Host.CreateDefaultBuilder()
                .ConfigureAppConfiguration((_, configurationBuilder) =>
                {
                    configurationBuilder.AddInMemoryCollection(new Dictionary<string, string?>
                    {
                        [$"{LaunchDarklyOptions.SECTION_NAME}:{nameof(LaunchDarklyOptions.SdkKey)}"] = "fake-sdk-key"
                    });
                })
                .UseLaunchDarklyToggles()
                // Trailing callback shares the same IServiceCollection instance, so it
                // captures the registration made inside UseLaunchDarklyToggles.
                .ConfigureServices((_, services) => captured = services)
                .Build();

            Assert.NotNull(captured);

            var descriptor = captured!.FirstOrDefault(d => d.ServiceType == typeof(IToggleProvider));

            Assert.NotNull(descriptor);
            Assert.Equal(ServiceLifetime.Singleton, descriptor!.Lifetime);
            Assert.Equal(typeof(LaunchDarklyToggleProvider), descriptor.ImplementationType);

            host.Dispose();
        }
    }
}
