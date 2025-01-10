namespace RecordPoint.Connectors.SDK.Client
{
    /// <summary>
    /// 
    /// </summary>
    public interface IApiClientFactory
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        IApiClient CreateApiClient(ApiClientFactorySettings settings);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        IAuthenticationProvider CreateAuthenticationProvider(AuthenticationHelperSettings settings);
    }
}
