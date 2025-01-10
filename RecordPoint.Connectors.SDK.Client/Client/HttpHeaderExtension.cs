namespace RecordPoint.Connectors.SDK.Client
{
    /// <summary>
    /// The http header extension.
    /// </summary>
    public static class HttpHeaderExtension
    {
        /// <summary>
        /// The authorization header name.
        /// </summary>
        public const string AuthorizationHeaderName = "Authorization";

        /// <summary>
        /// Add authorization header.
        /// </summary>
        /// <param name="headers">The headers.</param>
        /// <param name="tokenType">The token type.</param>
        /// <param name="token">The token.</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void AddAuthorizationHeader(this Dictionary<string, List<string>> headers, string tokenType, string token)
        {
            if (headers == null)
            {
                throw new ArgumentNullException(nameof(headers));
            }
            headers[AuthorizationHeaderName] = new List<string>() { $"{tokenType} {token}" };
        }
    }
}
