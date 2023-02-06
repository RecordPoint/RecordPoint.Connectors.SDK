using Microsoft.Extensions.Logging;
using System.Collections.Immutable;

namespace RecordPoint.Connectors.SDK.Observability
{

    /// <summary>
    /// Observability scope manager. Used to keep track of key state for observability operations like logging, exception reporting
    /// etc. Replaces KeyProperties in the Logging namespace
    /// </summary>
    public interface IScopeManager
    {

        /// <summary>
        /// Get the observability dimensions for the current scope
        /// </summary>
        ImmutableDictionary<string, string> Dimensions { get; }

        /// <summary>
        /// Begin a new observability scope, local to the current async task
        /// </summary>
        /// <param name="dimensions">Observability dimensions to add to the new scope</param>
        /// <returns>Discardable scope</returns>
        IDisposable BeginScope(Dimensions dimensions);

        /// <summary>
        /// Get the logger this scope manages
        /// </summary>
        ILogger Logger { get; }


    }
}
