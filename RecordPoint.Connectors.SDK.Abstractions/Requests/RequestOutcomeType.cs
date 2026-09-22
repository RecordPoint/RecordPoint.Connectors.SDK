namespace RecordPoint.Connectors.SDK.Requests
{
    /// <summary>The overall outcome of a request a connector processed.</summary>
    public enum RequestOutcomeType
    {
        /// <summary>The request completed successfully.</summary>
        Ok,

        /// <summary>The request failed. See <see cref="ConnectorRequestResponse.Messages"/> for why.</summary>
        Failed
    }
}
