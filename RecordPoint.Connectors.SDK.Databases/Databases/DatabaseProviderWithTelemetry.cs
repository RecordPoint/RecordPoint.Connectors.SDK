using RecordPoint.Connectors.SDK.Observability;

namespace RecordPoint.Connectors.SDK.Databases
{
    /// <summary>
    /// The database provider with telemetry.
    /// </summary>
    public class DatabaseProviderWithTelemetry : IConnectorDatabaseProvider
    {
        /// <summary>
        /// The database provider implementation.
        /// </summary>
        private readonly IConnectorDatabaseProvider _databaseProviderImplementation;
        /// <summary>
        /// The telemetry tracker.
        /// </summary>
        private readonly ITelemetryTracker _telemetryTracker;

        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseProviderWithTelemetry"/> class.
        /// </summary>
        /// <param name="databaseProviderImplementation">The database provider implementation.</param>
        /// <param name="telemetryTracker">The telemetry tracker.</param>
        public DatabaseProviderWithTelemetry(
            IConnectorDatabaseProvider databaseProviderImplementation,
            ITelemetryTracker telemetryTracker)
        {
            _databaseProviderImplementation = databaseProviderImplementation ?? throw new ArgumentNullException(nameof(databaseProviderImplementation));
            _telemetryTracker = telemetryTracker;
        }

        /// <summary>
        /// Get external system name.
        /// </summary>
        /// <returns>A string</returns>
        public string GetExternalSystemName() => _databaseProviderImplementation.GetExternalSystemName();

        /// <summary>
        /// Get connection string.
        /// </summary>
        /// <returns>A string</returns>
        public string GetConnectionString() => _databaseProviderImplementation.GetConnectionString();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool Exists() => _databaseProviderImplementation.Exists();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task PrepareAsync(CancellationToken cancellationToken) => await _databaseProviderImplementation.PrepareAsync(cancellationToken);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task CleanupAsync(CancellationToken cancellationToken) => await _databaseProviderImplementation.CleanupAsync(cancellationToken);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task RemoveAsync(CancellationToken cancellationToken) => await _databaseProviderImplementation.RemoveAsync(cancellationToken);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task ReadyAsync(CancellationToken cancellationToken) => await _databaseProviderImplementation.ReadyAsync(cancellationToken);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="exception"></param>
        public void SetReady(Exception exception) => _databaseProviderImplementation.SetReady(exception);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ConnectorDbContext CreateDbContext()
        {
            ConnectorDbContext result;
            try
            {
                result = _databaseProviderImplementation.CreateDbContext();
            }
            catch (Exception ex)
            {
                _telemetryTracker?.TrackException(ex);
                throw;
            }

            return result;
        }
    }
}