using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Moq;
using RecordPoint.Connectors.SDK.Caching;
using RecordPoint.Connectors.SDK.Caching.Semaphore;
using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.Connectors;
using RecordPoint.Connectors.SDK.Content;
using RecordPoint.Connectors.SDK.ContentManager;
using RecordPoint.Connectors.SDK.Databases;
using RecordPoint.Connectors.SDK.Test.Common.Mock.Caching.Semaphore;
using RecordPoint.Connectors.SDK.Test.Mock.Databases;
using RecordPoint.Connectors.SDK.Test.Mock.Work;
using RecordPoint.Connectors.SDK.Work;

namespace RecordPoint.Connectors.SDK.Test.ContentManager
{

    /// <summary>
    /// SUT base class for the content manager tests
    /// </summary>
    public class ContentManagerSutBase : CommonSutBase
    {
        protected override IHostBuilder CreateSutBuilder()
            => base
                .CreateSutBuilder()
                .UseWorkManager()
                .UseDatabaseConnectorConfigurationManager()
                .UseMockConnectorDatabase()
                .UseWorkStateManager<DatabaseManagedWorkStatusManager>()
                .UseInMemorySemaphoreLock()
                .ConfigureServices(svcs =>
                {
                    var mockContentManagerProvider = new MockContentManagerProvider(
                        ContentManagerCallbackActionFactory,
                        ChannelDiscoveryActionFactory,
                        ContentSynchronisationActionFactory,
                        ContentRegistrationActionFactory,
                        BinaryRetrievalActionFactory,
                        AggregationSubmissionCallbackActionFactory,
                        AuditEventSubmissionCallbackActionFactory,
                        RecordSubmissionCallbackActionFactory,
                        BinarySubmissionCallbackActionFactory,
                        RecordDisposalActionFactory);

                    svcs
                        .AddMockWorkQueue()
                        .AddScoped<ISemaphoreLockScopedKeyAction>(a => SemaphoreLockScopedKeyAction)
                        .AddSingleton<IContentManagerActionProvider>(mockContentManagerProvider);
                });

        public override async Task StopSUTAsync()
        {
            var databaseProvider = Services.GetRequiredService<IConnectorDatabaseProvider>();
            await databaseProvider.RemoveAsync(CancellationToken.None);
            await base.StopSUTAsync();
        }

        #region Semaphore Lock
        public MockSemaphoreLockScopedKeyAction SemaphoreLockScopedKeyAction { get; set; } = new();
        #endregion

        #region Connectors

        public const string CONNECTOR_CONFIGURATION_ID_1 = "18d41b06-20ec-4e82-aeeb-cf6d47d5c652";
        public const string CONNECTOR_TYPE_ID_1 = "18d41b06-20ec-4e82-aeeb-cf6d47d5c653";
        public const string CONNECTOR_DISPLAY_NAME_1 = "Test Connector 1";
        public const string TENANT_ID_1 = "18d41b06-20ec-4e82-aeeb-cf6d47d5c654";

        public static ConnectorConfigModel CreateConnector1() => new()
        {
            Id = CONNECTOR_CONFIGURATION_ID_1,
            ConnectorTypeId = CONNECTOR_TYPE_ID_1,
            DisplayName = CONNECTOR_DISPLAY_NAME_1,
            Status = "Enabled",
            TenantId = TENANT_ID_1
        };

        public IConnectorConfigurationManager GetConnectorManager() => Services.GetRequiredService<IConnectorConfigurationManager>();

        public static void DisableConnector(ConnectorConfigModel connectorConfig, DateTimeOffset disabledTime)
        {
            connectorConfig.Status = "Disabled";
            connectorConfig.SetProperty("DisabledTime", disabledTime.ToString("O"));
        }
        #endregion

        #region Aggregations

        public const string AGGREGATION_EXTERNAL_ID_1 = "Aggregation_1";
        public const string AGGREGATION_TITLE_1 = "Aggregation 1";

        public static AggregationModel CreateAggregation1() => new()
        {
            ExternalId = AGGREGATION_EXTERNAL_ID_1,
            Title = AGGREGATION_TITLE_1,
            ConnectorId = CONNECTOR_CONFIGURATION_ID_1
        };

        public IAggregationManager GetAggregationManager() => Services.GetRequiredService<IAggregationManager>();

        #endregion

        #region Channels

        public const string CHANNEL_EXTERNAL_ID_1 = "Channel_1";
        public const string CHANNEL_TITLE_1 = "Channel 1";

        public static ChannelModel CreateChannel1() => new()
        {
            ExternalId = CHANNEL_EXTERNAL_ID_1,
            Title = CHANNEL_TITLE_1,
            ConnectorId = CONNECTOR_CONFIGURATION_ID_1
        };

        public IChannelManager GetChannelManager() => Services.GetRequiredService<IChannelManager>();

        #endregion

        #region Work

        public IManagedWorkStatusManager GetWorkStatusManager() => Services.GetRequiredService<IManagedWorkStatusManager>();

        #endregion

        #region Content Manager Callback Action

        public Func<IContentManagerCallbackAction> ContentManagerCallbackActionFactory { get; set; }

        public void SelectContentManagerCallbackActionMock(IMock<IContentManagerCallbackAction> contentManagerCallbackAction)
        {
            ContentManagerCallbackActionFactory = () => contentManagerCallbackAction.Object;
        }

        public void SelectContentManagerCallbackAction(IContentManagerCallbackAction contentManagerCallbackAction)
        {
            ContentManagerCallbackActionFactory = () => contentManagerCallbackAction;
        }

        #endregion

        #region Channel Discovery

        public Func<IChannelDiscoveryAction> ChannelDiscoveryActionFactory { get; set; } = () => throw new NotImplementedException();

        public void SelectChannelDiscoveryActionMock(IMock<IChannelDiscoveryAction> channelDiscoveryActionMock)
        {
            ChannelDiscoveryActionFactory = () => channelDiscoveryActionMock.Object;
        }

        public void SelectChannelDiscoveryAction(IChannelDiscoveryAction channelDiscoveryAction)
        {
            ChannelDiscoveryActionFactory = () => channelDiscoveryAction;
        }

        #endregion

        #region Content Synchronisation
        public ContentSynchronisationConfiguration ContentSynchronisationConfiguration { get; set; }


        public Func<IContentSynchronisationAction> ContentSynchronisationActionFactory { get; set; }

        public void SelectContentSynchronisationActionMock(IMock<IContentSynchronisationAction> syncerMock)
        {
            ContentSynchronisationActionFactory = () => syncerMock.Object;
        }

        public void SelectContentSynchronisationAction(IContentSynchronisationAction contentSynchronisationAction)
        {
            ContentSynchronisationActionFactory = () => contentSynchronisationAction;
        }

        public static ContentSynchronisationConfiguration CreateSynchronisationConfiguration()
        {
            var syncConfiguration = new ContentSynchronisationConfiguration()
            {
                ConnectorConfigurationId = CONNECTOR_CONFIGURATION_ID_1
            };
            return syncConfiguration;
        }


        public void SelectContentSynchronisationConfiguration(ContentSynchronisationConfiguration syncContentConfiguration)
        {
            ContentSynchronisationConfiguration = syncContentConfiguration;
        }
        #endregion

        #region Content Registration
        public ContentRegistrationConfiguration ContentRegistrationConfiguration { get; set; }


        public Func<IContentRegistrationAction> ContentRegistrationActionFactory { get; set; }

        public void SelectContentRegistrationActionMock(IMock<IContentRegistrationAction> syncerMock)
        {
            ContentRegistrationActionFactory = () => syncerMock.Object;
        }

        public void SelectContentRegistrationAction(IContentRegistrationAction contentRegistrationAction)
        {
            ContentRegistrationActionFactory = () => contentRegistrationAction;
        }

        public static ContentRegistrationConfiguration CreateRegistrationConfiguration()
        {
            var syncConfiguration = new ContentRegistrationConfiguration()
            {
                ConnectorConfigurationId = CONNECTOR_CONFIGURATION_ID_1
            };
            return syncConfiguration;
        }


        public void SelectContentRegistrationConfiguration(ContentRegistrationConfiguration syncContentConfiguration)
        {
            ContentRegistrationConfiguration = syncContentConfiguration;
        }
        #endregion

        #region Binary Retrieval Action

        public Func<IBinaryRetrievalAction> BinaryRetrievalActionFactory { get; set; }

        public void SelectBinaryRetrievalActionMock(IMock<IBinaryRetrievalAction> binaryRetrievalActionMock)
        {
            BinaryRetrievalActionFactory = () => binaryRetrievalActionMock.Object;
        }

        public void SelectBinaryRetrievalAction(IBinaryRetrievalAction binaryRetrievalAction)
        {
            BinaryRetrievalActionFactory = () => binaryRetrievalAction;
        }

        #endregion

        #region Aggregation Submission Callback Action

        public Func<IAggregationSubmissionCallbackAction> AggregationSubmissionCallbackActionFactory { get; set; }

        public void SelectAggregationSubmissionCallbackActionMock(IMock<IAggregationSubmissionCallbackAction> aggregationSubmissionCallbackActionMock)
        {
            AggregationSubmissionCallbackActionFactory = () => aggregationSubmissionCallbackActionMock.Object;
        }

        public void SelectAggregationSubmissionCallbackAction(IAggregationSubmissionCallbackAction aggregationSubmissionCallbackAction)
        {
            AggregationSubmissionCallbackActionFactory = () => aggregationSubmissionCallbackAction;
        }

        #endregion

        #region Audit Event Submission Callback Action

        public Func<IAuditEventSubmissionCallbackAction> AuditEventSubmissionCallbackActionFactory { get; set; }

        public void SelectAuditEventSubmissionCallbackActionMock(IMock<IAuditEventSubmissionCallbackAction> aggregationSubmissionCallbackActionMock)
        {
            AuditEventSubmissionCallbackActionFactory = () => aggregationSubmissionCallbackActionMock.Object;
        }

        public void SelectAuditEventSubmissionCallbackAction(IAuditEventSubmissionCallbackAction aggregationSubmissionCallbackAction)
        {
            AuditEventSubmissionCallbackActionFactory = () => aggregationSubmissionCallbackAction;
        }

        #endregion

        #region Record Submission Callback Action

        public Func<IRecordSubmissionCallbackAction> RecordSubmissionCallbackActionFactory { get; set; }

        public void SelectRecordSubmissionCallbackActionMock(IMock<IRecordSubmissionCallbackAction> recordSubmissionCallbackActionMock)
        {
            RecordSubmissionCallbackActionFactory = () => recordSubmissionCallbackActionMock.Object;
        }

        public void SelectRecordSubmissionCallbackAction(IRecordSubmissionCallbackAction recordSubmissionCallbackAction)
        {
            RecordSubmissionCallbackActionFactory = () => recordSubmissionCallbackAction;
        }

        #endregion

        #region Binary Submission Callback Action

        public Func<IBinarySubmissionCallbackAction> BinarySubmissionCallbackActionFactory { get; set; }

        public void SelectBinarySubmissionCallbackActionMock(IMock<IBinarySubmissionCallbackAction> binarySubmissionCallbackActionMock)
        {
            BinarySubmissionCallbackActionFactory = () => binarySubmissionCallbackActionMock.Object;
        }

        public void SelectBinarySubmissionCallbackAction(IBinarySubmissionCallbackAction binarySubmissionCallbackAction)
        {
            BinarySubmissionCallbackActionFactory = () => binarySubmissionCallbackAction;
        }

        #endregion

        #region Record Disposal Action

        public Func<IRecordDisposalAction> RecordDisposalActionFactory { get; set; }

        public void SelectRecordDisposalActionMock(IMock<IRecordDisposalAction> RecordDisposalActionMock)
        {
            RecordDisposalActionFactory = () => RecordDisposalActionMock.Object;
        }

        public void SelectRecordDisposalAction(IRecordDisposalAction RecordDisposalAction)
        {
            RecordDisposalActionFactory = () => RecordDisposalAction;
        }

        #endregion

    }
}
