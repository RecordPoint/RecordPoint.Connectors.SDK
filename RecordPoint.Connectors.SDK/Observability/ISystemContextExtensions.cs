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
}
