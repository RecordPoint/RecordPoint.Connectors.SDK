using System;

namespace RecordPoint.Connectors.SDK.Interfaces
{
    public interface ICircuitProvider
    {
        bool IsCircuitClosed(out TimeSpan waitFor);
    }
}
