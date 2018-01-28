using System.Threading.Tasks;

namespace RecordPoint.Connectors.Client
{
    public interface IApiClientFactory
    {
        Task<IApiClient> CreateApiClientAsync(ApiClientFactorySettings settings);
    }
}
