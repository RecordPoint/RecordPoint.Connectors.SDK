using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RecordPoint.Connectors.SDK.Work;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RecordPoint.Connectors.SDK.ContentManager
{
    /// <summary>
    /// Background service that bootstraps the channel discovery infrastructure for the content source
    /// </summary>
    public class ContentManagerService : BackgroundService
    {
        private readonly IOptions<ContentManagerOptions> _options;
        private readonly ILogger<ContentManagerService> _logger;
        private readonly IManagedWorkFactory _managedWorkFactory;
        private readonly IManagedWorkStatusManager _managedWorkStatusManager;

        public ContentManagerService(
            IOptions<ContentManagerOptions> options,
            ILogger<ContentManagerService> logger,
            IManagedWorkFactory managedWorkFactory,
            IManagedWorkStatusManager managedWorkStatusManager)
        {
            _options = options;
            _logger = logger;
            _managedWorkFactory = managedWorkFactory;
            _managedWorkStatusManager = managedWorkStatusManager;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await CreateContentManagerOperationAsync(stoppingToken)
                    .ConfigureAwait(false);

                await Task
                    .Delay(TimeSpan.FromSeconds(_options.Value.DelaySeconds), stoppingToken)
                    .ConfigureAwait(false);
            }

            _logger.LogInformation("Content Manager Service execution has been cancelled.");
        }


        private async Task CreateContentManagerOperationAsync(CancellationToken cancellationToken)
        {
            var isContentManagerRunning = await _managedWorkStatusManager.IsAnyAsync(a => a.WorkType == ContentManagerOperation.WORK_TYPE, cancellationToken);
            if (!isContentManagerRunning)
            {
                var work = _managedWorkFactory.CreateContentManagerOperation();
                await work.StartAsync(cancellationToken);
            }
        }

    }
}

