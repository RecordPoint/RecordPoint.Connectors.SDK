namespace RecordPoint.Connectors.SDK.Client
{
    public interface IApiClientFactory
    {
        IApiClient CreateApiClient(ApiClientFactorySettings settings);
        IAuthenticationProvider CreateAuthenticationProvider(AuthenticationHelperSettings settings);
    }
}
