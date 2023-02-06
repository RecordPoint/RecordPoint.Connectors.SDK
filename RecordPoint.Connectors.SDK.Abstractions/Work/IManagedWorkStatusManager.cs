using System.Linq.Expressions;

namespace RecordPoint.Connectors.SDK.Work
{
    /// <summary>
    /// Definition for a class that manages the status of Managed Work
    /// </summary>
    public interface IManagedWorkStatusManager
    {
        /// <summary>
        /// Gets the status of the specified Work
        /// </summary>
        /// <param name="workStatusId">Id of the work status</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Work Status if it exists, otherwise null</returns>
        Task<ManagedWorkStatusModel?> GetWorkStatusAsync(string workStatusId, CancellationToken cancellationToken);

        /// <summary>
        /// Adds a new work status to storage
        /// </summary>
        /// <param name="managedWorkStatusModel">work status to add</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Task</returns>
        Task AddWorkStatusAsync(ManagedWorkStatusModel managedWorkStatusModel, CancellationToken cancellationToken);

        /// <summary>
        /// Sets the specified work status to complete
        /// </summary>
        /// <param name="workStatusId">Id of work status to update</param>
        /// <param name="cancellationToken">Cancellation token</param>
        Task SetWorkCompleteAsync(string workStatusId, CancellationToken cancellationToken);



        /// <summary>
        /// Sets the specified work status to failed
        /// </summary>
        /// <param name="workStatusId">Id of work status to update</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns></returns>
        Task SetWorkFailedAsync(string workStatusId, CancellationToken cancellationToken);

        /// <summary>
        /// Sets the specified work status to abandoned
        /// </summary>
        /// <param name="workStatusId">Id of work status to update</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns></returns>
        Task SetWorkAbandonedAsync(string workStatusId, CancellationToken cancellationToken);

        /// <summary>
        /// Sets the specified work status to running
        /// </summary>
        /// <param name="workStatusId">Id of work status to update</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns></returns>
        Task SetWorkRunningAsync(string workStatusId, CancellationToken cancellationToken);

        /// <summary>
        /// Sets the specified work status to continue
        /// </summary>
        /// <param name="workStatusId">Id of work status to update</param>
        /// <param name="continuedWorkId">Id of the work to continue on with</param>
        /// <param name="state">The serialised state of the work</param>
        /// <param name="cancellationToken">Cancellation token</param>
        Task SetWorkContinueAsync(string workStatusId, string continuedWorkId, string state, CancellationToken cancellationToken);

        /// <summary>
        /// Lists the status of all managed work
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>A list of work statuses</returns>
        Task<List<ManagedWorkStatusModel>> GetAllWorkStatusesAsync(CancellationToken cancellationToken);

        /// <summary>
        /// Lists the status of managed work that matches the given predicate
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<List<ManagedWorkStatusModel>> GetWorkStatusesAsync(Expression<Func<ManagedWorkStatusModel, bool>> predicate, CancellationToken cancellationToken);

        /// <summary>
        /// Determines if there is managed work that matches the given predicate
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<bool> IsAnyAsync(Expression<Func<ManagedWorkStatusModel, bool>> predicate, CancellationToken cancellationToken);

        /// <summary>
        /// Removes the specified work
        /// </summary>
        /// <param name="workIds">The Ids of the Work to remove</param>
        /// <param name="cancellationToken">Cancellation token</param>
        Task RemoveWorkStatusesAsync(string[] workIds, CancellationToken cancellationToken);

    }
}
