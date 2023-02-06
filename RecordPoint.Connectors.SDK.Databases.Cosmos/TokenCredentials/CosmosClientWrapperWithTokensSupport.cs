using Azure.Core;
using Microsoft.Azure.Cosmos;
using Microsoft.EntityFrameworkCore.Cosmos.Infrastructure.Internal;
using Microsoft.EntityFrameworkCore.Cosmos.Storage.Internal;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace RecordPoint.Connectors.SDK.Databases.Cosmos.TokenCredentials
{
    public class CosmosClientWrapperWithTokensSupport : ISingletonCosmosClientWrapper
    {
        private static readonly string UserAgent = " Microsoft.EntityFrameworkCore.Cosmos/" + ProductInfo.GetVersion();
        private readonly CosmosClientOptions _options;
        private readonly string? _endpoint;
        private readonly string? _key;
        private readonly string? _connectionString;
        private readonly TokenCredential? _tokenCredential;
        private CosmosClient? _client;

        public CosmosClientWrapperWithTokensSupport(ICosmosSingletonOptions options, CosmosTokenCredentialSingletonOptions singletonOptions)
        {
            _endpoint = options.AccountEndpoint;
            _key = options.AccountKey;
            _connectionString = options.ConnectionString;
            var configuration = new CosmosClientOptions
            { ApplicationName = UserAgent, Serializer = new JsonCosmosSerializer() };

            if (options.Region != null)
            {
                configuration.ApplicationRegion = options.Region;
            }

            if (options.LimitToEndpoint != null)
            {
                configuration.LimitToEndpoint = options.LimitToEndpoint.Value;
            }

            if (options.ConnectionMode != null)
            {
                configuration.ConnectionMode = options.ConnectionMode.Value;
            }

            if (options.WebProxy != null)
            {
                configuration.WebProxy = options.WebProxy;
            }

            if (options.RequestTimeout != null)
            {
                configuration.RequestTimeout = options.RequestTimeout.Value;
            }

            if (options.OpenTcpConnectionTimeout != null)
            {
                configuration.OpenTcpConnectionTimeout = options.OpenTcpConnectionTimeout.Value;
            }

            if (options.IdleTcpConnectionTimeout != null)
            {
                configuration.IdleTcpConnectionTimeout = options.IdleTcpConnectionTimeout.Value;
            }

            if (options.GatewayModeMaxConnectionLimit != null)
            {
                configuration.GatewayModeMaxConnectionLimit = options.GatewayModeMaxConnectionLimit.Value;
            }

            if (options.MaxTcpConnectionsPerEndpoint != null)
            {
                configuration.MaxTcpConnectionsPerEndpoint = options.MaxTcpConnectionsPerEndpoint.Value;
            }

            if (options.MaxRequestsPerTcpConnection != null)
            {
                configuration.MaxRequestsPerTcpConnection = options.MaxRequestsPerTcpConnection.Value;
            }

            if (options.HttpClientFactory != null)
            {
                configuration.HttpClientFactory = options.HttpClientFactory;
            }

            _tokenCredential = singletonOptions.TokenCredential;

            _options = configuration;
        }

        public virtual CosmosClient Client
            => _client ??= CreateCosmosClient();

        /// <inheritdoc />
        public void Dispose()
        {
            _client?.Dispose();
            _client = null;
        }

        private CosmosClient CreateCosmosClient()
        {
            if (_tokenCredential != null)
            {
                return new CosmosClient(_endpoint, _tokenCredential, _options);
            }

            return string.IsNullOrEmpty(_connectionString)
                ? new CosmosClient(_endpoint, _key, _options)
                : new CosmosClient(_connectionString, _options);
        }
    }
}
