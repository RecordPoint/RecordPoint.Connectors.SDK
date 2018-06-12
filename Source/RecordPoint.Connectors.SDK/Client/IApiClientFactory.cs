using System.Threading.Tasks;

namespace RecordPoint.Connectors.SDK.Client
{
    public interface IApiClientFactory
    {
        IApiClient CreateApiClient(ApiClientFactorySettings settings);
        IAuthenticationHelper CreateAuthenticationHelper();
    }
}
