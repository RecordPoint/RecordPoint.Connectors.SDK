using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RecordPoint.Connectors.SDK.Context;

namespace RecordPoint.Connectors.SDK.Databases.AzureSql
{


    public abstract class AzureSqlDbProvider<TDbContext> : CommonSqlDbProvider<TDbContext>, IAzureSqlDbProvider<TDbContext>
        where TDbContext : DbContext
    {
        private const string SQL_CREATE_SCHEMA_SCRIPT = "AzureSqlSchemaCreate.sql";

        private readonly IAzureSqlConnectionFactory _connectionFactory;

        protected AzureSqlDbProvider(
            ISystemContext systemContext,
            ILogger<AzureSqlDbProvider<TDbContext>> logger,
            IAzureSqlConnectionFactory connectionFactory,
            IOptions<AzureSqlConnectorDbOptions> options) : base(systemContext, logger)
        {
            if (options.Value.ConnectionString == null)
                throw new RequiredValueNullException(nameof(options.Value.ConnectionString));

            _connectionFactory = connectionFactory;
        }

        protected override void CheckDatabaseExists(CancellationToken cancellationToken)
        {
            var databaseName = GetDatabaseName();

            if (Exists())
            {
                _logger.LogInformation("Database '{databaseName}' connection successful.", databaseName);

                // Create schema is it doesn't exist
                using var connection = _connectionFactory.GetConnection(GetAdminConnectionString());
                connection.Open();
                CreateSchema(AzureSqlConnectorDbContext.DEFAULT_DB_SCHEMA_NAME, connection);
            }
            else
            {
                _logger.LogCritical("Database '{databaseName}' cannot be connected with or does not exist.", databaseName);
                throw new ConnectorDatabaseException($"Database '{databaseName}' cannot be connected with or does not exist.");
            }
        }

        private void CreateSchema(string schemaName, SqlConnection connection)
        {
            var paramName = "SchemaName";
            RunScript(SQL_CREATE_SCHEMA_SCRIPT, paramName, schemaName, connection);
            _logger.LogInformation("Schema '{schemaName}' created if not exists.", schemaName);
        }

        private void RunScript(string scriptName, string paramName, string paramValue, SqlConnection connection)
        {
            var sql = GetSqlDatabaseScript(scriptName, new Dictionary<string, string>()
            {
                [paramName] = paramValue
            });
            var createDatabaseCommand = new SqlCommand(sql, connection);
            createDatabaseCommand.ExecuteNonQuery();
        }

        protected override DbContextOptionsBuilder<TDbContext> GetAdminContextOptionsBuilder()
        {
            var builder = new DbContextOptionsBuilder<TDbContext>()
                .UseSqlServer(GetAdminConnectionString());
            return builder;
        }

        protected override DbContextOptionsBuilder<TDbContext> GetContextOptionsBuilder()
        {
            var builder = new DbContextOptionsBuilder<TDbContext>()
                .UseSqlServer(GetConnectionString());
            return builder;
        }

        protected override string GetSqlDatabaseScript(string scriptName, Dictionary<string, string> parameters)
        {
            var scriptAssembly = typeof(AzureSqlDbProvider<TDbContext>).Assembly;
            var sqlScriptName = scriptAssembly.GetManifestResourceNames().Single(name => name.EndsWith(scriptName));
            using var sqlFileStream = scriptAssembly.GetManifestResourceStream(sqlScriptName);
            using var streamReader = new StreamReader(sqlFileStream ?? throw new InvalidOperationException());
            string script = streamReader.ReadToEnd();
            foreach (var parameter in parameters)
            {
                script = script.Replace($"{{{parameter.Key}}}", parameter.Value);
            }
            return script;
        }
    }
}
