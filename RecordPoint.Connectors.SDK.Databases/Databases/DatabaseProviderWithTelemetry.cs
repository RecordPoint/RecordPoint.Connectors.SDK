using RecordPoint.Connectors.SDK.Observability;

namespace RecordPoint.Connectors.SDK.Databases
{
    public class DatabaseProviderWithTelemetry : IConnectorDatabaseProvider
    {
        private readonly IConnectorDatabaseProvider _databaseProviderImplementation;
        private readonly ITelemetryTracker _telemetryTracker;

        public DatabaseProviderWithTelemetry(
            IConnectorDatabaseProvider databaseProviderImplementation,
            ITelemetryTracker telemetryTracker)
        {
            _databaseProviderImplementation = databaseProviderImplementation ?? throw new ArgumentNullException(nameof(databaseProviderImplementation));
            _telemetryTracker = telemetryTracker;
        }

        public string GetExternalSystemName() => _databaseProviderImplementation.GetExternalSystemName();

        public string GetConnectionString() => _databaseProviderImplementation.GetConnectionString();

        public bool Exists() => _databaseProviderImplementation.Exists();

        public async Task PrepareAsync(CancellationToken cancellationToken) => await _databaseProviderImplementation.PrepareAsync(cancellationToken);

        public async Task CleanupAsync(CancellationToken cancellationToken) => await _databaseProviderImplementation.CleanupAsync(cancellationToken);

        public async Task RemoveAsync(CancellationToken cancellationToken) => await _databaseProviderImplementation.RemoveAsync(cancellationToken);

        public async Task ReadyAsync(CancellationToken cancellationToken) => await _databaseProviderImplementation.ReadyAsync(cancellationToken);

        public void SetReady(Exception exception) => _databaseProviderImplementation.SetReady(exception);

        public ConnectorDbContext CreateDbContext()
        {
            ConnectorDbContext result;
            try
            {
                result = _databaseProviderImplementation.CreateDbContext();
            }
            catch (Exception ex)
            {
                _telemetryTracker?.TrackException("Exception Thrown in CreateDbContext", ex);
                throw;
            }

            return result;
        }
    }
}