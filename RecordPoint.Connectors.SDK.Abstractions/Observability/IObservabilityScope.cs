using System.Collections.Immutable;

namespace RecordPoint.Connectors.SDK.Observability;


/// <summary>
/// Observability scope manager. Used to keep track of key state for observability operations like tracking events, traces, and exceptions
/// </summary>
public interface IObservabilityScope
{
    /// <summary>
    /// Get the observability dimensions for the current scope
    /// </summary>
    ImmutableDictionary<string, string?> Dimensions { get; }

    /// <summary>
    /// Get the observability measures for the current scope
    /// </summary>
    ImmutableDictionary<string, double> Measures { get; }

    /// <summary>
    /// Begin a new observability scope, local to the current async task
    /// </summary>
    /// <param name="dimensions">Observability dimensions to add to the new scope</param>
    /// <param name="measures">Observability measures to add to the new scope</param>
    /// <returns>Discardable scope</returns>
    IDisposable BeginScope(Dimensions? dimensions = null, Measures? measures = null);
}
