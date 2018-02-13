using FluentAssertions;
using RecordPoint.Connectors.SDK.Client;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
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
            ServicePointManager.SecurityProtocol.Should().Be(SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12);
            result.BaseUri.Should().Be(new Uri(DummyEndPointUrl), "Base URL is incorrect");
            ServicePointManager.ServerCertificateValidationCallback(null, null, null, System.Net.Security.SslPolicyErrors.None).Should().BeTrue();
        }

        [Fact]
        public void CreateApiClient_WhenCalledInParallel_CreatesTheRightSingletonApiClientAndSetsSecurityProtocolCorrectly()
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
            Task.WaitAll(tasks);
            var results = tasks.Select(t => t.GetAwaiter().GetResult()).ToList();

            // Assert
            results.Count.Should().Be(parallelTaskCount);
            var firstResult = results.First();
            firstResult.BaseUri.Should().Be(new Uri(DummyEndPointUrl), "Base URL is incorrect");
            ServicePointManager.SecurityProtocol.Should().Be(SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12);
            // Make sure that it's a singleton object
            results.ForEach(result => result.Should().BeSameAs(firstResult));
            ServicePointManager.ServerCertificateValidationCallback(null, null, null, System.Net.Security.SslPolicyErrors.None).Should().BeTrue();
        }

        [Fact]
        public void CreateAuthenticationHelper_WhenCalledFromASingleThread_CreatesAnInstanceOfAuthenticationHelper()
        {
            // Arrange
            var sutApiClientFactory = new ApiClientFactory();
            // Act
            var result = sutApiClientFactory.CreateAuthenticationHelper();
            // Assert
            result.Should().NotBeNull();
            result.Should().BeOfType<AuthenticationHelper>();
        }

        [Fact]
        public void CreateAuthenticationHelper_WhenCalledInParallel_CreatesASingletonObject()
        {
            // Arrange
            var sutApiClientFactory = new ApiClientFactory();

            // Act
            Func<IAuthenticationHelper> func = () => sutApiClientFactory.CreateAuthenticationHelper();
            int parallelTaskCount = 100;
            Task<IAuthenticationHelper>[] tasks = new Task<IAuthenticationHelper>[parallelTaskCount];
            for (int i = 0; i < parallelTaskCount; i++)
            {
                tasks[i] = Task.Factory.StartNew<IAuthenticationHelper>(func);
            }
            Task.WaitAll(tasks);
            var results = tasks.Select(t => t.GetAwaiter().GetResult()).ToList();

            // Assert
            results.Count.Should().Be(parallelTaskCount);
            var firstResult = results.First();
            // Make sure that it's a singleton object
            results.ForEach(result => result.Should().BeSameAs(firstResult));
        }
    }
}
