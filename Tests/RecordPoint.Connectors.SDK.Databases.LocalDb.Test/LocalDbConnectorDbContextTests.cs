#nullable enable
using RecordPoint.Connectors.SDK.Databases;
using RecordPoint.Connectors.SDK.Databases.LocalDb;
using Xunit;

namespace RecordPoint.Connectors.SDK.Databases.LocalDb.Test
{
    /// <summary>
    /// Unit tests for <see cref="LocalDbConnectorDbContext"/>.
    /// </summary>
    public sealed class LocalDbConnectorDbContextTests
    {
        [Fact]
        public void Constructor_BuildsContext_FromOptions()
        {
            var options = DesignTimeDbContextFactory.GetContextOptionsBuilder().Options;

            using var context = new LocalDbConnectorDbContext(options);

            Assert.NotNull(context);
            Assert.IsAssignableFrom<ConnectorDbContext>(context);
        }
    }
}
