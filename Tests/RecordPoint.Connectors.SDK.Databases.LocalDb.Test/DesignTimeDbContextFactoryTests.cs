#nullable enable
using System;
using RecordPoint.Connectors.SDK.Databases;
using RecordPoint.Connectors.SDK.Databases.LocalDb;
using Xunit;

namespace RecordPoint.Connectors.SDK.Databases.LocalDb.Test
{
    /// <summary>
    /// Unit tests for <see cref="DesignTimeDbContextFactory"/>.
    /// </summary>
    public sealed class DesignTimeDbContextFactoryTests
    {
        [Fact]
        public void GetContextOptionsBuilder_ReturnsConfiguredBuilder()
        {
            var builder = DesignTimeDbContextFactory.GetContextOptionsBuilder();

            Assert.NotNull(builder);
            Assert.True(builder.Options.Extensions.GetEnumerator().MoveNext());
        }

        [Fact]
        public void CreateDbContext_ReturnsLocalDbConnectorDbContext()
        {
            var factory = new DesignTimeDbContextFactory();

            using var context = factory.CreateDbContext(Array.Empty<string>());

            Assert.NotNull(context);
            Assert.IsType<LocalDbConnectorDbContext>(context);
            Assert.IsAssignableFrom<ConnectorDbContext>(context);
        }
    }
}
