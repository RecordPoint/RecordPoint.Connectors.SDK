using Microsoft.Extensions.Logging;
using RecordPoint.Connectors.SDK.Diagnostics;
using System;

namespace RecordPoint.Connectors.SDK.R365
{
    /// <summary>
    /// The SDK log adapter.
    /// </summary>
    public class SDKLogAdapter : ILog
    {

        /// <summary>
        /// The logger.
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        /// Instantiates a new SDKLogAdapter
        /// </summary>
        /// <param name="logger"></param>
        public SDKLogAdapter(ILogger<SDKLogAdapter> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Log the critical.
        /// </summary>
        /// <param name="callerType">The caller type.</param>
        /// <param name="methodName">The method name.</param>
        /// <param name="ex">The ex.</param>
        /// <param name="message">The message.</param>
        public void LogCritical(Type callerType, string methodName, Exception ex, string message = null)
        {
            _logger.LogCritical(ex, $"Connector SDK error CallerType[{callerType}] Method:[{methodName}] Message:[{message}]");
        }

        /// <summary>
        /// Log the error.
        /// </summary>
        /// <param name="callerType">The caller type.</param>
        /// <param name="methodName">The method name.</param>
        /// <param name="ex">The ex.</param>
        /// <param name="message">The message.</param>
        public void LogError(Type callerType, string methodName, Exception ex, string message = null)
        {
            _logger.LogError(ex, $"Error::Type:[{callerType}] Method:[{methodName}] Message:[{message}]");
        }

        /// <summary>
        /// Log the message.
        /// </summary>
        /// <param name="callerType">The caller type.</param>
        /// <param name="methodName">The method name.</param>
        /// <param name="message">The message.</param>
        /// <param name="elapsedTimeTicks">The elapsed time ticks.</param>
        public void LogMessage(Type callerType, string methodName, string message, long? elapsedTimeTicks = null)
        {
            _logger.LogInformation($"Information::Type:[{callerType}] Method:[{methodName}] Message:[{message}] ExecutedTime:[{elapsedTimeTicks ?? -1}]");
        }

        /// <summary>
        /// Log the verbose.
        /// </summary>
        /// <param name="callerType">The caller type.</param>
        /// <param name="methodName">The method name.</param>
        /// <param name="message">The message.</param>
        /// <param name="elapsedTimeTicks">The elapsed time ticks.</param>
        public void LogVerbose(Type callerType, string methodName, string message, long? elapsedTimeTicks = null)
        {
            _logger.LogTrace($"Verbose::Type:[{callerType}] Method:[{methodName}] Message:[{message}] ExecutedTime:[{elapsedTimeTicks ?? -1}]");
        }

        /// <summary>
        /// Log the warning.
        /// </summary>
        /// <param name="callerType">The caller type.</param>
        /// <param name="methodName">The method name.</param>
        /// <param name="message">The message.</param>
        /// <param name="elapsedTimeTicks">The elapsed time ticks.</param>
        public void LogWarning(Type callerType, string methodName, string message, long? elapsedTimeTicks = null)
        {
            _logger.LogWarning($"Warning::Type:[{callerType}] Method:[{methodName}] Message:[{message}] ExecutedTime:[{elapsedTimeTicks ?? -1}]");
        }
    }

}
