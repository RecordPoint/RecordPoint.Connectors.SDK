namespace RecordPoint.Connectors.SDK.Client
{
    public interface IAuthenticationProvider
    {
        Task<AuthenticationResult> AcquireTokenAsync(AuthenticationHelperSettings settings);
    }
}
