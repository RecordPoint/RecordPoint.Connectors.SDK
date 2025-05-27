using RecordPoint.Connectors.SDK.Observability;
using System;

namespace RecordPoint.Connectors.SDK.Context
{
    /// <summary>
    /// Telemetry extensions related to the context
    /// </summary>
    public static class ContextTelemetryExtensions
    {
        /// <summary>
        /// Begin system observability scope
        /// </summary>
        /// <param name="observabilityScope"></param>
        /// <param name="systemContext"></param>
        public static IDisposable BeginSystemScope(this IObservabilityScope observabilityScope, ISystemContext systemContext)
        {
            var dimensions = new Dimensions
            {
                [StandardDimensions.COMPANY] = systemContext.GetCompanyName(),
                [StandardDimensions.SYSTEM] = systemContext.GetConnectorName(),
                [StandardDimensions.SERVICE] = systemContext.GetServiceName()
            };
            return observabilityScope.BeginScope(dimensions);
        }

        /// <summary>
        /// Begin service observability scope
        /// </summary>
        /// <param name="observabilityScope"></param>
        /// <param name="serviceId">Unique correlation ID of the service</param>
        public static IDisposable BeginServiceScope(this IObservabilityScope observabilityScope, string serviceId)
        {
            var dimensions = new Dimensions
            {
                [StandardDimensions.SERVICE_ID] = serviceId
            };
            return observabilityScope.BeginScope(dimensions);
        }
    }
}