using RecordPoint.Connectors.SDK.Observability;

namespace RecordPoint.Connectors.SDK.Databases
{
    public static class DatabaseProviderTelemetryExtensions
    {
        public static IConnectorDatabaseProvider WithAddedTelemetry(
            this IConnectorDatabaseProvider provider, ITelemetryTracker telemetryTracker)
        {
            return new DatabaseProviderWithTelemetry(provider, telemetryTracker);
        }
    }
}