using System;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;

namespace RecordPoint.Connectors.SDK.Observability;

/// <summary>
/// Standard Observability Scope
/// </summary>
public class ObservabilityScope : IObservabilityScope
{
    private readonly AsyncLocal<ImmutableDictionary<string, string>> _dimensions = new();
    private readonly AsyncLocal<ImmutableDictionary<string, double>> _measures = new();

    /// <summary>
    /// Scope dimensions
    /// </summary>
    public ImmutableDictionary<string, string> Dimensions
    {
        get { return _dimensions.Value ?? ImmutableDictionary<string, string>.Empty; }
    }

    /// <summary>
    /// Scope dimensions
    /// </summary>
    public ImmutableDictionary<string, double> Measures
    {
        get { return _measures.Value ?? ImmutableDictionary<string, double>.Empty; }
    }

    /// <summary>
    /// Begins a new observability scope, local to the current async task.
    /// </summary>
    public IDisposable BeginScope(Dimensions dimensions = null, Measures measures = null)
    {
        var scope = new Scope(this, Dimensions, Measures);
        if (dimensions != null)
            _dimensions.Value = Dimensions.AddRange(dimensions.Where(kp => !Dimensions.ContainsKey(kp.Key)));
        if (measures != null)
            _measures.Value = Measures.AddRange(measures.Where(kp => !Measures.ContainsKey(kp.Key)));
        return scope;
    }

    private class Scope(ObservabilityScope observabilityScope, ImmutableDictionary<string, string> originalDimensions, ImmutableDictionary<string, double> originalMeasures) : IDisposable
    {
        private bool disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    observabilityScope._dimensions.Value = originalDimensions;
                    observabilityScope._measures.Value = originalMeasures;
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
