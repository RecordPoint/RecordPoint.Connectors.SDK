using System;

namespace RecordPoint.Connectors.SDK.Diagnostics
{
    /// <summary>
    /// Provides a generic log interface.
    /// </summary>
    public interface ILog
    {
        /// <summary>
        /// Logs a verbose message.
        /// </summary>
        /// <param name="callerType"></param>
        /// <param name="methodName"></param>
        /// <param name="message"></param>
        void LogVerbose(Type callerType, string methodName, string message);

        /// <summary>
        /// Logs a message.
        /// </summary>
        /// <param name="callerType"></param>
        /// <param name="methodName"></param>
        /// <param name="message"></param>
        void LogMessage(Type callerType, string methodName, string message);

        /// <summary>
        /// Logs a warning message.
        /// </summary>
        /// <param name="callerType"></param>
        /// <param name="methodName"></param>
        /// <param name="message"></param>
        void LogWarning(Type callerType, string methodName, string message);

        /// <summary>
        /// Logs an error message.
        /// </summary>
        /// <param name="callerType"></param>
        /// <param name="methodName"></param>
        /// <param name="ex"></param>
        void LogError(Type callerType, string methodName, Exception ex);

        /// <summary>
        /// Logs a critical message.
        /// </summary>
        /// <param name="callerType"></param>
        /// <param name="methodName"></param>
        /// <param name="ex"></param>
        void LogCritical(Type callerType, string methodName, Exception ex);

        /// <summary>
        /// Logs a timing event with the elapsed time in ticks.
        /// </summary>
        /// <param name="callerType"></param>
        /// <param name="methodName"></param>
        /// <param name="elapsedTimeTicks"></param>
        /// <param name="message"></param>
        void LogTimingEventTicks(Type callerType, string methodName, long elapsedTimeTicks, string message);
    }
}
