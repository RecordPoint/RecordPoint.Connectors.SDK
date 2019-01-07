using System;
using System.Threading;
using System.Threading.Tasks;

namespace RecordPoint.Connectors.SDK.Helpers
{
    /// <summary>
    /// Provides helpers for dealing with exceptions.
    /// </summary>
    public static class ExceptionHelper
    {
        /// <summary>
        /// The C# HttpClient throws TaskCanceledExceptions for timeouts.
        /// This ensures that any TaskCanceledException is a genuine exception that
        /// has originated from a cancelled task (associated with the input CancellationToken)
        /// rather than a timeout.
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static bool IsTaskCancellation(this Exception ex, CancellationToken cancellationToken)
        {
            Exception exception = ex;
            if (exception is AggregateException aex)
            {
                var flattened = aex.Flatten();
                exception = flattened.InnerException;
            }

            if (exception is TaskCanceledException && cancellationToken.IsCancellationRequested)
            {
                return true;
            }

            return false;
        }
    }
}
