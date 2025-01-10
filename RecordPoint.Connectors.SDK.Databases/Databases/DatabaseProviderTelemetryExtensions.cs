using RecordPoint.Connectors.SDK.Observability;

namespace RecordPoint.Connectors.SDK.Databases
{
    /// <summary>
    /// The database provider telemetry extensions.
    /// </summary>
    public static class DatabaseProviderTelemetryExtensions
    {
        /// <summary>
        /// With added telemetry.
        /// </summary>
        /// <param name="provider">The provider.</param>
        /// <param name="telemetryTracker">The telemetry tracker.</param>
        /// <returns>An IConnectorDatabaseProvider</returns>
        public static IConnectorDatabaseProvider WithAddedTelemetry(
            this IConnectorDatabaseProvider provider, ITelemetryTracker telemetryTracker)
        {
            return new DatabaseProviderWithTelemetry(provider, telemetryTracker);
        }
    }
}