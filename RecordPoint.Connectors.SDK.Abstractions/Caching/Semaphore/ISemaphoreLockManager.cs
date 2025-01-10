using RecordPoint.Connectors.SDK.Client.Models;

namespace RecordPoint.Connectors.SDK.Caching.Semaphore
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISemaphoreLockManager
    {
        /// <summary>
        /// The Connector Configuration for the executing context
        /// </summary>
        ConnectorConfigModel? ConnectorConfiguration { get; set; }

        /// <summary>
        /// Checks if a valid semaphore lock is active and delays execution until the lock expires
        /// </summary>
        /// <param name="workType"></param>
        /// <param name="context"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task CheckWaitSemaphoreAsync(string workType, object? context, CancellationToken cancellationToken);

        /// <summary>
        /// Checks if a sempahore lock is active and returns the Date/Time when the lock expires
        /// </summary>
        /// <param name="workType"></param>
        /// <param name="context"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<DateTimeOffset?> GetSemaphoreAsync(string workType, object? context, CancellationToken cancellationToken);

        /// <summary>
        /// Sets a semaphore lock with the provided context for the specified duration
        /// </summary>
        /// <param name="semaphoreLockType"></param>
        /// <param name="workType"></param>
        /// <param name="context"></param>
        /// <param name="duration"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task SetSemaphoreAsync(SemaphoreLockType semaphoreLockType, string workType, object? context, int duration, CancellationToken cancellationToken);
    }
}
