using System.Runtime.Serialization;

namespace RecordPoint.Connectors.SDK.Databases
{
    [Serializable]
    public class ConnectorDatabaseException : Exception
    {
        public ConnectorDatabaseException(string message) : base(message)
        {
        }

        protected ConnectorDatabaseException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
