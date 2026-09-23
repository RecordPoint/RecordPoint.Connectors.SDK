namespace RecordPoint.Connectors.SDK.Toggles.LaunchDarkly;

/// <summary>
/// Configuration Options for Launch Darkly Feature Toggle Provider
/// </summary>
public class LaunchDarklyOptions
{
    /// <summary>
    /// Configuration Section for Launch Darkly Configuration Options
    /// </summary>
    public const string SECTION_NAME = "LaunchDarkly";

    /// <summary>
    /// Sdk key used to access launch darkly
    /// </summary>
    public string SdkKey { get; set; } = string.Empty;

    /// <summary>
    /// Default user key used for Feature Toggle State filtering
    /// </summary>
    /// <remarks>
    /// Set to a personal value to change settings without impacting other users
    /// </remarks>
    public string DefaultUserKey { get; set; } = string.Empty;
}