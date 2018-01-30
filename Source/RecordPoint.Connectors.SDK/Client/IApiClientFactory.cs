using System.Threading.Tasks;

namespace RecordPoint.Connectors.SDK.Client
{
    public interface IApiClientFactory
    {
        Task<IApiClient> CreateApiClientAsync(ApiClientFactorySettings settings);
    }
}
