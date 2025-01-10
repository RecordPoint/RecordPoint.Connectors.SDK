using FluentAssertions;
using RecordPoint.Connectors.SDK.Client;
using System.Net;
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
            result.Should().NotBeNull();
            ServicePointManager.SecurityProtocol.Should().Be(SecurityProtocolType.Tls12 | SecurityProtocolType.Tls13);
            result.BaseUri.Should().Be(new Uri(DummyEndPointUrl), "Base URL is incorrect");
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
            results.Count.Should().Be(parallelTaskCount);
            var firstResult = results.First();
            firstResult.BaseUri.Should().Be(new Uri(DummyEndPointUrl), "Base URL is incorrect");
            ServicePointManager.SecurityProtocol.Should().Be(SecurityProtocolType.Tls12 | SecurityProtocolType.Tls13);
            // Make sure that it's a singleton object
            results.ForEach(result => result.Should().BeSameAs(firstResult));
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
            result.Should().NotBeNull();
            result.Should().BeOfType<ConfidentialClientAuthenticationProvider>();
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
            authProvider1.Should().NotBe(authProvider2);
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
