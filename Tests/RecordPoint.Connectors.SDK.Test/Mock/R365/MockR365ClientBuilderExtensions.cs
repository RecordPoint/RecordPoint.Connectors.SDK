using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RecordPoint.Connectors.SDK.R365;

namespace RecordPoint.Connectors.SDK.Test.Mock.R365
{
    public static class MockR365ClientBuilderExtensions
    {
        public static IHostBuilder UseMockR365Client(this IHostBuilder hostBuilder) =>
             hostBuilder
                .ConfigureServices(services =>
                    services
                        .AddSingleton<MockR365Client>()
                        .AddSingleton<IR365Client>(svcs => svcs.GetRequiredService<MockR365Client>())
                );
    }
}
