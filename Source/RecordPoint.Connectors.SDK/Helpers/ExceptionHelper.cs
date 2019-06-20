using System;
using System.Linq;
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

        /// <summary>
        /// Determines if an exception is of a given type, or if it is an AggregateException
        /// that contains an exception of the given type.
        /// If the item is of the given type, it must also satisfy the condition specified
        /// in code
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsAssignableFrom<T>(this Exception ex, Func<T, bool> code)
            where T : Exception
        {
            var type = typeof(T);

            if (ex is AggregateException)
            {
                var ae = ex as AggregateException;
                ae = ae.Flatten();
                if (ae.InnerExceptions.All(ie => ie.IsAssignableFrom<T>(code)))
                {
                    return true;
                }
            }
            else if (type.IsAssignableFrom(ex.GetType()))
            {
                if (ex is T tex && tex != null)
                {
                    return code(tex);
                }
            }

            return false;
        }
    }
}
