using RecordPoint.Connectors.SDK.Providers;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RecordPoint.Connectors.SDK.Health
{
    /// <summary>
    /// Health Checker that just adds Run information to the health check
    /// </summary>
    public class RunHealthChecker : IHealthCheckStrategy
    {
        public const string RUN_HEALTH_CHECK_TYPE = "Run";

        public const string ID_NAME = "ID";
        public const string UPTIME_SECONDS_NAME = "UptimeSeconds";

        private readonly IDateTimeProvider _dateTimeProvider;

        private readonly string _runId;
        private readonly DateTimeOffset _startTime;


        public RunHealthChecker(IDateTimeProvider dateTimeProvider)
        {
            _dateTimeProvider = dateTimeProvider;

            _runId = Guid.NewGuid().ToString();
            _startTime = _dateTimeProvider.UtcNow;
        }

        public string HealthCheckType => RUN_HEALTH_CHECK_TYPE;

        public Task<List<HealthCheckItem>> HealthCheckAsync(CancellationToken stoppingToken)
        {
            return Task.FromResult(new List<HealthCheckItem>()
            {
                new HealthCheckDimension() { HealthCheckType = RUN_HEALTH_CHECK_TYPE, Name = ID_NAME, Value = _runId },
                new HealthCheckMeasure() { HealthCheckType = RUN_HEALTH_CHECK_TYPE, Name = UPTIME_SECONDS_NAME, Value = (_dateTimeProvider.UtcNow - _startTime).TotalSeconds}
            });
        }
    }
}
