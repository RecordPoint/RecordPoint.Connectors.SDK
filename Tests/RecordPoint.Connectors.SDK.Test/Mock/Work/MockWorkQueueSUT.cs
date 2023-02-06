using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;
using RecordPoint.Connectors.SDK.Test.Work;

namespace RecordPoint.Connectors.SDK.Test.Mock.Work
{

    /// <summary>
    /// Mock work queue SUT
    /// </summary>
    public class MockWorkQueueSUT : WorkQueueSUT
    {

        protected override IHostBuilder CreateHostBuilder()
        {
            var workQueueOptions = Options.Create(new MockWorkQueueOptions
            {
                MaxRetries = 2,
                RetryTimespan = TimeSpan.FromSeconds(1)
            });

            return base.CreateHostBuilder()
                .ConfigureServices(svc => svc.AddSingleton(workQueueOptions)
                .AddMockWorkQueue());
        }

    }

}
