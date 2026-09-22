namespace RecordPoint.Connectors.SDK.Databases.Cosmos
{
    /// <summary>
    /// Shared metric name and dimension constants for Cosmos DB RU telemetry.
    /// </summary>
    internal static class CosmosMetricConstants
    {
        /// <summary>
        /// Metric name used to track Cosmos DB request charge (RU consumption).
        /// </summary>
        internal const string RequestCharge = "Cosmos.RequestCharge";

        /// <summary>
        /// Dimension name identifying the Cosmos DB container that incurred the charge.
        /// </summary>
        internal const string ContainerDimension = "Container";
    }
}
