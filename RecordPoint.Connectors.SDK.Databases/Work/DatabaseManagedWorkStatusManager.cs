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
        /// <summary>
        /// Telemetry dimension name for the managed work status identifier.
        /// </summary>
        public const string WORK_STATUS_ID_DIMENSION = "WorkStatusId";

        private readonly IConnectorDatabaseClient _databaseClient;
        private readonly IObservabilityScope _observabilityScope;

        /// <summary>
        /// Initializes a new instance of the <see cref="DatabaseManagedWorkStatusManager"/> class.
        /// </summary>
        /// <param name="databaseClient">Database client used to read managed work statuses.</param>
        /// <param name="observabilityScope">Observability scope used for dependency telemetry.</param>
        public DatabaseManagedWorkStatusManager(
            IConnectorDatabaseClient databaseClient,
            IObservabilityScope observabilityScope)
        {
            _databaseClient = databaseClient;
            _observabilityScope = observabilityScope;
        }

        /// <inheritdoc/>
        public async Task<List<ManagedWorkStatusModel>> GetWorkStatusesAsync(Expression<Func<ManagedWorkStatusModel, bool>> predicate, CancellationToken cancellationToken)
        {
            return await _observabilityScope.Invoke(GetDimensions(null), async () =>
            {
                using var dbContext = _databaseClient.CreateDbContext();
                return await dbContext.ManagedWorkStatuses.Where(predicate).ToListAsync(cancellationToken);
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
