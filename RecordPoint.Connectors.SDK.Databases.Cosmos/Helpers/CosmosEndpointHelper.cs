using RecordPoint.Connectors.SDK.Toggles;

namespace RecordPoint.Connectors.SDK.Databases.Cosmos.Helpers
{
    /// <summary>
    /// Cosmos endpoint helper methods
    /// </summary>
    public static class CosmosEndpointHelper
    {
        /// <summary>
        /// Generate the cosmos db connection string
        /// </summary>
        /// <param name="featureToggleProvider"></param>
        /// <param name="cosmosDbAccountName"></param>
        /// <returns> Cosmos DB connection string</returns>
        public static string BuildCosmosAccountEndpoint(IToggleProvider featureToggleProvider, string? cosmosDbAccountName)
        {
            return $"https://{cosmosDbAccountName}.{(featureToggleProvider.UseCosmosDedicatedGateway() ? "sqlx.cosmos.azure" : "documents.azure")}.com/";
        }
    }
}
