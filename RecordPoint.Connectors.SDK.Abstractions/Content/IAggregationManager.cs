using System.Linq.Expressions;

namespace RecordPoint.Connectors.SDK.Content
{
    /// <summary>
    /// Definition for a class that manages Aggregations
    /// </summary>
    public interface IAggregationManager
    {
        /// <summary>
        /// Checks if the specified Aggregation exists
        /// </summary>
        /// <param name="connectorId">The Connector Configuration for which the Aggregation belongs</param>
        /// <param name="externalId">External Id of the Aggregation</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>True if it exists</returns>
        Task<bool> AggregationExistsAsync(string connectorId, string externalId, CancellationToken cancellationToken);

        /// <summary>
        /// Gets the specified Aggregation
        /// </summary>
        /// <param name="connectorId">The Connector Configuration for which the Aggregation belongs</param>
        /// <param name="externalId">External Id of the Aggregation</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Aggregation if it exists, otherwise null</returns>
        Task<AggregationModel?> GetAggregationAsync(string connectorId, string externalId, CancellationToken cancellationToken);

        /// <summary>
        /// Gets all known Aggregations
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>List of Aggregations across all connectors</returns>
        Task<List<AggregationModel>> GetAggregationsAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Gets all known Aggregations
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>List of Aggregations across all connectors</returns>
        Task<List<AggregationModel>> GetAggregationsAsync(Expression<Func<AggregationModel, bool>> predicate, CancellationToken cancellationToken);

        /// <summary>
        /// Gets all Aggregations for the specified Connector
        /// </summary>
        /// <param name="connectorId">The Connector Configuration for which the Aggregations belong</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>List of Aggregations for the specified Connector</returns>
        Task<List<AggregationModel>> GetAggregationsAsync(string? connectorId, CancellationToken cancellationToken);

        /// <summary>
        /// Adds a Aggregation to Storage or updates the existing Aggregation
        /// </summary>
        /// <param name="aggregation">Aggregation to add</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Task</returns>
        Task UpsertAggregationAsync(AggregationModel aggregation, CancellationToken cancellationToken);

        /// <summary>
        /// Adds the List of Aggregations to Storage or Updates each existing Aggregation
        /// </summary>
        /// <param name="aggregations">The Aggregations to add</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns></returns>
        Task UpsertAggregationsAsync(List<AggregationModel> aggregations, CancellationToken cancellationToken);

        /// <summary>
        /// Removes a Aggregation from Storage
        /// </summary>
        /// <param name="connectorId">The Connector for which the Aggregation belongs</param>
        /// <param name="externalId">The Id of the Aggregation</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Task</returns>
        Task RemoveAggregationAsync(string connectorId, string externalId, CancellationToken cancellationToken);

        /// <summary>
        /// Removes the specified Aggregations from Storage
        /// </summary>
        /// <param name="connectorId">The Connector for which the Aggregation belongs</param>
        /// <param name="externalIds">The Ids of the Aggregations</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Task</returns>
        Task RemoveAggregationsAsync(string connectorId, string[] externalIds, CancellationToken cancellationToken);

        /// <summary>
        /// Removes the specified Aggregations from Storage
        /// </summary>
        /// <param name="aggregationModels">A list of Aggregation Models to remove</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Task</returns>
        Task RemoveAggregationsAsync(IEnumerable<AggregationModel> aggregationModels, CancellationToken cancellationToken);
    }
}
