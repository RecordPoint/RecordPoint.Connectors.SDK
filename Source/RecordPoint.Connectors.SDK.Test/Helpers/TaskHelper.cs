using System;
using System.Threading.Tasks;

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
            catch (OperationCanceledException oce)
            {

            }
        }
    }
}
