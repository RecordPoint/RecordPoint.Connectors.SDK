namespace RecordPoint.Connectors.SDK.Client
{
    /// <summary>
    /// The required value null exception.
    /// </summary>
    public class RequiredValueNullException : Exception
    {
        /// <summary>
        /// The NULL VALUE MESSAGE.
        /// </summary>
        private const string NULL_VALUE_MESSAGE = "Value {0} cannot be null.";

        /// <summary>
        /// The param name.
        /// </summary>
        private readonly string _paramName;
        /// <summary>
        /// Gets the param name.
        /// </summary>
        public virtual string ParamName => _paramName;

        /// <summary>
        /// Initializes a new instance of the <see cref="RequiredValueNullException"/> class.
        /// </summary>
        /// <param name="paramName">The param name.</param>
        public RequiredValueNullException(string paramName)
            : base(string.Format(NULL_VALUE_MESSAGE, paramName))
        {
            _paramName = paramName;
        }
    }
}
