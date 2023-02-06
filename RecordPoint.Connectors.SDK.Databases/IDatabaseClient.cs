namespace RecordPoint.Connectors.SDK.Databases
{
    /// <summary>
    /// Defines the database client used by components to access database services
    /// </summary>
    public interface IDatabaseClient<out TDbContext>
    {

        /// <summary>
        /// Task that returns when the database is ready
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        Task ReadyAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Get the db context
        /// </summary>
        /// <returns></returns>
        TDbContext CreateDbContext();

        /// <summary>
        /// Get the external system name that identifies the type of database for standard logging
        /// </summary>
        /// <returns></returns>
        string GetExternalSystemName();
    }
}
