using RecordPoint.Connectors.SDK.Databases.Cosmos.Manager;

namespace RecordPoint.Connectors.SDK.Databases.Cosmos.SemephoreLock;

/// <summary>
/// Represents a semaphore lock entry in a Cosmos Container
/// </summary>
public class SemaphoreLockCosmosDbItem : BaseCosmosDbItem
{
    /// <summary>
    /// The container name used for Semaphore Locks within the Cosmos Database
    /// </summary>
    public const string COSMOS_DB_CONTAINER_NAME = "semaphorelock";
    /// <summary>
    /// The expiry time of the semaphore lock
    /// </summary>
    public DateTimeOffset LockExpiry { get; set; }
}