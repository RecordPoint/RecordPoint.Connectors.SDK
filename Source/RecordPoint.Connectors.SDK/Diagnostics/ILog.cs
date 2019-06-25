﻿using System;

namespace RecordPoint.Connectors.SDK.Diagnostics
{
    /// <summary>
    /// Provides a generic log interface.
    /// </summary>
    public interface ILog
    {
        /// <summary>
        /// Logs a verbose message.
        /// Used for logging operations which are extremely numerous and are busness as usual operations
        /// </summary>
        /// <param name="callerType">The Type of the calling class (Use GetType() or typeof() depending on context)</param>
        /// <param name="methodName">The name of the method being timed. The name of the method will be appended with _Faulted if an exception occurs</param>
        /// <param name="message">Any message to be output. Generally, this should just be enough information to provide some context for the event. </param>
        /// <param name="elapsedTimeTicks">Optionally, provide the duration which the operation took in ticks.
        /// If a duration is not provided, no indication of the duration should occur in the final logging</param>
        void LogVerbose(Type callerType, string methodName, string message, long? elapsedTimeTicks = null);

        /// <summary>
        /// Logs a message.
        /// Used for logging relatively common operations which have succeeded (But are important enough that a record of their success is required
        /// or transient exceptions which are being retried and have not yet reached their maximum retries
        /// </summary>
        /// <param name="callerType">The Type of the calling class (Use GetType() or typeof() depending on context)</param>
        /// <param name="methodName">The name of the method being timed. The name of the method will be appended with _Faulted if an exception occurs</param>
        /// <param name="message">Any message to be output. Generally, this should just be enough information to provide some context for the event. </param>
        /// <param name="elapsedTimeTicks">Optionally, provide the duration which the operation took in ticks.
        /// If a duration is not provided, no indication of the duration should occur in the final logging</param>
        void LogMessage(Type callerType, string methodName, string message, long? elapsedTimeTicks = null);

        /// <summary>
        /// Logs a warning message.
        /// Used to indicate a fault of some sort that will not affect the overall service, most likely a handled exception
        /// </summary>
        /// <param name="callerType">The Type of the calling class (Use GetType() or typeof() depending on context)</param>
        /// <param name="methodName">The name of the method being timed. The name of the method will be appended with _Faulted if an exception occurs</param>
        /// <param name="message">Any message to be output. Generally, this should just be enough information to provide some context for the event. </param>
        /// <param name="elapsedTimeTicks">Optionally, provide the duration which the operation took in ticks.
        /// If a duration is not provided, no indication of the duration should occur in the final logging</param>
        void LogWarning(Type callerType, string methodName, string message, long? elapsedTimeTicks = null);

        /// <summary>
        /// Logs an error message.
        /// Used to indicate a significant fault of some sort, most likely an unhandled / unexpected exception 
        /// </summary>
        /// <param name="callerType">The Type of the calling class (Use GetType() or typeof() depending on context)</param>
        /// <param name="methodName">The name of the method being timed. The name of the method will be appended with _Faulted if an exception occurs</param>
        /// <param name="ex">The exception raised</param>
        /// <param name="message">Any message to be output. Generally, this should just be enough information to provide some context for the event,
        /// for Error / Critical events the fact than an exception of this type occurred at all may be more important than the context. If no message is 
        /// provided, only the exception should be logged</param>
        void LogError(Type callerType, string methodName, Exception ex, string message = null);

        /// <summary>
        /// Logs a critical message.
        /// Used to indicate a fault which has the potential to affect the overall service
        /// </summary>
        /// <param name="callerType">The Type of the calling class (Use GetType() or typeof() depending on context)</param>
        /// <param name="methodName">The name of the method being timed. The name of the method will be appended with _Faulted if an exception occurs</param>
        /// <param name="ex">The exception raised</param>
        /// <param name="message">Any message to be output. Generally, this should just be enough information to provide some context for the event,
        /// for Error / Critical events the fact than an exception of this type occurred at all may be more important than the context. If no message is 
        /// provided, only the exception should be logged</param>
        void LogCritical(Type callerType, string methodName, Exception ex, string message = null);
    }
}
