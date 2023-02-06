using Moq;
using System;
using System.Threading.Tasks;
using Xunit;
using Xunit.Sdk;

namespace RecordPoint.Connectors.SDK.Test.Helpers
{
    public static class TaskHelper
    {
        /// <summary>
        /// Awaits a task. Doesn't throw any exception if the 
        /// task was canceled.
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static async Task AwaitCompleteOrCancel(this Task t)
        {
            try
            {
                await t.ConfigureAwait(false);
            }
            catch (AggregateException aex)
            when (aex.InnerException is OperationCanceledException)
            {

            }
            catch (OperationCanceledException)
            {

            }
        }

        public static async Task WaitForVerification(this Task t, Action verificationCode, int maxTryCount = 10, bool waitForAllTries = false)
        {
            int tryCount = 0;
            while (true)
            {
                await t.EnsureIsStillRunning().ConfigureAwait(false);
                try
                {
                    verificationCode();

                    if (!waitForAllTries)
                    {
                        return;
                    }
                    else
                    {
                        if (tryCount == maxTryCount)
                        {
                            return;
                        }
                        else
                        {
                            tryCount++;
                            await Task.Delay(1000).ConfigureAwait(false);
                        }
                    }
                }
                catch (Exception e) when (e is MockException || e is TrueException || e is FalseException || e is EqualException)
                {
                    ++tryCount;
                    if (tryCount > maxTryCount)
                    {
                        throw;
                    }

                    await Task.Delay(1000).ConfigureAwait(false);
                }
            }
        }

        /// <summary>
        /// Ensures that the task is still running.
        /// If the task has faulted, the exception on the task will
        /// be thrown back up to the caller of this method.
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public static async Task EnsureIsStillRunning(this Task t)
        {
            if (t.IsFaulted)
            {
                await t.ConfigureAwait(false);
            }

            if (t.IsCompleted)
            {
                await t.ConfigureAwait(false);
                Assert.False(true, "Task has completed unexpectedly without an exception.");
            }
        }
    }
}
