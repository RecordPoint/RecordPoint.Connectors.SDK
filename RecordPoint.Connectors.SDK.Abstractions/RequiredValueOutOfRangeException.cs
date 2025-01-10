using System.Runtime.Serialization;

namespace RecordPoint.Connectors.SDK
{
    /// <summary>
    /// The required value out of range exception.
    /// </summary>
    public class RequiredValueOutOfRangeException : Exception
    {
        /// <summary>
        /// The NULL VALUE MESSAGE.
        /// </summary>
        private const string NULL_VALUE_MESSAGE = "Value {0} is out of range.";

        /// <summary>
        /// The param name.
        /// </summary>
        private readonly string? _paramName;
        /// <summary>
        /// Gets the param name.
        /// </summary>
        public virtual string? ParamName => _paramName;

        /// <summary>
        /// Initializes a new instance of the <see cref="RequiredValueOutOfRangeException"/> class.
        /// </summary>
        /// <param name="paramName">The param name.</param>
        public RequiredValueOutOfRangeException(string paramName)
            : base(string.Format(NULL_VALUE_MESSAGE, paramName))
        {
            _paramName = paramName;
        }
    }
}
