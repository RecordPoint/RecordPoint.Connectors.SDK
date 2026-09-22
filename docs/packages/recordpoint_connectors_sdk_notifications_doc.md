<a name='assembly'></a>
# RecordPoint.Connectors.SDK.Notifications

## Contents

- [AsyncNotificationOperation](#T-RecordPoint-Connectors-SDK-Notifications-Webhook-AsyncNotificationOperation 'RecordPoint.Connectors.SDK.Notifications.Webhook.AsyncNotificationOperation')
  - [#ctor(serviceProvider,notificationManager,systemContext,observabilityScope,telemetryTracker,dateTimeProvider)](#M-RecordPoint-Connectors-SDK-Notifications-Webhook-AsyncNotificationOperation-#ctor-System-IServiceProvider,RecordPoint-Connectors-SDK-Notifications-INotificationManager,RecordPoint-Connectors-SDK-Context-ISystemContext,RecordPoint-Connectors-SDK-Observability-IObservabilityScope,RecordPoint-Connectors-SDK-Observability-ITelemetryTracker,RecordPoint-Connectors-SDK-Providers-IDateTimeProvider- 'RecordPoint.Connectors.SDK.Notifications.Webhook.AsyncNotificationOperation.#ctor(System.IServiceProvider,RecordPoint.Connectors.SDK.Notifications.INotificationManager,RecordPoint.Connectors.SDK.Context.ISystemContext,RecordPoint.Connectors.SDK.Observability.IObservabilityScope,RecordPoint.Connectors.SDK.Observability.ITelemetryTracker,RecordPoint.Connectors.SDK.Providers.IDateTimeProvider)')
  - [WORK_TYPE](#F-RecordPoint-Connectors-SDK-Notifications-Webhook-AsyncNotificationOperation-WORK_TYPE 'RecordPoint.Connectors.SDK.Notifications.Webhook.AsyncNotificationOperation.WORK_TYPE')
  - [ServiceName](#P-RecordPoint-Connectors-SDK-Notifications-Webhook-AsyncNotificationOperation-ServiceName 'RecordPoint.Connectors.SDK.Notifications.Webhook.AsyncNotificationOperation.ServiceName')
  - [WorkType](#P-RecordPoint-Connectors-SDK-Notifications-Webhook-AsyncNotificationOperation-WorkType 'RecordPoint.Connectors.SDK.Notifications.Webhook.AsyncNotificationOperation.WorkType')
  - [GetCustomKeyDimensions()](#M-RecordPoint-Connectors-SDK-Notifications-Webhook-AsyncNotificationOperation-GetCustomKeyDimensions 'RecordPoint.Connectors.SDK.Notifications.Webhook.AsyncNotificationOperation.GetCustomKeyDimensions')
  - [InnerDispose()](#M-RecordPoint-Connectors-SDK-Notifications-Webhook-AsyncNotificationOperation-InnerDispose 'RecordPoint.Connectors.SDK.Notifications.Webhook.AsyncNotificationOperation.InnerDispose')
  - [InnerRunAsync(cancellationToken)](#M-RecordPoint-Connectors-SDK-Notifications-Webhook-AsyncNotificationOperation-InnerRunAsync-System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Notifications.Webhook.AsyncNotificationOperation.InnerRunAsync(System.Threading.CancellationToken)')
- [AsyncNotifications](#T-RecordPoint-Connectors-SDK-Notifications-AsyncNotifications 'RecordPoint.Connectors.SDK.Notifications.AsyncNotifications')
  - [NotificationTypes](#P-RecordPoint-Connectors-SDK-Notifications-AsyncNotifications-NotificationTypes 'RecordPoint.Connectors.SDK.Notifications.AsyncNotifications.NotificationTypes')
- [ConnectorConfigBuilderExtensions](#T-RecordPoint-Connectors-SDK-Notifications-Handlers-ConnectorConfigBuilderExtensions 'RecordPoint.Connectors.SDK.Notifications.Handlers.ConnectorConfigBuilderExtensions')
  - [UseConnectorConfigHandlers(hostBuilder)](#M-RecordPoint-Connectors-SDK-Notifications-Handlers-ConnectorConfigBuilderExtensions-UseConnectorConfigHandlers-Microsoft-Extensions-Hosting-IHostBuilder- 'RecordPoint.Connectors.SDK.Notifications.Handlers.ConnectorConfigBuilderExtensions.UseConnectorConfigHandlers(Microsoft.Extensions.Hosting.IHostBuilder)')
  - [UseConnectorSecretHandler\`\`1(hostBuilder)](#M-RecordPoint-Connectors-SDK-Notifications-Handlers-ConnectorConfigBuilderExtensions-UseConnectorSecretHandler``1-Microsoft-Extensions-Hosting-IHostBuilder- 'RecordPoint.Connectors.SDK.Notifications.Handlers.ConnectorConfigBuilderExtensions.UseConnectorSecretHandler``1(Microsoft.Extensions.Hosting.IHostBuilder)')
  - [UseContentRegistrationHandler\`\`1(hostBuilder)](#M-RecordPoint-Connectors-SDK-Notifications-Handlers-ConnectorConfigBuilderExtensions-UseContentRegistrationHandler``1-Microsoft-Extensions-Hosting-IHostBuilder- 'RecordPoint.Connectors.SDK.Notifications.Handlers.ConnectorConfigBuilderExtensions.UseContentRegistrationHandler``1(Microsoft.Extensions.Hosting.IHostBuilder)')
  - [UseNotificationHandlers\`\`2(hostBuilder)](#M-RecordPoint-Connectors-SDK-Notifications-Handlers-ConnectorConfigBuilderExtensions-UseNotificationHandlers``2-Microsoft-Extensions-Hosting-IHostBuilder- 'RecordPoint.Connectors.SDK.Notifications.Handlers.ConnectorConfigBuilderExtensions.UseNotificationHandlers``2(Microsoft.Extensions.Hosting.IHostBuilder)')
- [ConnectorConfigCreatedHandler](#T-RecordPoint-Connectors-SDK-Notifications-Handlers-ConnectorConfigCreatedHandler 'RecordPoint.Connectors.SDK.Notifications.Handlers.ConnectorConfigCreatedHandler')
  - [#ctor(connectorManager)](#M-RecordPoint-Connectors-SDK-Notifications-Handlers-ConnectorConfigCreatedHandler-#ctor-RecordPoint-Connectors-SDK-Connectors-IConnectorConfigurationManager- 'RecordPoint.Connectors.SDK.Notifications.Handlers.ConnectorConfigCreatedHandler.#ctor(RecordPoint.Connectors.SDK.Connectors.IConnectorConfigurationManager)')
  - [CONNECTOR_CONFIG_CREATED_NOTIFICATION_TYPE](#F-RecordPoint-Connectors-SDK-Notifications-Handlers-ConnectorConfigCreatedHandler-CONNECTOR_CONFIG_CREATED_NOTIFICATION_TYPE 'RecordPoint.Connectors.SDK.Notifications.Handlers.ConnectorConfigCreatedHandler.CONNECTOR_CONFIG_CREATED_NOTIFICATION_TYPE')
  - [_connectorManager](#F-RecordPoint-Connectors-SDK-Notifications-Handlers-ConnectorConfigCreatedHandler-_connectorManager 'RecordPoint.Connectors.SDK.Notifications.Handlers.ConnectorConfigCreatedHandler._connectorManager')
  - [NotificationType](#P-RecordPoint-Connectors-SDK-Notifications-Handlers-ConnectorConfigCreatedHandler-NotificationType 'RecordPoint.Connectors.SDK.Notifications.Handlers.ConnectorConfigCreatedHandler.NotificationType')
  - [HandleNotificationAsync(notification,cancellationToken)](#M-RecordPoint-Connectors-SDK-Notifications-Handlers-ConnectorConfigCreatedHandler-HandleNotificationAsync-RecordPoint-Connectors-SDK-Client-Models-ConnectorNotificationModel,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Notifications.Handlers.ConnectorConfigCreatedHandler.HandleNotificationAsync(RecordPoint.Connectors.SDK.Client.Models.ConnectorNotificationModel,System.Threading.CancellationToken)')
- [ConnectorConfigDeletedHandler](#T-RecordPoint-Connectors-SDK-Notifications-Handlers-ConnectorConfigDeletedHandler 'RecordPoint.Connectors.SDK.Notifications.Handlers.ConnectorConfigDeletedHandler')
  - [#ctor(connectorManager)](#M-RecordPoint-Connectors-SDK-Notifications-Handlers-ConnectorConfigDeletedHandler-#ctor-RecordPoint-Connectors-SDK-Connectors-IConnectorConfigurationManager- 'RecordPoint.Connectors.SDK.Notifications.Handlers.ConnectorConfigDeletedHandler.#ctor(RecordPoint.Connectors.SDK.Connectors.IConnectorConfigurationManager)')
  - [CONNECTOR_CONFIG_DELETED_NOTIFICATION_TYPE](#F-RecordPoint-Connectors-SDK-Notifications-Handlers-ConnectorConfigDeletedHandler-CONNECTOR_CONFIG_DELETED_NOTIFICATION_TYPE 'RecordPoint.Connectors.SDK.Notifications.Handlers.ConnectorConfigDeletedHandler.CONNECTOR_CONFIG_DELETED_NOTIFICATION_TYPE')
  - [_connectorManager](#F-RecordPoint-Connectors-SDK-Notifications-Handlers-ConnectorConfigDeletedHandler-_connectorManager 'RecordPoint.Connectors.SDK.Notifications.Handlers.ConnectorConfigDeletedHandler._connectorManager')
  - [NotificationType](#P-RecordPoint-Connectors-SDK-Notifications-Handlers-ConnectorConfigDeletedHandler-NotificationType 'RecordPoint.Connectors.SDK.Notifications.Handlers.ConnectorConfigDeletedHandler.NotificationType')
  - [HandleNotificationAsync(notification,cancellationToken)](#M-RecordPoint-Connectors-SDK-Notifications-Handlers-ConnectorConfigDeletedHandler-HandleNotificationAsync-RecordPoint-Connectors-SDK-Client-Models-ConnectorNotificationModel,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Notifications.Handlers.ConnectorConfigDeletedHandler.HandleNotificationAsync(RecordPoint.Connectors.SDK.Client.Models.ConnectorNotificationModel,System.Threading.CancellationToken)')
  - [InnerHandleNotificationAsync(connectorId,cancellationToken)](#M-RecordPoint-Connectors-SDK-Notifications-Handlers-ConnectorConfigDeletedHandler-InnerHandleNotificationAsync-System-String,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Notifications.Handlers.ConnectorConfigDeletedHandler.InnerHandleNotificationAsync(System.String,System.Threading.CancellationToken)')
- [ConnectorConfigUpdatedHandler](#T-RecordPoint-Connectors-SDK-Notifications-Handlers-ConnectorConfigUpdatedHandler 'RecordPoint.Connectors.SDK.Notifications.Handlers.ConnectorConfigUpdatedHandler')
  - [#ctor(connectorManager)](#M-RecordPoint-Connectors-SDK-Notifications-Handlers-ConnectorConfigUpdatedHandler-#ctor-RecordPoint-Connectors-SDK-Connectors-IConnectorConfigurationManager- 'RecordPoint.Connectors.SDK.Notifications.Handlers.ConnectorConfigUpdatedHandler.#ctor(RecordPoint.Connectors.SDK.Connectors.IConnectorConfigurationManager)')
  - [CONNECTOR_CONFIG_UPDATED_NOTIFICATION_TYPE](#F-RecordPoint-Connectors-SDK-Notifications-Handlers-ConnectorConfigUpdatedHandler-CONNECTOR_CONFIG_UPDATED_NOTIFICATION_TYPE 'RecordPoint.Connectors.SDK.Notifications.Handlers.ConnectorConfigUpdatedHandler.CONNECTOR_CONFIG_UPDATED_NOTIFICATION_TYPE')
  - [_connectorManager](#F-RecordPoint-Connectors-SDK-Notifications-Handlers-ConnectorConfigUpdatedHandler-_connectorManager 'RecordPoint.Connectors.SDK.Notifications.Handlers.ConnectorConfigUpdatedHandler._connectorManager')
  - [NotificationType](#P-RecordPoint-Connectors-SDK-Notifications-Handlers-ConnectorConfigUpdatedHandler-NotificationType 'RecordPoint.Connectors.SDK.Notifications.Handlers.ConnectorConfigUpdatedHandler.NotificationType')
  - [HandleNotificationAsync(notification,cancellationToken)](#M-RecordPoint-Connectors-SDK-Notifications-Handlers-ConnectorConfigUpdatedHandler-HandleNotificationAsync-RecordPoint-Connectors-SDK-Client-Models-ConnectorNotificationModel,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Notifications.Handlers.ConnectorConfigUpdatedHandler.HandleNotificationAsync(RecordPoint.Connectors.SDK.Client.Models.ConnectorNotificationModel,System.Threading.CancellationToken)')
- [ConnectorRequestBuilderExtensions](#T-RecordPoint-Connectors-SDK-Notifications-Handlers-ConnectorRequestBuilderExtensions 'RecordPoint.Connectors.SDK.Notifications.Handlers.ConnectorRequestBuilderExtensions')
  - [AddConnectorRequestHandler\`\`1(hostBuilder)](#M-RecordPoint-Connectors-SDK-Notifications-Handlers-ConnectorRequestBuilderExtensions-AddConnectorRequestHandler``1-Microsoft-Extensions-Hosting-IHostBuilder- 'RecordPoint.Connectors.SDK.Notifications.Handlers.ConnectorRequestBuilderExtensions.AddConnectorRequestHandler``1(Microsoft.Extensions.Hosting.IHostBuilder)')
  - [UseConnectorRequestHandlers(hostBuilder)](#M-RecordPoint-Connectors-SDK-Notifications-Handlers-ConnectorRequestBuilderExtensions-UseConnectorRequestHandlers-Microsoft-Extensions-Hosting-IHostBuilder- 'RecordPoint.Connectors.SDK.Notifications.Handlers.ConnectorRequestBuilderExtensions.UseConnectorRequestHandlers(Microsoft.Extensions.Hosting.IHostBuilder)')
- [ConnectorRequestCallbackClient](#T-RecordPoint-Connectors-SDK-Notifications-ConnectorRequestCallbackClient 'RecordPoint.Connectors.SDK.Notifications.ConnectorRequestCallbackClient')
  - [#ctor(apiClientFactory,configurationClient,observabilityScope)](#M-RecordPoint-Connectors-SDK-Notifications-ConnectorRequestCallbackClient-#ctor-RecordPoint-Connectors-SDK-Client-IApiClientFactory,RecordPoint-Connectors-SDK-Configuration-IR365ConfigurationClient,RecordPoint-Connectors-SDK-Observability-IObservabilityScope- 'RecordPoint.Connectors.SDK.Notifications.ConnectorRequestCallbackClient.#ctor(RecordPoint.Connectors.SDK.Client.IApiClientFactory,RecordPoint.Connectors.SDK.Configuration.IR365ConfigurationClient,RecordPoint.Connectors.SDK.Observability.IObservabilityScope)')
  - [MaxRetryAttempts](#F-RecordPoint-Connectors-SDK-Notifications-ConnectorRequestCallbackClient-MaxRetryAttempts 'RecordPoint.Connectors.SDK.Notifications.ConnectorRequestCallbackClient.MaxRetryAttempts')
  - [SendAsync()](#M-RecordPoint-Connectors-SDK-Notifications-ConnectorRequestCallbackClient-SendAsync-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel,RecordPoint-Connectors-SDK-Requests-ConnectorRequestResponse,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Notifications.ConnectorRequestCallbackClient.SendAsync(RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel,RecordPoint.Connectors.SDK.Requests.ConnectorRequestResponse,System.Threading.CancellationToken)')
  - [ToNewtonsoftReadable()](#M-RecordPoint-Connectors-SDK-Notifications-ConnectorRequestCallbackClient-ToNewtonsoftReadable-System-Object- 'RecordPoint.Connectors.SDK.Notifications.ConnectorRequestCallbackClient.ToNewtonsoftReadable(System.Object)')
- [ConnectorRequestHandler](#T-RecordPoint-Connectors-SDK-Notifications-Handlers-ConnectorRequestHandler 'RecordPoint.Connectors.SDK.Notifications.Handlers.ConnectorRequestHandler')
  - [#ctor(handlers,callbackClient,secretDecryptor,logger)](#M-RecordPoint-Connectors-SDK-Notifications-Handlers-ConnectorRequestHandler-#ctor-System-Collections-Generic-IEnumerable{RecordPoint-Connectors-SDK-Requests-IConnectorRequestHandler},RecordPoint-Connectors-SDK-Requests-IConnectorRequestCallbackClient,RecordPoint-Connectors-SDK-Notifications-Handlers-IConnectorSecretDecryptor,Microsoft-Extensions-Logging-ILogger{RecordPoint-Connectors-SDK-Notifications-Handlers-ConnectorRequestHandler}- 'RecordPoint.Connectors.SDK.Notifications.Handlers.ConnectorRequestHandler.#ctor(System.Collections.Generic.IEnumerable{RecordPoint.Connectors.SDK.Requests.IConnectorRequestHandler},RecordPoint.Connectors.SDK.Requests.IConnectorRequestCallbackClient,RecordPoint.Connectors.SDK.Notifications.Handlers.IConnectorSecretDecryptor,Microsoft.Extensions.Logging.ILogger{RecordPoint.Connectors.SDK.Notifications.Handlers.ConnectorRequestHandler})')
  - [CONNECTOR_REQUEST_NOTIFICATION_TYPE](#F-RecordPoint-Connectors-SDK-Notifications-Handlers-ConnectorRequestHandler-CONNECTOR_REQUEST_NOTIFICATION_TYPE 'RecordPoint.Connectors.SDK.Notifications.Handlers.ConnectorRequestHandler.CONNECTOR_REQUEST_NOTIFICATION_TYPE')
  - [NotificationType](#P-RecordPoint-Connectors-SDK-Notifications-Handlers-ConnectorRequestHandler-NotificationType 'RecordPoint.Connectors.SDK.Notifications.Handlers.ConnectorRequestHandler.NotificationType')
  - [HandleNotificationAsync(notification,cancellationToken)](#M-RecordPoint-Connectors-SDK-Notifications-Handlers-ConnectorRequestHandler-HandleNotificationAsync-RecordPoint-Connectors-SDK-Client-Models-ConnectorNotificationModel,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Notifications.Handlers.ConnectorRequestHandler.HandleNotificationAsync(RecordPoint.Connectors.SDK.Client.Models.ConnectorNotificationModel,System.Threading.CancellationToken)')
  - [TryDecryptSecrets()](#M-RecordPoint-Connectors-SDK-Notifications-Handlers-ConnectorRequestHandler-TryDecryptSecrets-RecordPoint-Connectors-SDK-Requests-ConnectorRequestEnvelope,RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel- 'RecordPoint.Connectors.SDK.Notifications.Handlers.ConnectorRequestHandler.TryDecryptSecrets(RecordPoint.Connectors.SDK.Requests.ConnectorRequestEnvelope,RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel)')
- [ConnectorSecretDecryptor](#T-RecordPoint-Connectors-SDK-Notifications-Handlers-ConnectorSecretDecryptor 'RecordPoint.Connectors.SDK.Notifications.Handlers.ConnectorSecretDecryptor')
  - [#ctor(configurationClient)](#M-RecordPoint-Connectors-SDK-Notifications-Handlers-ConnectorSecretDecryptor-#ctor-RecordPoint-Connectors-SDK-Configuration-IR365ConfigurationClient- 'RecordPoint.Connectors.SDK.Notifications.Handlers.ConnectorSecretDecryptor.#ctor(RecordPoint.Connectors.SDK.Configuration.IR365ConfigurationClient)')
  - [Decrypt()](#M-RecordPoint-Connectors-SDK-Notifications-Handlers-ConnectorSecretDecryptor-Decrypt-System-String,System-Byte[],System-Byte[]- 'RecordPoint.Connectors.SDK.Notifications.Handlers.ConnectorSecretDecryptor.Decrypt(System.String,System.Byte[],System.Byte[])')
  - [DecryptInPlace()](#M-RecordPoint-Connectors-SDK-Notifications-Handlers-ConnectorSecretDecryptor-DecryptInPlace-System-Collections-Generic-IList{RecordPoint-Connectors-SDK-Abstractions-Content-ConnectorSecret},RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel- 'RecordPoint.Connectors.SDK.Notifications.Handlers.ConnectorSecretDecryptor.DecryptInPlace(System.Collections.Generic.IList{RecordPoint.Connectors.SDK.Abstractions.Content.ConnectorSecret},RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel)')
- [ConnectorSecretHandler](#T-RecordPoint-Connectors-SDK-Notifications-Handlers-ConnectorSecretHandler 'RecordPoint.Connectors.SDK.Notifications.Handlers.ConnectorSecretHandler')
  - [#ctor(connectorManager,connectorSecretAction,secretDecryptor)](#M-RecordPoint-Connectors-SDK-Notifications-Handlers-ConnectorSecretHandler-#ctor-RecordPoint-Connectors-SDK-Connectors-IConnectorConfigurationManager,RecordPoint-Connectors-SDK-ContentManager-IConnectorSecretAction,RecordPoint-Connectors-SDK-Notifications-Handlers-IConnectorSecretDecryptor- 'RecordPoint.Connectors.SDK.Notifications.Handlers.ConnectorSecretHandler.#ctor(RecordPoint.Connectors.SDK.Connectors.IConnectorConfigurationManager,RecordPoint.Connectors.SDK.ContentManager.IConnectorSecretAction,RecordPoint.Connectors.SDK.Notifications.Handlers.IConnectorSecretDecryptor)')
  - [CONNECTOR_SECRET_NOTIFICATION_TYPE](#F-RecordPoint-Connectors-SDK-Notifications-Handlers-ConnectorSecretHandler-CONNECTOR_SECRET_NOTIFICATION_TYPE 'RecordPoint.Connectors.SDK.Notifications.Handlers.ConnectorSecretHandler.CONNECTOR_SECRET_NOTIFICATION_TYPE')
  - [NotificationType](#P-RecordPoint-Connectors-SDK-Notifications-Handlers-ConnectorSecretHandler-NotificationType 'RecordPoint.Connectors.SDK.Notifications.Handlers.ConnectorSecretHandler.NotificationType')
  - [HandleNotificationAsync(notification,cancellationToken)](#M-RecordPoint-Connectors-SDK-Notifications-Handlers-ConnectorSecretHandler-HandleNotificationAsync-RecordPoint-Connectors-SDK-Client-Models-ConnectorNotificationModel,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Notifications.Handlers.ConnectorSecretHandler.HandleNotificationAsync(RecordPoint.Connectors.SDK.Client.Models.ConnectorNotificationModel,System.Threading.CancellationToken)')
- [ContentRegistrationHandler](#T-RecordPoint-Connectors-SDK-Notifications-Handlers-ContentRegistrationHandler 'RecordPoint.Connectors.SDK.Notifications.Handlers.ContentRegistrationHandler')
  - [#ctor(connectorManager,managedWorkFactory,contentRegistrationRequestAction)](#M-RecordPoint-Connectors-SDK-Notifications-Handlers-ContentRegistrationHandler-#ctor-RecordPoint-Connectors-SDK-Connectors-IConnectorConfigurationManager,RecordPoint-Connectors-SDK-Work-IManagedWorkFactory,RecordPoint-Connectors-SDK-ContentManager-IContentRegistrationRequestAction- 'RecordPoint.Connectors.SDK.Notifications.Handlers.ContentRegistrationHandler.#ctor(RecordPoint.Connectors.SDK.Connectors.IConnectorConfigurationManager,RecordPoint.Connectors.SDK.Work.IManagedWorkFactory,RecordPoint.Connectors.SDK.ContentManager.IContentRegistrationRequestAction)')
  - [CONTENT_REGISTRATION_NOTIFICATION_TYPE](#F-RecordPoint-Connectors-SDK-Notifications-Handlers-ContentRegistrationHandler-CONTENT_REGISTRATION_NOTIFICATION_TYPE 'RecordPoint.Connectors.SDK.Notifications.Handlers.ContentRegistrationHandler.CONTENT_REGISTRATION_NOTIFICATION_TYPE')
  - [NotificationType](#P-RecordPoint-Connectors-SDK-Notifications-Handlers-ContentRegistrationHandler-NotificationType 'RecordPoint.Connectors.SDK.Notifications.Handlers.ContentRegistrationHandler.NotificationType')
  - [HandleNotificationAsync(notification,cancellationToken)](#M-RecordPoint-Connectors-SDK-Notifications-Handlers-ContentRegistrationHandler-HandleNotificationAsync-RecordPoint-Connectors-SDK-Client-Models-ConnectorNotificationModel,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Notifications.Handlers.ContentRegistrationHandler.HandleNotificationAsync(RecordPoint.Connectors.SDK.Client.Models.ConnectorNotificationModel,System.Threading.CancellationToken)')
- [Extensions](#T-RecordPoint-Connectors-SDK-Notifications-Handlers-Extensions 'RecordPoint.Connectors.SDK.Notifications.Handlers.Extensions')
  - [ContextToList\`\`1(context)](#M-RecordPoint-Connectors-SDK-Notifications-Handlers-Extensions-ContextToList``1-System-Object- 'RecordPoint.Connectors.SDK.Notifications.Handlers.Extensions.ContextToList``1(System.Object)')
  - [ContextToObject\`\`1(context)](#M-RecordPoint-Connectors-SDK-Notifications-Handlers-Extensions-ContextToObject``1-System-Object- 'RecordPoint.Connectors.SDK.Notifications.Handlers.Extensions.ContextToObject``1(System.Object)')
- [IConnectorSecretDecryptor](#T-RecordPoint-Connectors-SDK-Notifications-Handlers-IConnectorSecretDecryptor 'RecordPoint.Connectors.SDK.Notifications.Handlers.IConnectorSecretDecryptor')
  - [DecryptInPlace(secrets,connectorConfig)](#M-RecordPoint-Connectors-SDK-Notifications-Handlers-IConnectorSecretDecryptor-DecryptInPlace-System-Collections-Generic-IList{RecordPoint-Connectors-SDK-Abstractions-Content-ConnectorSecret},RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel- 'RecordPoint.Connectors.SDK.Notifications.Handlers.IConnectorSecretDecryptor.DecryptInPlace(System.Collections.Generic.IList{RecordPoint.Connectors.SDK.Abstractions.Content.ConnectorSecret},RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel)')
- [IR365NotificationClient](#T-RecordPoint-Connectors-SDK-Client-IR365NotificationClient 'RecordPoint.Connectors.SDK.Client.IR365NotificationClient')
  - [AcknowledgeNotificationAsync(notification,result,message,cancellationToken)](#M-RecordPoint-Connectors-SDK-Client-IR365NotificationClient-AcknowledgeNotificationAsync-RecordPoint-Connectors-SDK-Client-Models-ConnectorNotificationModel,RecordPoint-Connectors-SDK-Client-ProcessingResult,System-String,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Client.IR365NotificationClient.AcknowledgeNotificationAsync(RecordPoint.Connectors.SDK.Client.Models.ConnectorNotificationModel,RecordPoint.Connectors.SDK.Client.ProcessingResult,System.String,System.Threading.CancellationToken)')
  - [GetAllPendingNotifications(cancellationToken)](#M-RecordPoint-Connectors-SDK-Client-IR365NotificationClient-GetAllPendingNotifications-System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Client.IR365NotificationClient.GetAllPendingNotifications(System.Threading.CancellationToken)')
  - [IsConfigured()](#M-RecordPoint-Connectors-SDK-Client-IR365NotificationClient-IsConfigured 'RecordPoint.Connectors.SDK.Client.IR365NotificationClient.IsConfigured')
- [ItemDestroyedHandler](#T-RecordPoint-Connectors-SDK-Notifications-Handlers-ItemDestroyedHandler 'RecordPoint.Connectors.SDK.Notifications.Handlers.ItemDestroyedHandler')
  - [#ctor(connectorManager,workQueueClient)](#M-RecordPoint-Connectors-SDK-Notifications-Handlers-ItemDestroyedHandler-#ctor-RecordPoint-Connectors-SDK-Connectors-IConnectorConfigurationManager,RecordPoint-Connectors-SDK-Work-IWorkQueueClient- 'RecordPoint.Connectors.SDK.Notifications.Handlers.ItemDestroyedHandler.#ctor(RecordPoint.Connectors.SDK.Connectors.IConnectorConfigurationManager,RecordPoint.Connectors.SDK.Work.IWorkQueueClient)')
  - [ITEM_DESTROYED_NOTIFICATION_TYPE](#F-RecordPoint-Connectors-SDK-Notifications-Handlers-ItemDestroyedHandler-ITEM_DESTROYED_NOTIFICATION_TYPE 'RecordPoint.Connectors.SDK.Notifications.Handlers.ItemDestroyedHandler.ITEM_DESTROYED_NOTIFICATION_TYPE')
  - [NotificationType](#P-RecordPoint-Connectors-SDK-Notifications-Handlers-ItemDestroyedHandler-NotificationType 'RecordPoint.Connectors.SDK.Notifications.Handlers.ItemDestroyedHandler.NotificationType')
  - [HandleNotificationAsync(notification,cancellationToken)](#M-RecordPoint-Connectors-SDK-Notifications-Handlers-ItemDestroyedHandler-HandleNotificationAsync-RecordPoint-Connectors-SDK-Client-Models-ConnectorNotificationModel,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Notifications.Handlers.ItemDestroyedHandler.HandleNotificationAsync(RecordPoint.Connectors.SDK.Client.Models.ConnectorNotificationModel,System.Threading.CancellationToken)')
  - [ParseDateTime(dateTime)](#M-RecordPoint-Connectors-SDK-Notifications-Handlers-ItemDestroyedHandler-ParseDateTime-System-DateTime- 'RecordPoint.Connectors.SDK.Notifications.Handlers.ItemDestroyedHandler.ParseDateTime(System.DateTime)')
  - [ParseMetaDataModels(metaDataModels)](#M-RecordPoint-Connectors-SDK-Notifications-Handlers-ItemDestroyedHandler-ParseMetaDataModels-System-Collections-Generic-IList{RecordPoint-Connectors-SDK-Client-Models-MetaDataModel}- 'RecordPoint.Connectors.SDK.Notifications.Handlers.ItemDestroyedHandler.ParseMetaDataModels(System.Collections.Generic.IList{RecordPoint.Connectors.SDK.Client.Models.MetaDataModel})')
- [NotificationHandler](#T-RecordPoint-Connectors-SDK-Notifications-NotificationHandler 'RecordPoint.Connectors.SDK.Notifications.NotificationHandler')
  - [NotificationType](#P-RecordPoint-Connectors-SDK-Notifications-NotificationHandler-NotificationType 'RecordPoint.Connectors.SDK.Notifications.NotificationHandler.NotificationType')
  - [HandleNotificationAsync(notification,cancellationToken)](#M-RecordPoint-Connectors-SDK-Notifications-NotificationHandler-HandleNotificationAsync-RecordPoint-Connectors-SDK-Client-Models-ConnectorNotificationModel,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Notifications.NotificationHandler.HandleNotificationAsync(RecordPoint.Connectors.SDK.Client.Models.ConnectorNotificationModel,System.Threading.CancellationToken)')
- [NotificationManager](#T-RecordPoint-Connectors-SDK-Notifications-NotificationManager 'RecordPoint.Connectors.SDK.Notifications.NotificationManager')
  - [#ctor(notificationStrategies,observabilityScope,telemetryTracker)](#M-RecordPoint-Connectors-SDK-Notifications-NotificationManager-#ctor-System-Collections-Generic-IEnumerable{RecordPoint-Connectors-SDK-Notifications-INotificationStrategy},RecordPoint-Connectors-SDK-Observability-IObservabilityScope,RecordPoint-Connectors-SDK-Observability-ITelemetryTracker- 'RecordPoint.Connectors.SDK.Notifications.NotificationManager.#ctor(System.Collections.Generic.IEnumerable{RecordPoint.Connectors.SDK.Notifications.INotificationStrategy},RecordPoint.Connectors.SDK.Observability.IObservabilityScope,RecordPoint.Connectors.SDK.Observability.ITelemetryTracker)')
  - [_notificationStrategies](#F-RecordPoint-Connectors-SDK-Notifications-NotificationManager-_notificationStrategies 'RecordPoint.Connectors.SDK.Notifications.NotificationManager._notificationStrategies')
  - [_observabilityScope](#F-RecordPoint-Connectors-SDK-Notifications-NotificationManager-_observabilityScope 'RecordPoint.Connectors.SDK.Notifications.NotificationManager._observabilityScope')
  - [_telemetryTracker](#F-RecordPoint-Connectors-SDK-Notifications-NotificationManager-_telemetryTracker 'RecordPoint.Connectors.SDK.Notifications.NotificationManager._telemetryTracker')
  - [GetDimensions(notification)](#M-RecordPoint-Connectors-SDK-Notifications-NotificationManager-GetDimensions-RecordPoint-Connectors-SDK-Client-Models-ConnectorNotificationModel- 'RecordPoint.Connectors.SDK.Notifications.NotificationManager.GetDimensions(RecordPoint.Connectors.SDK.Client.Models.ConnectorNotificationModel)')
  - [GetOutcomeDimensions(outcome)](#M-RecordPoint-Connectors-SDK-Notifications-NotificationManager-GetOutcomeDimensions-RecordPoint-Connectors-SDK-Notifications-NotificationOutcome- 'RecordPoint.Connectors.SDK.Notifications.NotificationManager.GetOutcomeDimensions(RecordPoint.Connectors.SDK.Notifications.NotificationOutcome)')
  - [HandleNotificationAsync(notification,cancellationToken)](#M-RecordPoint-Connectors-SDK-Notifications-NotificationManager-HandleNotificationAsync-RecordPoint-Connectors-SDK-Client-Models-ConnectorNotificationModel,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Notifications.NotificationManager.HandleNotificationAsync(RecordPoint.Connectors.SDK.Client.Models.ConnectorNotificationModel,System.Threading.CancellationToken)')
- [NotificationPollService](#T-RecordPoint-Connectors-SDK-Notifications-NotificationPollService 'RecordPoint.Connectors.SDK.Notifications.NotificationPollService')
  - [#ctor()](#M-RecordPoint-Connectors-SDK-Notifications-NotificationPollService-#ctor-RecordPoint-Connectors-SDK-Context-ISystemContext,Microsoft-Extensions-Options-IOptions{RecordPoint-Connectors-SDK-Notifications-NotificationsPollerOptions},RecordPoint-Connectors-SDK-Observability-IObservabilityScope,RecordPoint-Connectors-SDK-Observability-ITelemetryTracker,System-IServiceProvider- 'RecordPoint.Connectors.SDK.Notifications.NotificationPollService.#ctor(RecordPoint.Connectors.SDK.Context.ISystemContext,Microsoft.Extensions.Options.IOptions{RecordPoint.Connectors.SDK.Notifications.NotificationsPollerOptions},RecordPoint.Connectors.SDK.Observability.IObservabilityScope,RecordPoint.Connectors.SDK.Observability.ITelemetryTracker,System.IServiceProvider)')
  - [ExecuteAsync(stoppingToken)](#M-RecordPoint-Connectors-SDK-Notifications-NotificationPollService-ExecuteAsync-System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Notifications.NotificationPollService.ExecuteAsync(System.Threading.CancellationToken)')
- [NotificationsBuilderExtensions](#T-RecordPoint-Connectors-SDK-Notifications-NotificationsBuilderExtensions 'RecordPoint.Connectors.SDK.Notifications.NotificationsBuilderExtensions')
  - [UseAsyncNotificationOperation(hostBuilder)](#M-RecordPoint-Connectors-SDK-Notifications-NotificationsBuilderExtensions-UseAsyncNotificationOperation-Microsoft-Extensions-Hosting-IHostBuilder- 'RecordPoint.Connectors.SDK.Notifications.NotificationsBuilderExtensions.UseAsyncNotificationOperation(Microsoft.Extensions.Hosting.IHostBuilder)')
  - [UseNotifications()](#M-RecordPoint-Connectors-SDK-Notifications-NotificationsBuilderExtensions-UseNotifications-Microsoft-Extensions-Hosting-IHostBuilder- 'RecordPoint.Connectors.SDK.Notifications.NotificationsBuilderExtensions.UseNotifications(Microsoft.Extensions.Hosting.IHostBuilder)')
  - [UseNotifications\`\`1()](#M-RecordPoint-Connectors-SDK-Notifications-NotificationsBuilderExtensions-UseNotifications``1-Microsoft-Extensions-Hosting-IHostBuilder- 'RecordPoint.Connectors.SDK.Notifications.NotificationsBuilderExtensions.UseNotifications``1(Microsoft.Extensions.Hosting.IHostBuilder)')
  - [UseNotifications\`\`2()](#M-RecordPoint-Connectors-SDK-Notifications-NotificationsBuilderExtensions-UseNotifications``2-Microsoft-Extensions-Hosting-IHostBuilder- 'RecordPoint.Connectors.SDK.Notifications.NotificationsBuilderExtensions.UseNotifications``2(Microsoft.Extensions.Hosting.IHostBuilder)')
  - [UsePolledNotifications(hostBuilder)](#M-RecordPoint-Connectors-SDK-Notifications-NotificationsBuilderExtensions-UsePolledNotifications-Microsoft-Extensions-Hosting-IHostBuilder- 'RecordPoint.Connectors.SDK.Notifications.NotificationsBuilderExtensions.UsePolledNotifications(Microsoft.Extensions.Hosting.IHostBuilder)')
  - [UsePolledNotifications\`\`1(hostBuilder)](#M-RecordPoint-Connectors-SDK-Notifications-NotificationsBuilderExtensions-UsePolledNotifications``1-Microsoft-Extensions-Hosting-IHostBuilder- 'RecordPoint.Connectors.SDK.Notifications.NotificationsBuilderExtensions.UsePolledNotifications``1(Microsoft.Extensions.Hosting.IHostBuilder)')
  - [UsePolledNotifications\`\`2(hostBuilder)](#M-RecordPoint-Connectors-SDK-Notifications-NotificationsBuilderExtensions-UsePolledNotifications``2-Microsoft-Extensions-Hosting-IHostBuilder- 'RecordPoint.Connectors.SDK.Notifications.NotificationsBuilderExtensions.UsePolledNotifications``2(Microsoft.Extensions.Hosting.IHostBuilder)')
  - [UseWebhookNotifications(hostBuilder)](#M-RecordPoint-Connectors-SDK-Notifications-NotificationsBuilderExtensions-UseWebhookNotifications-Microsoft-Extensions-Hosting-IHostBuilder- 'RecordPoint.Connectors.SDK.Notifications.NotificationsBuilderExtensions.UseWebhookNotifications(Microsoft.Extensions.Hosting.IHostBuilder)')
  - [UseWebhookNotifications\`\`1(hostBuilder)](#M-RecordPoint-Connectors-SDK-Notifications-NotificationsBuilderExtensions-UseWebhookNotifications``1-Microsoft-Extensions-Hosting-IHostBuilder- 'RecordPoint.Connectors.SDK.Notifications.NotificationsBuilderExtensions.UseWebhookNotifications``1(Microsoft.Extensions.Hosting.IHostBuilder)')
  - [UseWebhookNotifications\`\`2(hostBuilder)](#M-RecordPoint-Connectors-SDK-Notifications-NotificationsBuilderExtensions-UseWebhookNotifications``2-Microsoft-Extensions-Hosting-IHostBuilder- 'RecordPoint.Connectors.SDK.Notifications.NotificationsBuilderExtensions.UseWebhookNotifications``2(Microsoft.Extensions.Hosting.IHostBuilder)')
- [PingHandler](#T-RecordPoint-Connectors-SDK-Notifications-Handlers-PingHandler 'RecordPoint.Connectors.SDK.Notifications.Handlers.PingHandler')
  - [#ctor(connectorClient)](#M-RecordPoint-Connectors-SDK-Notifications-Handlers-PingHandler-#ctor-RecordPoint-Connectors-SDK-Connectors-IConnectorConfigurationManager- 'RecordPoint.Connectors.SDK.Notifications.Handlers.PingHandler.#ctor(RecordPoint.Connectors.SDK.Connectors.IConnectorConfigurationManager)')
  - [PING_NOTIFICATION_TYPE](#F-RecordPoint-Connectors-SDK-Notifications-Handlers-PingHandler-PING_NOTIFICATION_TYPE 'RecordPoint.Connectors.SDK.Notifications.Handlers.PingHandler.PING_NOTIFICATION_TYPE')
  - [NotificationType](#P-RecordPoint-Connectors-SDK-Notifications-Handlers-PingHandler-NotificationType 'RecordPoint.Connectors.SDK.Notifications.Handlers.PingHandler.NotificationType')
  - [HandleNotificationAsync(notification,cancellationToken)](#M-RecordPoint-Connectors-SDK-Notifications-Handlers-PingHandler-HandleNotificationAsync-RecordPoint-Connectors-SDK-Client-Models-ConnectorNotificationModel,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Notifications.Handlers.PingHandler.HandleNotificationAsync(RecordPoint.Connectors.SDK.Client.Models.ConnectorNotificationModel,System.Threading.CancellationToken)')
- [PollNotificationsOperation](#T-RecordPoint-Connectors-SDK-Notifications-PollNotificationsOperation 'RecordPoint.Connectors.SDK.Notifications.PollNotificationsOperation')
  - [#ctor(serviceProvider,notificationManager,r365NotificationClient,observabilityScope,telemetryTracker,dateTimeProvider)](#M-RecordPoint-Connectors-SDK-Notifications-PollNotificationsOperation-#ctor-System-IServiceProvider,RecordPoint-Connectors-SDK-Notifications-INotificationManager,RecordPoint-Connectors-SDK-Client-IR365NotificationClient,RecordPoint-Connectors-SDK-Observability-IObservabilityScope,RecordPoint-Connectors-SDK-Observability-ITelemetryTracker,RecordPoint-Connectors-SDK-Providers-IDateTimeProvider- 'RecordPoint.Connectors.SDK.Notifications.PollNotificationsOperation.#ctor(System.IServiceProvider,RecordPoint.Connectors.SDK.Notifications.INotificationManager,RecordPoint.Connectors.SDK.Client.IR365NotificationClient,RecordPoint.Connectors.SDK.Observability.IObservabilityScope,RecordPoint.Connectors.SDK.Observability.ITelemetryTracker,RecordPoint.Connectors.SDK.Providers.IDateTimeProvider)')
  - [POLL_WORK_TYPE](#F-RecordPoint-Connectors-SDK-Notifications-PollNotificationsOperation-POLL_WORK_TYPE 'RecordPoint.Connectors.SDK.Notifications.PollNotificationsOperation.POLL_WORK_TYPE')
  - [_notificationManager](#F-RecordPoint-Connectors-SDK-Notifications-PollNotificationsOperation-_notificationManager 'RecordPoint.Connectors.SDK.Notifications.PollNotificationsOperation._notificationManager')
  - [_r365NotificationClient](#F-RecordPoint-Connectors-SDK-Notifications-PollNotificationsOperation-_r365NotificationClient 'RecordPoint.Connectors.SDK.Notifications.PollNotificationsOperation._r365NotificationClient')
  - [NotificationCount](#P-RecordPoint-Connectors-SDK-Notifications-PollNotificationsOperation-NotificationCount 'RecordPoint.Connectors.SDK.Notifications.PollNotificationsOperation.NotificationCount')
  - [WorkType](#P-RecordPoint-Connectors-SDK-Notifications-PollNotificationsOperation-WorkType 'RecordPoint.Connectors.SDK.Notifications.PollNotificationsOperation.WorkType')
  - [GetCustomResultMeasures()](#M-RecordPoint-Connectors-SDK-Notifications-PollNotificationsOperation-GetCustomResultMeasures 'RecordPoint.Connectors.SDK.Notifications.PollNotificationsOperation.GetCustomResultMeasures')
  - [InnerRunAsync(cancellationToken)](#M-RecordPoint-Connectors-SDK-Notifications-PollNotificationsOperation-InnerRunAsync-System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Notifications.PollNotificationsOperation.InnerRunAsync(System.Threading.CancellationToken)')
- [PullNotificationManager](#T-RecordPoint-Connectors-SDK-Notifications-PullNotificationManager 'RecordPoint.Connectors.SDK.Notifications.PullNotificationManager')
  - [#ctor(r365NotificationClient,notificationStrategies,observabilityScope,telemetryTracker)](#M-RecordPoint-Connectors-SDK-Notifications-PullNotificationManager-#ctor-RecordPoint-Connectors-SDK-Client-IR365NotificationClient,System-Collections-Generic-IEnumerable{RecordPoint-Connectors-SDK-Notifications-INotificationStrategy},RecordPoint-Connectors-SDK-Observability-IObservabilityScope,RecordPoint-Connectors-SDK-Observability-ITelemetryTracker- 'RecordPoint.Connectors.SDK.Notifications.PullNotificationManager.#ctor(RecordPoint.Connectors.SDK.Client.IR365NotificationClient,System.Collections.Generic.IEnumerable{RecordPoint.Connectors.SDK.Notifications.INotificationStrategy},RecordPoint.Connectors.SDK.Observability.IObservabilityScope,RecordPoint.Connectors.SDK.Observability.ITelemetryTracker)')
  - [_r365NotificationClient](#F-RecordPoint-Connectors-SDK-Notifications-PullNotificationManager-_r365NotificationClient 'RecordPoint.Connectors.SDK.Notifications.PullNotificationManager._r365NotificationClient')
  - [HandleNotificationAsync(notification,cancellationToken)](#M-RecordPoint-Connectors-SDK-Notifications-PullNotificationManager-HandleNotificationAsync-RecordPoint-Connectors-SDK-Client-Models-ConnectorNotificationModel,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Notifications.PullNotificationManager.HandleNotificationAsync(RecordPoint.Connectors.SDK.Client.Models.ConnectorNotificationModel,System.Threading.CancellationToken)')
- [PushNotificationManager](#T-RecordPoint-Connectors-SDK-Notifications-PushNotificationManager 'RecordPoint.Connectors.SDK.Notifications.PushNotificationManager')
  - [#ctor(notificationStrategies,observabilityScope,telemetryTracker)](#M-RecordPoint-Connectors-SDK-Notifications-PushNotificationManager-#ctor-System-Collections-Generic-IEnumerable{RecordPoint-Connectors-SDK-Notifications-INotificationStrategy},RecordPoint-Connectors-SDK-Observability-IObservabilityScope,RecordPoint-Connectors-SDK-Observability-ITelemetryTracker- 'RecordPoint.Connectors.SDK.Notifications.PushNotificationManager.#ctor(System.Collections.Generic.IEnumerable{RecordPoint.Connectors.SDK.Notifications.INotificationStrategy},RecordPoint.Connectors.SDK.Observability.IObservabilityScope,RecordPoint.Connectors.SDK.Observability.ITelemetryTracker)')
  - [HandleNotificationAsync(notification,cancellationToken)](#M-RecordPoint-Connectors-SDK-Notifications-PushNotificationManager-HandleNotificationAsync-RecordPoint-Connectors-SDK-Client-Models-ConnectorNotificationModel,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Notifications.PushNotificationManager.HandleNotificationAsync(RecordPoint.Connectors.SDK.Client.Models.ConnectorNotificationModel,System.Threading.CancellationToken)')
- [R365NotificationClient](#T-RecordPoint-Connectors-SDK-Notifications-R365NotificationClient 'RecordPoint.Connectors.SDK.Notifications.R365NotificationClient')
  - [#ctor()](#M-RecordPoint-Connectors-SDK-Notifications-R365NotificationClient-#ctor-RecordPoint-Connectors-SDK-Configuration-IR365ConfigurationClient,RecordPoint-Connectors-SDK-Observability-IObservabilityScope,RecordPoint-Connectors-SDK-Observability-ITelemetryTracker,Microsoft-Extensions-Options-IOptions{RecordPoint-Connectors-SDK-Notifications-NotificationsPollerOptions},RecordPoint-Connectors-SDK-Notifications-INotificationApiManager- 'RecordPoint.Connectors.SDK.Notifications.R365NotificationClient.#ctor(RecordPoint.Connectors.SDK.Configuration.IR365ConfigurationClient,RecordPoint.Connectors.SDK.Observability.IObservabilityScope,RecordPoint.Connectors.SDK.Observability.ITelemetryTracker,Microsoft.Extensions.Options.IOptions{RecordPoint.Connectors.SDK.Notifications.NotificationsPollerOptions},RecordPoint.Connectors.SDK.Notifications.INotificationApiManager)')
  - [AcknowledgeNotificationAsync(notification,result,message,cancellationToken)](#M-RecordPoint-Connectors-SDK-Notifications-R365NotificationClient-AcknowledgeNotificationAsync-RecordPoint-Connectors-SDK-Client-Models-ConnectorNotificationModel,RecordPoint-Connectors-SDK-Client-ProcessingResult,System-String,System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Notifications.R365NotificationClient.AcknowledgeNotificationAsync(RecordPoint.Connectors.SDK.Client.Models.ConnectorNotificationModel,RecordPoint.Connectors.SDK.Client.ProcessingResult,System.String,System.Threading.CancellationToken)')
  - [GetAllPendingNotifications(cancellationToken)](#M-RecordPoint-Connectors-SDK-Notifications-R365NotificationClient-GetAllPendingNotifications-System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Notifications.R365NotificationClient.GetAllPendingNotifications(System.Threading.CancellationToken)')
  - [IsConfigured()](#M-RecordPoint-Connectors-SDK-Notifications-R365NotificationClient-IsConfigured 'RecordPoint.Connectors.SDK.Notifications.R365NotificationClient.IsConfigured')
  - [LoadConfiguration()](#M-RecordPoint-Connectors-SDK-Notifications-R365NotificationClient-LoadConfiguration-System-String- 'RecordPoint.Connectors.SDK.Notifications.R365NotificationClient.LoadConfiguration(System.String)')
- [WebhookOperation](#T-RecordPoint-Connectors-SDK-Notifications-Webhook-WebhookOperation 'RecordPoint.Connectors.SDK.Notifications.Webhook.WebhookOperation')
  - [#ctor(serviceProvider,notificationManager,workQueueClient,observabilityScope,telemetryTracker,dateTimeProvider)](#M-RecordPoint-Connectors-SDK-Notifications-Webhook-WebhookOperation-#ctor-System-IServiceProvider,RecordPoint-Connectors-SDK-Notifications-INotificationManager,RecordPoint-Connectors-SDK-Work-IWorkQueueClient,RecordPoint-Connectors-SDK-Observability-IObservabilityScope,RecordPoint-Connectors-SDK-Observability-ITelemetryTracker,RecordPoint-Connectors-SDK-Providers-IDateTimeProvider- 'RecordPoint.Connectors.SDK.Notifications.Webhook.WebhookOperation.#ctor(System.IServiceProvider,RecordPoint.Connectors.SDK.Notifications.INotificationManager,RecordPoint.Connectors.SDK.Work.IWorkQueueClient,RecordPoint.Connectors.SDK.Observability.IObservabilityScope,RecordPoint.Connectors.SDK.Observability.ITelemetryTracker,RecordPoint.Connectors.SDK.Providers.IDateTimeProvider)')
  - [WEBHOOK_WORK_TYPE](#F-RecordPoint-Connectors-SDK-Notifications-Webhook-WebhookOperation-WEBHOOK_WORK_TYPE 'RecordPoint.Connectors.SDK.Notifications.Webhook.WebhookOperation.WEBHOOK_WORK_TYPE')
  - [ConnectorNotification](#P-RecordPoint-Connectors-SDK-Notifications-Webhook-WebhookOperation-ConnectorNotification 'RecordPoint.Connectors.SDK.Notifications.Webhook.WebhookOperation.ConnectorNotification')
  - [WorkType](#P-RecordPoint-Connectors-SDK-Notifications-Webhook-WebhookOperation-WorkType 'RecordPoint.Connectors.SDK.Notifications.Webhook.WebhookOperation.WorkType')
  - [GetCustomKeyDimensions()](#M-RecordPoint-Connectors-SDK-Notifications-Webhook-WebhookOperation-GetCustomKeyDimensions 'RecordPoint.Connectors.SDK.Notifications.Webhook.WebhookOperation.GetCustomKeyDimensions')
  - [InnerRunAsync(cancellationToken)](#M-RecordPoint-Connectors-SDK-Notifications-Webhook-WebhookOperation-InnerRunAsync-System-Threading-CancellationToken- 'RecordPoint.Connectors.SDK.Notifications.Webhook.WebhookOperation.InnerRunAsync(System.Threading.CancellationToken)')

<a name='T-RecordPoint-Connectors-SDK-Notifications-Webhook-AsyncNotificationOperation'></a>
## AsyncNotificationOperation `type`

##### Namespace

RecordPoint.Connectors.SDK.Notifications.Webhook

##### Summary

Queueable work operation that replays notification handling outside of the webhook request path.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| serviceProvider | [T:RecordPoint.Connectors.SDK.Notifications.Webhook.AsyncNotificationOperation](#T-T-RecordPoint-Connectors-SDK-Notifications-Webhook-AsyncNotificationOperation 'T:RecordPoint.Connectors.SDK.Notifications.Webhook.AsyncNotificationOperation') | The service provider. |

##### Remarks

Initializes a new instance of the [AsyncNotificationOperation](#T-RecordPoint-Connectors-SDK-Notifications-Webhook-AsyncNotificationOperation 'RecordPoint.Connectors.SDK.Notifications.Webhook.AsyncNotificationOperation') class.

<a name='M-RecordPoint-Connectors-SDK-Notifications-Webhook-AsyncNotificationOperation-#ctor-System-IServiceProvider,RecordPoint-Connectors-SDK-Notifications-INotificationManager,RecordPoint-Connectors-SDK-Context-ISystemContext,RecordPoint-Connectors-SDK-Observability-IObservabilityScope,RecordPoint-Connectors-SDK-Observability-ITelemetryTracker,RecordPoint-Connectors-SDK-Providers-IDateTimeProvider-'></a>
### #ctor(serviceProvider,notificationManager,systemContext,observabilityScope,telemetryTracker,dateTimeProvider) `constructor`

##### Summary

Queueable work operation that replays notification handling outside of the webhook request path.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| serviceProvider | [System.IServiceProvider](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.IServiceProvider 'System.IServiceProvider') | The service provider. |
| notificationManager | [RecordPoint.Connectors.SDK.Notifications.INotificationManager](#T-RecordPoint-Connectors-SDK-Notifications-INotificationManager 'RecordPoint.Connectors.SDK.Notifications.INotificationManager') | The notification manager. |
| systemContext | [RecordPoint.Connectors.SDK.Context.ISystemContext](#T-RecordPoint-Connectors-SDK-Context-ISystemContext 'RecordPoint.Connectors.SDK.Context.ISystemContext') | The system context. |
| observabilityScope | [RecordPoint.Connectors.SDK.Observability.IObservabilityScope](#T-RecordPoint-Connectors-SDK-Observability-IObservabilityScope 'RecordPoint.Connectors.SDK.Observability.IObservabilityScope') | The scope manager. |
| telemetryTracker | [RecordPoint.Connectors.SDK.Observability.ITelemetryTracker](#T-RecordPoint-Connectors-SDK-Observability-ITelemetryTracker 'RecordPoint.Connectors.SDK.Observability.ITelemetryTracker') | The telemetry tracker. |
| dateTimeProvider | [RecordPoint.Connectors.SDK.Providers.IDateTimeProvider](#T-RecordPoint-Connectors-SDK-Providers-IDateTimeProvider 'RecordPoint.Connectors.SDK.Providers.IDateTimeProvider') | The date time provider. |

##### Remarks

Initializes a new instance of the [AsyncNotificationOperation](#T-RecordPoint-Connectors-SDK-Notifications-Webhook-AsyncNotificationOperation 'RecordPoint.Connectors.SDK.Notifications.Webhook.AsyncNotificationOperation') class.

<a name='F-RecordPoint-Connectors-SDK-Notifications-Webhook-AsyncNotificationOperation-WORK_TYPE'></a>
### WORK_TYPE `constants`

##### Summary

Async notifications work type.

<a name='P-RecordPoint-Connectors-SDK-Notifications-Webhook-AsyncNotificationOperation-ServiceName'></a>
### ServiceName `property`

##### Summary

Gets the service name.

<a name='P-RecordPoint-Connectors-SDK-Notifications-Webhook-AsyncNotificationOperation-WorkType'></a>
### WorkType `property`

##### Summary

Gets the work type.

<a name='M-RecordPoint-Connectors-SDK-Notifications-Webhook-AsyncNotificationOperation-GetCustomKeyDimensions'></a>
### GetCustomKeyDimensions() `method`

##### Summary

Get custom key dimensions.

##### Returns

A Dimensions

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Notifications-Webhook-AsyncNotificationOperation-InnerDispose'></a>
### InnerDispose() `method`

##### Summary

Dispose invocation results

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Notifications-Webhook-AsyncNotificationOperation-InnerRunAsync-System-Threading-CancellationToken-'></a>
### InnerRunAsync(cancellationToken) `method`

##### Summary

Processes a notification that was previously accepted by the webhook and dispatched to the
async notifications queue. This executes the normal notification handling pipeline after the
original HTTP request has already completed.

##### Returns

A Task

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token. |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [System.InvalidOperationException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.InvalidOperationException 'System.InvalidOperationException') |  |

<a name='T-RecordPoint-Connectors-SDK-Notifications-AsyncNotifications'></a>
## AsyncNotifications `type`

##### Namespace

RecordPoint.Connectors.SDK.Notifications

##### Summary

Configuration for notification types that should be routed to the async notifications queue.
By default, all notification types are processed synchronously in the webhook request path.
Add a notification type to this list only after the connector has registered
`UseAsyncNotificationOperation()` and the underlying work queue for
`AsyncNotificationOperation.WORK_TYPE` is available in the deployment environment.

<a name='P-RecordPoint-Connectors-SDK-Notifications-AsyncNotifications-NotificationTypes'></a>
### NotificationTypes `property`

##### Summary

List of notification types that should be processed asynchronously.
For Azure Service Bus, the queue name is derived from the work type using the standard SDK rule:
`{QueuePrefix}-async-notifications` when `AzureServiceBusSettings:QueuePrefix` is configured,
otherwise `async-notifications`. The queue name is lower-case with spaces replaced by hyphens.

<a name='T-RecordPoint-Connectors-SDK-Notifications-Handlers-ConnectorConfigBuilderExtensions'></a>
## ConnectorConfigBuilderExtensions `type`

##### Namespace

RecordPoint.Connectors.SDK.Notifications.Handlers

##### Summary

Host builder extensions for the connectors service

<a name='M-RecordPoint-Connectors-SDK-Notifications-Handlers-ConnectorConfigBuilderExtensions-UseConnectorConfigHandlers-Microsoft-Extensions-Hosting-IHostBuilder-'></a>
### UseConnectorConfigHandlers(hostBuilder) `method`

##### Summary

Use base connector notification handlers

##### Returns

Updated host builder

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| hostBuilder | [Microsoft.Extensions.Hosting.IHostBuilder](#T-Microsoft-Extensions-Hosting-IHostBuilder 'Microsoft.Extensions.Hosting.IHostBuilder') | Host builder to configure |

<a name='M-RecordPoint-Connectors-SDK-Notifications-Handlers-ConnectorConfigBuilderExtensions-UseConnectorSecretHandler``1-Microsoft-Extensions-Hosting-IHostBuilder-'></a>
### UseConnectorSecretHandler\`\`1(hostBuilder) `method`

##### Summary

Use the connector secret handler

##### Returns

Updated host builder

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| hostBuilder | [Microsoft.Extensions.Hosting.IHostBuilder](#T-Microsoft-Extensions-Hosting-IHostBuilder 'Microsoft.Extensions.Hosting.IHostBuilder') | Host builder to configure |

<a name='M-RecordPoint-Connectors-SDK-Notifications-Handlers-ConnectorConfigBuilderExtensions-UseContentRegistrationHandler``1-Microsoft-Extensions-Hosting-IHostBuilder-'></a>
### UseContentRegistrationHandler\`\`1(hostBuilder) `method`

##### Summary

Use the connector content registration handler

##### Returns

Updated host builder

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| hostBuilder | [Microsoft.Extensions.Hosting.IHostBuilder](#T-Microsoft-Extensions-Hosting-IHostBuilder 'Microsoft.Extensions.Hosting.IHostBuilder') | Host builder to configure |

<a name='M-RecordPoint-Connectors-SDK-Notifications-Handlers-ConnectorConfigBuilderExtensions-UseNotificationHandlers``2-Microsoft-Extensions-Hosting-IHostBuilder-'></a>
### UseNotificationHandlers\`\`2(hostBuilder) `method`

##### Summary

Use all connector notification handlers

##### Returns

Updated host builder

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| hostBuilder | [Microsoft.Extensions.Hosting.IHostBuilder](#T-Microsoft-Extensions-Hosting-IHostBuilder 'Microsoft.Extensions.Hosting.IHostBuilder') | Host builder to configure |

<a name='T-RecordPoint-Connectors-SDK-Notifications-Handlers-ConnectorConfigCreatedHandler'></a>
## ConnectorConfigCreatedHandler `type`

##### Namespace

RecordPoint.Connectors.SDK.Notifications.Handlers

##### Summary

The connector config created handler.

<a name='M-RecordPoint-Connectors-SDK-Notifications-Handlers-ConnectorConfigCreatedHandler-#ctor-RecordPoint-Connectors-SDK-Connectors-IConnectorConfigurationManager-'></a>
### #ctor(connectorManager) `constructor`

##### Summary

Initializes a new instance of the [ConnectorConfigCreatedHandler](#T-RecordPoint-Connectors-SDK-Notifications-Handlers-ConnectorConfigCreatedHandler 'RecordPoint.Connectors.SDK.Notifications.Handlers.ConnectorConfigCreatedHandler') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| connectorManager | [RecordPoint.Connectors.SDK.Connectors.IConnectorConfigurationManager](#T-RecordPoint-Connectors-SDK-Connectors-IConnectorConfigurationManager 'RecordPoint.Connectors.SDK.Connectors.IConnectorConfigurationManager') | The connector manager. |

<a name='F-RecordPoint-Connectors-SDK-Notifications-Handlers-ConnectorConfigCreatedHandler-CONNECTOR_CONFIG_CREATED_NOTIFICATION_TYPE'></a>
### CONNECTOR_CONFIG_CREATED_NOTIFICATION_TYPE `constants`

##### Summary

The CONNECTOR CONFIG CREATED NOTIFICATION TYPE.

<a name='F-RecordPoint-Connectors-SDK-Notifications-Handlers-ConnectorConfigCreatedHandler-_connectorManager'></a>
### _connectorManager `constants`

##### Summary

The connector manager.

<a name='P-RecordPoint-Connectors-SDK-Notifications-Handlers-ConnectorConfigCreatedHandler-NotificationType'></a>
### NotificationType `property`

##### Summary

Gets the notification type.

<a name='M-RecordPoint-Connectors-SDK-Notifications-Handlers-ConnectorConfigCreatedHandler-HandleNotificationAsync-RecordPoint-Connectors-SDK-Client-Models-ConnectorNotificationModel,System-Threading-CancellationToken-'></a>
### HandleNotificationAsync(notification,cancellationToken) `method`

##### Summary

Handle the notification asynchronously.

##### Returns

Task<NotificationOutcome>

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| notification | [RecordPoint.Connectors.SDK.Client.Models.ConnectorNotificationModel](#T-RecordPoint-Connectors-SDK-Client-Models-ConnectorNotificationModel 'RecordPoint.Connectors.SDK.Client.Models.ConnectorNotificationModel') | The notification. |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token. |

<a name='T-RecordPoint-Connectors-SDK-Notifications-Handlers-ConnectorConfigDeletedHandler'></a>
## ConnectorConfigDeletedHandler `type`

##### Namespace

RecordPoint.Connectors.SDK.Notifications.Handlers

##### Summary

The connector config deleted handler.

<a name='M-RecordPoint-Connectors-SDK-Notifications-Handlers-ConnectorConfigDeletedHandler-#ctor-RecordPoint-Connectors-SDK-Connectors-IConnectorConfigurationManager-'></a>
### #ctor(connectorManager) `constructor`

##### Summary

Initializes a new instance of the [ConnectorConfigDeletedHandler](#T-RecordPoint-Connectors-SDK-Notifications-Handlers-ConnectorConfigDeletedHandler 'RecordPoint.Connectors.SDK.Notifications.Handlers.ConnectorConfigDeletedHandler') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| connectorManager | [RecordPoint.Connectors.SDK.Connectors.IConnectorConfigurationManager](#T-RecordPoint-Connectors-SDK-Connectors-IConnectorConfigurationManager 'RecordPoint.Connectors.SDK.Connectors.IConnectorConfigurationManager') | The connector manager. |

<a name='F-RecordPoint-Connectors-SDK-Notifications-Handlers-ConnectorConfigDeletedHandler-CONNECTOR_CONFIG_DELETED_NOTIFICATION_TYPE'></a>
### CONNECTOR_CONFIG_DELETED_NOTIFICATION_TYPE `constants`

##### Summary

The CONNECTOR CONFIG DELETED NOTIFICATION TYPE.

<a name='F-RecordPoint-Connectors-SDK-Notifications-Handlers-ConnectorConfigDeletedHandler-_connectorManager'></a>
### _connectorManager `constants`

##### Summary

The connector manager.

<a name='P-RecordPoint-Connectors-SDK-Notifications-Handlers-ConnectorConfigDeletedHandler-NotificationType'></a>
### NotificationType `property`

##### Summary

Gets the notification type.

<a name='M-RecordPoint-Connectors-SDK-Notifications-Handlers-ConnectorConfigDeletedHandler-HandleNotificationAsync-RecordPoint-Connectors-SDK-Client-Models-ConnectorNotificationModel,System-Threading-CancellationToken-'></a>
### HandleNotificationAsync(notification,cancellationToken) `method`

##### Summary

Handle the notification asynchronously.

##### Returns

Task<NotificationOutcome>

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| notification | [RecordPoint.Connectors.SDK.Client.Models.ConnectorNotificationModel](#T-RecordPoint-Connectors-SDK-Client-Models-ConnectorNotificationModel 'RecordPoint.Connectors.SDK.Client.Models.ConnectorNotificationModel') | The notification. |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token. |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [RecordPoint.Connectors.SDK.RequiredValueNullException](#T-RecordPoint-Connectors-SDK-RequiredValueNullException 'RecordPoint.Connectors.SDK.RequiredValueNullException') |  |

<a name='M-RecordPoint-Connectors-SDK-Notifications-Handlers-ConnectorConfigDeletedHandler-InnerHandleNotificationAsync-System-String,System-Threading-CancellationToken-'></a>
### InnerHandleNotificationAsync(connectorId,cancellationToken) `method`

##### Summary

Inner handle notification asynchronously.

##### Returns

Task<NotificationOutcome>

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| connectorId | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') | The connector id. |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token. |

<a name='T-RecordPoint-Connectors-SDK-Notifications-Handlers-ConnectorConfigUpdatedHandler'></a>
## ConnectorConfigUpdatedHandler `type`

##### Namespace

RecordPoint.Connectors.SDK.Notifications.Handlers

##### Summary

The connector config updated handler.

<a name='M-RecordPoint-Connectors-SDK-Notifications-Handlers-ConnectorConfigUpdatedHandler-#ctor-RecordPoint-Connectors-SDK-Connectors-IConnectorConfigurationManager-'></a>
### #ctor(connectorManager) `constructor`

##### Summary

Initializes a new instance of the [ConnectorConfigUpdatedHandler](#T-RecordPoint-Connectors-SDK-Notifications-Handlers-ConnectorConfigUpdatedHandler 'RecordPoint.Connectors.SDK.Notifications.Handlers.ConnectorConfigUpdatedHandler') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| connectorManager | [RecordPoint.Connectors.SDK.Connectors.IConnectorConfigurationManager](#T-RecordPoint-Connectors-SDK-Connectors-IConnectorConfigurationManager 'RecordPoint.Connectors.SDK.Connectors.IConnectorConfigurationManager') | The connector manager. |

<a name='F-RecordPoint-Connectors-SDK-Notifications-Handlers-ConnectorConfigUpdatedHandler-CONNECTOR_CONFIG_UPDATED_NOTIFICATION_TYPE'></a>
### CONNECTOR_CONFIG_UPDATED_NOTIFICATION_TYPE `constants`

##### Summary

The CONNECTOR CONFIG UPDATED NOTIFICATION TYPE.

<a name='F-RecordPoint-Connectors-SDK-Notifications-Handlers-ConnectorConfigUpdatedHandler-_connectorManager'></a>
### _connectorManager `constants`

##### Summary

The connector manager.

<a name='P-RecordPoint-Connectors-SDK-Notifications-Handlers-ConnectorConfigUpdatedHandler-NotificationType'></a>
### NotificationType `property`

##### Summary

Gets the notification type.

<a name='M-RecordPoint-Connectors-SDK-Notifications-Handlers-ConnectorConfigUpdatedHandler-HandleNotificationAsync-RecordPoint-Connectors-SDK-Client-Models-ConnectorNotificationModel,System-Threading-CancellationToken-'></a>
### HandleNotificationAsync(notification,cancellationToken) `method`

##### Summary

Handle the notification asynchronously.

##### Returns

Task<NotificationOutcome>

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| notification | [RecordPoint.Connectors.SDK.Client.Models.ConnectorNotificationModel](#T-RecordPoint-Connectors-SDK-Client-Models-ConnectorNotificationModel 'RecordPoint.Connectors.SDK.Client.Models.ConnectorNotificationModel') | The notification. |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token. |

<a name='T-RecordPoint-Connectors-SDK-Notifications-Handlers-ConnectorRequestBuilderExtensions'></a>
## ConnectorRequestBuilderExtensions `type`

##### Namespace

RecordPoint.Connectors.SDK.Notifications.Handlers

##### Summary

Host builder extensions for answering connector requests.

##### Remarks

Registered as an INotificationStrategy, so webhook and polled both pick it up.

<a name='M-RecordPoint-Connectors-SDK-Notifications-Handlers-ConnectorRequestBuilderExtensions-AddConnectorRequestHandler``1-Microsoft-Extensions-Hosting-IHostBuilder-'></a>
### AddConnectorRequestHandler\`\`1(hostBuilder) `method`

##### Summary

Registers one request handler. Call once per request type answered.

##### Returns

Updated host builder.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| hostBuilder | [Microsoft.Extensions.Hosting.IHostBuilder](#T-Microsoft-Extensions-Hosting-IHostBuilder 'Microsoft.Extensions.Hosting.IHostBuilder') | Host builder to configure. |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| THandler | The handler to register. |

##### Remarks

Also enables connector requests. Two for one type is allowed; the last wins.

<a name='M-RecordPoint-Connectors-SDK-Notifications-Handlers-ConnectorRequestBuilderExtensions-UseConnectorRequestHandlers-Microsoft-Extensions-Hosting-IHostBuilder-'></a>
### UseConnectorRequestHandlers(hostBuilder) `method`

##### Summary

Enables connector requests. Add handlers with AddConnectorRequestHandler.

##### Returns

Updated host builder.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| hostBuilder | [Microsoft.Extensions.Hosting.IHostBuilder](#T-Microsoft-Extensions-Hosting-IHostBuilder 'Microsoft.Extensions.Hosting.IHostBuilder') | Host builder to configure. |

##### Remarks

Safe to call twice: two entries for one notification type would break dispatch.

<a name='T-RecordPoint-Connectors-SDK-Notifications-ConnectorRequestCallbackClient'></a>
## ConnectorRequestCallbackClient `type`

##### Namespace

RecordPoint.Connectors.SDK.Notifications

##### Summary

Returns a connector's answer by posting it to the Connector API, authenticated as the connector.

##### Remarks

Shaped like NotificationApiManager.DisposalCallback: the caller supplies auth and retry.

<a name='M-RecordPoint-Connectors-SDK-Notifications-ConnectorRequestCallbackClient-#ctor-RecordPoint-Connectors-SDK-Client-IApiClientFactory,RecordPoint-Connectors-SDK-Configuration-IR365ConfigurationClient,RecordPoint-Connectors-SDK-Observability-IObservabilityScope-'></a>
### #ctor(apiClientFactory,configurationClient,observabilityScope) `constructor`

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| apiClientFactory | [RecordPoint.Connectors.SDK.Client.IApiClientFactory](#T-RecordPoint-Connectors-SDK-Client-IApiClientFactory 'RecordPoint.Connectors.SDK.Client.IApiClientFactory') | Supplies the API client and the authentication provider. |
| configurationClient | [RecordPoint.Connectors.SDK.Configuration.IR365ConfigurationClient](#T-RecordPoint-Connectors-SDK-Configuration-IR365ConfigurationClient 'RecordPoint.Connectors.SDK.Configuration.IR365ConfigurationClient') | Supplies the Connector API address and credentials. |
| observabilityScope | [RecordPoint.Connectors.SDK.Observability.IObservabilityScope](#T-RecordPoint-Connectors-SDK-Observability-IObservabilityScope 'RecordPoint.Connectors.SDK.Observability.IObservabilityScope') | Wraps the call for telemetry, as the other clients do. |

<a name='F-RecordPoint-Connectors-SDK-Notifications-ConnectorRequestCallbackClient-MaxRetryAttempts'></a>
### MaxRetryAttempts `constants`

##### Summary

Attempts before an answer is given up on, matching the other API clients.

<a name='M-RecordPoint-Connectors-SDK-Notifications-ConnectorRequestCallbackClient-SendAsync-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel,RecordPoint-Connectors-SDK-Requests-ConnectorRequestResponse,System-Threading-CancellationToken-'></a>
### SendAsync() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Notifications-ConnectorRequestCallbackClient-ToNewtonsoftReadable-System-Object-'></a>
### ToNewtonsoftReadable() `method`

##### Summary

Puts an answer's data into a form the generated client can serialise.

##### Parameters

This method has no parameters.

##### Remarks

Newtonsoft renders a JsonElement as an empty object, so a relayed payload is re-read.

<a name='T-RecordPoint-Connectors-SDK-Notifications-Handlers-ConnectorRequestHandler'></a>
## ConnectorRequestHandler `type`

##### Namespace

RecordPoint.Connectors.SDK.Notifications.Handlers

##### Summary

Reads a ConnectorRequest notification, dispatches it, and returns the answer to the platform.

##### Remarks

One notification type for every request type, so a new one needs no transport change.

<a name='M-RecordPoint-Connectors-SDK-Notifications-Handlers-ConnectorRequestHandler-#ctor-System-Collections-Generic-IEnumerable{RecordPoint-Connectors-SDK-Requests-IConnectorRequestHandler},RecordPoint-Connectors-SDK-Requests-IConnectorRequestCallbackClient,RecordPoint-Connectors-SDK-Notifications-Handlers-IConnectorSecretDecryptor,Microsoft-Extensions-Logging-ILogger{RecordPoint-Connectors-SDK-Notifications-Handlers-ConnectorRequestHandler}-'></a>
### #ctor(handlers,callbackClient,secretDecryptor,logger) `constructor`

##### Summary

Handles the connector request notification.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| handlers | [System.Collections.Generic.IEnumerable{RecordPoint.Connectors.SDK.Requests.IConnectorRequestHandler}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IEnumerable 'System.Collections.Generic.IEnumerable{RecordPoint.Connectors.SDK.Requests.IConnectorRequestHandler}') | The request handlers this connector registered, one per request type. |
| callbackClient | [RecordPoint.Connectors.SDK.Requests.IConnectorRequestCallbackClient](#T-RecordPoint-Connectors-SDK-Requests-IConnectorRequestCallbackClient 'RecordPoint.Connectors.SDK.Requests.IConnectorRequestCallbackClient') | Returns the answer to the platform. |
| secretDecryptor | [RecordPoint.Connectors.SDK.Notifications.Handlers.IConnectorSecretDecryptor](#T-RecordPoint-Connectors-SDK-Notifications-Handlers-IConnectorSecretDecryptor 'RecordPoint.Connectors.SDK.Notifications.Handlers.IConnectorSecretDecryptor') | Decrypts the request's secrets before a handler sees them. |
| logger | [Microsoft.Extensions.Logging.ILogger{RecordPoint.Connectors.SDK.Notifications.Handlers.ConnectorRequestHandler}](#T-Microsoft-Extensions-Logging-ILogger{RecordPoint-Connectors-SDK-Notifications-Handlers-ConnectorRequestHandler} 'Microsoft.Extensions.Logging.ILogger{RecordPoint.Connectors.SDK.Notifications.Handlers.ConnectorRequestHandler}') | Logger. |

<a name='F-RecordPoint-Connectors-SDK-Notifications-Handlers-ConnectorRequestHandler-CONNECTOR_REQUEST_NOTIFICATION_TYPE'></a>
### CONNECTOR_REQUEST_NOTIFICATION_TYPE `constants`

##### Summary

Connector Request Notification Type

<a name='P-RecordPoint-Connectors-SDK-Notifications-Handlers-ConnectorRequestHandler-NotificationType'></a>
### NotificationType `property`

##### Summary

Connector Request Notification Type

<a name='M-RecordPoint-Connectors-SDK-Notifications-Handlers-ConnectorRequestHandler-HandleNotificationAsync-RecordPoint-Connectors-SDK-Client-Models-ConnectorNotificationModel,System-Threading-CancellationToken-'></a>
### HandleNotificationAsync(notification,cancellationToken) `method`

##### Summary

Answers the request carried on the notification.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| notification | [RecordPoint.Connectors.SDK.Client.Models.ConnectorNotificationModel](#T-RecordPoint-Connectors-SDK-Client-Models-ConnectorNotificationModel 'RecordPoint.Connectors.SDK.Client.Models.ConnectorNotificationModel') | The notification, carrying the request envelope as its context. |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | Cancellation token. |

##### Remarks

OK whenever an answer was sent, even a failed one. Neither causes redelivery.

<a name='M-RecordPoint-Connectors-SDK-Notifications-Handlers-ConnectorRequestHandler-TryDecryptSecrets-RecordPoint-Connectors-SDK-Requests-ConnectorRequestEnvelope,RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel-'></a>
### TryDecryptSecrets() `method`

##### Summary

Turns the request's secrets into values a handler can use.

##### Returns

False when they could not be read, so the request is answered as failed.

##### Parameters

This method has no parameters.

<a name='T-RecordPoint-Connectors-SDK-Notifications-Handlers-ConnectorSecretDecryptor'></a>
## ConnectorSecretDecryptor `type`

##### Namespace

RecordPoint.Connectors.SDK.Notifications.Handlers

##### Summary

*Inherit from parent.*

<a name='M-RecordPoint-Connectors-SDK-Notifications-Handlers-ConnectorSecretDecryptor-#ctor-RecordPoint-Connectors-SDK-Configuration-IR365ConfigurationClient-'></a>
### #ctor(configurationClient) `constructor`

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| configurationClient | [RecordPoint.Connectors.SDK.Configuration.IR365ConfigurationClient](#T-RecordPoint-Connectors-SDK-Configuration-IR365ConfigurationClient 'RecordPoint.Connectors.SDK.Configuration.IR365ConfigurationClient') | Supplies the client id and secret the key derives from. |

<a name='M-RecordPoint-Connectors-SDK-Notifications-Handlers-ConnectorSecretDecryptor-Decrypt-System-String,System-Byte[],System-Byte[]-'></a>
### Decrypt() `method`

##### Parameters

This method has no parameters.

##### Remarks

One decryptor per secret: CBC carries state, so a reused one returns rubbish.

<a name='M-RecordPoint-Connectors-SDK-Notifications-Handlers-ConnectorSecretDecryptor-DecryptInPlace-System-Collections-Generic-IList{RecordPoint-Connectors-SDK-Abstractions-Content-ConnectorSecret},RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel-'></a>
### DecryptInPlace() `method`

##### Summary

*Inherit from parent.*

##### Parameters

This method has no parameters.

<a name='T-RecordPoint-Connectors-SDK-Notifications-Handlers-ConnectorSecretHandler'></a>
## ConnectorSecretHandler `type`

##### Namespace

RecordPoint.Connectors.SDK.Notifications.Handlers

##### Summary

Handler for a connector secret notification

<a name='M-RecordPoint-Connectors-SDK-Notifications-Handlers-ConnectorSecretHandler-#ctor-RecordPoint-Connectors-SDK-Connectors-IConnectorConfigurationManager,RecordPoint-Connectors-SDK-ContentManager-IConnectorSecretAction,RecordPoint-Connectors-SDK-Notifications-Handlers-IConnectorSecretDecryptor-'></a>
### #ctor(connectorManager,connectorSecretAction,secretDecryptor) `constructor`

##### Summary

Handler for a connector secret notification

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| connectorManager | [RecordPoint.Connectors.SDK.Connectors.IConnectorConfigurationManager](#T-RecordPoint-Connectors-SDK-Connectors-IConnectorConfigurationManager 'RecordPoint.Connectors.SDK.Connectors.IConnectorConfigurationManager') |  |
| connectorSecretAction | [RecordPoint.Connectors.SDK.ContentManager.IConnectorSecretAction](#T-RecordPoint-Connectors-SDK-ContentManager-IConnectorSecretAction 'RecordPoint.Connectors.SDK.ContentManager.IConnectorSecretAction') |  |
| secretDecryptor | [RecordPoint.Connectors.SDK.Notifications.Handlers.IConnectorSecretDecryptor](#T-RecordPoint-Connectors-SDK-Notifications-Handlers-IConnectorSecretDecryptor 'RecordPoint.Connectors.SDK.Notifications.Handlers.IConnectorSecretDecryptor') |  |

<a name='F-RecordPoint-Connectors-SDK-Notifications-Handlers-ConnectorSecretHandler-CONNECTOR_SECRET_NOTIFICATION_TYPE'></a>
### CONNECTOR_SECRET_NOTIFICATION_TYPE `constants`

##### Summary

Connector Secret Notification Type

<a name='P-RecordPoint-Connectors-SDK-Notifications-Handlers-ConnectorSecretHandler-NotificationType'></a>
### NotificationType `property`

##### Summary

Connector Secret Notification Type

<a name='M-RecordPoint-Connectors-SDK-Notifications-Handlers-ConnectorSecretHandler-HandleNotificationAsync-RecordPoint-Connectors-SDK-Client-Models-ConnectorNotificationModel,System-Threading-CancellationToken-'></a>
### HandleNotificationAsync(notification,cancellationToken) `method`

##### Summary

Handle the Connector Secret Notification

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| notification | [RecordPoint.Connectors.SDK.Client.Models.ConnectorNotificationModel](#T-RecordPoint-Connectors-SDK-Client-Models-ConnectorNotificationModel 'RecordPoint.Connectors.SDK.Client.Models.ConnectorNotificationModel') |  |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') |  |

<a name='T-RecordPoint-Connectors-SDK-Notifications-Handlers-ContentRegistrationHandler'></a>
## ContentRegistrationHandler `type`

##### Namespace

RecordPoint.Connectors.SDK.Notifications.Handlers

##### Summary

Handler for a Content Registration Notification

<a name='M-RecordPoint-Connectors-SDK-Notifications-Handlers-ContentRegistrationHandler-#ctor-RecordPoint-Connectors-SDK-Connectors-IConnectorConfigurationManager,RecordPoint-Connectors-SDK-Work-IManagedWorkFactory,RecordPoint-Connectors-SDK-ContentManager-IContentRegistrationRequestAction-'></a>
### #ctor(connectorManager,managedWorkFactory,contentRegistrationRequestAction) `constructor`

##### Summary



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| connectorManager | [RecordPoint.Connectors.SDK.Connectors.IConnectorConfigurationManager](#T-RecordPoint-Connectors-SDK-Connectors-IConnectorConfigurationManager 'RecordPoint.Connectors.SDK.Connectors.IConnectorConfigurationManager') |  |
| managedWorkFactory | [RecordPoint.Connectors.SDK.Work.IManagedWorkFactory](#T-RecordPoint-Connectors-SDK-Work-IManagedWorkFactory 'RecordPoint.Connectors.SDK.Work.IManagedWorkFactory') |  |
| contentRegistrationRequestAction | [RecordPoint.Connectors.SDK.ContentManager.IContentRegistrationRequestAction](#T-RecordPoint-Connectors-SDK-ContentManager-IContentRegistrationRequestAction 'RecordPoint.Connectors.SDK.ContentManager.IContentRegistrationRequestAction') |  |

<a name='F-RecordPoint-Connectors-SDK-Notifications-Handlers-ContentRegistrationHandler-CONTENT_REGISTRATION_NOTIFICATION_TYPE'></a>
### CONTENT_REGISTRATION_NOTIFICATION_TYPE `constants`

##### Summary

Content Registration Notification Type

<a name='P-RecordPoint-Connectors-SDK-Notifications-Handlers-ContentRegistrationHandler-NotificationType'></a>
### NotificationType `property`

##### Summary

Content Registration Notification Type

<a name='M-RecordPoint-Connectors-SDK-Notifications-Handlers-ContentRegistrationHandler-HandleNotificationAsync-RecordPoint-Connectors-SDK-Client-Models-ConnectorNotificationModel,System-Threading-CancellationToken-'></a>
### HandleNotificationAsync(notification,cancellationToken) `method`

##### Summary

Handle the Content Registration Notification

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| notification | [RecordPoint.Connectors.SDK.Client.Models.ConnectorNotificationModel](#T-RecordPoint-Connectors-SDK-Client-Models-ConnectorNotificationModel 'RecordPoint.Connectors.SDK.Client.Models.ConnectorNotificationModel') |  |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') |  |

<a name='T-RecordPoint-Connectors-SDK-Notifications-Handlers-Extensions'></a>
## Extensions `type`

##### Namespace

RecordPoint.Connectors.SDK.Notifications.Handlers

##### Summary

Extension methods for deserializing the context of a notification.

<a name='M-RecordPoint-Connectors-SDK-Notifications-Handlers-Extensions-ContextToList``1-System-Object-'></a>
### ContextToList\`\`1(context) `method`

##### Summary

Returns a list of deserialized objects from the context of a notification.

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| context | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') |  |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T |  |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [System.ArgumentException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.ArgumentException 'System.ArgumentException') |  |

<a name='M-RecordPoint-Connectors-SDK-Notifications-Handlers-Extensions-ContextToObject``1-System-Object-'></a>
### ContextToObject\`\`1(context) `method`

##### Summary

Returns a deserialized object from the context of a notification.

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| context | [System.Object](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Object 'System.Object') |  |

##### Generic Types

| Name | Description |
| ---- | ----------- |
| T |  |

##### Exceptions

| Name | Description |
| ---- | ----------- |
| [System.ArgumentException](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.ArgumentException 'System.ArgumentException') |  |

<a name='T-RecordPoint-Connectors-SDK-Notifications-Handlers-IConnectorSecretDecryptor'></a>
## IConnectorSecretDecryptor `type`

##### Namespace

RecordPoint.Connectors.SDK.Notifications.Handlers

##### Summary

Decrypts platform secrets, whether they arrive on a notification or a request.

<a name='M-RecordPoint-Connectors-SDK-Notifications-Handlers-IConnectorSecretDecryptor-DecryptInPlace-System-Collections-Generic-IList{RecordPoint-Connectors-SDK-Abstractions-Content-ConnectorSecret},RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel-'></a>
### DecryptInPlace(secrets,connectorConfig) `method`

##### Summary

Replaces each secret's value with its decrypted form, in place.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| secrets | [System.Collections.Generic.IList{RecordPoint.Connectors.SDK.Abstractions.Content.ConnectorSecret}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IList 'System.Collections.Generic.IList{RecordPoint.Connectors.SDK.Abstractions.Content.ConnectorSecret}') | The secrets to decrypt. Null or empty is a no-op. |
| connectorConfig | [RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel](#T-RecordPoint-Connectors-SDK-Client-Models-ConnectorConfigModel 'RecordPoint.Connectors.SDK.Client.Models.ConnectorConfigModel') | The configuration whose credentials supply the key. |

<a name='T-RecordPoint-Connectors-SDK-Client-IR365NotificationClient'></a>
## IR365NotificationClient `type`

##### Namespace

RecordPoint.Connectors.SDK.Client

##### Summary

Provides access to R365's Notification APIs.

<a name='M-RecordPoint-Connectors-SDK-Client-IR365NotificationClient-AcknowledgeNotificationAsync-RecordPoint-Connectors-SDK-Client-Models-ConnectorNotificationModel,RecordPoint-Connectors-SDK-Client-ProcessingResult,System-String,System-Threading-CancellationToken-'></a>
### AcknowledgeNotificationAsync(notification,result,message,cancellationToken) `method`

##### Summary

Acknowledges a Notification from R365

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| notification | [RecordPoint.Connectors.SDK.Client.Models.ConnectorNotificationModel](#T-RecordPoint-Connectors-SDK-Client-Models-ConnectorNotificationModel 'RecordPoint.Connectors.SDK.Client.Models.ConnectorNotificationModel') |  |
| result | [RecordPoint.Connectors.SDK.Client.ProcessingResult](#T-RecordPoint-Connectors-SDK-Client-ProcessingResult 'RecordPoint.Connectors.SDK.Client.ProcessingResult') |  |
| message | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') |  |

<a name='M-RecordPoint-Connectors-SDK-Client-IR365NotificationClient-GetAllPendingNotifications-System-Threading-CancellationToken-'></a>
### GetAllPendingNotifications(cancellationToken) `method`

##### Summary

Get Pending Notifications. Only supported for On-prem version. Cloud versions should use a webhook.

##### Returns

Returns all the pull notifications for a configured connector types for a tenant

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') |  |

<a name='M-RecordPoint-Connectors-SDK-Client-IR365NotificationClient-IsConfigured'></a>
### IsConfigured() `method`

##### Summary

Is Records 365 Configured?

##### Returns

True if records 365 has been configured and we should be able to invoke it, otherwise false

##### Parameters

This method has no parameters.

##### Remarks

Note that the client can be "ready" (All its dependant services are ready) but the configuration not yet loaded.

<a name='T-RecordPoint-Connectors-SDK-Notifications-Handlers-ItemDestroyedHandler'></a>
## ItemDestroyedHandler `type`

##### Namespace

RecordPoint.Connectors.SDK.Notifications.Handlers

##### Summary

The item destroyed handler.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| connectorManager | [T:RecordPoint.Connectors.SDK.Notifications.Handlers.ItemDestroyedHandler](#T-T-RecordPoint-Connectors-SDK-Notifications-Handlers-ItemDestroyedHandler 'T:RecordPoint.Connectors.SDK.Notifications.Handlers.ItemDestroyedHandler') | The connector manager. |

##### Remarks

Initializes a new instance of the [ItemDestroyedHandler](#T-RecordPoint-Connectors-SDK-Notifications-Handlers-ItemDestroyedHandler 'RecordPoint.Connectors.SDK.Notifications.Handlers.ItemDestroyedHandler') class.

<a name='M-RecordPoint-Connectors-SDK-Notifications-Handlers-ItemDestroyedHandler-#ctor-RecordPoint-Connectors-SDK-Connectors-IConnectorConfigurationManager,RecordPoint-Connectors-SDK-Work-IWorkQueueClient-'></a>
### #ctor(connectorManager,workQueueClient) `constructor`

##### Summary

The item destroyed handler.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| connectorManager | [RecordPoint.Connectors.SDK.Connectors.IConnectorConfigurationManager](#T-RecordPoint-Connectors-SDK-Connectors-IConnectorConfigurationManager 'RecordPoint.Connectors.SDK.Connectors.IConnectorConfigurationManager') | The connector manager. |
| workQueueClient | [RecordPoint.Connectors.SDK.Work.IWorkQueueClient](#T-RecordPoint-Connectors-SDK-Work-IWorkQueueClient 'RecordPoint.Connectors.SDK.Work.IWorkQueueClient') | The work queue client. |

##### Remarks

Initializes a new instance of the [ItemDestroyedHandler](#T-RecordPoint-Connectors-SDK-Notifications-Handlers-ItemDestroyedHandler 'RecordPoint.Connectors.SDK.Notifications.Handlers.ItemDestroyedHandler') class.

<a name='F-RecordPoint-Connectors-SDK-Notifications-Handlers-ItemDestroyedHandler-ITEM_DESTROYED_NOTIFICATION_TYPE'></a>
### ITEM_DESTROYED_NOTIFICATION_TYPE `constants`

##### Summary

The ITEM DESTROYED NOTIFICATION TYPE.

<a name='P-RecordPoint-Connectors-SDK-Notifications-Handlers-ItemDestroyedHandler-NotificationType'></a>
### NotificationType `property`

##### Summary

Gets the notification type.

<a name='M-RecordPoint-Connectors-SDK-Notifications-Handlers-ItemDestroyedHandler-HandleNotificationAsync-RecordPoint-Connectors-SDK-Client-Models-ConnectorNotificationModel,System-Threading-CancellationToken-'></a>
### HandleNotificationAsync(notification,cancellationToken) `method`

##### Summary

Handle the notification asynchronously.

##### Returns

Task<NotificationOutcome>

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| notification | [RecordPoint.Connectors.SDK.Client.Models.ConnectorNotificationModel](#T-RecordPoint-Connectors-SDK-Client-Models-ConnectorNotificationModel 'RecordPoint.Connectors.SDK.Client.Models.ConnectorNotificationModel') | The notification. |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token. |

<a name='M-RecordPoint-Connectors-SDK-Notifications-Handlers-ItemDestroyedHandler-ParseDateTime-System-DateTime-'></a>
### ParseDateTime(dateTime) `method`

##### Summary

Converts a DateTime to a DateTimeOffset
If the input value is null, it will return a new instance of a DateTimeOffset

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| dateTime | [System.DateTime](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.DateTime 'System.DateTime') |  |

<a name='M-RecordPoint-Connectors-SDK-Notifications-Handlers-ItemDestroyedHandler-ParseMetaDataModels-System-Collections-Generic-IList{RecordPoint-Connectors-SDK-Client-Models-MetaDataModel}-'></a>
### ParseMetaDataModels(metaDataModels) `method`

##### Summary

Converts a List of MetaDataModels to a List of MetaDataItems

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| metaDataModels | [System.Collections.Generic.IList{RecordPoint.Connectors.SDK.Client.Models.MetaDataModel}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IList 'System.Collections.Generic.IList{RecordPoint.Connectors.SDK.Client.Models.MetaDataModel}') |  |

<a name='T-RecordPoint-Connectors-SDK-Notifications-NotificationHandler'></a>
## NotificationHandler `type`

##### Namespace

RecordPoint.Connectors.SDK.Notifications

##### Summary

Base class for notification handlers

##### Remarks

Provides logging, metrics and error handling for notification handlers

<a name='P-RecordPoint-Connectors-SDK-Notifications-NotificationHandler-NotificationType'></a>
### NotificationType `property`

##### Summary



<a name='M-RecordPoint-Connectors-SDK-Notifications-NotificationHandler-HandleNotificationAsync-RecordPoint-Connectors-SDK-Client-Models-ConnectorNotificationModel,System-Threading-CancellationToken-'></a>
### HandleNotificationAsync(notification,cancellationToken) `method`

##### Summary



##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| notification | [RecordPoint.Connectors.SDK.Client.Models.ConnectorNotificationModel](#T-RecordPoint-Connectors-SDK-Client-Models-ConnectorNotificationModel 'RecordPoint.Connectors.SDK.Client.Models.ConnectorNotificationModel') |  |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') |  |

<a name='T-RecordPoint-Connectors-SDK-Notifications-NotificationManager'></a>
## NotificationManager `type`

##### Namespace

RecordPoint.Connectors.SDK.Notifications

##### Summary

Notifications manager that switches on the WorkType and kicks off the correct work item

<a name='M-RecordPoint-Connectors-SDK-Notifications-NotificationManager-#ctor-System-Collections-Generic-IEnumerable{RecordPoint-Connectors-SDK-Notifications-INotificationStrategy},RecordPoint-Connectors-SDK-Observability-IObservabilityScope,RecordPoint-Connectors-SDK-Observability-ITelemetryTracker-'></a>
### #ctor(notificationStrategies,observabilityScope,telemetryTracker) `constructor`

##### Summary



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| notificationStrategies | [System.Collections.Generic.IEnumerable{RecordPoint.Connectors.SDK.Notifications.INotificationStrategy}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IEnumerable 'System.Collections.Generic.IEnumerable{RecordPoint.Connectors.SDK.Notifications.INotificationStrategy}') |  |
| observabilityScope | [RecordPoint.Connectors.SDK.Observability.IObservabilityScope](#T-RecordPoint-Connectors-SDK-Observability-IObservabilityScope 'RecordPoint.Connectors.SDK.Observability.IObservabilityScope') |  |
| telemetryTracker | [RecordPoint.Connectors.SDK.Observability.ITelemetryTracker](#T-RecordPoint-Connectors-SDK-Observability-ITelemetryTracker 'RecordPoint.Connectors.SDK.Observability.ITelemetryTracker') |  |

<a name='F-RecordPoint-Connectors-SDK-Notifications-NotificationManager-_notificationStrategies'></a>
### _notificationStrategies `constants`

##### Summary



<a name='F-RecordPoint-Connectors-SDK-Notifications-NotificationManager-_observabilityScope'></a>
### _observabilityScope `constants`

##### Summary



<a name='F-RecordPoint-Connectors-SDK-Notifications-NotificationManager-_telemetryTracker'></a>
### _telemetryTracker `constants`

##### Summary



<a name='M-RecordPoint-Connectors-SDK-Notifications-NotificationManager-GetDimensions-RecordPoint-Connectors-SDK-Client-Models-ConnectorNotificationModel-'></a>
### GetDimensions(notification) `method`

##### Summary

Get observability dimensions for the notification

##### Returns

Key properties

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| notification | [RecordPoint.Connectors.SDK.Client.Models.ConnectorNotificationModel](#T-RecordPoint-Connectors-SDK-Client-Models-ConnectorNotificationModel 'RecordPoint.Connectors.SDK.Client.Models.ConnectorNotificationModel') | Notification to get key properties for |

<a name='M-RecordPoint-Connectors-SDK-Notifications-NotificationManager-GetOutcomeDimensions-RecordPoint-Connectors-SDK-Notifications-NotificationOutcome-'></a>
### GetOutcomeDimensions(outcome) `method`

##### Summary

Get observability outcome dimensions

##### Returns

Key properties

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| outcome | [RecordPoint.Connectors.SDK.Notifications.NotificationOutcome](#T-RecordPoint-Connectors-SDK-Notifications-NotificationOutcome 'RecordPoint.Connectors.SDK.Notifications.NotificationOutcome') | Notification to get key properties for |

<a name='M-RecordPoint-Connectors-SDK-Notifications-NotificationManager-HandleNotificationAsync-RecordPoint-Connectors-SDK-Client-Models-ConnectorNotificationModel,System-Threading-CancellationToken-'></a>
### HandleNotificationAsync(notification,cancellationToken) `method`

##### Summary



##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| notification | [RecordPoint.Connectors.SDK.Client.Models.ConnectorNotificationModel](#T-RecordPoint-Connectors-SDK-Client-Models-ConnectorNotificationModel 'RecordPoint.Connectors.SDK.Client.Models.ConnectorNotificationModel') |  |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') |  |

<a name='T-RecordPoint-Connectors-SDK-Notifications-NotificationPollService'></a>
## NotificationPollService `type`

##### Namespace

RecordPoint.Connectors.SDK.Notifications

##### Summary

Service that regularily polls records 365 for notifications

<a name='M-RecordPoint-Connectors-SDK-Notifications-NotificationPollService-#ctor-RecordPoint-Connectors-SDK-Context-ISystemContext,Microsoft-Extensions-Options-IOptions{RecordPoint-Connectors-SDK-Notifications-NotificationsPollerOptions},RecordPoint-Connectors-SDK-Observability-IObservabilityScope,RecordPoint-Connectors-SDK-Observability-ITelemetryTracker,System-IServiceProvider-'></a>
### #ctor() `constructor`

##### Summary



##### Parameters

This constructor has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Notifications-NotificationPollService-ExecuteAsync-System-Threading-CancellationToken-'></a>
### ExecuteAsync(stoppingToken) `method`

##### Summary



##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| stoppingToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') |  |

<a name='T-RecordPoint-Connectors-SDK-Notifications-NotificationsBuilderExtensions'></a>
## NotificationsBuilderExtensions `type`

##### Namespace

RecordPoint.Connectors.SDK.Notifications

##### Summary

Notifications host builder extensions

<a name='M-RecordPoint-Connectors-SDK-Notifications-NotificationsBuilderExtensions-UseAsyncNotificationOperation-Microsoft-Extensions-Hosting-IHostBuilder-'></a>
### UseAsyncNotificationOperation(hostBuilder) `method`

##### Summary

Use asynchronous notification processing operation.

##### Returns

An IHostBuilder

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| hostBuilder | [Microsoft.Extensions.Hosting.IHostBuilder](#T-Microsoft-Extensions-Hosting-IHostBuilder 'Microsoft.Extensions.Hosting.IHostBuilder') | The host builder. |

<a name='M-RecordPoint-Connectors-SDK-Notifications-NotificationsBuilderExtensions-UseNotifications-Microsoft-Extensions-Hosting-IHostBuilder-'></a>
### UseNotifications() `method`

##### Summary

Registers the notifications components for the Connector.
An environment variable 'UsePolledNotifications' can be set with a value of 'true' to enable Poll based notifications
Otherwise the Webhook based notifications will be registered.

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Notifications-NotificationsBuilderExtensions-UseNotifications``1-Microsoft-Extensions-Hosting-IHostBuilder-'></a>
### UseNotifications\`\`1() `method`

##### Summary

Registers the notifications components for the Connector.
An environment variable 'UsePolledNotifications' can be set with a value of 'true' to enable Poll based notifications
Otherwise the Webhook based notifications will be registered.

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Notifications-NotificationsBuilderExtensions-UseNotifications``2-Microsoft-Extensions-Hosting-IHostBuilder-'></a>
### UseNotifications\`\`2() `method`

##### Summary

Registers the notifications components for the Connector.
An environment variable 'UsePolledNotifications' can be set with a value of 'true' to enable Poll based notifications
Otherwise the Webhook based notifications will be registered.

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Notifications-NotificationsBuilderExtensions-UsePolledNotifications-Microsoft-Extensions-Hosting-IHostBuilder-'></a>
### UsePolledNotifications(hostBuilder) `method`

##### Summary

Configure the host to use polled notifications

##### Returns

Configured host builder

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| hostBuilder | [Microsoft.Extensions.Hosting.IHostBuilder](#T-Microsoft-Extensions-Hosting-IHostBuilder 'Microsoft.Extensions.Hosting.IHostBuilder') | Host builder to configure |

<a name='M-RecordPoint-Connectors-SDK-Notifications-NotificationsBuilderExtensions-UsePolledNotifications``1-Microsoft-Extensions-Hosting-IHostBuilder-'></a>
### UsePolledNotifications\`\`1(hostBuilder) `method`

##### Summary

Configure the host to use polled notifications

##### Returns

Configured host builder

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| hostBuilder | [Microsoft.Extensions.Hosting.IHostBuilder](#T-Microsoft-Extensions-Hosting-IHostBuilder 'Microsoft.Extensions.Hosting.IHostBuilder') | Host builder to configure |

<a name='M-RecordPoint-Connectors-SDK-Notifications-NotificationsBuilderExtensions-UsePolledNotifications``2-Microsoft-Extensions-Hosting-IHostBuilder-'></a>
### UsePolledNotifications\`\`2(hostBuilder) `method`

##### Summary

Configure the host to use polled notifications

##### Returns

Configured host builder

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| hostBuilder | [Microsoft.Extensions.Hosting.IHostBuilder](#T-Microsoft-Extensions-Hosting-IHostBuilder 'Microsoft.Extensions.Hosting.IHostBuilder') | Host builder to configure |

<a name='M-RecordPoint-Connectors-SDK-Notifications-NotificationsBuilderExtensions-UseWebhookNotifications-Microsoft-Extensions-Hosting-IHostBuilder-'></a>
### UseWebhookNotifications(hostBuilder) `method`

##### Summary

Configure the host to use Webhook based notifications

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| hostBuilder | [Microsoft.Extensions.Hosting.IHostBuilder](#T-Microsoft-Extensions-Hosting-IHostBuilder 'Microsoft.Extensions.Hosting.IHostBuilder') | Host builder to configure |

<a name='M-RecordPoint-Connectors-SDK-Notifications-NotificationsBuilderExtensions-UseWebhookNotifications``1-Microsoft-Extensions-Hosting-IHostBuilder-'></a>
### UseWebhookNotifications\`\`1(hostBuilder) `method`

##### Summary

Configure the host to use Webhook based notifications

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| hostBuilder | [Microsoft.Extensions.Hosting.IHostBuilder](#T-Microsoft-Extensions-Hosting-IHostBuilder 'Microsoft.Extensions.Hosting.IHostBuilder') | Host builder to configure |

<a name='M-RecordPoint-Connectors-SDK-Notifications-NotificationsBuilderExtensions-UseWebhookNotifications``2-Microsoft-Extensions-Hosting-IHostBuilder-'></a>
### UseWebhookNotifications\`\`2(hostBuilder) `method`

##### Summary

Configure the host to use Webhook based notifications

##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| hostBuilder | [Microsoft.Extensions.Hosting.IHostBuilder](#T-Microsoft-Extensions-Hosting-IHostBuilder 'Microsoft.Extensions.Hosting.IHostBuilder') | Host builder to configure |

<a name='T-RecordPoint-Connectors-SDK-Notifications-Handlers-PingHandler'></a>
## PingHandler `type`

##### Namespace

RecordPoint.Connectors.SDK.Notifications.Handlers

##### Summary

The ping handler.

<a name='M-RecordPoint-Connectors-SDK-Notifications-Handlers-PingHandler-#ctor-RecordPoint-Connectors-SDK-Connectors-IConnectorConfigurationManager-'></a>
### #ctor(connectorClient) `constructor`

##### Summary

Initializes a new instance of the [PingHandler](#T-RecordPoint-Connectors-SDK-Notifications-Handlers-PingHandler 'RecordPoint.Connectors.SDK.Notifications.Handlers.PingHandler') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| connectorClient | [RecordPoint.Connectors.SDK.Connectors.IConnectorConfigurationManager](#T-RecordPoint-Connectors-SDK-Connectors-IConnectorConfigurationManager 'RecordPoint.Connectors.SDK.Connectors.IConnectorConfigurationManager') | The connector client. |

<a name='F-RecordPoint-Connectors-SDK-Notifications-Handlers-PingHandler-PING_NOTIFICATION_TYPE'></a>
### PING_NOTIFICATION_TYPE `constants`

##### Summary

The PING NOTIFICATION TYPE.

<a name='P-RecordPoint-Connectors-SDK-Notifications-Handlers-PingHandler-NotificationType'></a>
### NotificationType `property`

##### Summary

Gets the notification type.

<a name='M-RecordPoint-Connectors-SDK-Notifications-Handlers-PingHandler-HandleNotificationAsync-RecordPoint-Connectors-SDK-Client-Models-ConnectorNotificationModel,System-Threading-CancellationToken-'></a>
### HandleNotificationAsync(notification,cancellationToken) `method`

##### Summary

Handle the notification asynchronously.

##### Returns

Task<NotificationOutcome>

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| notification | [RecordPoint.Connectors.SDK.Client.Models.ConnectorNotificationModel](#T-RecordPoint-Connectors-SDK-Client-Models-ConnectorNotificationModel 'RecordPoint.Connectors.SDK.Client.Models.ConnectorNotificationModel') | The notification. |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token. |

<a name='T-RecordPoint-Connectors-SDK-Notifications-PollNotificationsOperation'></a>
## PollNotificationsOperation `type`

##### Namespace

RecordPoint.Connectors.SDK.Notifications

##### Summary

The poll (pull) notifications operation.

<a name='M-RecordPoint-Connectors-SDK-Notifications-PollNotificationsOperation-#ctor-System-IServiceProvider,RecordPoint-Connectors-SDK-Notifications-INotificationManager,RecordPoint-Connectors-SDK-Client-IR365NotificationClient,RecordPoint-Connectors-SDK-Observability-IObservabilityScope,RecordPoint-Connectors-SDK-Observability-ITelemetryTracker,RecordPoint-Connectors-SDK-Providers-IDateTimeProvider-'></a>
### #ctor(serviceProvider,notificationManager,r365NotificationClient,observabilityScope,telemetryTracker,dateTimeProvider) `constructor`

##### Summary

Initializes a new instance of the [PollNotificationsOperation](#T-RecordPoint-Connectors-SDK-Notifications-PollNotificationsOperation 'RecordPoint.Connectors.SDK.Notifications.PollNotificationsOperation') class.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| serviceProvider | [System.IServiceProvider](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.IServiceProvider 'System.IServiceProvider') | The service provider. |
| notificationManager | [RecordPoint.Connectors.SDK.Notifications.INotificationManager](#T-RecordPoint-Connectors-SDK-Notifications-INotificationManager 'RecordPoint.Connectors.SDK.Notifications.INotificationManager') | The notification manager. |
| r365NotificationClient | [RecordPoint.Connectors.SDK.Client.IR365NotificationClient](#T-RecordPoint-Connectors-SDK-Client-IR365NotificationClient 'RecordPoint.Connectors.SDK.Client.IR365NotificationClient') | The r365 notification client. |
| observabilityScope | [RecordPoint.Connectors.SDK.Observability.IObservabilityScope](#T-RecordPoint-Connectors-SDK-Observability-IObservabilityScope 'RecordPoint.Connectors.SDK.Observability.IObservabilityScope') | The scope manager. |
| telemetryTracker | [RecordPoint.Connectors.SDK.Observability.ITelemetryTracker](#T-RecordPoint-Connectors-SDK-Observability-ITelemetryTracker 'RecordPoint.Connectors.SDK.Observability.ITelemetryTracker') | The telemetry tracker. |
| dateTimeProvider | [RecordPoint.Connectors.SDK.Providers.IDateTimeProvider](#T-RecordPoint-Connectors-SDK-Providers-IDateTimeProvider 'RecordPoint.Connectors.SDK.Providers.IDateTimeProvider') | The date time provider. |

<a name='F-RecordPoint-Connectors-SDK-Notifications-PollNotificationsOperation-POLL_WORK_TYPE'></a>
### POLL_WORK_TYPE `constants`

##### Summary

The POLL WORK TYPE.

<a name='F-RecordPoint-Connectors-SDK-Notifications-PollNotificationsOperation-_notificationManager'></a>
### _notificationManager `constants`

##### Summary

The notification manager.

<a name='F-RecordPoint-Connectors-SDK-Notifications-PollNotificationsOperation-_r365NotificationClient'></a>
### _r365NotificationClient `constants`

##### Summary

The r365 notification client.

<a name='P-RecordPoint-Connectors-SDK-Notifications-PollNotificationsOperation-NotificationCount'></a>
### NotificationCount `property`

##### Summary

Gets the notification count.

<a name='P-RecordPoint-Connectors-SDK-Notifications-PollNotificationsOperation-WorkType'></a>
### WorkType `property`

##### Summary

Gets the work type.

<a name='M-RecordPoint-Connectors-SDK-Notifications-PollNotificationsOperation-GetCustomResultMeasures'></a>
### GetCustomResultMeasures() `method`

##### Summary

Get custom result measures.

##### Returns

A Measures

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Notifications-PollNotificationsOperation-InnerRunAsync-System-Threading-CancellationToken-'></a>
### InnerRunAsync(cancellationToken) `method`

##### Summary

Inner the run asynchronously.

##### Returns

A Task

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token. |

<a name='T-RecordPoint-Connectors-SDK-Notifications-PullNotificationManager'></a>
## PullNotificationManager `type`

##### Namespace

RecordPoint.Connectors.SDK.Notifications

##### Summary

Notifications manager that switches on the WorkType and kicks off the correct work item

<a name='M-RecordPoint-Connectors-SDK-Notifications-PullNotificationManager-#ctor-RecordPoint-Connectors-SDK-Client-IR365NotificationClient,System-Collections-Generic-IEnumerable{RecordPoint-Connectors-SDK-Notifications-INotificationStrategy},RecordPoint-Connectors-SDK-Observability-IObservabilityScope,RecordPoint-Connectors-SDK-Observability-ITelemetryTracker-'></a>
### #ctor(r365NotificationClient,notificationStrategies,observabilityScope,telemetryTracker) `constructor`

##### Summary



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| r365NotificationClient | [RecordPoint.Connectors.SDK.Client.IR365NotificationClient](#T-RecordPoint-Connectors-SDK-Client-IR365NotificationClient 'RecordPoint.Connectors.SDK.Client.IR365NotificationClient') |  |
| notificationStrategies | [System.Collections.Generic.IEnumerable{RecordPoint.Connectors.SDK.Notifications.INotificationStrategy}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IEnumerable 'System.Collections.Generic.IEnumerable{RecordPoint.Connectors.SDK.Notifications.INotificationStrategy}') |  |
| observabilityScope | [RecordPoint.Connectors.SDK.Observability.IObservabilityScope](#T-RecordPoint-Connectors-SDK-Observability-IObservabilityScope 'RecordPoint.Connectors.SDK.Observability.IObservabilityScope') |  |
| telemetryTracker | [RecordPoint.Connectors.SDK.Observability.ITelemetryTracker](#T-RecordPoint-Connectors-SDK-Observability-ITelemetryTracker 'RecordPoint.Connectors.SDK.Observability.ITelemetryTracker') |  |

<a name='F-RecordPoint-Connectors-SDK-Notifications-PullNotificationManager-_r365NotificationClient'></a>
### _r365NotificationClient `constants`

##### Summary



<a name='M-RecordPoint-Connectors-SDK-Notifications-PullNotificationManager-HandleNotificationAsync-RecordPoint-Connectors-SDK-Client-Models-ConnectorNotificationModel,System-Threading-CancellationToken-'></a>
### HandleNotificationAsync(notification,cancellationToken) `method`

##### Summary



##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| notification | [RecordPoint.Connectors.SDK.Client.Models.ConnectorNotificationModel](#T-RecordPoint-Connectors-SDK-Client-Models-ConnectorNotificationModel 'RecordPoint.Connectors.SDK.Client.Models.ConnectorNotificationModel') |  |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') |  |

<a name='T-RecordPoint-Connectors-SDK-Notifications-PushNotificationManager'></a>
## PushNotificationManager `type`

##### Namespace

RecordPoint.Connectors.SDK.Notifications

##### Summary

Notifications manager that switches on the WorkType and kicks off the correct work item

<a name='M-RecordPoint-Connectors-SDK-Notifications-PushNotificationManager-#ctor-System-Collections-Generic-IEnumerable{RecordPoint-Connectors-SDK-Notifications-INotificationStrategy},RecordPoint-Connectors-SDK-Observability-IObservabilityScope,RecordPoint-Connectors-SDK-Observability-ITelemetryTracker-'></a>
### #ctor(notificationStrategies,observabilityScope,telemetryTracker) `constructor`

##### Summary



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| notificationStrategies | [System.Collections.Generic.IEnumerable{RecordPoint.Connectors.SDK.Notifications.INotificationStrategy}](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Collections.Generic.IEnumerable 'System.Collections.Generic.IEnumerable{RecordPoint.Connectors.SDK.Notifications.INotificationStrategy}') |  |
| observabilityScope | [RecordPoint.Connectors.SDK.Observability.IObservabilityScope](#T-RecordPoint-Connectors-SDK-Observability-IObservabilityScope 'RecordPoint.Connectors.SDK.Observability.IObservabilityScope') |  |
| telemetryTracker | [RecordPoint.Connectors.SDK.Observability.ITelemetryTracker](#T-RecordPoint-Connectors-SDK-Observability-ITelemetryTracker 'RecordPoint.Connectors.SDK.Observability.ITelemetryTracker') |  |

<a name='M-RecordPoint-Connectors-SDK-Notifications-PushNotificationManager-HandleNotificationAsync-RecordPoint-Connectors-SDK-Client-Models-ConnectorNotificationModel,System-Threading-CancellationToken-'></a>
### HandleNotificationAsync(notification,cancellationToken) `method`

##### Summary



##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| notification | [RecordPoint.Connectors.SDK.Client.Models.ConnectorNotificationModel](#T-RecordPoint-Connectors-SDK-Client-Models-ConnectorNotificationModel 'RecordPoint.Connectors.SDK.Client.Models.ConnectorNotificationModel') |  |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') |  |

<a name='T-RecordPoint-Connectors-SDK-Notifications-R365NotificationClient'></a>
## R365NotificationClient `type`

##### Namespace

RecordPoint.Connectors.SDK.Notifications

##### Summary

R365 standard Notifications client.

<a name='M-RecordPoint-Connectors-SDK-Notifications-R365NotificationClient-#ctor-RecordPoint-Connectors-SDK-Configuration-IR365ConfigurationClient,RecordPoint-Connectors-SDK-Observability-IObservabilityScope,RecordPoint-Connectors-SDK-Observability-ITelemetryTracker,Microsoft-Extensions-Options-IOptions{RecordPoint-Connectors-SDK-Notifications-NotificationsPollerOptions},RecordPoint-Connectors-SDK-Notifications-INotificationApiManager-'></a>
### #ctor() `constructor`

##### Summary



##### Parameters

This constructor has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Notifications-R365NotificationClient-AcknowledgeNotificationAsync-RecordPoint-Connectors-SDK-Client-Models-ConnectorNotificationModel,RecordPoint-Connectors-SDK-Client-ProcessingResult,System-String,System-Threading-CancellationToken-'></a>
### AcknowledgeNotificationAsync(notification,result,message,cancellationToken) `method`

##### Summary



##### Returns



##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| notification | [RecordPoint.Connectors.SDK.Client.Models.ConnectorNotificationModel](#T-RecordPoint-Connectors-SDK-Client-Models-ConnectorNotificationModel 'RecordPoint.Connectors.SDK.Client.Models.ConnectorNotificationModel') |  |
| result | [RecordPoint.Connectors.SDK.Client.ProcessingResult](#T-RecordPoint-Connectors-SDK-Client-ProcessingResult 'RecordPoint.Connectors.SDK.Client.ProcessingResult') |  |
| message | [System.String](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.String 'System.String') |  |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') |  |

<a name='M-RecordPoint-Connectors-SDK-Notifications-R365NotificationClient-GetAllPendingNotifications-System-Threading-CancellationToken-'></a>
### GetAllPendingNotifications(cancellationToken) `method`

##### Summary

Returns all the pull notifications for a configured connector types for a tenant

##### Returns

List>

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') |  |

<a name='M-RecordPoint-Connectors-SDK-Notifications-R365NotificationClient-IsConfigured'></a>
### IsConfigured() `method`

##### Summary



##### Returns



##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Notifications-R365NotificationClient-LoadConfiguration-System-String-'></a>
### LoadConfiguration() `method`

##### Summary

Ensure configuration is loaded

##### Parameters

This method has no parameters.

<a name='T-RecordPoint-Connectors-SDK-Notifications-Webhook-WebhookOperation'></a>
## WebhookOperation `type`

##### Namespace

RecordPoint.Connectors.SDK.Notifications.Webhook

##### Summary

The webhook operation.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| serviceProvider | [T:RecordPoint.Connectors.SDK.Notifications.Webhook.WebhookOperation](#T-T-RecordPoint-Connectors-SDK-Notifications-Webhook-WebhookOperation 'T:RecordPoint.Connectors.SDK.Notifications.Webhook.WebhookOperation') | The service provider. |

##### Remarks

Initializes a new instance of the [WebhookOperation](#T-RecordPoint-Connectors-SDK-Notifications-Webhook-WebhookOperation 'RecordPoint.Connectors.SDK.Notifications.Webhook.WebhookOperation') class.

<a name='M-RecordPoint-Connectors-SDK-Notifications-Webhook-WebhookOperation-#ctor-System-IServiceProvider,RecordPoint-Connectors-SDK-Notifications-INotificationManager,RecordPoint-Connectors-SDK-Work-IWorkQueueClient,RecordPoint-Connectors-SDK-Observability-IObservabilityScope,RecordPoint-Connectors-SDK-Observability-ITelemetryTracker,RecordPoint-Connectors-SDK-Providers-IDateTimeProvider-'></a>
### #ctor(serviceProvider,notificationManager,workQueueClient,observabilityScope,telemetryTracker,dateTimeProvider) `constructor`

##### Summary

The webhook operation.

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| serviceProvider | [System.IServiceProvider](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.IServiceProvider 'System.IServiceProvider') | The service provider. |
| notificationManager | [RecordPoint.Connectors.SDK.Notifications.INotificationManager](#T-RecordPoint-Connectors-SDK-Notifications-INotificationManager 'RecordPoint.Connectors.SDK.Notifications.INotificationManager') | The notification manager. |
| workQueueClient | [RecordPoint.Connectors.SDK.Work.IWorkQueueClient](#T-RecordPoint-Connectors-SDK-Work-IWorkQueueClient 'RecordPoint.Connectors.SDK.Work.IWorkQueueClient') | The work queue client. |
| observabilityScope | [RecordPoint.Connectors.SDK.Observability.IObservabilityScope](#T-RecordPoint-Connectors-SDK-Observability-IObservabilityScope 'RecordPoint.Connectors.SDK.Observability.IObservabilityScope') | The scope manager. |
| telemetryTracker | [RecordPoint.Connectors.SDK.Observability.ITelemetryTracker](#T-RecordPoint-Connectors-SDK-Observability-ITelemetryTracker 'RecordPoint.Connectors.SDK.Observability.ITelemetryTracker') | The telemetry tracker. |
| dateTimeProvider | [RecordPoint.Connectors.SDK.Providers.IDateTimeProvider](#T-RecordPoint-Connectors-SDK-Providers-IDateTimeProvider 'RecordPoint.Connectors.SDK.Providers.IDateTimeProvider') | The date time provider. |

##### Remarks

Initializes a new instance of the [WebhookOperation](#T-RecordPoint-Connectors-SDK-Notifications-Webhook-WebhookOperation 'RecordPoint.Connectors.SDK.Notifications.Webhook.WebhookOperation') class.

<a name='F-RecordPoint-Connectors-SDK-Notifications-Webhook-WebhookOperation-WEBHOOK_WORK_TYPE'></a>
### WEBHOOK_WORK_TYPE `constants`

##### Summary

The WEBHOOK WORK TYPE.

<a name='P-RecordPoint-Connectors-SDK-Notifications-Webhook-WebhookOperation-ConnectorNotification'></a>
### ConnectorNotification `property`

##### Summary

Gets the connector notification.

<a name='P-RecordPoint-Connectors-SDK-Notifications-Webhook-WebhookOperation-WorkType'></a>
### WorkType `property`

##### Summary

Gets the work type.

<a name='M-RecordPoint-Connectors-SDK-Notifications-Webhook-WebhookOperation-GetCustomKeyDimensions'></a>
### GetCustomKeyDimensions() `method`

##### Summary

Get custom key dimensions.

##### Returns

A Dimensions

##### Parameters

This method has no parameters.

<a name='M-RecordPoint-Connectors-SDK-Notifications-Webhook-WebhookOperation-InnerRunAsync-System-Threading-CancellationToken-'></a>
### InnerRunAsync(cancellationToken) `method`

##### Summary

Inner the run asynchronously.

##### Returns

A Task

##### Parameters

| Name | Type | Description |
| ---- | ---- | ----------- |
| cancellationToken | [System.Threading.CancellationToken](http://msdn.microsoft.com/query/dev14.query?appId=Dev14IDEF1&l=EN-US&k=k:System.Threading.CancellationToken 'System.Threading.CancellationToken') | The cancellation token. |
