using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using RecordPoint.Connectors.SDK.Context;

namespace RecordPoint.Connectors.SDK.Databases.LocalDb
{

    public abstract class LocalDbDatabaseProvider<TDbContext> : ILocalDbDatabaseProvider<TDbContext>
        where TDbContext : DbContext
    {

        public const string LOCALDB_SYSTEM_NAME = "LocalDB";

        public const string DEFAULT_DATABASE_NAME = "Connector";
        public const string DEFAULT_LOCALDB_NAME = "(localdb)\\MSSQLLocalDB";

        private const string SQL_ATTACH_SCRIPT = "LocalDb_DbAttach.sql";
        private const string SQL_DETACH_SCRIPT = "LocalDb_DbDetach.sql";
        private const string SQL_CREATE_SCRIPT = "LocalDb_DbCreate.sql";


        private readonly ISystemContext _systemContext;

        private readonly TaskCompletionSource<Exception> _readySource = new();

        protected LocalDbDatabaseProvider(ISystemContext systemContext)
        {
            _systemContext = systemContext;
        }

        /// <summary>
        /// Required override to get the database name
        /// </summary>
        /// <returns></returns>
        public abstract string GetDatabaseName();

        /// <summary>
        /// Get the data directory
        /// </summary>
        /// <returns></returns>
        public virtual string GetDataDirectory()
        {
            return _systemContext.GetDataRootPath();
        }

        /// <summary>
        /// Sql server name
        /// </summary>
        public virtual string GetSqlServer() => DEFAULT_LOCALDB_NAME;

        /// <summary>
        /// Connection string of the server itself
        /// </summary>
        public virtual string GetSqlServerConnectionString() => $"Server = {GetSqlServer()}";

        /// <summary>
        /// Data path
        /// </summary>
        public virtual string GetDataPath() => _systemContext.GetDataRootPath();

        /// <summary>
        /// Database filename
        /// </summary>
        public virtual string GetDatabaseFileName() => $"{GetDatabaseName()}.mdf";

        /// <summary>
        /// Database log file
        /// </summary>
        public virtual string GetDatabaseLogFileName() => $"{GetDatabaseName()}.ldf";

        /// <summary>
        /// Workqueue status file
        /// </summary>
        public string GetDatabaseStatusFileName() => $"{GetDatabaseName()}Status.txt";

        /// <summary>
        /// Get the full path to the database database
        /// </summary>
        /// <returns>Path to the selected database</returns>
        public virtual string GetDatabasePath() => Path.Combine(GetDataPath(), GetDatabaseFileName());

        /// <summary>
        /// Full path of database log
        /// </summary>
        public virtual string GetDatabaseLogFilePath() => Path.Combine(GetDataPath(), GetDatabaseLogFileName());

        /// <summary>
        /// Full path of database status file
        /// </summary>
        public virtual string GetDatabaseStatusFilePath() => Path.Combine(GetDataPath(), GetDatabaseStatusFileName());

        /// <summary>
        /// Get the connection string for a database
        /// </summary>
        /// <returns>Connection string for the database</returns>
        public virtual string GetConnectionString() => $"Server = {GetSqlServer()};Integrated Security=true; AttachDBFilename = {GetDatabasePath()}";

        public async Task PrepareAsync(CancellationToken cancellationToken)
        {
            // Make sure the data directory exists
            Directory.CreateDirectory(GetDataDirectory());

            await AttachAsync(cancellationToken);

            using var dbContext = CreateDbContext();
            await dbContext.Database.MigrateAsync(cancellationToken);
        }

        /// <summary>
        /// Database cleanup
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        public Task CleanupAsync(CancellationToken cancellationToken)
        {
            return DetachAsync(cancellationToken);
        }

        /// <summary>
        /// Does the database exist?
        /// </summary>
        /// <returns>True if the database exists, otherwise false</returns>
        public bool Exists()
        {
            var filePath = new FileInfo(GetDatabasePath());
            return filePath.Exists;
        }

        public Task RemoveAsync(CancellationToken cancellationToken)
        {
            File.Delete(GetDatabasePath());
            return Task.CompletedTask;
        }

        public virtual DbContextOptionsBuilder<TDbContext> GetContextOptionsBuilder()
        {
            var builder = new DbContextOptionsBuilder<TDbContext>()
                .UseSqlServer(GetConnectionString());
            return builder;
        }

        /// <summary>
        /// Called by dependant services to determine if the database is ready for operation.
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>Readiness Task</returns>
        public async Task ReadyAsync(CancellationToken cancellationToken)
        {
            // Wait till ready or its been cancelled
            var tcs = new TaskCompletionSource<bool>();
            cancellationToken.Register(s => tcs.SetResult(true), tcs);
            await Task.WhenAny(_readySource.Task, tcs.Task).ConfigureAwait(false);

            cancellationToken.ThrowIfCancellationRequested();

            var exception = await _readySource.Task.ConfigureAwait(false);
            if (exception != null)
                throw exception;
        }

        /// <summary>
        /// Set the readiness for this service
        /// </summary>
        /// <param name="exception">Exception that indicates that the service has critically failed</param>
        public void SetReady(Exception exception)
        {
            _readySource.SetResult(exception);
        }

        /// <summary>
        /// Create a connector db context
        /// </summary>
        /// <returns>Connector db context</returns>
        public abstract TDbContext CreateDbContext();

        /// <summary>
        /// Get the external system name
        /// </summary>
        /// <returns></returns>
        public virtual string GetExternalSystemName()
        {
            return LOCALDB_SYSTEM_NAME;
        }

        /// <summary>
        /// Attach the database from the local db server
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>

        public async Task AttachAsync(CancellationToken cancellationToken)
        {
            var dataFileExists = File.Exists(GetDatabasePath());
            var scriptName = dataFileExists ? SQL_ATTACH_SCRIPT : SQL_CREATE_SCRIPT;
            var sql = GetSqlDatabaseScript(scriptName, new Dictionary<string, string>()
            {
                ["DatabaseName"] = GetDatabaseName(),
                ["DatabasePath"] = GetDatabasePath(),
                ["DatabaseLogFilePath"] = GetDatabaseLogFilePath()
            });

            using var connection = new SqlConnection(GetSqlServerConnectionString());
            connection.Open();
            var command = new SqlCommand(sql, connection);
            await command.ExecuteNonQueryAsync(cancellationToken);

            File.WriteAllLines(GetDatabaseStatusFilePath(), new string[] { $"Database: {GetDatabaseName()} attached" });
        }


        /// <summary>
        /// Detach the database from the local db server
        /// </summary>
        /// <param name="cancellationToken">Cancellation token</param>
        public async Task DetachAsync(CancellationToken cancellationToken)
        {
            using var connection = new SqlConnection(GetSqlServerConnectionString());
            connection.Open();

            var sql = GetSqlDatabaseScript(SQL_DETACH_SCRIPT, new Dictionary<string, string>()
            {
                ["DatabaseName"] = GetDatabaseName()
            });
            var command = new SqlCommand(sql, connection);
            await command.ExecuteNonQueryAsync(cancellationToken);

            File.WriteAllLines(GetDatabaseStatusFilePath(), new string[] { $"Database: {GetDatabaseName()} detached" });
        }

        public string GetSqlDatabaseScript(string scriptName, Dictionary<string, string> parameters)
        {
            var scriptAssembly = typeof(LocalDbDatabaseProvider<TDbContext>).Assembly;
            var sqlScriptName = scriptAssembly.GetManifestResourceNames().Single(name => name.EndsWith(scriptName));
            using var sqlFileStream = scriptAssembly.GetManifestResourceStream(sqlScriptName);
            if (sqlFileStream == null)
            {
                throw new RequiredValueNullException(nameof(sqlFileStream));
            }
            using var streamReader = new StreamReader(sqlFileStream);
            var script = streamReader.ReadToEnd();
            parameters.ToList().ForEach(parameter => script = script.Replace($"{{{parameter.Key}}}", parameter.Value));
            return script;
        }
    }
}
