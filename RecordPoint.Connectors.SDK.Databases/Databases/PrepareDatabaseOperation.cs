using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RecordPoint.Connectors.SDK.Observability;
using RecordPoint.Connectors.SDK.Providers;
using RecordPoint.Connectors.SDK.Work;

namespace RecordPoint.Connectors.SDK.Databases
{

    /// <summary>
    /// The prepare database operation.
    /// </summary>
    /// <typeparam name="TDbContext"/>
    /// <typeparam name="TDbProvider"/>
    public class PrepareDatabaseOperation<TDbContext, TDbProvider> : WorkBase<object>
        where TDbContext : DbContext
        where TDbProvider : IDatabaseProvider<TDbContext>
    {

        /// <summary>
        /// The database provider.
        /// </summary>
        private readonly TDbProvider _databaseProvider;

        /// <summary>
        /// PREPARE DATABASE WORK TYPE.
        /// </summary>
        public const string PREPARE_DATABASE_WORK_TYPE = "Prepare Database";

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <param name="databaseProvider">The database provider.</param>
        /// <param name="scopeManager">The scope manager.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="telemetryTracker">The telemetry tracker.</param>
        /// <param name="dateTimeProvider">The date time provider.</param>
        public PrepareDatabaseOperation(TDbProvider databaseProvider, IScopeManager scopeManager, ILogger logger, ITelemetryTracker telemetryTracker, IDateTimeProvider dateTimeProvider)
            : base(scopeManager, logger, telemetryTracker, dateTimeProvider)
        {
            _databaseProvider = databaseProvider;
        }

        /// <summary>
        /// Gets the work type.
        /// </summary>
        public override string WorkType => PREPARE_DATABASE_WORK_TYPE;

        /// <summary>
        /// Inner the run asynchronously.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A Task</returns>
        protected override async Task InnerRunAsync(CancellationToken cancellationToken)
        {
            await _databaseProvider.PrepareAsync(cancellationToken);
            await CompleteAsync("Database preparation complete", cancellationToken);
        }
    }
}
