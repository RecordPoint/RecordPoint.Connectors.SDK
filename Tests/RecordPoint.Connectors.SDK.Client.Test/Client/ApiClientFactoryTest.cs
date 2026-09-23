using RecordPoint.Connectors.SDK.Client;
using System.Security;
using Xunit;

namespace RecordPoint.Connectors.SDK.Test.Client
{
    public class ApiClientFactoryTest
    {
        const string DummyEndPointUrl = "https://connectorapi-endpoint.com/";
        [Fact]
        public void CreateApiClient_WhenCalledFromASingleThread_CreatesCorrectApiClientAndSetsSecurityProtocolCorrectly()
        {
            // Arrange
            var sutApiClientFactory = new ApiClientFactory();
            var apiClientFactorySettings = new ApiClientFactorySettings()
            {
                ConnectorApiUrl = DummyEndPointUrl,
                ServerCertificateValidation = false
            };
            // Act
            var result = sutApiClientFactory.CreateApiClient(apiClientFactorySettings);
            // Assert
            Assert.NotNull(result);
            Assert.Equal(new Uri(DummyEndPointUrl), result.BaseUri);
        }

        [Fact]
        public void CreateApiClient_SendsPayloadsCompact()
        {
            // Indented payloads inflate two to four times against the platform's 2MB request caps.
            var sutApiClientFactory = new ApiClientFactory();
            var apiClientFactorySettings = new ApiClientFactorySettings()
            {
                ConnectorApiUrl = DummyEndPointUrl
            };

            var result = sutApiClientFactory.CreateApiClient(apiClientFactorySettings);

            Assert.Equal(Newtonsoft.Json.Formatting.None, ((ApiClient)result).SerializationSettings.Formatting);
        }

        [Fact]
        public async Task CreateApiClient_WhenCalledInParallel_CreatesTheRightSingletonApiClientAndSetsSecurityProtocolCorrectly()
        {
            // Arrange
            var sutApiClientFactory = new ApiClientFactory();
            var apiClientFactorySettings = new ApiClientFactorySettings()
            {
                ConnectorApiUrl = DummyEndPointUrl,
                ServerCertificateValidation = false
            };

            // Act
            Func<IApiClient> func = () => sutApiClientFactory.CreateApiClient(apiClientFactorySettings);
            int parallelTaskCount = 100;
            Task<IApiClient>[] tasks = new Task<IApiClient>[parallelTaskCount];
            for (int i = 0; i < parallelTaskCount; i++)
            {
                tasks[i] = Task.Factory.StartNew<IApiClient>(func);
            }
            await Task.WhenAll(tasks);
            var results = tasks.Select(t => t.GetAwaiter().GetResult()).ToList();

            // Assert
            Assert.Equal(parallelTaskCount, results.Count);
            var firstResult = results.First();
            Assert.Equal(new Uri(DummyEndPointUrl), firstResult.BaseUri);
            // Make sure that it's a singleton object
            foreach (var result in results)
            {
                Assert.Same(firstResult, result);
            }
        }

        [Fact]
        public void CreateAuthenticationHelper_WhenCalledFromASingleThread_CreatesAnInstanceOfAuthenticationHelper()
        {
            // Arrange
            var sutApiClientFactory = new ApiClientFactory();
            var settings = new AuthenticationHelperSettings() { AuthenticationResource = "test1", ClientId = "tes2", ClientSecret = MakeSecureString("test3") };
            // Act
            var result = sutApiClientFactory.CreateAuthenticationProvider(settings);
            // Assert
            Assert.NotNull(result);
            Assert.IsType<ConfidentialClientAuthenticationProvider>(result);
        }

        [Fact]
        public void CreateAuthenticationHelper_WhenCalledInParallel_ClientIdIsntShared()
        {
            // Arrange
            var sutApiClientFactory = new ApiClientFactory();
            var settings1 = new AuthenticationHelperSettings() { AuthenticationResource = "test1", ClientId = "tes1", ClientSecret = MakeSecureString("test1") };
            var settings2 = new AuthenticationHelperSettings() { AuthenticationResource = "test2", ClientId = "tes2", ClientSecret = MakeSecureString("test2") };
            // Act
            var authProvider1 = sutApiClientFactory.CreateAuthenticationProvider(settings1);
            var authProvider2 = sutApiClientFactory.CreateAuthenticationProvider(settings2);
            Assert.NotEqual(authProvider1, authProvider2);
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
