using LDContext = LaunchDarkly.Sdk.Context;
using LaunchDarkly.Sdk.Server;
using Microsoft.Extensions.Options;

namespace RecordPoint.Connectors.SDK.Toggles.LaunchDarkly;

/// <summary>
/// The launch darkly toggle provider.
/// </summary>
public sealed class LaunchDarklyToggleProvider : IToggleProvider
{
    private const string SystemUserId = "00000000-0000-0000-0000-000000000000";
    private readonly LaunchDarklyOptions _options;
    private readonly LdClient _ldClient;

    /// <summary>
    /// Launch darkly feature toggle provider class for use with the connector sdk
    /// </summary>
    /// <param name="options"></param>
    public LaunchDarklyToggleProvider(IOptions<LaunchDarklyOptions> options)
    {
        _options = options.Value;
        _ldClient = new LdClient(_options.SdkKey);
    }

    /// <summary>
    /// Get toggle value for given toggle (non tenanted)
    /// </summary>
    /// <param name="toggle"></param>
    /// <param name="default"></param>
    /// <returns></returns>
    public bool GetToggleBool(string toggle, bool @default)
        => _ldClient.BoolVariation(toggle, DefaultContext(), defaultValue: @default);

    /// <summary>
    /// Get a tenanted toggle value for given toggle
    /// </summary>
    /// <param name="toggle"></param>
    /// <param name="userKey"></param>
    /// <param name="default"></param>
    /// <returns></returns>
    public bool GetToggleBool(string toggle, string userKey, bool @default)
        => _ldClient.BoolVariation(toggle, LDContext.New(userKey), defaultValue: @default);

    /// <inheritdoc/>
    public int GetToggleNumber(string toggle, string userKey, int @default)
        => _ldClient.IntVariation(toggle, LDContext.New(userKey), @default);

    /// <inheritdoc/>
    public int GetToggleNumber(string toggle, int @default)
        => _ldClient.IntVariation(toggle, DefaultContext(), @default);

    /// <inheritdoc/>
    public string? GetToggleString(string toggle, string userKey, string? @default = null)
        => _ldClient.StringVariation(toggle, LDContext.New(userKey), @default);

    /// <inheritdoc/>
    public string? GetToggleString(string toggle, string? @default = null)
        => _ldClient.StringVariation(toggle, DefaultContext(), @default);

    private LDContext DefaultContext()
    {
        var defaultUserKey = !string.IsNullOrEmpty(_options.DefaultUserKey)
            ? _options.DefaultUserKey
            : SystemUserId;

        return LDContext.New(defaultUserKey);
    }
}