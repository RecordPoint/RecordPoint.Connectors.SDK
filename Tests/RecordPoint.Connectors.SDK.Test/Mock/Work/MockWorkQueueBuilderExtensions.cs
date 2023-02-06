using Microsoft.Extensions.DependencyInjection;
using RecordPoint.Connectors.SDK.Work;

namespace RecordPoint.Connectors.SDK.Test.Mock.Work
{
    /// <summary>
    /// Builder extensions used to configure a mock work queue
    /// </summary>
    public static class MockWorkQueueBuilderExtensions
    {

        /// <summary>
        /// Add the mock work queue 
        /// </summary>
        /// <param name="services">Services to extend</param>
        /// <returns>Updated services</returns>
        public static IServiceCollection AddMockWorkQueue(this IServiceCollection services)
        {
            return services
                .AddSingleton<MockWorkQueueClient>()
                .AddSingleton<IWorkQueueClient>(svcs => svcs.GetRequiredService<MockWorkQueueClient>())
                .AddHostedService<MockWorkQueueService>();
        }

    }
}
