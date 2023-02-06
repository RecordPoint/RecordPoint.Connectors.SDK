namespace RecordPoint.Connectors.SDK.Databases
{
    /// <summary>
    /// Defines a database provider
    /// </summary>
    /// <remarks>
    /// Database providers are responsible for providing database specific funtionality to the database subsystem
    /// </remarks>
    public interface IDatabaseProvider<out TDbContext>
    //where TDbContext : DbContext
    {
        /// <summary>
        /// Get the name of the external system this provider manages
        /// </summary>
        /// <returns></returns>
        string GetExternalSystemName();

        /// <summary>
        /// Get the connection string for this database, useful for providing it to external systems that may need to use the database
        /// </summary>
        /// <returns>Connection string</returns>
        string GetConnectionString();

        /// <summary>
        /// Does the database exist?
        /// </summary>
        /// <returns>True if the database exists, otherwise false</returns>
        bool Exists();

        /// <summary>
        /// Prepare the database for operation
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        Task PrepareAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Cleanup the database on shutdown
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        Task CleanupAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Remove the database, deleting it from the system
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        Task RemoveAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Called by dependant services to determine if the database is ready for operation.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Readiness Task</returns>
        Task ReadyAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Set that the database is ready
        /// </summary>
        /// <param name="exception">Exception that indicates that the service has critically failed. Any calls to Ready will fail</param>
        void SetReady(Exception exception);

        /// <summary>
        /// Create a db context used to make db requests if the database is ready,
        /// otherwise raises the reason why the database is not ready
        /// </summary>
        /// <returns>Connector DB Context if the database is ready</returns>
        TDbContext CreateDbContext();
    }
}