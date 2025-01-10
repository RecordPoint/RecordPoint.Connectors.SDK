namespace RecordPoint.Connectors.SDK.Observability
{
    /// <summary>
    /// The dimensions.
    /// </summary>
    public class Dimensions : Dictionary<string, string?>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Dimensions"/> class.
        /// </summary>
        public Dimensions() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Dimensions"/> class.
        /// </summary>
        /// <param name="collection">The collection.</param>
        public Dimensions(IEnumerable<KeyValuePair<string, string?>> collection) : base(collection) { }

        /// <summary>
        /// Convert dimensions to log scope state suitable for being passed to a logging BeingScope
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, object?> ToLogState()
        {
            return this.AsEnumerable()
                .ToDictionary(kp => kp.Key, kp => (object?)kp.Value);
        }
    }
}
