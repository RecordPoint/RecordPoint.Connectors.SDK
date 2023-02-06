using RecordPoint.Connectors.SDK.Context;
using RecordPoint.Connectors.SDK.Toggles;

namespace RecordPoint.Connectors.SDK.Connectors
{
    public static class ConnectorToggleExtensions
    {
        public static bool GetConnectorEnabled(this IToggleProvider toggleProvider,
            ISystemContext systemContext, string tenantId)
        {
            //DEFAULT TRUE: As this is not a KillSwitch, toggle will default to true if the underlying Toggle platform cannot be reached. 
            return toggleProvider.GetToggleBool($"{systemContext.GetConnectorName()}Connector", tenantId, true);
        }

        public static bool GetConnectorBinarySubmissionKillswitch(this IToggleProvider toggleProvider,
            ISystemContext systemContext, string tenantId)
        {
            //DEFAULT FALSE: As this is a KillSwitch, toggle will default to false if the underlying Toggle platform cannot be reached. 
            return toggleProvider.GetToggleBool($"Killswitch-{systemContext.GetConnectorName()}-BinarySubmission", tenantId, false);
        }

        public static bool GetConnectorKillswitch(this IToggleProvider toggleProvider,
            ISystemContext systemContext)
        {
            //DEFAULT FALSE: As this is a KillSwitch, toggle will default to false if the underlying Toggle platform cannot be reached. 
            return toggleProvider.GetToggleBool($"Killswitch-{systemContext.GetConnectorName()}", false);
        }
    }
}