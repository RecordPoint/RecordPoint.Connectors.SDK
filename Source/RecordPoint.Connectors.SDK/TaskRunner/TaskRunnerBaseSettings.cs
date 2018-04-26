using System;

namespace RecordPoint.Connectors.SDK.TaskRunner
{
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
        public int WaitTimePowBase = 3;

        /// <summary>
        /// The maximum amount of times that a task which has repeatedly thrown unhandled exceptions will be retried
        /// </summary>
        public int MaxUnhandledExceptions = 5;

        /// <summary>
        /// Gets a Timespan that indicates how long it will be until the Task is retried.
        /// With the default WaitTimeSecondsBase (2) and WaitTimePowBase (3) and MaxUnhandledExceptions (5):
        /// 0: 2s
        /// 1: 6s
        /// 2: 18s
        /// 3: 54s
        /// 4: 162s
        /// </summary>
        /// <param name="exceptionCount"></param>
        /// <returns></returns>
        public virtual TimeSpan GetWaitTime(int exceptionCount)
        {
            return TimeSpan.FromSeconds(WaitTimeSecondsBase * Math.Pow(WaitTimePowBase, exceptionCount));
        }
    }
}
