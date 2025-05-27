using System;

namespace RecordPoint.Connectors.SDK.Observability.Lightrun;

/// <summary>
/// Configurable Options for Lightrun
/// </summary>
public class LightrunOptions
{
    /// <summary>
    /// Configuration Section Name
    /// </summary>
    public const string SECTION_NAME = "Lightrun";

    /// <summary>
    /// Whether to enable Certificate Pinning. Default: FALSE
    /// Overidden default setting as we don't require this against internal LR servers
    /// </summary>
    public bool CertificatePinningEnabled { get; set; } = false;

    /// <summary>
    /// Your organization's Lightrun secret key.
    /// </summary>
    public string Secret { get; set; } = string.Empty;

    /// <summary>
    /// Lightrun server URL.
    /// </summary>
    /// <remarks>
    /// Lightrun is disabled if not provided
    /// </remarks>
    public Uri ServerUrl { get; set; } = null;

    /// <summary>
    /// Agent display name. DEFAULT: hostname of the machine running the application and the application's process identifier
    /// </summary>
    public string DisplayName { get; set; } = string.Empty;

    /// <summary>
    /// The list of tags assigned to the agent.
    /// </summary>
    public string[] Tags { get; set; } = [];

    /// <summary>
    /// Maximum length of strings to serialize. Default: 256
    /// </summary>
    public int MaxStringLength { get; set; } = 256;

    /// <summary>
    /// Maximum items within a collection to serialize. Default: 10
    /// </summary>
    public int MaxCollectionSize { get; set; } = 10;

    /// <summary>
    /// Maximum number of fields to serialize. Default: 100
    /// </summary>
    public int MaxFieldCount { get; set; } = 100;

    /// <summary>
    /// Maximum depth of objects to serialize. Default: 5
    /// </summary>
    public int MaxDepthToSerialize { get; set; } = 5;

    /// <summary>
    /// The directory where the agent will store its logs. Default: Use Lightrun Default
    /// </summary>
    public string AgentLogTargetDir { get; set; } = null;
}
