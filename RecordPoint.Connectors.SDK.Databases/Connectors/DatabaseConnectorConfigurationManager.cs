using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.Content;
using RecordPoint.Connectors.SDK.Context;
using RecordPoint.Connectors.SDK.Databases;
using RecordPoint.Connectors.SDK.Observability;
using RecordPoint.Connectors.SDK.Toggles;
using System.Text.Json;

namespace RecordPoint.Connectors.SDK.Connectors
{
    /// <summary>
    /// The database connector configuration manager.
    /// </summary>
    public class DatabaseConnectorConfigurationManager : IConnectorConfigurationManager
    {
        /// <summary>
        /// The CONNECTOR FEATURE.
        /// </summary>
        public const string CONNECTOR_FEATURE = "Connector";
        /// <summary>
        /// The SUBMISSION FEATURE.
        /// </summary>
        public const string SUBMISSION_FEATURE = "Submission";
        /// <summary>
        /// The BINARY SUBMISSION FEATURE.
        /// </summary>
        public const string BINARY_SUBMISSION_FEATURE = "Binary Submission";

        /// <summary>
        /// The CONNECTOR NOT FOUND REASON.
        /// </summary>
        public const string CONNECTOR_NOT_FOUND_REASON = "Connector not found";
        /// <summary>
        /// The CONNECTOR DISABLED REASON.
        /// </summary>
        public const string CONNECTOR_DISABLED_REASON = "Connector is not enabled";
        /// <summary>
        /// The CONNECTOR FEATURE DISABLED REASON.
        /// </summary>
        public const string CONNECTOR_FEATURE_DISABLED_REASON = "Connector feature is not enabled";
        /// <summary>
        /// The CONNECTOR ENABLED REASON.
        /// </summary>
        public const string CONNECTOR_ENABLED_REASON = "Connector enabled";

        /// <summary>
        /// The SUBMISSION APPSETTING OFF REASON.
        /// </summary>
        public const string SUBMISSION_APPSETTING_OFF_REASON = "Submission is disabled in the applications setting";
        /// <summary>
        /// The SUBMISSION KILLSWITCH ON REASON.
        /// </summary>
        public const string SUBMISSION_KILLSWITCH_ON_REASON = "Submission is disabled in launch darkly's kill switch";
        /// <summary>
        /// The SUBMISSION ENABLED REASON.
        /// </summary>
        public const string SUBMISSION_ENABLED_REASON = "Submission enabled";

        /// <summary>
        /// The BINARY APPSETTING OFF REASON.
        /// </summary>
        public const string BINARY_APPSETTING_OFF_REASON = "Binary submission is disabled in the applications setting";
        /// <summary>
        /// The BINARY SUBMISSION ENABLED REASON.
        /// </summary>
        public const string BINARY_SUBMISSION_ENABLED_REASON = "Binary submission enabled";

        /// <summary>
        /// The CONNECTOR ID DIMENSION.
        /// </summary>
        public const string CONNECTOR_ID_DIMENSION = "ConnectorId";
        /// <summary>
        /// Timestamp for when a Connector Configuration was disabled
        /// </summary>
        public const string DISABLED_TIME = "DisabledTime";

        /// <summary>
        /// The 'Enabled' state of a Connector Configuration
        /// </summary>
        private const string ENABLED = "Enabled";
        /// <summary>
        /// The 'Disabled' state of a Connector Configuration
        /// </summary>
        private const string DISABLED = "Disabled";

        /// <summary>
        /// The connector options.
        /// </summary>
        private readonly IOptions<ConnectorOptions> _connectorOptions;

        /// <summary>
        /// The database client.
        /// </summary>
        private readonly IConnectorDatabaseClient _databaseClient;
        /// <summary>
        /// The scope manager.
        /// </summary>
        private readonly IObservabilityScope _observabilityScope;
        /// <summary>
        /// The system context.
        /// </summary>
        private readonly ISystemContext _systemContext;
        /// <summary>
        /// The toggle provider.
        /// </summary>
        private readonly IToggleProvider _toggleProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseConnectorConfigurationManager"/> class.
        /// </summary>
        /// <param name="databaseClient">The database client.</param>
        /// <param name="observabilityScope">The scope manager.</param>
        /// <param name="connectorOptions">The connector options.</param>
        /// <param name="systemContext">The system context.</param>
        /// <param name="toggleProvider">The toggle provider.</param>
        public DatabaseConnectorConfigurationManager(
            IConnectorDatabaseClient databaseClient,
            IObservabilityScope observabilityScope,
            IOptions<ConnectorOptions> connectorOptions,
            ISystemContext systemContext,
            IToggleProvider toggleProvider)
        {
            _databaseClient = databaseClient;
            _observabilityScope = observabilityScope;
            _connectorOptions = connectorOptions;
            _systemContext = systemContext;
            _toggleProvider = toggleProvider;
        }

        /// <summary>
        /// Connectors configuration exists asynchronously.
        /// </summary>
        /// <param name="connectorId">The connector id.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns><![CDATA[Task<bool>]]></returns>
        public async Task<bool> ConnectorConfigurationExistsAsync(string connectorId, CancellationToken cancellationToken)
        {
            return await _observabilityScope.Invoke(GetDimensions(connectorId), async () =>
            {
                using var dbContext = _databaseClient.CreateDbContext();
                return (await dbContext.Connectors
                    .Where(a => a.ConnectorId == connectorId)
                    .ToListAsync(cancellationToken))
                    .Any();
            });
        }

        /// <summary>
        /// Get connector configuration asynchronously.
        /// </summary>
        /// <param name="connectorId">The connector id.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns><![CDATA[Task<ConnectorConfigurationModel>]]></returns>
        public async Task<ConnectorConfigurationModel> GetConnectorConfigurationAsync(string connectorId, CancellationToken cancellationToken)
        {
            return await _observabilityScope.Invoke(GetDimensions(connectorId), async () =>
            {
                using var dbContext = _databaseClient.CreateDbContext();
                return await dbContext.Connectors.FirstOrDefaultAsync(a => a.ConnectorId == connectorId, cancellationToken);
            });
        }

        /// <summary>
        /// Set connector configuration asynchronously.
        /// </summary>
        /// <param name="connectorData">The connector data.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A Task</returns>
        public async Task SetConnectorConfigurationAsync(ConnectorConfigurationModel connectorData, CancellationToken cancellationToken)
        {
            await _observabilityScope.Invoke(GetDimensions(connectorData.ConnectorId), async () =>
            {
                using var dbContext = _databaseClient.CreateDbContext();

                var existing = await dbContext.Connectors
                    .FirstOrDefaultAsync(a => a.ConnectorId == connectorData.ConnectorId, cancellationToken);

                if (existing == null)
                {
                    existing = new ConnectorConfigurationModel
                    {
                        ConnectorId = connectorData.ConnectorId,
                        ConnectorTypeId = connectorData.ConnectorTypeId,
                        TenantId = connectorData.TenantId,
                        Data = connectorData.Data
                    };
                    await dbContext.Connectors.AddAsync(existing, cancellationToken);
                }

                if (connectorData.Status == DISABLED)
                {
                    var existingConnectorConfig = JsonSerializer.Deserialize<ConnectorConfigModel>(existing.Data);
                    var disabledTime = existingConnectorConfig.GetPropertyOrDefault(DISABLED_TIME);
                    if (string.IsNullOrEmpty(disabledTime))
                        disabledTime = DateTimeOffset.Now.ToString("o");

                    var newConnectorConfig = JsonSerializer.Deserialize<ConnectorConfigModel>(connectorData.Data);
                    newConnectorConfig.SetProperty(DISABLED_TIME, disabledTime);
                    connectorData.Data = JsonSerializer.Serialize(newConnectorConfig);
                }

                existing.Data = connectorData.Data;
                existing.DisplayName = connectorData.DisplayName;
                existing.ReportLocation = connectorData.ReportLocation;
                existing.Status = connectorData.Status;

                await dbContext.SaveChangesAsync(cancellationToken);
            });
        }

        /// <summary>
        /// Deletes connector configuration asynchronously.
        /// </summary>
        /// <param name="connectorId">The connector id.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A Task</returns>
        public async Task DeleteConnectorConfigurationAsync(string connectorId, CancellationToken cancellationToken)
        {
            await _observabilityScope.Invoke(GetDimensions(connectorId), async () =>
            {
                using var dbContext = _databaseClient.CreateDbContext();

                var existing = await dbContext.Connectors.FirstOrDefaultAsync(a => a.ConnectorId == connectorId, cancellationToken);
                if (existing != null)
                {
                    dbContext.Connectors.Remove(existing);
                    await dbContext.SaveChangesAsync(cancellationToken);
                }
            });
        }

        /// <summary>
        /// Get all connector configurations asynchronously.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns><![CDATA[Task<List<ConnectorConfigurationModel>>]]></returns>
        public async Task<List<ConnectorConfigurationModel>> GetAllConnectorConfigurationsAsync(CancellationToken cancellationToken)
        {
            return await _observabilityScope.Invoke(GetDimensions(null), async () =>
            {
                using var dbContext = _databaseClient.CreateDbContext();
                return await dbContext.Connectors.ToListAsync(cancellationToken);
            });
        }

        /// <summary>
        /// Get the connector features status
        /// </summary>
        /// <param name="connectorId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>Feature status</returns>
        public async Task<ConnectorFeatureStatus> GetConnectorStatusAsync(string connectorId, CancellationToken cancellationToken)
        {
            var connectorData = await GetConnectorConfigurationAsync(connectorId, cancellationToken);
            if (connectorData == null)
                return new ConnectorFeatureStatus
                {
                    ConnectorId = connectorId,
                    FeatureName = CONNECTOR_FEATURE,
                    Enabled = false,
                    EnabledReason = CONNECTOR_NOT_FOUND_REASON
                };

            if (!_toggleProvider.GetConnectorEnabled(_systemContext, connectorData.TenantId))
                return new ConnectorFeatureStatus
                {
                    ConnectorId = connectorId,
                    FeatureName = CONNECTOR_FEATURE,
                    Enabled = false,
                    EnabledReason = CONNECTOR_FEATURE_DISABLED_REASON
                };

            if (connectorData.Status != ENABLED)
                return new ConnectorFeatureStatus
                {
                    ConnectorId = connectorId,
                    FeatureName = CONNECTOR_FEATURE,
                    Enabled = false,
                    EnabledReason = CONNECTOR_DISABLED_REASON
                };

            return new ConnectorFeatureStatus
            {
                ConnectorId = connectorId,
                FeatureName = CONNECTOR_FEATURE,
                Enabled = true,
                EnabledReason = CONNECTOR_ENABLED_REASON
            };
        }

        /// <summary>
        /// Get the connector submission feature status
        /// </summary>
        /// <param name="connectorId">ID of the connector to get the status for</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Feature status</returns>
        public async Task<ConnectorFeatureStatus> GetSubmissionStatusAsync(string connectorId, CancellationToken cancellationToken)
        {
            var connectorStatus = await GetConnectorStatusAsync(connectorId, cancellationToken);
            if (!connectorStatus.Enabled)
                return new ConnectorFeatureStatus
                {
                    ConnectorId = connectorId,
                    Enabled = false,
                    EnabledReason = connectorStatus.EnabledReason,
                    FeatureName = SUBMISSION_FEATURE
                };

            // Check the application settings
            if (!_connectorOptions.Value.SubmissionEnabled)
                return new ConnectorFeatureStatus
                {
                    ConnectorId = connectorId,
                    Enabled = false,
                    EnabledReason = SUBMISSION_APPSETTING_OFF_REASON,
                    FeatureName = SUBMISSION_FEATURE
                };

            return new ConnectorFeatureStatus
            {
                ConnectorId = connectorId,
                Enabled = true,
                EnabledReason = SUBMISSION_ENABLED_REASON,
                FeatureName = SUBMISSION_FEATURE
            };
        }

        /// <summary>
        /// Get the connector binary submission feature status
        /// </summary>
        /// <param name="connectorId">ID of the connector to get the status for</param>
        /// <param name="cancellationToken"></param>
        /// <returns>Feature status</returns>
        public async Task<ConnectorFeatureStatus> GetBinarySubmissionStatusAsync(string connectorId, CancellationToken cancellationToken)
        {
            var connectorStatus = await GetConnectorStatusAsync(connectorId, cancellationToken);
            if (!connectorStatus.Enabled)
                return new ConnectorFeatureStatus
                {
                    ConnectorId = connectorId,
                    Enabled = false,
                    EnabledReason = connectorStatus.EnabledReason,
                    FeatureName = BINARY_SUBMISSION_FEATURE
                };

            // Check the application settings
            if (!_connectorOptions.Value.SubmissionEnabled)
                return new ConnectorFeatureStatus
                {
                    ConnectorId = connectorId,
                    Enabled = false,
                    EnabledReason = SUBMISSION_APPSETTING_OFF_REASON,
                    FeatureName = BINARY_SUBMISSION_FEATURE
                };

            if (!_connectorOptions.Value.BinariesEnabled)
                return new ConnectorFeatureStatus
                {
                    ConnectorId = connectorId,
                    Enabled = false,
                    EnabledReason = BINARY_APPSETTING_OFF_REASON,
                    FeatureName = BINARY_SUBMISSION_FEATURE
                };

            var connectorData = await GetConnectorConfigurationAsync(connectorId, cancellationToken);
            if (connectorData != null && _toggleProvider.GetConnectorBinarySubmissionKillswitch(_systemContext, connectorData.TenantId))
            {
                return new ConnectorFeatureStatus
                {
                    ConnectorId = connectorId,
                    Enabled = false,
                    EnabledReason = SUBMISSION_KILLSWITCH_ON_REASON,
                    FeatureName = BINARY_SUBMISSION_FEATURE
                };
            }

            return new ConnectorFeatureStatus
            {
                ConnectorId = connectorId,
                Enabled = true,
                EnabledReason = BINARY_SUBMISSION_ENABLED_REASON,
                FeatureName = BINARY_SUBMISSION_FEATURE
            };
        }

        /// <summary>
        /// Get the dimensions.
        /// </summary>
        /// <param name="connectorId">The connector id.</param>
        /// <returns>A Dimensions</returns>
        private Dimensions GetDimensions(string connectorId)
        {
            return new Dimensions
            {
                [StandardDimensions.DEPENDANCY_TYPE] = DependancyType.Database.ToString(),
                [StandardDimensions.DEPENDANCY] = _databaseClient.GetExternalSystemName(),
                [CONNECTOR_ID_DIMENSION] = connectorId
            };
        }
    }
}