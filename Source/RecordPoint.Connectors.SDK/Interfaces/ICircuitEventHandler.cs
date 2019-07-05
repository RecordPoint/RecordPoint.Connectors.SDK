using System;

namespace RecordPoint.Connectors.SDK.Interfaces
{
    public interface ICircuitEventHandler
    {
        /// <summary>
        /// The handler of a circuit OnBreak event. 
        /// TimeSpan: The time for the handler to wait after the circuit being broken
        /// </summary>
        event EventHandler<TimeSpan> BreakEvent;
    }
}
