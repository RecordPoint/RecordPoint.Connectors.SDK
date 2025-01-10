using RecordPoint.Connectors.SDK.Toggles;

namespace RecordPoint.Connectors.SDK.Databases.Cosmos
{
    /// <summary>
    /// Connector config extensions as part of the framework for cosmos DB.
    /// </summary>
    public static class ConnectorToggleExtensions
    {

        /// <summary>
        /// Retrieve toggle status for the Tmp-UseCosmosDedicatedGateway toggle
        /// </summary>
        /// <param name="toggleProvider">IToggleProvider instance to retrieve toggle status from</param>
        /// <returns>Tmp-UseCosmosDedicatedGateway Toggle value</returns>
        public static bool UseCosmosDedicatedGateway(this IToggleProvider toggleProvider)
        {
            //DEFAULT FALSE: Toggle will default to false if the underlying Toggle platform cannot be reached. 
            return toggleProvider.GetToggleBool("Tmp-UseCosmosDedicatedGateway", false);
        }
    }
}

