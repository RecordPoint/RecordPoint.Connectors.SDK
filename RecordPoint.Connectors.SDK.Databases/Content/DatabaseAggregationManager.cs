using Microsoft.EntityFrameworkCore;
using RecordPoint.Connectors.SDK.Databases;
using RecordPoint.Connectors.SDK.Observability;
using System.Linq.Expressions;

namespace RecordPoint.Connectors.SDK.Content
{
    /// <summary>
    /// Implementation of a Aggregation manager that uses the connector database for persistence
    /// </summary>
    public class DatabaseAggregationManager : IAggregationManager
    {
        /// <summary>
        /// 
        /// </summary>
        public const string CONNECTOR_ID_DIMENSION = "ConnectorId";

        private readonly IConnectorDatabaseClient _databaseClient;
        private readonly IObservabilityScope _observabilityScope;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="databaseClient"></param>
        /// <param name="observabilityScope"></param>
        public DatabaseAggregationManager(IConnectorDatabaseClient databaseClient, IObservabilityScope observabilityScope)
        {
            _databaseClient = databaseClient;
            _observabilityScope = observabilityScope;
        }

        /// <inheritdoc/>
        public async Task<bool> AggregationExistsAsync(string connectorId, string externalId, CancellationToken cancellationToken)
        {
            return await _observabilityScope.Invoke(GetDimensions(connectorId), async () =>
            {
                using var dbContext = _databaseClient.CreateDbContext();
                return (await dbContext.Aggregations
                        .Where(a => a.ConnectorId == connectorId && a.ExternalId == externalId)
                        .ToListAsync(cancellationToken))
                        .Any();
            });

        }

        /// <inheritdoc/>
        public async Task<AggregationModel> GetAggregationAsync(string connectorId, string externalId, CancellationToken cancellationToken)
        {
            return await _observabilityScope.Invoke(GetDimensions(connectorId), async () =>
            {
                using var dbContext = _databaseClient.CreateDbContext();
                return await dbContext.Aggregations
                    .FirstOrDefaultAsync(a => a.ConnectorId == connectorId && a.ExternalId == externalId, cancellationToken);
            });
        }

        /// <inheritdoc/>
        public async Task<List<AggregationModel>> GetAggregationsAsync(CancellationToken cancellationToken)
        {
            return await _observabilityScope.Invoke(GetDimensions(null), async () =>
            {
                using var dbContext = _databaseClient.CreateDbContext();
                return await dbContext.Aggregations.ToListAsync(cancellationToken);
            });
        }

        /// <inheritdoc/>
        public async Task<List<AggregationModel>> GetAggregationsAsync(Expression<Func<AggregationModel, bool>> predicate, CancellationToken cancellationToken)
        {
            return await _observabilityScope.Invoke(GetDimensions(null), async () =>
            {
                using var dbContext = _databaseClient.CreateDbContext();
                return await dbContext.Aggregations
                    .Where(predicate)
                    .ToListAsync(cancellationToken);
            });
        }

        /// <inheritdoc/>
        public async Task<List<AggregationModel>> GetAggregationsAsync(string connectorId, CancellationToken cancellationToken)
        {
            return await _observabilityScope.Invoke(GetDimensions(connectorId), async () =>
            {
                using var dbContext = _databaseClient.CreateDbContext();
                return await dbContext.Aggregations
                    .Where(a => a.ConnectorId == connectorId)
                    .ToListAsync(cancellationToken);
            });
        }

        /// <inheritdoc/>
        public async Task UpsertAggregationAsync(AggregationModel aggregation, CancellationToken cancellationToken)
        {
            await _observabilityScope.Invoke(GetDimensions(null), async () =>
            {
                using var dbContext = _databaseClient.CreateDbContext();

                var existingAggregation = await dbContext.Aggregations
                    .FirstOrDefaultAsync(a => a.ConnectorId == aggregation.ConnectorId && a.ExternalId == aggregation.ExternalId, cancellationToken);
                if (existingAggregation == null)
                {
                    dbContext.Aggregations.Add(aggregation);
                    await dbContext.SaveChangesAsync(cancellationToken);
                }
                else
                {
                    if (!aggregation.Equals(existingAggregation))
                    {
                        existingAggregation.Title = aggregation.Title;
                        existingAggregation.MetaData = aggregation.MetaData;
                        await dbContext.SaveChangesAsync(cancellationToken);
                    }
                }
            });
        }

        /// <inheritdoc/>
        public async Task UpsertAggregationsAsync(List<AggregationModel> aggregations, CancellationToken cancellationToken)
        {
            await _observabilityScope.Invoke(GetDimensions(null), async () =>
            {
                using var dbContext = _databaseClient.CreateDbContext();

                var connectorIdGroups = aggregations.GroupBy(a => a.ConnectorId, a => a);
                foreach (var aggregationsGroupedByConnectorId in connectorIdGroups)
                {
                    var externalIds = aggregationsGroupedByConnectorId.Select(a => a.ExternalId);
                    var existingAggregations = await dbContext.Aggregations
                        .Where(a => a.ConnectorId == aggregationsGroupedByConnectorId.Key && externalIds.Contains(a.ExternalId))
                        .ToListAsync(cancellationToken);

                    var existingAggregationIds = existingAggregations
                        .Select(a => a.ExternalId)
                        .ToList();

                    //Handle New Aggregations
                    await HandleNewAggregationsAsync(dbContext, aggregationsGroupedByConnectorId, existingAggregationIds, cancellationToken);

                    //Handle Updated Aggregations
                    await HandleUpdatedAggregationsAsync(dbContext, aggregationsGroupedByConnectorId, existingAggregations, existingAggregationIds, cancellationToken);
                }
            });
        }

        // Determines which aggregations do not currently existing in the cache and persists them
        private static async Task HandleNewAggregationsAsync(ConnectorDbContext dbContext, IGrouping<string, AggregationModel> aggregationsGroupedByConnectorId, List<string> existingAggregationIds, CancellationToken cancellationToken)
        {
            var newAggregations = aggregationsGroupedByConnectorId
                .Where(a => !existingAggregationIds.Contains(a.ExternalId));

            if (newAggregations.Any())
            {
                dbContext.Aggregations.AddRange(newAggregations);
                await dbContext.SaveChangesAsync(cancellationToken);
            }
        }

        // Determines which aggregations already exist in the cache and updates them
        private static async Task HandleUpdatedAggregationsAsync(ConnectorDbContext dbContext, IGrouping<string, AggregationModel> aggregationsGroupedByConnectorId, List<AggregationModel> existingAggregations, List<string> existingAggregationIds, CancellationToken cancellationToken)
        {
            var updateAggregations = aggregationsGroupedByConnectorId
                .Where(a => existingAggregationIds.Contains(a.ExternalId));

            if (updateAggregations.Any())
            {
                var hasUpdates = false;
                foreach (var aggregation in updateAggregations)
                {
                    var existingAggregation = existingAggregations.First(a => a.ExternalId == aggregation.ExternalId);
                    if (!aggregation.Equals(existingAggregation))
                    {
                        existingAggregation.Title = aggregation.Title;
                        existingAggregation.MetaData = aggregation.MetaData;
                        hasUpdates = true;
                    }
                }
                if (hasUpdates)
                    await dbContext.SaveChangesAsync(cancellationToken);
            }
        }

        /// <inheritdoc/>
        public async Task RemoveAggregationAsync(string connectorId, string externalId, CancellationToken cancellationToken)
        {
            await _observabilityScope.Invoke(GetDimensions(connectorId), async () =>
             {
                 using var dbContext = _databaseClient.CreateDbContext();
                 var aggregation = await dbContext.Aggregations
                     .FirstOrDefaultAsync(a => a.ConnectorId == connectorId && a.ExternalId == externalId, cancellationToken);

                 if (aggregation == null) return;

                 dbContext.Aggregations.Remove(aggregation);
                 await dbContext.SaveChangesAsync(cancellationToken);
             });
        }

        /// <inheritdoc/>
        public async Task RemoveAggregationsAsync(string connectorId, string[] externalIds, CancellationToken cancellationToken)
        {
            await _observabilityScope.Invoke(GetDimensions(connectorId), async () =>
            {
                using var dbContext = _databaseClient.CreateDbContext();
                var aggregations = await dbContext.Aggregations
                    .Where(a => a.ConnectorId == connectorId && externalIds.Contains(a.ExternalId))
                    .ToListAsync(cancellationToken);

                dbContext.Aggregations.RemoveRange(aggregations);
                await dbContext.SaveChangesAsync(cancellationToken);
            });
        }

        /// <inheritdoc/>
        public async Task RemoveAggregationsAsync(IEnumerable<AggregationModel> aggregationModels, CancellationToken cancellationToken)
        {
            await _observabilityScope.Invoke(GetDimensions(null), async () =>
            {
                using var dbContext = _databaseClient.CreateDbContext();

                //Group the Aggregations by ConnectorId so we can efficiently select & remove the Aggregations from the DbSet
                var groupedAggregations = aggregationModels
                    .GroupBy(a => a.ConnectorId)
                    .Select(a => new { ConnectorId = a.Key, Aggregations = a.ToList() });

                foreach (var aggregationGroup in groupedAggregations)
                {
                    var externalIds = aggregationGroup.Aggregations.Select(a => a.ExternalId);
                    var aggregations = await dbContext.Aggregations
                        .Where(a => a.ConnectorId == aggregationGroup.ConnectorId && externalIds.Contains(a.ExternalId))
                        .ToListAsync(cancellationToken);

                    dbContext.Aggregations.RemoveRange(aggregations);
                }

                await dbContext.SaveChangesAsync(cancellationToken);
            });
        }


        private Dimensions GetDimensions(string connectorId)
        {
            return new Dimensions
            {
                [StandardDimensions.DEPENDANCY_TYPE] = DependancyType.Database.ToString(),
                [StandardDimensions.DEPENDANCY] = _databaseClient.GetExternalSystemName(),
                [CONNECTOR_ID_DIMENSION] = connectorId
            };
        }

    }
}
