using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RecordPoint.Connectors.SDK.Connectors;
using System;

namespace RecordPoint.Connectors.SDK.Work
{
    /// <summary>
    /// Implemtation of a Managed Work Factory
    /// </summary>
    public class ManagedWorkFactory : IManagedWorkFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public ManagedWorkFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <inheritdoc/>
        public IManagedWorkManager CreateWork(string workStatusId, string workType, string connectorId, string configurationType, string configuration)
        {
            var connectorOptions = _serviceProvider.GetService<IOptions<ConnectorOptions>>().Value;
            var workStatusManager = _serviceProvider.GetRequiredService<IManagedWorkManager>();
            workStatusManager.WorkStatus.Id = workStatusId;
            workStatusManager.WorkStatus.WorkType = workType;
            workStatusManager.WorkStatus.ConnectorId = connectorId;
            workStatusManager.WorkStatus.ConfigurationType = configurationType;
            workStatusManager.WorkStatus.Configuration = configuration;
            workStatusManager.WorkStatus.State = string.Empty;
            workStatusManager.WorkStatus.WorkId = Guid.NewGuid().ToString();
            workStatusManager.WorkStatus.RetryOnFailure = connectorOptions?.RetryOnFailure ?? true;
            workStatusManager.WorkStatus.MaxRetries = connectorOptions?.MaxRetries ?? 5;
            workStatusManager.WorkStatus.MaxRetryDelay = connectorOptions?.MaxRetryDelay ?? 3600;
            workStatusManager.WorkStatus.RetryDelay = connectorOptions?.RetryDelay ?? 30;
            workStatusManager.WorkStatus.ExponentialRetryDelay = connectorOptions?.ExponentialRetryDelay ?? true;

            return workStatusManager;
        }

        /// <inheritdoc/>
        public IManagedWorkManager LoadWork(ManagedWorkStatusModel workStatus)
        {
            var connectorOptions = _serviceProvider.GetService<IOptions<ConnectorOptions>>().Value;
            var workStatusManager = _serviceProvider.GetRequiredService<IManagedWorkManager>();
            workStatus.RetryOnFailure = connectorOptions?.RetryOnFailure ?? true;
            workStatus.MaxRetries = connectorOptions?.MaxRetries ?? 5;
            workStatus.RetryDelay = connectorOptions?.RetryDelay ?? 30;
            workStatusManager.WorkStatus.MaxRetryDelay = connectorOptions?.MaxRetryDelay ?? 3600;
            workStatus.ExponentialRetryDelay = connectorOptions?.ExponentialRetryDelay ?? true;
            workStatus.CopyTo(workStatusManager.WorkStatus);
            return workStatusManager;
        }

    }
}
