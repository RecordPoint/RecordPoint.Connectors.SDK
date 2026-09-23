using Microsoft.Extensions.Logging;
using RecordPoint.Connectors.SDK.Observability;
using System.Globalization;

namespace RecordPoint.Connectors.SDK.Databases.Cosmos.Telemetry
{
    /// <summary>
    /// Logger that intercepts EF Core Cosmos "Executed" events and emits RU charge telemetry.
    /// Thread-safe: this class has no mutable state and a single instance is shared across DbContexts.
    /// </summary>
    /// <remarks>
    /// Targets these <c>CosmosEventId</c> events (all carry request charge data):
    /// <list type="bullet">
    ///   <item><c>ExecutedReadNext</c> (30102) — query page results</item>
    ///   <item><c>ExecutedReadItem</c> (30103) — point reads</item>
    ///   <item><c>ExecutedCreateItem</c> (30104) — inserts</item>
    ///   <item><c>ExecutedReplaceItem</c> (30105) — updates</item>
    ///   <item><c>ExecutedDeleteItem</c> (30106) — deletes</item>
    /// </list>
    /// The structured log state from EF Core contains <c>{charge}</c> (RU cost) and
    /// <c>{container}</c> (Cosmos container name) as named key-value pairs.
    /// </remarks>
    internal sealed class CosmosRuTelemetryLogger : ILogger
    {
        // CosmosEventId values from Microsoft.EntityFrameworkCore.Diagnostics
        // Base: CoreEventId.ProviderBaseId (30000) + 100 = 30100 for ExecutingSqlQuery
        private const int ExecutedReadNextId = 30102;
        private const int ExecutedReadItemId = 30103;
        private const int ExecutedCreateItemId = 30104;
        private const int ExecutedReplaceItemId = 30105;
        private const int ExecutedDeleteItemId = 30106;

        private static readonly HashSet<int> TrackedEventIds = new()
        {
            ExecutedReadNextId,
            ExecutedReadItemId,
            ExecutedCreateItemId,
            ExecutedReplaceItemId,
            ExecutedDeleteItemId
        };

        private readonly ITelemetryTracker _telemetryTracker;

        public CosmosRuTelemetryLogger(ITelemetryTracker telemetryTracker)
        {
            _telemetryTracker = telemetryTracker;
        }

        public bool IsEnabled(LogLevel logLevel) => logLevel >= LogLevel.Information;

        public IDisposable? BeginScope<TState>(TState state) where TState : notnull => null;

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            if (!TrackedEventIds.Contains(eventId.Id))
            {
                return;
            }

            // EF Core structured log state implements IReadOnlyList<KeyValuePair<string, object?>>
            // via LogValues<> (item operations) or FormattedLogValues (ReadNext queries)
            if (state is not IReadOnlyList<KeyValuePair<string, object?>> values)
            {
                return;
            }

            double charge = 0;
            string? containerName = null;

            foreach (var kvp in values)
            {
                switch (kvp.Key)
                {
                    case "charge":
                        // Item operations pass charge as string (via .ToString()),
                        // ReadNext passes charge as double (via FormattedLogValues)
                        charge = kvp.Value switch
                        {
                            double d => d,
                            string s when double.TryParse(s, NumberStyles.Float, CultureInfo.InvariantCulture, out var parsed) => parsed,
                            _ => 0
                        };
                        break;

                    case "container":
                        containerName = kvp.Value?.ToString();
                        break;
                }
            }

            if (charge > 0 && !string.IsNullOrEmpty(containerName))
            {
                _telemetryTracker.TrackMetric(CosmosMetricConstants.RequestCharge, charge, CosmosMetricConstants.ContainerDimension, containerName);
            }
        }
    }
}
