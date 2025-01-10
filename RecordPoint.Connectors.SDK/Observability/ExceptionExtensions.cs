using System;
using System.Collections.Immutable;

namespace RecordPoint.Connectors.SDK.Observability
{
    /// <summary>
    /// Observability exception extensions
    /// </summary>
    /// <remarks>
    /// Adds methods used to decorate exceptions with information used by the observability infrastructure
    /// </remarks>
    public static class ExceptionExtensions
    {

        /// <summary>
        /// Data properties
        /// </summary>
        /// 
        public const string HAS_SCOPE_PROPERTY = "HasScope";
        /// <summary>
        /// 
        /// </summary>
        public const string DIMENSIONS_PROPERTY = "Dimensions";
        /// <summary>
        /// 
        /// </summary>
        public const string LOG_MESSAGE_PROPERTY = "LogMessage";

        /// <summary>
        /// Get observability dimensions for an exception
        /// </summary>
        /// <param name="ex"></param>
        /// <returns>Key properties, empty by default</returns>
        public static ImmutableDictionary<string, string> GetDimensions(this Exception ex)
        {
            if (!ex.Data.Contains(DIMENSIONS_PROPERTY))
                ex.Data[DIMENSIONS_PROPERTY] = ImmutableDictionary<string, string>.Empty;
            return (ImmutableDictionary<string, string>)ex.Data[DIMENSIONS_PROPERTY];
        }

        /// <summary>
        /// Was this exception associated with an observability scope
        /// </summary>
        /// <param name="ex">Exception to check</param>
        /// <returns>True if the  Key properties, empty by default</returns>
        public static bool HasScope(this Exception ex)
        {
            if (!ex.Data.Contains(HAS_SCOPE_PROPERTY))
                return false;
            return (bool)ex.Data[HAS_SCOPE_PROPERTY];
        }

        /// <summary>
        /// Scope this exception to a given scope tracker
        /// </summary>
        /// <param name="ex">Exception to update</param>
        /// <param name="scopeManager">Key properties to set</param>
        /// <remarks>
        /// This method is used to attach information about the observability scope to the exception. This must be called within that
        /// observability scope if the exception is not tracked within that observability scope.
        /// 
        /// The standard observability patterns ensure that this is done.
        /// </remarks>
        public static void ScopeTo(this Exception ex, IScopeManager scopeManager)
        {
            if (HasScope(ex))
                return;
            ex.Data[DIMENSIONS_PROPERTY] = scopeManager.Dimensions;
            ex.Data[HAS_SCOPE_PROPERTY] = true;
        }

        /// <summary>
        /// Get the log message set for this exception
        /// </summary>
        /// <param name="ex">Owning exception</param>
        /// <returns>Log message, null if not set</returns>
        public static string GetLogMessage(this Exception ex)
        {
            if (!ex.Data.Contains(LOG_MESSAGE_PROPERTY))
                return null;
            return (string)ex.Data[LOG_MESSAGE_PROPERTY];
        }

        /// <summary>
        /// Set the log message for this exception
        /// </summary>
        /// <param name="ex">Exception to update</param>
        /// <param name="message">Message to set</param>
        public static void SetLogMessage(this Exception ex, string message)
        {
            ex.Data[LOG_MESSAGE_PROPERTY] = message;
        }

        /// <summary>
        /// Set a log message if none is set, otherwise leave the existing value
        /// </summary>
        /// <param name="ex">Exception to update</param>
        /// <param name="message">Message to set</param>
        public static void EnsureLogMessage(this Exception ex, string message)
        {
            if (!ex.Data.Contains(LOG_MESSAGE_PROPERTY))
                ex.Data[LOG_MESSAGE_PROPERTY] = message;
        }

    }

}
