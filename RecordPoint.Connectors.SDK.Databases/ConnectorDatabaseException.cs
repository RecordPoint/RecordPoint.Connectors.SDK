namespace RecordPoint.Connectors.SDK.Databases
{
    /// <summary>
    /// The connector database exception.
    /// </summary>
    public class ConnectorDatabaseException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConnectorDatabaseException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public ConnectorDatabaseException(string message) : base(message)
        {
        }
    }
}
