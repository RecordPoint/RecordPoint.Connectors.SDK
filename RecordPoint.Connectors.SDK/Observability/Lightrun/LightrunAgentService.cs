using Lightrun;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using RecordPoint.Connectors.SDK.Context;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace RecordPoint.Connectors.SDK.Observability.Lightrun;

/// <summary>
/// Background Host used to start the Lightrun Agent within a console host
/// </summary>
public class LightrunAgentService(ISystemContext systemContext, ITelemetryTracker telemetryTracker, IOptions<LightrunOptions> options) : BackgroundService
{
    private bool _hasStarted = false;

    /// <summary>
    /// Is light run enabled?
    /// </summary>
    /// <remarks>
    /// Lightrun is enabled only if the Server URL and Secret is provided
    /// </remarks>
    public bool Enabled => !string.IsNullOrEmpty(options.Value?.Secret) && !string.IsNullOrEmpty(options.Value?.ServerUrl?.OriginalString);

    /// <summary>
    /// Starts the underlying lightrun agent if enabled
    /// </summary>
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        if (_hasStarted)
            throw new InvalidOperationException("Lightrun has already been started");

        _hasStarted = true;

        using var lightrunScope = telemetryTracker.CreateRootServiceScope(systemContext);

        try
        {
            if (Enabled)
            {
                telemetryTracker.TrackTrace($"Starting Lightrun Agent", SeverityLevel.Information);
                LightrunAgent.Start(GetAgentOptions());
            }
            else
            {
                telemetryTracker.TrackTrace($"Skipping Lightrun Agent start", SeverityLevel.Information);
            }
        }
        catch (Exception ex)
        {
            // Don't permit failure of lightrun to cause app to fail.
            // Just log and proceed.
            telemetryTracker.TrackException(ex);
        }

        while (!stoppingToken.IsCancellationRequested)
        {
            await Task.Delay(Timeout.InfiniteTimeSpan, stoppingToken);
        }
    }

    /// <summary>
    /// Get the options needed for the underlying Lightrun Agent
    /// </summary>
    /// <returns>Lightrun agent options</returns>
    private AgentOptions GetAgentOptions()
    {
        return new AgentOptions
        {
            CertificatePinningEnabled = options.Value?.CertificatePinningEnabled ?? false,
            CustomDynamicLogger = new LightrunDynamicLogger(telemetryTracker),
            DisplayName = GetAgentDisplayName(),
            Secret = options.Value?.Secret,
            ServerUrl = options.Value?.ServerUrl,
            Tags = GetAgentTags(),
            UseSandbox = true,
            AgentLogTargetDir = options.Value?.AgentLogTargetDir ?? Path.GetTempPath(),
            MaxStringLength = options.Value?.MaxStringLength ?? 256,
            MaxCollectionSize = options.Value?.MaxCollectionSize ?? 10,
            MaxFieldCount = options.Value?.MaxFieldCount ?? 100,
            MaxDepthToSerialize = options.Value?.MaxDepthToSerialize ?? 5
        };
    }

    private string GetAgentDisplayName()
    {
        var processId = Environment.ProcessId.ToString();
        var hostName = Environment.MachineName;
        return $"{systemContext.GetConnectorName()}.{systemContext.GetServiceName()} ({hostName}:{processId})";
    }

    private string[] GetAgentTags()
    {
        var hostName = Environment.MachineName;
        var defaultTags = new[]
        {
            systemContext.GetConnectorName(),
            $"{systemContext.GetConnectorName()}.{systemContext.GetServiceName()}",
            hostName
        };
        return options.Value?.Tags != null
            ? [.. options.Value?.Tags, .. defaultTags]
            : defaultTags;
    }

}