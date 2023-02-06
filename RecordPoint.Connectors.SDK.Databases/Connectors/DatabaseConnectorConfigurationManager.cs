using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RecordPoint.Connectors.SDK.Context;
using RecordPoint.Connectors.SDK.Databases;
using RecordPoint.Connectors.SDK.Observability;
using RecordPoint.Connectors.SDK.Toggles;

namespace RecordPoint.Connectors.SDK.Connectors
{
    /// <summary>
    /// Default connector manager implementation that uses the connector database
    /// </summary>
    public class DatabaseConnectorConfigurationManager : IConnectorConfigurationManager
    {
        public const string CONNECTOR_FEATURE = "Connector";
        public const string SUBMISSION_FEATURE = "Submission";
        public const string BINARY_SUBMISSION_FEATURE = "Binary Submission";

        public const string CONNECTOR_NOT_FOUND_REASON = "Connector not found";
        public const string CONNECTOR_DISABLED_REASON = "Connector is not enabled";
        public const string CONNECTOR_FEATURE_DISABLED_REASON = "Connector feature is not enabled";
        public const string CONNECTOR_ENABLED_REASON = "Connector enabled";

        public const string SUBMISSION_APPSETTING_OFF_REASON = "Submission is disabled in the applications setting";
        public const string SUBMISSION_KILLSWITCH_ON_REASON = "Submission is disabled in launch darkly's kill switch";
        public const string SUBMISSION_ENABLED_REASON = "Submission enabled";

        public const string BINARY_APPSETTING_OFF_REASON = "Binary submission is disabled in the applications setting";
        public const string BINARY_SUBMISSION_ENABLED_REASON = "Binary submission enabled";

        public const string CONNECTOR_ID_DIMENSION = "ConnectorId";
        private readonly IOptions<ConnectorOptions> _connectorOptions;

        private readonly IConnectorDatabaseClient _databaseClient;
        private readonly IScopeManager _scopeManager;
        private readonly ISystemContext _systemContext;
        private readonly IToggleProvider _toggleProvider;

        public DatabaseConnectorConfigurationManager(
            IConnectorDatabaseClient databaseClient,
            IScopeManager scopeManager,
            IOptions<ConnectorOptions> connectorOptions,
            ISystemContext systemContext,
            IToggleProvider toggleProvider)
        {
            _databaseClient = databaseClient;
            _scopeManager = scopeManager;
            _connectorOptions = connectorOptions;
            _systemContext = systemContext;
            _toggleProvider = toggleProvider;
        }

        public async Task<bool> ConnectorConfigurationExistsAsync(string connectorId, CancellationToken cancellationToken)
        {
            return await _scopeManager.Invoke(GetDimensions(connectorId), async () =>
            {
                using var dbContext = _databaseClient.CreateDbContext();
                return (await dbContext.Connectors
                    .Where(a => a.ConnectorId == connectorId)
                    .ToListAsync(cancellationToken))
                    .Any();
            });
        }

        public async Task<ConnectorConfigurationModel> GetConnectorConfigurationAsync(string connectorId, CancellationToken cancellationToken)
        {
            return await _scopeManager.Invoke(GetDimensions(connectorId), async () =>
            {
                using var dbContext = _databaseClient.CreateDbContext();
                return await dbContext.Connectors.FirstOrDefaultAsync(a => a.ConnectorId == connectorId, cancellationToken);
            });
        }

        public async Task SetConnectorConfigurationAsync(ConnectorConfigurationModel connectorData, CancellationToken cancellationToken)
        {
            await _scopeManager.Invoke(GetDimensions(connectorData.ConnectorId), async () =>
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
                        TenantId = connectorData.TenantId
                    };
                    await dbContext.Connectors.AddAsync(existing, cancellationToken);
                }

                existing.Data = connectorData.Data;
                existing.DisplayName = connectorData.DisplayName;
                existing.ReportLocation = connectorData.ReportLocation;
                existing.Status = connectorData.Status;

                await dbContext.SaveChangesAsync(cancellationToken);
            });
        }

        public async Task DeleteConnectorConfigurationAsync(string connectorId, CancellationToken cancellationToken)
        {
            await _scopeManager.Invoke(GetDimensions(connectorId), async () =>
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

        public async Task<List<ConnectorConfigurationModel>> GetAllConnectorConfigurationsAsync(CancellationToken cancellationToken)
        {
            return await _scopeManager.Invoke(GetDimensions(null), async () =>
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

            if (connectorData.Status != "Enabled")
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