using System;

namespace RecordPoint.Connectors.SDK.Interfaces
{
    /// <summary>
    /// Should not be used directly, as this restricts our ability to do an IoC injection of a CircuitProvider.
    /// We may need the ability to have two different CircuitProviders in a single service (e.g. one for PG, one for ASB)
    /// and services may wish to react to these CircuitProviders in different fashions
    /// Instead, should create an empty interface that inherits from this, e.g. IPostgresqlCircuitProvider
    /// </summary>
    public interface ICircuitProvider
    {
        /// <summary>
        /// Returns the state of this Circuit. Can be referenced to understand whether or not a long-running operation
        /// (e.g. queue processing or an ASB message pump) should be enabled and attempt operations which will interact with the 
        /// resource the circuit protects.
        /// Additionally, outputs a recomended time to wait for before retrying.
        /// </summary>
        /// <returns>True if the circuit is closed (the resource can be used)
        /// False if the circuit is open (you should wait before using the resource)</returns>
        bool IsCircuitClosed(out TimeSpan waitFor);
    }
}
