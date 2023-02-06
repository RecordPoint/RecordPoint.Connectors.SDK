using Microsoft.Extensions.Logging;
using RecordPoint.Connectors.SDK.Observability;
using RecordPoint.Connectors.SDK.Providers;
using RecordPoint.Connectors.SDK.Work;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RecordPoint.Connectors.SDK.Health
{

    /// <summary>
    /// Work item responsible for performing a health check
    /// </summary>
    public class HealthCheckOperation : WorkBase<object>
    {

        public const string HEALTH_CHECK_WORK = "Health Check";

        public const string OVERALL_HEALTH_NAME = "OverallHealth";
        public const string HEALTH_CHECK_FAULT_NAME = "HealthCheckFault";

        private readonly IHealthCheckStrategy[] _healthCheckStrategies;

        public override string WorkType => HEALTH_CHECK_WORK;

        /// <summary>
        /// Health check items
        /// </summary>
        public List<HealthCheckItem> HealthCheckItems { get; set; }

        /// <summary>
        /// Overall health level
        /// </summary>
        public HealthLevel HealthLevel =>
            (HealthLevel)(HealthCheckItems.Any() ? HealthCheckItems.Select(i => (int)i.HealthLevel).Max() : 0);

        public HealthCheckOperation(
            IEnumerable<IHealthCheckStrategy> healthCheckerStrategies,
            IScopeManager scopeManager, ILogger<HealthCheckOperation> logger,
            ITelemetryTracker telemetryTracker, IDateTimeProvider dateTimeProvider)
            : base(scopeManager, logger, telemetryTracker, dateTimeProvider)
        {
            _healthCheckStrategies = healthCheckerStrategies.ToArray();
        }

        protected async override Task InnerRunAsync(CancellationToken cancellationToken)
        {
            var strategies = _healthCheckStrategies;
            var items = new List<HealthCheckItem>();
            foreach (var strategy in strategies)
            {
                var strategyItems = await RunHealthCheckStrategyAsync(strategy, cancellationToken).ConfigureAwait(false);
                strategyItems.ForEach(i => i.HealthCheckType = strategy.HealthCheckType);
                items.AddRange(strategyItems);
                cancellationToken.ThrowIfCancellationRequested();
            }

            HealthCheckItems = items;

            var reason = "All health checks completed";
            await CompleteAsync(reason, cancellationToken);
        }

        private async Task<List<HealthCheckItem>> RunHealthCheckStrategyAsync(IHealthCheckStrategy strategy, CancellationToken cancellationToken)
        {
            List<HealthCheckItem> items;
            try
            {
                var dimensions = new Dimensions();
                items = await ScopeManager.InvokeAsync(dimensions, () =>
                {
                    return strategy.HealthCheckAsync(cancellationToken);
                });
            }
            catch (Exception ex)
            {
                // If an exception occurs don't fail the entire health check, just report a special health warning
                items = new List<HealthCheckItem>()
                {
                    new HealthCheckDimension()
                    {
                        HealthLevel = HealthLevel.Warning,
                        Name = HEALTH_CHECK_FAULT_NAME,
                        Value = ex.Message
                    }

                };
            }
            return items;
        }

        protected static string GetHealthCheckTelemetryName(HealthCheckItem healthCheckItem) =>
            $"{healthCheckItem.HealthCheckType}.{healthCheckItem.Name}";

        protected override Dimensions GetCustomResultDimensions()
        {
            var dimensions = base.GetCustomResultDimensions();
            dimensions.Add(OVERALL_HEALTH_NAME, $"{HealthLevel}");
            foreach (var item in HealthCheckItems.OfType<HealthCheckDimension>())
            {
                dimensions.Add(GetHealthCheckTelemetryName(item), item.Value);
            }
            return dimensions;
        }

        protected override Measures GetCoreResultMeasures()
        {
            var measures = base.GetCoreResultMeasures();
            foreach (var item in HealthCheckItems.OfType<HealthCheckMeasure>())
            {
                measures.Add(GetHealthCheckTelemetryName(item), item.Value);
            }
            return measures;
        }

        /// <summary>
        /// Grouping events by HealthLevel
        protected override void TrackFinish()
        {
            EnsureHasOutcome();

            var eventName = $"{WorkType}.{ResultType}";
            var dimensions = new Dimensions(GetCoreResultDimensions().Concat(GetCustomResultDimensions()));
            var measures = new Measures(GetCoreResultMeasures().Concat(GetCustomResultMeasures()));
            if (Exception == null)
            {
                // Track Normal 
                TrackEventByHealthLevel(eventName, HealthLevel.Normal);

                // Track Warning
                TrackEventByHealthLevel(eventName, HealthLevel.Warning);

                // Track Failure
                TrackEventByHealthLevel(eventName, HealthLevel.Failure);
            }
            else
            {
                TelemetryTracker.TrackException(eventName, Exception, dimensions, measures);
                Logger.LogEventException(eventName, Exception, dimensions, measures);
            }
        }

        private Dimensions GetCustomOutcomeDimensionsByHealthLevel(HealthLevel healthLevel)
        {
            var items = HealthCheckItems.OfType<HealthCheckDimension>().Where(x => x.HealthLevel == healthLevel);
            if (!items.Any()) return new Dimensions();

            var dimensions = base.GetCoreResultDimensions();

            dimensions.Add(OVERALL_HEALTH_NAME, $"{HealthLevel}");
            foreach (var item in items)
            {
                dimensions.Add(GetHealthCheckTelemetryName(item), item.Value);
            }
            return dimensions;
        }

        private Measures GetCustomOutcomeMeasuresByHealthLevel(HealthLevel healthLevel)
        {
            var items = HealthCheckItems.OfType<HealthCheckMeasure>().Where(x => x.HealthLevel == healthLevel);
            if (!items.Any()) return new Measures();

            var measures = base.GetCoreResultMeasures();
            foreach (var item in items)
            {
                measures.Add(GetHealthCheckTelemetryName(item), item.Value);
            }
            return measures;
        }

        /// <summary>
        /// Track event by health level
        /// </summary>
        /// <param name="eventName"></param>
        /// <param name="healthLevel"></param>
        private void TrackEventByHealthLevel(string eventName, HealthLevel healthLevel)
        {
            var dimensions = GetCustomOutcomeDimensionsByHealthLevel(healthLevel);
            var measures = GetCustomOutcomeMeasuresByHealthLevel(healthLevel);
            if ((dimensions == null || dimensions.Count == 0) && (measures == null || measures.Count == 0))
                return;

            var dimensionsCount = dimensions != null ? dimensions.Count : 0;
            var measuresCount = measures != null ? measures.Count : 0;
            var name = $"{eventName}.{healthLevel} - dimensions [{dimensionsCount}], measures [{measuresCount}]";

            TelemetryTracker.TrackEvent(name, dimensions, measures);
            Logger.LogEvent(name, dimensions, measures);
        }
    }
}
