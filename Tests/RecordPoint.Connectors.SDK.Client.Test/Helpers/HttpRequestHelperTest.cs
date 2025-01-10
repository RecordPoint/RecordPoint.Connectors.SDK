namespace RecordPoint.Connectors.SDK.Test.Helpers
{
    public class HttpRequestHelperTest
    {
        //This test has been commented out as it relies on an external resource and uses hardcoded client ids which is not practical for a unit test.

        //[Fact(Skip = "Dependent on external resource")]
        //public async Task AcquireToken_DoesNotThrowNullException()
        //{
        //    // These are auth credentials from the AAD app "ConnectorSDKTest"
        //    var authSettings = new AuthenticationHelperSettings()
        //    {
        //        AuthenticationResource = "api://f7aabf18-3ed3-41f2-af6a-bc0ba572cc22",
        //        TenantDomainName = "recordpoint.com",
        //        ClientId = "f7aabf18-3ed3-41f2-af6a-bc0ba572cc22",
        //        ClientSecret = MakeSecureString("2thu0/=15oE@dIfS/15W2Z4o[=C319-6")
        //    };

        //    var authHelper = new ConfidentialClientAuthenticationProvider();
        //    authHelper.Initialize(authSettings);
        //    for (var i = 0; i < 3; i++)
        //    {

        //        var authHeader = await authHelper.GetHttpRequestHeaders(authSettings);
        //        Assert.NotNull(authHeader["Authorization"]);
        //    }
        //}

        //[Fact(Skip = "Dependent on external resource")]
        //public async Task AcquireToken_GetsTenantedToken()
        //{
        //    // These are auth credentials from the AAD app "ConnectorSDKTest"
        //    var authSettings = new AuthenticationHelperSettings()
        //    {
        //        AuthenticationResource = "api://f7aabf18-3ed3-41f2-af6a-bc0ba572cc22",
        //        TenantDomainName = "recordpoint.com",
        //        ClientId = "f7aabf18-3ed3-41f2-af6a-bc0ba572cc22",
        //        ClientSecret = MakeSecureString("2thu0/=15oE@dIfS/15W2Z4o[=C319-6")
        //    };

        //    var authHelper = new ConfidentialClientAuthenticationProvider();
        //    authHelper.Initialize(authSettings);
        //    var tenants = new[] { "recordpoint.com", "solrwinds.onmicrosoft.com", "recordpoint.com" };
        //    authSettings.TenantDomainName = tenants[0];
        //    var authHeader1 = await authHelper.GetHttpRequestHeaders(authSettings);
        //    authSettings.TenantDomainName = tenants[1];
        //    var authHeader2 = await authHelper.GetHttpRequestHeaders(authSettings);
        //    authSettings.TenantDomainName = tenants[2];
        //    var authHeader3 = await authHelper.GetHttpRequestHeaders(authSettings);

        //    Assert.Equal(authHeader1, authHeader3);
        //    Assert.NotEqual(authHeader1, authHeader2);
        //    Assert.NotEqual(authHeader1, authHeader2);
        //}

        //private static SecureString MakeSecureString(string inputString)
        //{
        //    var result = new SecureString();
        //    foreach (var ch in inputString)
        //    {
        //        result.AppendChar(ch);
        //    }
        //    return result;
        //}
    }
}
