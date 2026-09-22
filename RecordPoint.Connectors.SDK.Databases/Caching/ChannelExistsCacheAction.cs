using Microsoft.EntityFrameworkCore;
using RecordPoint.Connectors.SDK.Caching;
using RecordPoint.Connectors.SDK.Observability;

namespace RecordPoint.Connectors.SDK.Databases.Caching;

/// <summary>
/// Cache action for checking if a Channel exists based on ConnectorId and ExternalId. This is used to optimize lookups for Channel existence, which can be a common operation in various parts of the system.
/// </summary>
public class ChannelExistsCacheAction : ICacheAction<bool>
{
    private readonly IConnectorDatabaseClient _databaseClient;
    private readonly IObservabilityScope _observabilityScope;
    private const int CacheExpirationMinutes = 60; // Cache expiration time in minutes

    /// <param name="databaseClient"></param>
    /// <param name="observabilityScope"></param>
    public ChannelExistsCacheAction(
        IConnectorDatabaseClient databaseClient,
        IObservabilityScope observabilityScope)
    {
        _databaseClient = databaseClient;
        _observabilityScope = observabilityScope;
    }

    /// <summary>
    /// Get channels matching the ConnectorId and ExternalId from the context.
    /// If any channels are found, it returns true (indicating that the channel exists), otherwise it returns false (indicating that the channel does not exist). 
    /// The result is cached for a specified duration to optimize performance for subsequent lookups with the same parameters.
    /// </summary>
    /// <param name="context"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    public async Task<CacheActionResult<bool>> ExecuteAsync(CacheActionContext context, CancellationToken cancellationToken = (default))
    {
        var connectorId = context != null && context.Properties.TryGetValue("ConnectorId", out var cid) ? cid as string : null;
        var externalId = context != null && context.Properties.TryGetValue("ExternalId", out var eid) ? eid as string : null;
        var exists = await _observabilityScope.Invoke(
            new Dimensions { ["ConnectorId"] = connectorId },
            async () =>
            {
                using var dbContext = _databaseClient.CreateDbContext();
                return (await dbContext.Channels
                    .Where(a => a.ConnectorId == connectorId && a.ExternalId == externalId)
                    .ToListAsync(cancellationToken))
                    .Count > 0;

            });

        // Set cache expiration as needed
        return new CacheActionResult<bool>
        {
            CacheItem = exists,
            Expires = DateTimeOffset.UtcNow.AddMinutes(CacheExpirationMinutes)
        };
    }
}