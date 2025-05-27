using System;

namespace RecordPoint.Connectors.SDK.Observability.Null;

/// <summary>
/// The null observability scope.
/// </summary>
public class NullObservabilityScope : IDisposable
{
    private bool disposedValue;

    /// <summary>
    /// Inner Dispose
    /// </summary>
    /// <param name="disposing"></param>
    protected virtual void Dispose(bool disposing)
    {
        if (!disposedValue)
        {
            disposedValue = true;
        }
    }

    /// <summary>
    /// Dispose
    /// </summary>
    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
}
