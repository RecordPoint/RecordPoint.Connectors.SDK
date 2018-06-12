using RecordPoint.Connectors.SDK.Client;
using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.Helpers;
using RecordPoint.Connectors.SDK.TaskRunner;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static RecordPoint.Connectors.SDK.TaskRunner.TaskRunnerInformationBase;

namespace RecordPoint.Connectors.SDK.NotificationTasks
{
    /// <summary>
    /// Generic NotificationTaskRunner.
    /// Is intended to abstract away the task of polling for notifications and triggering their processing
    /// from a Connector Type developer.
    /// </summary>
    public class NotificationTaskRunner : TaskRunnerBase
    {
        /// <summary>
        /// A function which produces NotificationTasks 
        /// Recommend using IoC to generate NotificationTasks with all
        /// their properties intact
        /// </summary>
        public Func<NotificationTask> NotificationTaskFactory { get; set; }

        /// <summary>
        /// Optional function to perform an action when an unhandled exception
        /// is thrown from a long-running task
        /// </summary>
        public ExceptionHandlerFunction ExceptionHandlerFunction { get; set; } = null;

        /// <summary>
        /// Optional function to determine how long a long-running task can run for
        /// before we start considering any exceptions thrown to be unconnected to
        /// previous exceptions thrown by this task. If null, defaults to the settings
        /// in the TaskRunnerBaseSettings injected into TaskRunnerBase
        /// </summary>
        public RepeatedTaskFailureTimeFunction RepeatedTaskFailureTimeFunction { get; set; } = null;

        /// <summary>
        /// Stores the ConnectorConfigModels for which notifications will be polled and processed
        /// </summary>
        private static ConcurrentDictionary<Guid, ConnectorConfigModel> _connectors = new ConcurrentDictionary<Guid, ConnectorConfigModel>();

        /// <summary>
        /// Allows an initial set of connectors to be defined, e.g. from persistent storage
        /// </summary>
        /// <param name="initialConnectors"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task Run(IEnumerable<ConnectorConfigModel> initialConnectors, CancellationToken cancellationToken)
        {
            ValidationHelper.ArgumentNotNull(initialConnectors, nameof(initialConnectors));
            // NotificationTaskRunner should be a singleton - This is not thread safe
            _connectors = new ConcurrentDictionary<Guid, ConnectorConfigModel>(initialConnectors.Select(x => new KeyValuePair<Guid, ConnectorConfigModel>(x.IdAsGuid, x)));

            await Run(cancellationToken).ConfigureAwait(false);
        }

        /// <summary>
        /// Adds or updates a ConnectorConfigModel.
        /// Notifications will be polled and processed For all Enabled ConnectorConfigModels added.
        /// </summary>
        /// <param name="connectorConfig"></param>
        public void UpsertConnectorConfigModel(ConnectorConfigModel connectorConfig)
        {
            ValidationHelper.ArgumentNotNull(connectorConfig, nameof(connectorConfig));
            var shouldRefresh = false;

            _connectors.AddOrUpdate(connectorConfig.IdAsGuid, 
                // Added a new Connector
                (key) =>
                {
                    // Only need to refresh the tasks if the new Connector is enabled
                    shouldRefresh = connectorConfig.Status == ConnectorConfigStatus.Enabled;
                    return connectorConfig;
                }, 
                // Updating an existing connector
                (key, value) =>
                {
                    // Always refresh in case the connector has changed
                    shouldRefresh = true;
                    return connectorConfig;
                });

            if(shouldRefresh)
            {
                Refresh();
            }
        }

        /// <summary>
        /// Removes a ConnectorConfigModel.
        /// Notifications will no longer be polled for the ConnectorConfigModel.
        /// </summary>
        /// <param name="connectorConfigId"></param>
        /// <returns>Whether or not a ConnectorConfigModel with this id was found and removed</returns>
        public bool TryRemoveConnectorConfigModel(Guid connectorConfigId)
        {
            var removed = _connectors.TryRemove(connectorConfigId, out var dummy);
            if(removed && dummy.Status == ConnectorConfigStatus.Enabled)
            {
                // Refresh all running tasks because we no longer need to run a task for the 
                // removed ConnectorConfigModel
                Refresh();
            }

            return removed;
        }

        protected override Task<IEnumerable<TaskRunnerInformationBase>> GetTaskRunnerInformation(CancellationToken ct)
        {
            IEnumerable<TaskRunnerInformationBase> output =_connectors.Values.Where(x => x.Status == ConnectorConfigStatus.Enabled).Select(x =>
            {
                var notificationTask = NotificationTaskFactory();
                var notificationInformation = new TaskRunnerInformation<NotificationTask>(
                    x.TenantIdAsGuid,
                    x.IdAsGuid,
                    null, // We'll populate the function once we have the logprefix from the TaskRunnerInformation
                    ExceptionHandlerFunction,
                    RepeatedTaskFailureTimeFunction,
                    notificationTask,
                    ct
                );
                notificationInformation.Function = () => notificationTask.RunAsync(x, notificationInformation.LogPrefix, ct);

                return notificationInformation;
            });

            return Task.FromResult(output);
        }
    }
}
