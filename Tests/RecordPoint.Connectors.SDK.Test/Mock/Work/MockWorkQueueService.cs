using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace RecordPoint.Connectors.SDK.Test.Mock.Work
{

    /// <summary>
    /// Background service that runs the mock work queue
    /// </summary>
    public class MockWorkQueueService : BackgroundService
    {

        private readonly MockWorkQueueClient _workQueueManager;
        private readonly IOptions<MockWorkQueueOptions> _options;

        public MockWorkQueueService(MockWorkQueueClient workQueueManager, IOptions<MockWorkQueueOptions> options)
        {
            _workQueueManager = workQueueManager;
            _options = options;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await _workQueueManager.RunRequestsAsync(stoppingToken);
                await Task.Delay(_options.Value.PollTimespan, stoppingToken);
            }
        }

    }
}
