using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RecordPoint.Connectors.SDK.Configuration;
using RecordPoint.Connectors.SDK.Context;

namespace RecordPoint.Connectors.SDK.Databases.Cosmos
{
    public abstract class CosmosDbDatabaseProvider<TDbContext> : CommonSqlDbProvider<TDbContext>, IDatabaseProvider<TDbContext>
        where TDbContext : DbContext
    {
        IOptions<CosmosDbConnectorDatabaseOptions> _options;
        private readonly IConfiguration _configuration;

        protected CosmosDbDatabaseProvider(
            ISystemContext systemContext,
            IConfiguration configuration,
            ILogger<CosmosDbDatabaseProvider<TDbContext>> logger,
            IOptions<CosmosDbConnectorDatabaseOptions> options) : base(systemContext, logger)
        {
            _configuration = configuration;
            _options = options;
        }

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

        protected override DbContextOptionsBuilder<TDbContext> GetAdminContextOptionsBuilder() => GetContextOptionsBuilder();

        protected override DbContextOptionsBuilder<TDbContext> GetContextOptionsBuilder()
        {

            var connectionString = GetConnectionString();
            if (!string.IsNullOrEmpty(connectionString))
            {
                return new DbContextOptionsBuilder<TDbContext>()
                    .UseCosmos(GetConnectionString(), GetDatabaseName());
            }
            else
            {
                var azureAuthenticationOptions = _configuration.GetSection(AzureAuthenticationOptions.SECTION_NAME).Get<AzureAuthenticationOptions>();
                var credential = azureAuthenticationOptions.GetTokenCredential();

                var accountEndpoint = $"https://{_options.Value.CosmosDbAccountName}.documents.azure.com/";
                return new DbContextOptionsBuilder<TDbContext>()
                    .UseCosmos(accountEndpoint, credential, GetDatabaseName());
            }
        }

        protected override string GetSqlDatabaseScript(string scriptName, Dictionary<string, string> parameters) => String.Empty;

        /// <remarks>
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

        public new bool Exists() => true;
    }
}
