#nullable enable
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using RecordPoint.Connectors.SDK.Connectors;
using RecordPoint.Connectors.SDK.Databases;
using RecordPoint.Connectors.SDK.Databases.Sqlite;
using Xunit;

namespace RecordPoint.Connectors.SDK.Databases.Sqlite.Test
{
    public class SqliteConnectorDatabaseProviderTests
    {
        [Fact]
        public void GetDatabaseName_ReturnsConfiguredName()
        {
            using var helper = new SqliteTestHelper();
            var provider = helper.CreateProvider(databaseName: "CustomName");

            Assert.Equal("CustomName", provider.GetDatabaseName());
        }

        [Fact]
        public void GetDatabaseName_ReturnsDefaultWhenNotOverridden()
        {
            using var helper = new SqliteTestHelper();
            var provider = helper.CreateProvider();

            Assert.Equal(SqliteConnectorDatabaseOptions.DEFAULT_DATABASE_NAME, provider.GetDatabaseName());
        }

        [Fact]
        public void GetDataDirectory_ReturnsSystemContextRootPath()
        {
            using var helper = new SqliteTestHelper();
            var provider = helper.CreateProvider();

            Assert.Equal(helper.DataRootPath, provider.GetDataDirectory());
        }

        [Fact]
        public void GetDatabasePath_CombinesDirectoryNameAndExtension()
        {
            using var helper = new SqliteTestHelper();
            var provider = helper.CreateProvider(databaseName: "MyDb");

            var expected = Path.Join(helper.DataRootPath, "MyDb.sqlite");
            Assert.Equal(expected, provider.GetDatabasePath());
        }

        [Fact]
        public void GetConnectionString_ContainsDatabasePath()
        {
            using var helper = new SqliteTestHelper();
            var provider = helper.CreateProvider(databaseName: "MyDb");

            var connectionString = provider.GetConnectionString();

            Assert.Contains("MyDb.sqlite", connectionString);
            Assert.Contains("Data Source", connectionString, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public void GetExternalSystemName_ReturnsSqlite()
        {
            using var helper = new SqliteTestHelper();
            var provider = helper.CreateProvider();

            Assert.Equal("Sqlite", provider.GetExternalSystemName());
            Assert.Equal("Sqlite", SqliteDatabaseProvider<ConnectorDbContext>.SQLITE_SYSTEM_NAME);
            Assert.Equal("sqlite", SqliteDatabaseProvider<ConnectorDbContext>.SQLITE_EXTENSION);
        }

        [Fact]
        public void CreateDbContext_ReturnsSqliteConnectorDbContext()
        {
            using var helper = new SqliteTestHelper();
            var provider = helper.CreateProvider();

            using ConnectorDbContext context = provider.CreateDbContext();

            Assert.IsType<SqliteConnectorDbContext>(context);
            Assert.Equal("Microsoft.EntityFrameworkCore.Sqlite", context.Database.ProviderName);
        }

        [Fact]
        public void GetContextOptionsBuilder_IsConfiguredForSqlite()
        {
            using var helper = new SqliteTestHelper();
            var provider = helper.CreateProvider();

            var builder = provider.GetContextOptionsBuilder();

            using var context = new SqliteConnectorDbContext(builder.Options);
            Assert.Equal("Microsoft.EntityFrameworkCore.Sqlite", context.Database.ProviderName);
        }

        [Fact]
        public void Exists_ReturnsFalseWhenDatabaseFileMissing()
        {
            using var helper = new SqliteTestHelper();
            var provider = helper.CreateProvider();

            Assert.False(provider.Exists());
        }

        [Fact]
        public async Task PrepareAsync_CreatesDirectoryAndDatabaseFile()
        {
            using var helper = new SqliteTestHelper();
            var provider = helper.CreateProvider();

            // The data directory does not exist yet; PrepareAsync must create it.
            Assert.False(Directory.Exists(helper.DataRootPath));

            await provider.PrepareAsync(CancellationToken.None);

            Assert.True(Directory.Exists(helper.DataRootPath));
            Assert.True(provider.Exists());
        }

        [Fact]
        public async Task PrepareAsync_AppliesMigrationsSoAllTablesAreQueryable()
        {
            using var helper = new SqliteTestHelper();
            var provider = helper.CreateProvider();

            await provider.PrepareAsync(CancellationToken.None);

            using var context = provider.CreateDbContext();

            // Querying each mapped set proves the migrations built the schema.
            Assert.Null(context.Connectors.FirstOrDefault());
            Assert.Null(context.Channels.FirstOrDefault());
            Assert.Null(context.Aggregations.FirstOrDefault());
            Assert.Null(context.ManagedWorkStatuses.FirstOrDefault());
        }

        [Fact]
        public async Task PrepareAsync_PersistsAndReadsBackData()
        {
            using var helper = new SqliteTestHelper();
            var provider = helper.CreateProvider();

            await provider.PrepareAsync(CancellationToken.None);

            using (var writeContext = provider.CreateDbContext())
            {
                writeContext.Connectors.Add(new ConnectorConfigurationModel
                {
                    ConnectorId = "c1",
                    ConnectorTypeId = "t1",
                    DisplayName = "Display",
                    Status = "Active",
                    TenantId = "tenant1",
                    Data = "payload"
                });
                await writeContext.SaveChangesAsync();
            }

            using var readContext = provider.CreateDbContext();
            var stored = readContext.Connectors.Single();
            Assert.Equal("c1", stored.ConnectorId);
            Assert.Equal("payload", stored.Data);
        }

        [Fact]
        public async Task RemoveAsync_DeletesDatabaseFile()
        {
            using var helper = new SqliteTestHelper();
            var provider = helper.CreateProvider();

            await provider.PrepareAsync(CancellationToken.None);
            Assert.True(provider.Exists());

            // Release the pooled connection so the underlying file handle is closed.
            SqliteConnection.ClearAllPools();

            await provider.RemoveAsync(CancellationToken.None);

            Assert.False(provider.Exists());
        }

        [Fact]
        public async Task CleanupAsync_CompletesSuccessfully()
        {
            using var helper = new SqliteTestHelper();
            var provider = helper.CreateProvider();

            Assert.Null(await Record.ExceptionAsync(
                async () => await provider.CleanupAsync(CancellationToken.None)));
        }

        [Fact]
        public async Task ReadyAsync_CompletesWhenSetReadyWithNullException()
        {
            using var helper = new SqliteTestHelper();
            var provider = helper.CreateProvider();

            provider.SetReady(null!);

            Assert.Null(await Record.ExceptionAsync(
                async () => await provider.ReadyAsync(CancellationToken.None)));
        }

        [Fact]
        public async Task ReadyAsync_ThrowsWhenSetReadyWithException()
        {
            using var helper = new SqliteTestHelper();
            var provider = helper.CreateProvider();

            var failure = new InvalidOperationException("boom");
            provider.SetReady(failure);

            var thrown = await Assert.ThrowsAsync<InvalidOperationException>(
                () => provider.ReadyAsync(CancellationToken.None));
            Assert.Same(failure, thrown);
        }

        [Fact]
        public async Task ReadyAsync_ThrowsWhenCancelledBeforeReady()
        {
            using var helper = new SqliteTestHelper();
            var provider = helper.CreateProvider();

            using var cts = new CancellationTokenSource();
            cts.Cancel();

            await Assert.ThrowsAnyAsync<OperationCanceledException>(
                () => provider.ReadyAsync(cts.Token));
        }
    }
}
