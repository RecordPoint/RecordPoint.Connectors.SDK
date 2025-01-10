using Microsoft.Azure.Cosmos;

namespace RecordPoint.Connectors.SDK.Databases.Cosmos.Manager
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface ICosmosDbManager<T> where T : BaseCosmosDbItem
    {
        /// <summary>
        /// Update or create item Async
        /// </summary>
        /// <param name="partitionKey"></param>
        /// <param name="item"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task UpsertAsync(string partitionKey, T item, CancellationToken cancellationToken);

        /// <summary>
        /// Get Item Async from cosmos
        /// </summary>
        /// <param name="partitionKey"></param>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<T?> GetAsync(string partitionKey, string id, CancellationToken cancellationToken);

        /// <summary>
        /// Deletes Cosmos DB Item
        /// </summary>
        /// <param name="partitionKey"></param>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task DeleteAsync(string partitionKey, string id, CancellationToken cancellationToken = default);

        /// <summary>
        /// Passing an SQL query to this will execute it and will yield return it
        /// </summary>
        /// <param name="partitionKey"></param>
        /// <param name="query"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        IAsyncEnumerable<T> QuerySqlAsync(string partitionKey, QueryDefinition query, CancellationToken cancellationToken);

        /// <summary>
        /// Passing a Linq query to this will execute it and return results.
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        IAsyncEnumerable<T> QueryLinqAsync(IQueryable<T> query);

        /// <summary>
        /// Get Container query for DIY queries.
        /// </summary>
        /// <returns></returns>
        IOrderedQueryable<T> GetContainerQuery();

        /// <summary>
        /// Passing a query and returns a Feed Iterator
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        FeedIterator<T> GetFeedIterator(IQueryable<T> query);

        /// <summary>
        /// Replaces an existing cosmos item
        /// </summary>
        /// <param name="partitionKey"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        Task<T> ReplaceAsync(string partitionKey, T item);

        /// <summary>
        /// Upserts based on Etag
        /// </summary>
        /// <param name="partitionKey"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        Task<T> UpsertMatchingEtagAsync(string partitionKey, T item);
    }
}
