using RecordPoint.Connectors.SDK.Client;
using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.Diagnostics;
using RecordPoint.Connectors.SDK.Exceptions;
using RecordPoint.Connectors.SDK.Helpers;
using RecordPoint.Connectors.SDK.Notifications;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RecordPoint.Connectors.SDK.NotificationTasks
{
    /// <summary>
    /// A long-running task that handles pull notifications.
    /// </summary>
    public class NotificationTask
    {
        /// <summary>
        /// Settings which govern the behaviour of this long-running task.
        /// </summary>
        public NotificationRunnerSettings NotificationRunnerSettings { get; set; }

        /// <summary>
        /// Settings used to call into the Records365 vNext Connector API.
        /// </summary>
        public ApiClientFactorySettings ApiClientFactorySettings { get; set; }

        /// <summary>
        /// A function that can create a AuthenticationHelperSettings given a ConnectorConfigModel.
        /// This will be used to generate a token used to access Records365 vNext on behalf of the Connector
        /// for the purpose of acknowledging Notifications.
        /// </summary>
        public Func<ConnectorConfigModel, AuthenticationHelperSettings> AuthenticationHelperSettingsFactory { get; set; }

        /// <summary>
        /// An INotificationPullManager that will handle the work of polling for new 
        /// Notifications and acknowledging them. Recommend injecting the NotificationPullManager
        /// for this.
        /// </summary>
        public INotificationPullManager NotificationPullManager { get; set; }

        /// <summary>
        /// An INotificationHandler that will handle the work of processing Notifications 
        /// for this Connector. Will require at least custom per-content source implementation for
        /// ItemDestroyed events.
        /// </summary>
        public INotificationHandler NotificationHandler { get; set; }

        /// <summary>
        /// A log.
        /// </summary>
        public ILog Log { get; set; }

        private string _logPrefix = string.Empty;
        private AuthenticationHelperSettings _authenticationHelperSettings;
        private ConnectorConfigModel _connectorConfig;
        private CancellationToken _cancellationToken;

        /// <summary>
        /// Runs the notification task.
        /// The task will continue to run indefinitely until the cancellationToken is canceled.
        /// </summary>
        /// <param name="connectorConfig"></param>
        /// <param name="logPrefix"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task RunAsync(ConnectorConfigModel connectorConfig, string logPrefix, CancellationToken cancellationToken)
        {
            // Set up properties that will be used consistently by multiple methods
            _connectorConfig = connectorConfig;
            _cancellationToken = cancellationToken;
            _logPrefix = logPrefix;
            _authenticationHelperSettings = AuthenticationHelperSettingsFactory(connectorConfig);

            while(!cancellationToken.IsCancellationRequested)
            {
                // Get all pending notifications
                var notifications = await NotificationPullManager.GetAllPendingConnectorNotifications(
                    ApiClientFactorySettings,
                    _authenticationHelperSettings,
                    _connectorConfig.Id,
                    _cancellationToken).ConfigureAwait(false);

                // Process all pending notifications
                foreach(var notification in notifications)
                {
                    if(cancellationToken.IsCancellationRequested)
                    {
                        break;
                    }

                    await ProcessNotification(notification).ConfigureAwait(false);
                }

                // Wait the prescribed period of time before polling for more new Notifications
                await Task.Delay(TimeSpan.FromSeconds(NotificationRunnerSettings?.NotificationPollIntervalSeconds ?? NotificationRunnerSettings.DefaultNotificationPollIntervalSeconds)).ConfigureAwait(false);
            }
        }

        private async Task ProcessNotification(ConnectorNotificationModel notification)
        {
            string processingErrorMessage = string.Empty;
            ProcessingResult processingResult = ProcessingResult.OK;

            try
            {
                await NotificationHandler.HandleNotification(_connectorConfig, notification, _cancellationToken).ConfigureAwait(false);
            }
            catch (Exception ex) when (!ex.IsTaskCancellation(_cancellationToken))
            {
                Log?.LogWarning(GetType(), nameof(ProcessNotification), $"{_logPrefix} - Unhandled exception encountered - [{ex}]");
                processingResult = ProcessingResult.NotificationError;
                processingErrorMessage = ex.Message;
            }

            await AcknowledgeNotification(notification, processingResult, processingErrorMessage).ConfigureAwait(false);
        }

        private async Task AcknowledgeNotification(ConnectorNotificationModel model, ProcessingResult processingResult, string message)
        {
            try
            {
                await NotificationPullManager.AcknowledgeNotification(
                    ApiClientFactorySettings,
                    AuthenticationHelperSettingsFactory(_connectorConfig),
                    model.ToAcknowledge(processingResult, message)).ConfigureAwait(false);
            }
            catch (ResourceNotFoundException)
            {
                // The notification couldn't be acked because it wasn't found.
                // This may happen if the notification was already acked - in this case,
                // the next iteration of the poller won't receive this notification again.
                Log?.LogWarning(GetType(), nameof(AcknowledgeNotification), $"{_logPrefix} - Notification with ID [{model.Id}] was not acknowledged because it could not be found " +
                    "in the Records365 notification queue. It may have already been acknowledged.");
            }
        }
    }
}
