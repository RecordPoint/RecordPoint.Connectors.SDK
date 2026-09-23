using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.Configuration;
using RecordPoint.Connectors.SDK.Context;
using RecordPoint.Connectors.SDK.Toggles;

namespace RecordPoint.Connectors.SDK.Connectors
{
    /// <summary>
    /// The connector toggle extensions.
    /// </summary>
    public static class ConnectorToggleExtensions
    {
        /// <summary>
        /// Get connector enabled.
        /// <para>DEFAULT TRUE: As this is not a KillSwitch, toggle will default to true if the underlying Toggle platform cannot be reached. </para>
        /// </summary>
        /// <param name="toggleProvider">The toggle provider.</param>
        /// <param name="systemContext">The system context.</param>
        /// <param name="tenantId">The tenant id.</param>
        /// <returns>A bool</returns>
        public static bool GetConnectorEnabled(this IToggleProvider toggleProvider, ISystemContext systemContext, string tenantId)
        {
            return toggleProvider.GetToggleBool($"{systemContext.GetConnectorName()}Connector", tenantId, true);
        }

        /// <summary>
        /// Get connector binary submission killswitch.
        /// <para>DEFAULT FALSE: As this is a KillSwitch, toggle will default to false if the underlying Toggle platform cannot be reached. </para>
        /// </summary>
        /// <param name="toggleProvider">The toggle provider.</param>
        /// <param name="systemContext">The system context.</param>
        /// <param name="tenantId">The tenant id.</param>
        /// <returns>A bool</returns>
        public static bool GetConnectorBinarySubmissionKillswitch(this IToggleProvider toggleProvider, ISystemContext systemContext, string tenantId)
        {
            return toggleProvider.GetToggleBool($"Killswitch-{systemContext.GetConnectorName()}-BinarySubmission", tenantId, false);
        }

        /// <summary>
        /// Get connector content protection.
        /// <para>DEFAULT TRUE: The toggle will default to true so binaries are by default submitted to the platform.</para>  
        /// </summary>
        /// <param name="toggleProvider"></param>
        /// <param name="systemContext"></param>
        /// <param name="tenantId"></param>
        /// <returns></returns>
        public static bool GetConnectorContentProtection(this IToggleProvider toggleProvider, ISystemContext systemContext, string tenantId)
        {
            return toggleProvider.GetToggleBool($"{systemContext.GetConnectorName()}-Content-Protection", tenantId, true);
        }

        /// <summary>
        /// Whether the config-driven binary submission feature is enabled for this tenant.
        /// <para>DEFAULT FALSE: while the feature rolls out, tenants keep using the legacy content-protection toggle.</para>
        /// </summary>
        /// <param name="toggleProvider">The toggle provider.</param>
        /// <param name="tenantId">The tenant id.</param>
        /// <returns>A bool</returns>
        public static bool GetConfigDrivenBinarySubmissionEnabled(this IToggleProvider toggleProvider, string tenantId)
        {
            return toggleProvider.GetToggleBool("Enable-Config-Driven-Binary-Submission", tenantId, false);
        }

        /// <summary>
        /// Decides whether binaries should be submitted for this connector. When the config-driven
        /// feature is enabled for the tenant, the connector config property is authoritative
        /// (default true); otherwise the legacy per-connector content-protection toggle is used.
        /// This is the single place the binary-submission decision lives for all connectors.
        /// </summary>
        /// <param name="toggleProvider">The toggle provider.</param>
        /// <param name="systemContext">The system context.</param>
        /// <param name="connectorConfig">The connector configuration.</param>
        /// <returns>A bool</returns>
        public static bool ShouldSubmitBinaries(this IToggleProvider toggleProvider, ISystemContext systemContext, ConnectorConfigModel connectorConfig)
        {
            // Only connectors that actually have the switcher (a stored BinarySubmissionEnabled value) use
            // config, and only while the feature is on for the tenant. Everything else uses the legacy toggle.
            if (toggleProvider.GetConfigDrivenBinarySubmissionEnabled(connectorConfig.TenantId))
            {
                var configValue = connectorConfig.GetBinarySubmissionEnabled();
                if (configValue.HasValue)
                {
                    return configValue.Value;
                }
            }

            return toggleProvider.GetConnectorContentProtection(systemContext, connectorConfig.TenantId);
        }

        /// <summary>
        /// Get connector killswitch.
        /// <para>DEFAULT FALSE: As this is a KillSwitch, toggle will default to false if the underlying Toggle platform cannot be reached. </para>
        /// </summary>
        /// <param name="toggleProvider">The toggle provider.</param>
        /// <param name="systemContext">The system context.</param>
        /// <returns>A bool</returns>
        public static bool GetConnectorKillswitch(this IToggleProvider toggleProvider, ISystemContext systemContext)
        {
            return toggleProvider.GetToggleBool($"Killswitch-{systemContext.GetConnectorName()}", false);
        }

    }
}