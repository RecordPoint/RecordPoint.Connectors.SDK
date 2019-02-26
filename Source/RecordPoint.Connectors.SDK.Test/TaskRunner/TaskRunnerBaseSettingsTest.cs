using RecordPoint.Connectors.SDK.TaskRunner;
using System;
using Xunit;

namespace RecordPoint.Connectors.SDK.Test.TaskRunner
{
    public class TaskRunnerBaseSettingsTest
    {
        [Theory]
        [InlineData(1, 5, false)]
        [InlineData(10, 5, true)]
        [InlineData(2000000000000, 5, true)]
        public void WaitTimeIsDerivedFromTheGreaterOfMaxUnhandedExceptionsAndExceptionCount(long exceptionCount, int maxUnhandledExceptions, bool expectMaxOverride)
        {
            var settings = GetSettings(maxUnhandledExceptions);
            var waitTime = settings.GetWaitTime(exceptionCount);

            if (expectMaxOverride)
            {
                Assert.Equal(GetWaitTime(settings, maxUnhandledExceptions), waitTime);
            }
            else
            {
                Assert.Equal(GetWaitTime(settings, exceptionCount), waitTime);
            }
        }

        [Theory]
        [InlineData(1, false)]
        [InlineData(15, true)]
        [InlineData(31, true)]
        public void WaitTimeIsDerivedFromTheGreaterOfWaitTimeAndMaxWaitTime(long exceptionCount, bool expectMaxOverride)
        {
            var settings = GetSettings(30);
            var waitTime = settings.GetWaitTime(exceptionCount);

            if (expectMaxOverride)
            {
                Assert.Equal(settings.MaxWaitTime, waitTime);
            }
            else
            {
                Assert.Equal(GetWaitTime(settings, exceptionCount), waitTime);
            }
        }

        private TaskRunnerBaseSettings GetSettings(int maxUnhandedExceptions, int maxWaitTimeHours = 1)
        {
            return new TaskRunnerBaseSettings()
            {
                WaitTimePowBase = 2,
                WaitTimeSecondsBase = 1,
                MaxUnhandledExceptions = maxUnhandedExceptions,
                MaxWaitTime = TimeSpan.FromHours(maxWaitTimeHours)
            };
        }

        // Mirrors the function currently used in TaskRunnerBaseSettings
        private TimeSpan GetWaitTime(TaskRunnerBaseSettings settings, long exceptionCount)
        {
            return TimeSpan.FromSeconds(settings.WaitTimeSecondsBase * Math.Pow(settings.WaitTimePowBase, exceptionCount));
        }
    }
}
