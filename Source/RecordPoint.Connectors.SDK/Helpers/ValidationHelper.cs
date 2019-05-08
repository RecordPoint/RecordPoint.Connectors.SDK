using System;
using System.Security;

namespace RecordPoint.Connectors.SDK.Helpers
{
    /// <summary>
    /// Provides helpers for validating arguments to methods.
    /// </summary>
    public static class ValidationHelper
    {
        /// <summary>
        /// Throws an exception if the tested string argument is null or a string that contains only whitespace.
        /// </summary>
        /// <exception cref="ArgumentNullException">The string value is null or contains only whitespace.</exception>
        /// <param name="argumentValue">The argument value to test.</param>
        /// <param name="argumentName">The name of the argument to test.</param>
        public static void ArgumentNotNullOrWhiteSpace(string argumentValue, string argumentName)
        {
            if (string.IsNullOrWhiteSpace(argumentValue))
            {
                throw new ArgumentNullException($"{argumentName} cannot be null, empty, or only whitespace.");
            }
        }

        /// <summary>
        /// Throws an exception if the tested string argument is null or an empty string.
        /// </summary>
        /// <exception cref="ArgumentNullException">The string value is null or empty.</exception>
        /// <param name="argumentValue">The argument value to test.</param>
        /// <param name="argumentName">The name of the argument to test.</param>
        public static void ArgumentNotNullOrEmpty(string argumentValue, string argumentName)
        {
            if (string.IsNullOrEmpty(argumentValue))
            {
                throw new ArgumentNullException($"{argumentName} cannot be null or empty.");
            }
        }

        /// <summary>
        /// Throws an exception if the tested SecureString argument is null or an empty string.
        /// </summary>
        /// <exception cref="ArgumentNullException">The string value is null.</exception>
        /// <param name="argumentValue">The argument value to test.</param>
        /// <param name="argumentName">The name of the argument to test.</param>
        public static void ArgumentNotNullOrEmpty(SecureString argumentValue, string argumentName)
        {
            if ((argumentValue == null) || (argumentValue.Length == 0))
            {
                throw new ArgumentNullException($"{argumentName} cannot be null or empty.");
            }
        }

        /// <summary>
        /// Throws <see cref="ArgumentNullException"/> if the given argument is null.
        /// </summary>
        /// <exception cref="ArgumentNullException">The value is null.</exception>
        /// <param name="argumentValue">The argument value to test.</param>
        /// <param name="argumentName">The name of the argument to test.</param>
        public static void ArgumentNotNull(object argumentValue, string argumentName)
        {
            if (argumentValue == null)
            {
                throw new ArgumentNullException(argumentName);
            }
        }
    }
}
