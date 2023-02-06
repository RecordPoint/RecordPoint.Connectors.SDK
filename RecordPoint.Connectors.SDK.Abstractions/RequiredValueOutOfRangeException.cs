using System.Runtime.Serialization;

namespace RecordPoint.Connectors.SDK
{
    [Serializable]
    public class RequiredValueOutOfRangeException : Exception
    {
        private const string NULL_VALUE_MESSAGE = "Value {0} is out of range.";

        private readonly string? _paramName;
        public virtual string? ParamName => _paramName;

        public RequiredValueOutOfRangeException(string paramName)
            : base(string.Format(NULL_VALUE_MESSAGE, paramName))
        {
            _paramName = paramName;
        }

        protected RequiredValueOutOfRangeException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
