using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordPoint.Connectors.SDK.Providers
{
    public class CircuitBreakerOptions
    {
        /// <summary>
        /// 0.01 - 1
        /// What proportion of calls which throw Circuit Breaker exceptions have to 
        /// fail before the circuit is tripped. 0 would trip the circuit breaker 
        /// on the first failure, 1 would only trip the circuit breaker if 100% of the calls
        /// failed, 0.5 would trip the circuit breaker if 50% of the calls failed.
        /// </summary>
        public double FailureThreshold { get; set; } = 0.5;
        /// <summary>
        /// 2 - 2147483647 (Int 32.MaxValue)
        /// The number of attempts within a period which have to be made for a circuit to consider
        /// the number of failures to be count towards opening the circuit. For example, 
        /// if the MinimumAttempts is 10 and the failure threshold is 0.5, it takes 10 consecutive failed 
        /// attempts before the circuit breaker will trip, despite the failure threshold being 50%.
        /// PLEASE NOTE: Circuit breakers are per-service, services are spread across multiple
        /// nodes. Keep this in mind when determining a value for minimum attempts, as even with tens of
        /// thousands of submissions a day, we're still only likely to get single digit attempts on a 
        /// single node per 30s increment.
        /// </summary>
        public int MinimumAttempts { get; set; } = 10;
        /// <summary>
        /// 2 - 922337203685 (Timespan.MaxValue = ~ 922337203685.4s)
        /// How long a period to sample for failures in. The amount of calls which failed with
        /// a Circuit Breaker exception is averaged across this period of time, and if the proportion 
        /// of failed calls is greater than FailureThreshold, then the circuit breaks open.
        /// </summary>
        public long SamplingDurationS { get; set; } = 30;
        /// <summary>
        /// 1 - 922337203685 (Timespan.MaxValue = ~ 922337203685.4s)
        /// How long the circuit will remain open after breaking.
        /// </summary>
        public long DurationOfBreakS { get; set; } = 10;
    }
}
