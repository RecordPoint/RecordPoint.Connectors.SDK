using RecordPoint.Connectors.SDK.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RecordPoint.Connectors.SDK.Health
{
    /// <summary>
    /// Main front end for the health check component
    /// </summary>
    public class HealthCheckManager : IHealthCheckManager
    {
        private readonly IHealthCheckStrategy[] _healthCheckStrategies;
        private readonly IDateTimeProvider _dateTimeProvider;

        /// <summary>
        /// Health Check Fault dimension key
        /// </summary>
        public const string HEALTH_CHECK_FAULT_NAME = "HealthCheckFault";

        /// <inheritdoc />
        public HealthCheckResult HealthCheckResult { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="healthCheckStrategies"></param>
        /// <param name="dateTimeProvider"></param>
        public HealthCheckManager(IEnumerable<IHealthCheckStrategy> healthCheckStrategies, IDateTimeProvider dateTimeProvider)
        {
            _healthCheckStrategies = healthCheckStrategies.ToArray();
            _dateTimeProvider = dateTimeProvider;
        }

        /// <inheritdoc />
        public async Task RunHealthCheckAsync(CancellationToken cancellationToken)
        {

            var strategies = _healthCheckStrategies;
            var result = new HealthCheckResult
            {
                LastUpdate = _dateTimeProvider.UtcNow
            };
            foreach (var strategy in strategies)
            {
                var strategyResult = await RunHealthCheckStrategyAsync(strategy, cancellationToken).ConfigureAwait(false);
                strategyResult.Dimensions.ForEach(i => i.HealthCheckType = strategy.HealthCheckType);
                strategyResult.Measures.ForEach(i => i.HealthCheckType = strategy.HealthCheckType);
                result.Dimensions.AddRange(strategyResult.Dimensions);
                result.Measures.AddRange(strategyResult.Measures);
                cancellationToken.ThrowIfCancellationRequested();
            }

            HealthCheckResult = result;

        }

        private static async Task<HealthCheckResult> RunHealthCheckStrategyAsync(IHealthCheckStrategy strategy, CancellationToken cancellationToken)
        {
            HealthCheckResult result;
            try
            {
                return await strategy.HealthCheckAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                // If an exception occurs don't fail the entire health check, just report a special health warning
                result = new HealthCheckResult
                {
                    Dimensions = new List<HealthCheckDimension> { new() { HealthLevel = HealthLevel.Warning, Name = HEALTH_CHECK_FAULT_NAME, Value = ex.Message } }
                };
            }
            return result;
        }

    }
}
