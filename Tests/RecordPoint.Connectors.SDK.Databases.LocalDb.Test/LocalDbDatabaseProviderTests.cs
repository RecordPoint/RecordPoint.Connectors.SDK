#nullable enable
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Moq;
using RecordPoint.Connectors.SDK.Context;
using RecordPoint.Connectors.SDK.Databases;
using RecordPoint.Connectors.SDK.Databases.LocalDb;
using Xunit;

namespace RecordPoint.Connectors.SDK.Databases.LocalDb.Test
{
    /// <summary>
    /// Unit tests for the shared, non database-bound logic on
    /// <see cref="LocalDbDatabaseProvider{TDbContext}"/>.
    /// </summary>
    public sealed class LocalDbDatabaseProviderTests : IDisposable
    {
        private const string DatabaseName = "MyTestDb";

        private readonly string _dataRoot;
        private readonly Mock<ISystemContext> _systemContext;
        private readonly TestLocalDbDatabaseProvider _sut;

        public LocalDbDatabaseProviderTests()
        {
            _dataRoot = Path.Combine(Path.GetTempPath(), "localdbtests_" + Guid.NewGuid().ToString("N"));
            Directory.CreateDirectory(_dataRoot);

            _systemContext = new Mock<ISystemContext>();
            _systemContext.Setup(x => x.GetDataRootPath()).Returns(_dataRoot);

            _sut = new TestLocalDbDatabaseProvider(_systemContext.Object, DatabaseName);
        }

        [Fact]
        public void Constants_HaveExpectedValues()
        {
            Assert.Equal("LocalDB", LocalDbDatabaseProvider<ConnectorDbContext>.LOCALDB_SYSTEM_NAME);
            Assert.Equal("Connector", LocalDbDatabaseProvider<ConnectorDbContext>.DEFAULT_DATABASE_NAME);
            Assert.Equal("(localdb)\\MSSQLLocalDB", LocalDbDatabaseProvider<ConnectorDbContext>.DEFAULT_LOCALDB_NAME);
        }

        [Fact]
        public void GetDatabaseName_ReturnsConfiguredName()
        {
            Assert.Equal(DatabaseName, _sut.GetDatabaseName());
        }

        [Fact]
        public void GetSqlServer_ReturnsDefaultLocalDbName()
        {
            Assert.Equal(LocalDbDatabaseProvider<ConnectorDbContext>.DEFAULT_LOCALDB_NAME, _sut.GetSqlServer());
        }

        [Fact]
        public void GetSqlServerConnectionString_UsesSqlServerName()
        {
            Assert.Equal("Server = (localdb)\\MSSQLLocalDB", _sut.GetSqlServerConnectionString());
        }

        [Fact]
        public void GetDataDirectory_ReturnsSystemContextDataRoot()
        {
            Assert.Equal(_dataRoot, _sut.GetDataDirectory());
            _systemContext.Verify(x => x.GetDataRootPath(), Times.AtLeastOnce);
        }

        [Fact]
        public void GetDataPath_ReturnsSystemContextDataRoot()
        {
            Assert.Equal(_dataRoot, _sut.GetDataPath());
        }

        [Fact]
        public void GetDatabaseFileName_HasMdfExtension()
        {
            Assert.Equal(DatabaseName + ".mdf", _sut.GetDatabaseFileName());
        }

        [Fact]
        public void GetDatabaseLogFileName_HasLdfExtension()
        {
            Assert.Equal(DatabaseName + ".ldf", _sut.GetDatabaseLogFileName());
        }

        [Fact]
        public void GetDatabaseStatusFileName_HasStatusTxtSuffix()
        {
            Assert.Equal(DatabaseName + "Status.txt", _sut.GetDatabaseStatusFileName());
        }

        [Fact]
        public void GetDatabasePath_CombinesDataPathAndFileName()
        {
            Assert.Equal(Path.Combine(_dataRoot, DatabaseName + ".mdf"), _sut.GetDatabasePath());
        }

        [Fact]
        public void GetDatabaseLogFilePath_CombinesDataPathAndLogFileName()
        {
            Assert.Equal(Path.Combine(_dataRoot, DatabaseName + ".ldf"), _sut.GetDatabaseLogFilePath());
        }

        [Fact]
        public void GetDatabaseStatusFilePath_CombinesDataPathAndStatusFileName()
        {
            Assert.Equal(Path.Combine(_dataRoot, DatabaseName + "Status.txt"), _sut.GetDatabaseStatusFilePath());
        }

        [Fact]
        public void GetConnectionString_ContainsServerSecurityAndAttachFile()
        {
            var expected = $"Server = (localdb)\\MSSQLLocalDB;Integrated Security=true; AttachDBFilename = {_sut.GetDatabasePath()}";
            Assert.Equal(expected, _sut.GetConnectionString());
        }

        [Fact]
        public void GetExternalSystemName_ReturnsLocalDb()
        {
            Assert.Equal("LocalDB", _sut.GetExternalSystemName());
        }

        [Fact]
        public void GetContextOptionsBuilder_ReturnsConfiguredSqlServerBuilder()
        {
            var builder = _sut.GetContextOptionsBuilder();

            Assert.NotNull(builder);
            // The SqlServer provider registers a relational extension identifiable by its provider name.
            Assert.Contains(
                builder.Options.Extensions,
                ext => ext.GetType().Name.Contains("SqlServer", StringComparison.OrdinalIgnoreCase));
        }

        [Fact]
        public void Exists_ReturnsFalse_WhenDatabaseFileMissing()
        {
            Assert.False(_sut.Exists());
        }

        [Fact]
        public void Exists_ReturnsTrue_WhenDatabaseFilePresent()
        {
            File.WriteAllText(_sut.GetDatabasePath(), "data");
            Assert.True(_sut.Exists());
        }

        [Fact]
        public async Task RemoveAsync_DeletesDatabaseFile()
        {
            var path = _sut.GetDatabasePath();
            File.WriteAllText(path, "data");
            Assert.True(File.Exists(path));

            await _sut.RemoveAsync(CancellationToken.None);

            Assert.False(File.Exists(path));
        }

        [Fact]
        public async Task ReadyAsync_Completes_WhenSetReadyWithNull()
        {
            _sut.SetReady(null!);

            // Should complete without throwing.
            Assert.Null(await Record.ExceptionAsync(() => _sut.ReadyAsync(CancellationToken.None)));
        }

        [Fact]
        public async Task ReadyAsync_Throws_WhenSetReadyWithException()
        {
            var failure = new InvalidOperationException("boom");
            _sut.SetReady(failure);

            var thrown = await Assert.ThrowsAsync<InvalidOperationException>(
                () => _sut.ReadyAsync(CancellationToken.None));
            Assert.Same(failure, thrown);
        }

        [Fact]
        public async Task ReadyAsync_Throws_WhenCancelledBeforeReady()
        {
            using var cts = new CancellationTokenSource();
            cts.Cancel();

            await Assert.ThrowsAnyAsync<OperationCanceledException>(
                () => _sut.ReadyAsync(cts.Token));
        }

        [Theory]
        [InlineData("LocalDb_DbCreate.sql")]
        [InlineData("LocalDb_DbAttach.sql")]
        public void GetSqlDatabaseScript_ReplacesAllPlaceholders_ForCreateAndAttach(string scriptName)
        {
            var parameters = new Dictionary<string, string>
            {
                ["DatabaseName"] = DatabaseName,
                ["DatabasePath"] = _sut.GetDatabasePath(),
                ["DatabaseLogFilePath"] = _sut.GetDatabaseLogFilePath(),
            };

            var script = _sut.GetSqlDatabaseScript(scriptName, parameters);

            Assert.Contains(DatabaseName, script);
            Assert.Contains(_sut.GetDatabasePath(), script);
            Assert.Contains(_sut.GetDatabaseLogFilePath(), script);
            Assert.DoesNotContain("{DatabaseName}", script);
            Assert.DoesNotContain("{DatabasePath}", script);
            Assert.DoesNotContain("{DatabaseLogFilePath}", script);
        }

        [Fact]
        public void GetSqlDatabaseScript_ReplacesPlaceholders_ForDetach()
        {
            var parameters = new Dictionary<string, string>
            {
                ["DatabaseName"] = DatabaseName,
            };

            var script = _sut.GetSqlDatabaseScript("LocalDb_DbDetach.sql", parameters);

            Assert.Contains(DatabaseName, script);
            Assert.DoesNotContain("{DatabaseName}", script);
        }

        [Fact]
        public void GetSqlDatabaseScript_Throws_WhenScriptNameNotFound()
        {
            Assert.Throws<InvalidOperationException>(
                () => _sut.GetSqlDatabaseScript("Does_Not_Exist.sql", new Dictionary<string, string>()));
        }

        public void Dispose()
        {
            if (Directory.Exists(_dataRoot))
            {
                Directory.Delete(_dataRoot, true);
            }
        }
    }
}
