using System.Runtime.Serialization;

namespace RecordPoint.Connectors.SDK
{
    [Serializable]
    public class RequiredValueNullException : Exception
    {
        private const string NULL_VALUE_MESSAGE = "Value {0} cannot be null.";

        private readonly string? _paramName;
        public virtual string? ParamName => _paramName;

        public RequiredValueNullException(string paramName)
            : base(string.Format(NULL_VALUE_MESSAGE, paramName))
        {
            _paramName = paramName;
        }

        protected RequiredValueNullException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public static void ThrowIfNull(object? value, string paramName)
        {
            if (value == null)
                throw new RequiredValueNullException(paramName);
        }

        public static void ThrowIfNullOrEmpty(string? value, string paramName)
        {
            if (string.IsNullOrEmpty(value))
                throw new RequiredValueNullException(paramName);
        }
    }
}
