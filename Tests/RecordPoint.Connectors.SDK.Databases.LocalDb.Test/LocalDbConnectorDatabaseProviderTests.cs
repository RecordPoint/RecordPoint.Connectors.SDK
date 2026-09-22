#nullable enable
using System;
using System.IO;
using Microsoft.Extensions.Options;
using Moq;
using RecordPoint.Connectors.SDK.Context;
using RecordPoint.Connectors.SDK.Databases;
using RecordPoint.Connectors.SDK.Databases.LocalDb;
using Xunit;

namespace RecordPoint.Connectors.SDK.Databases.LocalDb.Test
{
    /// <summary>
    /// Unit tests for <see cref="LocalDbConnectorDatabaseProvider"/>.
    /// </summary>
    public sealed class LocalDbConnectorDatabaseProviderTests : IDisposable
    {
        private readonly string _dataRoot;
        private readonly Mock<ISystemContext> _systemContext;

        public LocalDbConnectorDatabaseProviderTests()
        {
            _dataRoot = Path.Combine(Path.GetTempPath(), "localdbconn_" + Guid.NewGuid().ToString("N"));
            Directory.CreateDirectory(_dataRoot);

            _systemContext = new Mock<ISystemContext>();
            _systemContext.Setup(x => x.GetDataRootPath()).Returns(_dataRoot);
        }

        private LocalDbConnectorDatabaseProvider CreateSut(LocalDbConnectorDatabaseOptions? options = null)
        {
            var opts = Options.Create(options ?? new LocalDbConnectorDatabaseOptions());
            return new LocalDbConnectorDatabaseProvider(_systemContext.Object, opts);
        }

        [Fact]
        public void GetDatabaseName_ReturnsOptionsDefault()
        {
            var sut = CreateSut();
            Assert.Equal(LocalDbConnectorDatabaseOptions.DEFAULT_DATABASE_NAME, sut.GetDatabaseName());
        }

        [Fact]
        public void GetDatabaseName_ReturnsCustomName()
        {
            var sut = CreateSut(new LocalDbConnectorDatabaseOptions { DatabaseName = "CustomName" });
            Assert.Equal("CustomName", sut.GetDatabaseName());
        }

        [Fact]
        public void CreateDbContext_ReturnsLocalDbConnectorDbContext()
        {
            var sut = CreateSut();
            using var context = sut.CreateDbContext();

            Assert.NotNull(context);
            Assert.IsType<LocalDbConnectorDbContext>(context);
        }

        [Fact]
        public void IsAssignableTo_ConnectorDatabaseProvider()
        {
            var sut = CreateSut();
            Assert.IsAssignableFrom<IConnectorDatabaseProvider>(sut);
        }

        [Fact]
        public void GetExternalSystemName_ReturnsLocalDb()
        {
            var sut = CreateSut();
            Assert.Equal("LocalDB", sut.GetExternalSystemName());
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
