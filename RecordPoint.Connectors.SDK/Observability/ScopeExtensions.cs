using System;
using System.Threading.Tasks;

namespace RecordPoint.Connectors.SDK.Observability
{

    /// <summary>
    /// Scope extension methods
    /// </summary>
    public static class ScopeExtensions
    {

        /// <summary>
        /// Invoke an action within a observability scope + ensuring that all exceptions are properly decorated
        /// </summary>
        /// <param name="scopeManager">Target scope manager</param>
        /// <param name="dimensions">Dimensions to extend the scope with</param>
        /// <param name="action">Action to invoke</param>
        public static void Invoke(this IScopeManager scopeManager, Dimensions dimensions, Action action)
        {
            using var scope = scopeManager.BeginScope(dimensions);
            try
            {
                action();
            }
            catch (Exception ex)
            {
                ex.ScopeTo(scopeManager);
                throw;
            }
        }

        /// <summary>
        /// Invoke an action within a observability scope + ensuring that all exceptions are properly decorated
        /// </summary>
        /// <param name="scopeManager">Target scope manager</param>
        /// <param name="dimensions">Dimensions to extend the scope with</param>
        /// <param name="func">Action to invoke</param>
        /// <typeparam name="T">Return type</typeparam>
        public static T Invoke<T>(this IScopeManager scopeManager, Dimensions dimensions, Func<T> func)
        {
            using var scope = scopeManager.BeginScope(dimensions);
            try
            {
                return func();
            }
            catch (Exception ex)
            {
                ex.ScopeTo(scopeManager);
                throw;
            }
        }

        /// <summary>
        /// Invoke an action within a observability scope + ensuring that all exceptions are properly decorated
        /// </summary>
        /// <param name="scopeManager">Target scope manager</param>
        /// <param name="dimensions">Dimensions to extend the scope with</param>
        /// <param name="action">Action to invoke</param>
        public async static Task InvokeAsync(this IScopeManager scopeManager, Dimensions dimensions, Func<Task> action)
        {
            using var scope = scopeManager.BeginScope(dimensions);
            try
            {
                await action().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                ex.ScopeTo(scopeManager);
                throw;
            }
        }

        /// <summary>
        /// Invoke a function so that key properties are attached to log messages and any
        /// exceptions raised during execution
        /// </summary>
        /// <param name="logger">Logger</param>
        /// <param name="keyProperties">Key properties to attach to all log message</param>
        /// <param name="func">Action to invoke</param>
        /// <typeparam name="T">Return type</typeparam>
        public async static Task<T> InvokeAsync<T>(this IScopeManager scopeManager, Dimensions dimensions, Func<Task<T>> func)
        {
            using var scope = scopeManager.BeginScope(dimensions);
            try
            {
                return await func().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                ex.ScopeTo(scopeManager);
                throw;
            }
        }
    }
}
