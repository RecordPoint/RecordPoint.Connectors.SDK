using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;
using Microsoft.Extensions.Logging;
using System.Runtime.CompilerServices;

namespace RecordPoint.Connectors.SDK.Databases.Cosmos.Manager
{
    /// <summary>
    /// The cosmos db manager.
    /// </summary>
    /// <typeparam name="T"/>
    public class CosmosDbManager<T> : ICosmosDbManager<T> where T : BaseCosmosDbItem
    {
        /// <summary>
        /// The logger.
        /// </summary>
        private readonly ILogger<CosmosDbManager<T>> _logger;
        /// <summary>
        /// The container.
        /// </summary>
        private readonly Container? _container;

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <param name="cosmosClient">The cosmos client.</param>
        /// <param name="databaseId">The database id.</param>
        /// <param name="containerId">The container id.</param>
        /// <param name="logger">The logger.</param>
        public CosmosDbManager(CosmosClient cosmosClient, string databaseId, string containerId, ILogger<CosmosDbManager<T>> logger)
        {
            _container = cosmosClient.GetContainer(databaseId, containerId);
            _logger = logger;
        }

        /// <inheritdoc/>
        public async Task UpsertAsync(string partitionKey, T item, CancellationToken cancellationToken)
        {
            try
            {
                _ = await _container!.UpsertItemAsync(item, new PartitionKey(partitionKey), cancellationToken: cancellationToken);
            }
            catch (CosmosException ex)
            {
                _logger.LogError(ex, "Error");
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task<T?> GetAsync(string partitionKey, string id, CancellationToken cancellationToken)
        {
            try
            {
                var response = await _container!.ReadItemAsync<T>(id, new PartitionKey(partitionKey), null, cancellationToken: cancellationToken);
                return (T)response;
            }
            catch (CosmosException ex)
            {
                if (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return null;
                }
                throw;
            }
        }

        /// <inheritdoc/>
        public async Task DeleteAsync(string partitionKey, string id, CancellationToken cancellationToken = default)
        {
            await _container!.DeleteItemAsync<T>(id, new PartitionKey(partitionKey), cancellationToken: cancellationToken);
        }

        /// <inheritdoc/>
        public async IAsyncEnumerable<T> QuerySqlAsync(string partitionKey, QueryDefinition query, [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            using var feedIterator = _container!.GetItemQueryIterator<T>(query, null,
                requestOptions: string.IsNullOrWhiteSpace(partitionKey) ? default : new QueryRequestOptions { PartitionKey = new PartitionKey(partitionKey) });

            while (feedIterator.HasMoreResults)
            {
                var items = await feedIterator.ReadNextAsync(cancellationToken);

                foreach (var item in items)
                {
                    yield return item;
                }
            }
        }

        /// <inheritdoc/>
        public async IAsyncEnumerable<T> QueryLinqAsync(IQueryable<T> query)
        {
            using var feedIterator = GetFeedIterator(query);
            while (feedIterator.HasMoreResults)
            {
                var items = await feedIterator.ReadNextAsync();

                foreach (var item in items)
                {
                    yield return item;
                }
            }
        }

        /// <inheritdoc/>
        public IOrderedQueryable<T> GetContainerQuery()
        {
            return _container!.GetItemLinqQueryable<T>();
        }

        /// <inheritdoc/>
        public FeedIterator<T> GetFeedIterator(IQueryable<T> query)
        {
            return query.ToFeedIterator();
        }

        /// <inheritdoc/>
        public async Task<T> ReplaceAsync(string partitionKey, T item)
        {
            var response = await _container!.ReplaceItemAsync(item, item.Id, new PartitionKey(partitionKey));
            return response.Resource;
        }

        /// <inheritdoc/>
        public async Task<T> UpsertMatchingEtagAsync(string partitionKey, T item)
        {
            var response = await _container!.UpsertItemAsync(item, new PartitionKey(partitionKey),
                new ItemRequestOptions { IfMatchEtag = item.ETag });
            return response.Resource;
        }
    }
}
