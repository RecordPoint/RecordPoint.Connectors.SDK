using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using RecordPoint.Connectors.SDK.Observability;

namespace RecordPoint.Connectors.SDK.Databases.Cosmos.Telemetry
{
    /// <summary>
    /// Logger provider that captures Cosmos DB request charges (RU) from EF Core Cosmos operations
    /// and emits them as <c>Cosmos.RequestCharge</c> telemetry metrics.
    /// </summary>
    /// <remarks>
    /// EF Core 8 Cosmos provider logs every executed operation at <see cref="LogLevel.Information"/>
    /// with structured data including request charge and container name. This provider intercepts
    /// those log events and extracts the RU cost, emitting the same <c>Cosmos.RequestCharge</c>
    /// metric with <c>Container</c> dimension that <see cref="Manager.CosmosDbManager{T}"/> uses
    /// for direct Cosmos SDK operations.
    /// </remarks>
    internal sealed class CosmosRuTelemetryLoggerProvider : ILoggerProvider
    {
        private const string CommandCategory = "Microsoft.EntityFrameworkCore.Database.Command";

        private readonly CosmosRuTelemetryLogger _commandLogger;

        public CosmosRuTelemetryLoggerProvider(ITelemetryTracker telemetryTracker)
        {
            _commandLogger = new CosmosRuTelemetryLogger(telemetryTracker);
        }

        public ILogger CreateLogger(string categoryName)
        {
            return categoryName == CommandCategory
                ? _commandLogger
                : NullLogger.Instance;
        }

        public void Dispose()
        {
            // Nothing to dispose — the logger holds no unmanaged resources
        }
    }
}
