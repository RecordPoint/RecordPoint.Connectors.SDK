namespace RecordPoint.Connectors.SDK.Client
{
    public interface IAuthenticationProvider
    {
        void Initialize(AuthenticationHelperSettings settings);
        Task<AuthenticationResult> AcquireTokenAsync(AuthenticationHelperSettings settings);
    }
}
