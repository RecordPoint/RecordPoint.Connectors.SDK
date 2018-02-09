using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System.Threading.Tasks;

namespace RecordPoint.Connectors.SDK.Client
{
    public interface IAuthenticationHelper
    {
        Task<AuthenticationResult> AcquireTokenAsync(AuthenticationHelperSettings settings, bool useTokenCache = true);
    }
}
