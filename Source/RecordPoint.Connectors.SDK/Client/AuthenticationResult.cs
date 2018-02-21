namespace RecordPoint.Connectors.SDK.Client
{
    public class AuthenticationResult
    {
        /// <summary>
        /// The type of the access token, e.g., "Bearer".
        /// </summary>
        public string AccessTokenType { get; set; }

        /// <summary>
        /// The access token.
        /// </summary>
        public string AccessToken { get; set; }
    }
}
