using Microsoft.Extensions.Hosting;
using RecordPoint.Connectors.SDK.Tests.SUT;
using RecordPoint.Connectors.SDK.Work;

namespace RecordPoint.Connectors.SDK.Test.Work
{

    /// <summary>
    /// Work Queue SUT
    /// </summary>
    public class WorkQueueSUT
    {

        public IHost Host { get; private set; }

        /// <summary>
        /// Optional override that builds the SUT host
        /// </summary>
        /// <returns></returns>
        protected virtual IHostBuilder CreateHostBuilder()
        {
            return Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder()
                .UseTestServices()
                .UseWorkManager();
        }

        /// <summary>
        /// Start the SUT running
        /// </summary>
        /// <returns>Start task</returns>
        public async Task StartAsync()
        {
            var hostBuilder = CreateHostBuilder();
            Host = hostBuilder.Build();
            await Host.StartAsync().ConfigureAwait(false);
        }

        public void Dispose()
        {
            Host?.Dispose();
        }

    }

}
