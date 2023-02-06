using Microsoft.EntityFrameworkCore;
using RecordPoint.Connectors.SDK.Databases;
using RecordPoint.Connectors.SDK.Observability;
using System.Linq.Expressions;

namespace RecordPoint.Connectors.SDK.Work
{
    /// <summary>
    /// Implementation of a Work status manager that uses the connector database for persistence
    /// </summary>
    public class DatabaseManagedWorkStatusManager : IManagedWorkStatusManager
    {
        public const string WORK_STATUS_ID_DIMENSION = "WorkStatusId";

        private readonly IConnectorDatabaseClient _databaseClient;
        private readonly IScopeManager _scopeManager;

        public DatabaseManagedWorkStatusManager(
            IConnectorDatabaseClient databaseClient,
            IScopeManager scopeManager)
        {
            _databaseClient = databaseClient;
            _scopeManager = scopeManager;
        }

        /// <inheritdoc/>
        public async Task<ManagedWorkStatusModel> GetWorkStatusAsync(string workStatusId, CancellationToken cancellationToken)
        {
            return await _scopeManager.Invoke(GetDimensions(workStatusId), async () =>
            {
                using var dbContext = _databaseClient.CreateDbContext();
                return await dbContext.ManagedWorkStatuses.FirstOrDefaultAsync(a => a.Id == workStatusId, cancellationToken);
            });
        }

        /// <inheritdoc/>
        public async Task AddWorkStatusAsync(ManagedWorkStatusModel managedWorkStatusModel, CancellationToken cancellationToken)
        {
            await _scopeManager.Invoke(GetDimensions(null), async () =>
            {
                using var dbContext = _databaseClient.CreateDbContext();
                await dbContext.ManagedWorkStatuses.AddAsync(managedWorkStatusModel, cancellationToken);
                managedWorkStatusModel.LastStatusUpdate = DateTimeOffset.Now;
                await dbContext.SaveChangesAsync(cancellationToken);
            });
        }

        /// <inheritdoc/>
        public Task SetWorkCompleteAsync(string workStatusId, CancellationToken cancellationToken)
        {
            return SetWorkStatusAsync(workStatusId, ManagedWorkStatuses.Complete, cancellationToken);
        }

        /// <inheritdoc/>
        public Task SetWorkRunningAsync(string workStatusId, CancellationToken cancellationToken)
        {
            return SetWorkStatusAsync(workStatusId, ManagedWorkStatuses.Running, cancellationToken);
        }

        /// <inheritdoc/>
        public Task SetWorkFailedAsync(string workStatusId, CancellationToken cancellationToken)
        {
            return SetWorkStatusAsync(workStatusId, ManagedWorkStatuses.Failed, cancellationToken);
        }

        /// <inheritdoc/>
        public Task SetWorkAbandonedAsync(string workStatusId, CancellationToken cancellationToken)
        {
            return SetWorkStatusAsync(workStatusId, ManagedWorkStatuses.Abandoned, cancellationToken);
        }

        private async Task SetWorkStatusAsync(string workStatusId, ManagedWorkStatuses status, CancellationToken cancellationToken)
        {
            await _scopeManager.Invoke(GetDimensions(workStatusId), async () =>
            {
                using var dbContext = _databaseClient.CreateDbContext();
                var workStatus = await dbContext.ManagedWorkStatuses.FirstOrDefaultAsync(a => a.Id == workStatusId, cancellationToken);
                if (workStatus == null) return;
                workStatus.Status = status;
                workStatus.LastStatusUpdate = DateTimeOffset.Now;
                await dbContext.SaveChangesAsync(cancellationToken);
            });
        }

        /// <inheritdoc/>
        public async Task SetWorkContinueAsync(string workStatusId, string continuedWorkId, string state, CancellationToken cancellationToken)
        {
            await _scopeManager.Invoke(GetDimensions(workStatusId), async () =>
            {
                using var dbContext = _databaseClient.CreateDbContext();
                var workStatus = await dbContext.ManagedWorkStatuses.FirstOrDefaultAsync(a => a.Id == workStatusId, cancellationToken);
                if (workStatus == null) return;
                workStatus.WorkId = continuedWorkId;
                workStatus.LastStatusUpdate = DateTimeOffset.Now;
                workStatus.State = state;
                await dbContext.SaveChangesAsync(cancellationToken);
            });
        }

        /// <inheritdoc/>
        public async Task<List<ManagedWorkStatusModel>> GetAllWorkStatusesAsync(CancellationToken cancellationToken)
        {
            return await _scopeManager.Invoke(GetDimensions(null), async () =>
            {
                using var dbContext = _databaseClient.CreateDbContext();
                return await dbContext.ManagedWorkStatuses.ToListAsync(cancellationToken);
            });
        }

        /// <inheritdoc/>
        public async Task<bool> IsAnyAsync(Expression<Func<ManagedWorkStatusModel, bool>> predicate, CancellationToken cancellationToken)
        {
            return await _scopeManager.Invoke(GetDimensions(null), async () =>
            {
                using var dbContext = _databaseClient.CreateDbContext();
                //We have to use linq any on the client side, as the cosmos provider for EF doesn't support it
                return (await dbContext.ManagedWorkStatuses
                    .Where(predicate)
                    .ToListAsync(cancellationToken))
                    .Any();
            });
        }

        /// <inheritdoc/>
        public async Task<List<ManagedWorkStatusModel>> GetWorkStatusesAsync(Expression<Func<ManagedWorkStatusModel, bool>> predicate, CancellationToken cancellationToken)
        {
            return await _scopeManager.Invoke(GetDimensions(null), async () =>
            {
                using var dbContext = _databaseClient.CreateDbContext();
                return await dbContext.ManagedWorkStatuses.Where(predicate).ToListAsync(cancellationToken);
            });
        }

        /// <inheritdoc/>
        public async Task RemoveWorkStatusesAsync(string[] workIds, CancellationToken cancellationToken)
        {
            await _scopeManager.Invoke(GetDimensions(null), async () =>
            {
                using var dbContext = _databaseClient.CreateDbContext();
                var workStatuses = await dbContext.ManagedWorkStatuses
                    .Where(a => workIds.Contains(a.Id))
                    .ToListAsync(cancellationToken);
                dbContext.RemoveRange(workStatuses);
                await dbContext.SaveChangesAsync(cancellationToken);
            });
        }

        private Dimensions GetDimensions(string workStatusId)
        {
            return new Dimensions
            {
                [StandardDimensions.DEPENDANCY_TYPE] = DependancyType.Database.ToString(),
                [StandardDimensions.DEPENDANCY] = _databaseClient.GetExternalSystemName(),
                [WORK_STATUS_ID_DIMENSION] = workStatusId
            };
        }
    }
}
