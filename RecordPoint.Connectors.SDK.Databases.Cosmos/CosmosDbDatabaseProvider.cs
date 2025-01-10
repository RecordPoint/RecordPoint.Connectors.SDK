using Microsoft.Azure.Cosmos;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RecordPoint.Connectors.SDK.Configuration;
using RecordPoint.Connectors.SDK.Context;
using RecordPoint.Connectors.SDK.Databases.Cosmos.Helpers;
using RecordPoint.Connectors.SDK.Toggles;

namespace RecordPoint.Connectors.SDK.Databases.Cosmos
{
    /// <summary>
    /// The cosmos db database provider.
    /// </summary>
    /// <typeparam name="TDbContext"/>
    public abstract class CosmosDbDatabaseProvider<TDbContext> : CommonSqlDbProvider<TDbContext>, IDatabaseProvider<TDbContext>
        where TDbContext : DbContext
    {
        /// <summary>
        /// The options.
        /// </summary>
        private readonly IOptions<CosmosDbConnectorDatabaseOptions> _options;
        /// <summary>
        /// The configuration.
        /// </summary>
        private readonly IConfiguration _configuration;
        /// <summary>
        /// The toggle provider.
        /// </summary>
        private readonly IToggleProvider _toggleProvider;

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <param name="systemContext">The system context.</param>
        /// <param name="configuration">The configuration.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="toggleProvider">The toggle provider.</param>
        /// <param name="options">The options.</param>
        protected CosmosDbDatabaseProvider(
            ISystemContext systemContext,
            IConfiguration configuration,
            ILogger<CosmosDbDatabaseProvider<TDbContext>> logger,
            IToggleProvider toggleProvider,
            IOptions<CosmosDbConnectorDatabaseOptions> options) : base(systemContext, logger)
        {
            _configuration = configuration;
            _options = options;
            _toggleProvider = toggleProvider;
        }

        /// <summary>
        /// Check database exists.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <exception cref="ConnectorDatabaseException"></exception>
        protected override void CheckDatabaseExists(CancellationToken cancellationToken)
        {
            var databaseName = GetDatabaseName();

            if (Exists())
            {
                _logger.LogInformation("Database '{databaseName}' connection successful.", databaseName);
            }
            else
            {
                _logger.LogCritical("Database '{databaseName}' cannot be connected with or does not exist.", databaseName);
                throw new ConnectorDatabaseException($"Database '{databaseName}' cannot be connected with or does not exist.");
            }
        }

        /// <summary>
        /// Get admin context options builder.
        /// </summary>
        /// <returns><![CDATA[DbContextOptionsBuilder<TDbContext>]]></returns>
        protected override DbContextOptionsBuilder<TDbContext> GetAdminContextOptionsBuilder() => GetContextOptionsBuilder();

        /// <summary>
        /// Get context options builder.
        /// </summary>
        /// <returns><![CDATA[DbContextOptionsBuilder<TDbContext>]]></returns>
        protected override DbContextOptionsBuilder<TDbContext> GetContextOptionsBuilder()
        {
            string cosmosDedicatedGatewaySignifier = "sqlx.cosmos.azure.com";

            var connectionString = GetConnectionString();
            if (!string.IsNullOrEmpty(connectionString))
            {
                return new DbContextOptionsBuilder<TDbContext>()
                    .UseCosmos(connectionString, GetDatabaseName(), options =>
                    {
                        options.ConnectionMode(connectionString.Contains(cosmosDedicatedGatewaySignifier) || _options.Value.UseGateWayConnectionMode
                            ? ConnectionMode.Gateway
                            : ConnectionMode.Direct);
                    });
            }
            else
            {
                var azureAuthenticationOptions = _configuration.GetRequiredSection(AzureAuthenticationOptions.SECTION_NAME).Get<AzureAuthenticationOptions>()
                    ?? throw new InvalidOperationException($"{nameof(AzureAuthenticationOptions)} not found");
                var credential = azureAuthenticationOptions.GetTokenCredential();

                var accountEndpoint = CosmosEndpointHelper.BuildCosmosAccountEndpoint(_toggleProvider, _options.Value.CosmosDbAccountName);
                return new DbContextOptionsBuilder<TDbContext>()
                    .UseCosmos(accountEndpoint, credential, GetDatabaseName(), options =>
                    {
                        options.ConnectionMode(accountEndpoint.Contains(cosmosDedicatedGatewaySignifier) || _options.Value.UseGateWayConnectionMode
                            ? ConnectionMode.Gateway
                            : ConnectionMode.Direct);
                    });
            }
        }

        /// <summary>
        /// Get sql database script.
        /// </summary>
        /// <param name="scriptName">The script name.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns>A string</returns>
        protected override string GetSqlDatabaseScript(string scriptName, Dictionary<string, string> parameters) => String.Empty;

        /// <summary>
        /// The following methods override the base functionality as EFCore Cosmos provider does not support the required implementations
        /// </summary>
        public new Task PrepareAsync(CancellationToken cancellationToken)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return Task.CompletedTask;
            }

            CheckDatabaseExists(cancellationToken);

            return Task.CompletedTask;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public new bool Exists() => true;

    }
}
