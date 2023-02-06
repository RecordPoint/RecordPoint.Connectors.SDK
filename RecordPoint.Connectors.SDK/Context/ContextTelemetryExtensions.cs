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
        /// <param name="scopeManager"></param>
        /// <param name="systemContext"></param>
        public static IDisposable BeginSystemScope(this IScopeManager scopeManager, ISystemContext systemContext)
        {
            var dimensions = new Dimensions
            {
                [StandardDimensions.COMPANY] = systemContext.GetCompanyName(),
                [StandardDimensions.SYSTEM] = systemContext.GetConnectorName()
            };
            return scopeManager.BeginScope(dimensions);
        }

        /// <summary>
        /// Begin service observability scope
        /// </summary>
        /// <param name="scopeManager"></param>
        /// <param name="service">Name of the service</param>
        /// <param name="serviceId">Unique correlation ID of the service</param>
        public static IDisposable BeginServiceScope(this IScopeManager scopeManager, string service, string serviceId)
        {
            var dimensions = new Dimensions
            {
                [StandardDimensions.SERVICE] = service,
                [StandardDimensions.SERVICE_ID] = serviceId
            };
            return scopeManager.BeginScope(dimensions);
        }
    }
}