using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RecordPoint.Connectors.SDK.Context;
using System.Data.SqlClient;

namespace RecordPoint.Connectors.SDK.Databases.PostgreSql
{


    /// <summary>
    /// The postgre sql db provider.
    /// </summary>
    /// <typeparam name="TDbContext"/>
    public abstract class PostgreSqlDbProvider<TDbContext> : CommonSqlDbProvider<TDbContext>, IPostgreSqlDbProvider<TDbContext>
        where TDbContext : DbContext
    {
        /// <summary>
        /// The SQL CREATE SCHEMA SCRIPT.
        /// </summary>
        private const string SQL_CREATE_SCHEMA_SCRIPT = "PostgreSqlSchemaCreate.sql";
        /// <summary>
        /// The connection factory.
        /// </summary>
        private readonly IPostgreSqlConnectionFactory _connectionFactory;

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <param name="systemContext">The system context.</param>
        /// <param name="logger">The logger.</param>
        /// <param name="connectionFactory">The connection factory.</param>
        /// <param name="options">The options.</param>
        protected PostgreSqlDbProvider(
            ISystemContext systemContext,
            ILogger<PostgreSqlDbProvider<TDbContext>> logger,
            IPostgreSqlConnectionFactory connectionFactory,
            IOptions<PostgreSqlConnectorDbOptions> options) : base(systemContext, logger)
        {
            if (options.Value.ConnectionString == null)
                throw new RequiredValueNullException(nameof(options.Value.ConnectionString));

            _connectionFactory = connectionFactory;
        }

        /// <summary>
        /// Check database exists.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <exception cref="InvalidOperationException"></exception>
        protected override void CheckDatabaseExists(CancellationToken cancellationToken)
        {
            var databaseName = GetDatabaseName();

            if (Exists())
            {
                _logger.LogInformation("Database '{databaseName}' connection successful.", databaseName);

                // Create schema is it doesn't exist
                using var connection = _connectionFactory.GetConnection(GetAdminConnectionString());
                connection.Open();
                CreateSchema(PostgreSqlConnectorDbContext.DEFAULT_DB_SCHEMA_NAME, connection);
            }
            else
            {
                _logger.LogCritical("Database '{databaseName}' cannot be connected with or does not exist.", databaseName);
                throw new InvalidOperationException($"Database '{databaseName}' cannot be connected with or does not exist.");
            }
        }

        /// <summary>
        /// Creates the schema.
        /// </summary>
        /// <param name="schemaName">The schema name.</param>
        /// <param name="connection">The connection.</param>
        private void CreateSchema(string schemaName, SqlConnection connection)
        {
            var paramName = "SchemaName";
            RunScript(SQL_CREATE_SCHEMA_SCRIPT, paramName, schemaName, connection);
            _logger.LogInformation("Schema '{schemaName}' created if not exists.", schemaName);
        }

        /// <summary>
        /// Run the script.
        /// </summary>
        /// <param name="scriptName">The script name.</param>
        /// <param name="paramName">The param name.</param>
        /// <param name="paramValue">The param value.</param>
        /// <param name="connection">The connection.</param>
        private void RunScript(string scriptName, string paramName, string paramValue, SqlConnection connection)
        {
            var sql = GetSqlDatabaseScript(scriptName, new Dictionary<string, string>()
            {
                [paramName] = paramValue
            });
            var createDatabaseCommand = new SqlCommand(sql, connection);
            createDatabaseCommand.ExecuteNonQuery();
        }

        /// <summary>
        /// Get admin context options builder.
        /// </summary>
        /// <returns><![CDATA[DbContextOptionsBuilder<TDbContext>]]></returns>
        protected override DbContextOptionsBuilder<TDbContext> GetAdminContextOptionsBuilder()
        {
            var builder = new DbContextOptionsBuilder<TDbContext>()
                .UseNpgsql(GetAdminConnectionString());
            return builder;
        }

        /// <summary>
        /// Get context options builder.
        /// </summary>
        /// <returns><![CDATA[DbContextOptionsBuilder<TDbContext>]]></returns>
        protected override DbContextOptionsBuilder<TDbContext> GetContextOptionsBuilder()
        {
            var builder = new DbContextOptionsBuilder<TDbContext>()
                .UseNpgsql(GetConnectionString());
            return builder;
        }

        /// <summary>
        /// Get sql database script.
        /// </summary>
        /// <param name="scriptName">The script name.</param>
        /// <param name="parameters">The parameters.</param>
        /// <exception cref="InvalidOperationException"></exception>
        /// <returns>A string</returns>
        protected override string GetSqlDatabaseScript(string scriptName, Dictionary<string, string> parameters)
        {
            var scriptAssembly = typeof(PostgreSqlDbProvider<TDbContext>).Assembly;
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
