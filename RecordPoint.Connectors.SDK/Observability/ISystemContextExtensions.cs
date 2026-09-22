using RecordPoint.Connectors.SDK.Context;

namespace RecordPoint.Connectors.SDK.Observability;

/// <summary>
/// Extension methods for <see cref="ISystemContext"/>.
/// </summary>
public static class ISystemContextExtensions
{
    /// <summary>
    /// Gets standard dimensions from the system context
    /// </summary>
    /// <param name="systemContext"></param>
    /// <returns></returns>
    public static Dimensions GetDimensions(this ISystemContext systemContext)
    {
        return new Dimensions
        {
            { StandardDimensions.SYSTEM, systemContext.GetConnectorName() },
            { StandardDimensions.COMPANY, systemContext.GetCompanyName() }
        };
    }

    /// <summary>
    /// Gets dimensions suitable for pre-aggregated metrics from the system context.
    /// Returns only System (not Company) to conserve the limited dimension slots
    /// available in metric pre-aggregation (max 4 in App Insights).
    /// </summary>
    public static Dimensions GetMetricDimensions(this ISystemContext systemContext)
    {
        var connectorName = systemContext.GetConnectorName();
        return string.IsNullOrEmpty(connectorName)
            ? new Dimensions()
            : new Dimensions { { StandardDimensions.SYSTEM, connectorName } };
    }
}
