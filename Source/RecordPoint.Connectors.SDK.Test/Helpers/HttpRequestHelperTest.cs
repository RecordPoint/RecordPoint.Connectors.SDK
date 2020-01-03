using RecordPoint.Connectors.SDK.Client;
using RecordPoint.Connectors.SDK.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace RecordPoint.Connectors.SDK.Test.Helpers
{
    public class HttpRequestHelperTest
    {
        [Fact]
        public async Task AcquireToken_DoesNotThrowNullException()
        {
            // These are auth credentials from the AAD app "ConnectorSDKTest"
            var authSettings = new AuthenticationHelperSettings()
            {
                AuthenticationResource = "api://f7aabf18-3ed3-41f2-af6a-bc0ba572cc22",
                TenantDomainName = "recordpoint.com",
                ClientId = "f7aabf18-3ed3-41f2-af6a-bc0ba572cc22",
                ClientSecret = MakeSecureString("2thu0/=15oE@dIfS/15W2Z4o[=C319-6")
            };

            for(var i = 0; i < 3; i++)
            {
                var authHelper = new AuthenticationHelper();
                var authHeader = await authHelper.GetHttpRequestHeaders(authSettings);
                Assert.NotNull(authHeader["Authorization"]);
            }
        }

        private static SecureString MakeSecureString(string inputString)
        {
            var result = new SecureString();
            foreach (var ch in inputString)
            {
                result.AppendChar(ch);
            }
            return result;
        }
    }
}
