using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using RecordPoint.Connectors.SDK.Context;

namespace RecordPoint.Connectors.SDK.Test.Mock.Context
{

    /// <summary>
    /// Mock system context builder extensions
    /// </summary>
    public static class MockSystemContextBuilderExtensions
    {

        public static IHostBuilder UseMockSystemContext(this IHostBuilder hostBuilder, string sutName)
        {
            var systemOptions = new MockSystemOptions()
            {
                SUTName = sutName
            };
            hostBuilder.ConfigureServices(svcs => svcs
                .AddSingleton(Options.Create(systemOptions))
                .AddSingleton<ISystemContext, MockSystemContext>()
            );
            return hostBuilder;
        }

    }
}
