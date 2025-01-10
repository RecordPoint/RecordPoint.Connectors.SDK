namespace RecordPoint.Connectors.SDK.Client
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAuthenticationProvider
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        Task<AuthenticationResult> AcquireTokenAsync(AuthenticationHelperSettings settings);
    }
}
