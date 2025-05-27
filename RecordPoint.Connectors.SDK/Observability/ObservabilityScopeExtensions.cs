using System;
using System.Threading.Tasks;

namespace RecordPoint.Connectors.SDK.Observability
{

    /// <summary>
    /// Observability Scope extension methods
    /// </summary>
    public static class ObservabilityScopeExtensions
    {

        /// <summary>
        /// Invoke an action within a observability scope + ensuring that all exceptions are properly decorated
        /// </summary>
        /// <param name="observabilityScope">Target scope manager</param>
        /// <param name="dimensions">Dimensions to extend the scope with</param>
        /// <param name="action">Action to invoke</param>
        public static void Invoke(this IObservabilityScope observabilityScope, Dimensions dimensions, Action action)
        {
            using var scope = observabilityScope.BeginScope(dimensions);
            try
            {
                action();
            }
            catch (Exception ex)
            {
                ex.ScopeTo(observabilityScope);
                throw;
            }
        }

        /// <summary>
        /// Invoke an action within a observability scope + ensuring that all exceptions are properly decorated
        /// </summary>
        /// <param name="observabilityScope">Target scope manager</param>
        /// <param name="dimensions">Dimensions to extend the scope with</param>
        /// <param name="func">Action to invoke</param>
        /// <typeparam name="T">Return type</typeparam>
        public static T Invoke<T>(this IObservabilityScope observabilityScope, Dimensions dimensions, Func<T> func)
        {
            using var scope = observabilityScope.BeginScope(dimensions);
            try
            {
                return func();
            }
            catch (Exception ex)
            {
                ex.ScopeTo(observabilityScope);
                throw;
            }
        }

        /// <summary>
        /// Invoke an action within a observability scope + ensuring that all exceptions are properly decorated
        /// </summary>
        /// <param name="observabilityScope">Target scope manager</param>
        /// <param name="dimensions">Dimensions to extend the scope with</param>
        /// <param name="action">Action to invoke</param>
        public async static Task InvokeAsync(this IObservabilityScope observabilityScope, Dimensions dimensions, Func<Task> action)
        {
            using var scope = observabilityScope.BeginScope(dimensions);
            try
            {
                await action().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                ex.ScopeTo(observabilityScope);
                throw;
            }
        }

        /// <summary>
        /// Invoke a function so that key properties are attached to log messages and any
        /// exceptions raised during execution
        /// </summary>
        /// <param name="observabilityScope">Scope manager</param>
        /// <param name="dimensions">Logger</param>
        /// <param name="func">Action to invoke</param>
        /// <typeparam name="T">Return type</typeparam>
        public async static Task<T> InvokeAsync<T>(this IObservabilityScope observabilityScope, Dimensions dimensions, Func<Task<T>> func)
        {
            using var scope = observabilityScope.BeginScope(dimensions);
            try
            {
                return await func().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                ex.ScopeTo(observabilityScope);
                throw;
            }
        }
    }
}
