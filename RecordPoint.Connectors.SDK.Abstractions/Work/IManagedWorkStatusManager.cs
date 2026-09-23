using System.Linq.Expressions;

namespace RecordPoint.Connectors.SDK.Work
{
    /// <summary>
    /// Definition for a class that manages the status of Managed Work
    /// </summary>
    public interface IManagedWorkStatusManager
    {
        /// <summary>
        /// Lists the status of managed work that matches the given predicate
        /// </summary>
        /// <param name="predicate">A function to test each element for a condition.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<List<ManagedWorkStatusModel>> GetWorkStatusesAsync(Expression<Func<ManagedWorkStatusModel, bool>> predicate, CancellationToken cancellationToken);
    }
}
