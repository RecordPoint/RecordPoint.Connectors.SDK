using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RecordPoint.Connectors.SDK.Observability;
using RecordPoint.Connectors.SDK.Providers;
using RecordPoint.Connectors.SDK.Work;

namespace RecordPoint.Connectors.SDK.Databases
{

    public class PrepareDatabaseOperation<TDbContext, TDbProvider> : WorkBase<object>
        where TDbContext : DbContext
        where TDbProvider : IDatabaseProvider<TDbContext>
    {

        private readonly TDbProvider _databaseProvider;

        public const string PREPARE_DATABASE_WORK_TYPE = "Prepare Database";

        public PrepareDatabaseOperation(TDbProvider databaseProvider, IScopeManager scopeManager, ILogger logger, ITelemetryTracker telemetryTracker, IDateTimeProvider dateTimeProvider)
            : base(scopeManager, logger, telemetryTracker, dateTimeProvider)
        {
            _databaseProvider = databaseProvider;
        }

        public override string WorkType => PREPARE_DATABASE_WORK_TYPE;

        protected override async Task InnerRunAsync(CancellationToken cancellationToken)
        {
            await _databaseProvider.PrepareAsync(cancellationToken);
            await CompleteAsync("Database preparation complete", cancellationToken);
        }
    }
}
