namespace RecordPoint.Connectors.SDK.Observability
{
    /// <summary>
    /// The measures.
    /// </summary>
    public class Measures : Dictionary<string, double>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Measures"/> class.
        /// </summary>
        public Measures() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Measures"/> class.
        /// </summary>
        /// <param name="collection">The collection.</param>
        public Measures(IEnumerable<KeyValuePair<string, double>> collection) : base(collection) { }
    }
}
