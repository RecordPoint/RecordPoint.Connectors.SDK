namespace RecordPoint.Connectors.SDK.Health;

/// <summary>
/// Reports the results of a Health Check
/// </summary>
public class HealthCheckResult
{
    /// <summary>
    /// Resturns the highest overall health level of all returned dimensions and measures
    /// </summary>
    public HealthLevel OverallHealthLevel
    {
        get
        {
            var allHealthLevels = Dimensions.Select(a => a.HealthLevel)
                .Concat(Measures.Select(a => a.HealthLevel));

            return allHealthLevels.Any() ? allHealthLevels.Max() : HealthLevel.Normal;
        }
    }

    /// <summary>
    /// The time of the most recent health check
    /// </summary>
    public DateTimeOffset? LastUpdate { get; set; } = null;

    /// <summary>
    /// List of health check dimensions
    /// </summary>
    public List<HealthCheckDimension> Dimensions { get; set; } = new();

    /// <summary>
    /// List of health check measures
    /// </summary>
    public List<HealthCheckMeasure> Measures { get; set; } = new();
}
