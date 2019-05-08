using System;

namespace RecordPoint.Connectors.SDK.TaskRunner
{
    /// <summary>
    /// Defines the behaviour of a class derived from TaskRunnerBase in situations where long-running tasks
    /// fault in either a one-off or a consistent fashion
    /// </summary>
    public class TaskRunnerBaseSettings
    {
        /// <summary>
        /// The amount of time allowed for a task to fail with an unhandled error.
        /// If a task fails within this time repeatedly, there is likely something wrong.
        /// </summary>
        public TimeSpan DefaultRepeatedTaskFailureTime = TimeSpan.FromSeconds(3);

        /// <summary>
        /// The minimum wait time before a task with an unhandled exception will be tried again
        /// </summary>
        public double WaitTimeSecondsBase = 2;

        /// <summary>
        /// The number used as a base (Will be raised using the exception count as a power and multipled by the WaitTimeSecondsBase)
        /// </summary>
        public int WaitTimePowBase = 2;

        /// <summary>
        /// The maximum amount of times that a task which has repeatedly thrown unhandled exceptions will be retried
        /// </summary>
        public int MaxUnhandledExceptions = 10;

        /// <summary>
        /// The maximum waitTime allowed before a task is retried.
        /// </summary>
        public TimeSpan MaxWaitTime = TimeSpan.FromHours(1);

        /// <summary>
        /// Gets a Timespan that indicates how long it will be until the Task is retried.
        /// With the default WaitTimeSecondsBase (2) and WaitTimePowBase (2) and MaxUnhandledExceptions (10):
        /// 0: 4s
        /// 1: 8s
        /// 2: 16s
        /// 3: 32s
        /// 4: 64s
        /// 5: 128s
        /// 6: 256s
        /// 7: 512s
        /// 8: 1024s
        /// 9: 2048s
        /// 10: 4096s
        /// </summary>
        /// <param name="exceptionCount"></param>
        /// <returns></returns>
        public virtual TimeSpan GetWaitTime(long exceptionCount)
        {
            // If the exceptionCount is more than the MaxUnhandledExceptions, use the wait time from the maximum unhandled exceptions instead
            // of using the unhandled exceptionCount - We're about to use the exceptionCount as an exponent and can't let the exceptionCount
            // grow  without bounds. The user should ensure that combination of WaitTimeSecondsBase / WaitTimePowBase / MaxUnhandledExceptions
            // cannot exceed TimeSpan.MaxValue: https://docs.microsoft.com/en-us/dotnet/api/system.timespan.maxvalue
            var restrictedExceptionCount = exceptionCount > MaxUnhandledExceptions ? MaxUnhandledExceptions : exceptionCount; 

            var waitTime = TimeSpan.FromSeconds(WaitTimeSecondsBase * Math.Pow(WaitTimePowBase, restrictedExceptionCount));
            // The MaxWaitTime is respected regardless of whether or not we've overridden the exceptionCount
            if (waitTime > MaxWaitTime){
                return MaxWaitTime;
            }
            return waitTime;
        }
    }
}
