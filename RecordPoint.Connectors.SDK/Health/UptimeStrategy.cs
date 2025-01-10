using RecordPoint.Connectors.SDK.Providers;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RecordPoint.Connectors.SDK.Health
{
    /// <summary>
    /// Health Checker that determines how long the service has been running.
    /// </summary>
    public class UptimeStrategy : IHealthCheckStrategy
    {
        /// <summary>
        /// Health Check Type for Uptime.
        /// </summary>
        public const string HEALTH_CHECK_TYPE = "Uptime";

        /// <summary>
        /// Health Check Measure Type
        /// </summary>
        public const string UPTIME_SECONDS_NAME = "UptimeSeconds";

        private readonly IDateTimeProvider _dateTimeProvider;

        private readonly DateTimeOffset _startTime;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dateTimeProvider"></param>
        public UptimeStrategy(IDateTimeProvider dateTimeProvider)
        {
            _dateTimeProvider = dateTimeProvider;
            _startTime = _dateTimeProvider.UtcNow;
        }

        /// <summary>
        /// Health Check Type for Uptime.
        /// </summary>
        public string HealthCheckType => HEALTH_CHECK_TYPE;

        /// <summary>
        /// Produces a health check for the service uptime
        /// </summary>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>
        public Task<HealthCheckResult> HealthCheckAsync(CancellationToken stoppingToken)
        {
            return Task.FromResult(new HealthCheckResult
            {
                Measures = new List<HealthCheckMeasure> { new() { HealthCheckType = HEALTH_CHECK_TYPE, Name = UPTIME_SECONDS_NAME, Value = (_dateTimeProvider.UtcNow - _startTime).TotalSeconds } }
            });
        }
    }
}
