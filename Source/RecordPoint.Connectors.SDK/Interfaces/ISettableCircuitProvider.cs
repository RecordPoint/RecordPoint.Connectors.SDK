using System;

namespace RecordPoint.Connectors.SDK.Interfaces
{
    /// <summary>
    /// Interface to the Settable Circuit Provider.
    /// This is a special type of circuit breaker which is set manually when a problem is detected.
    /// To use:
    /// * Inject into dependant code as a singleton.
    /// * Call IsCircuitClosed before accessing protected resources.
    /// * If the protected resouce indicates it is under heavy load, call SetOpenUntil.
    /// </summary>
    public interface ISettableCircuitProvider : ICircuitProvider
    {
        /// <summary>
        /// Dependant code should call this when the protected resource is under heavy load.
        /// </summary>
        /// <param name="newTime">The time that the dependant code should wait until before attempting
        /// to use the resource again.</param>
        void SetOpenUntil(DateTime newTime);
    }
}
