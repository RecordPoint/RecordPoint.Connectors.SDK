using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using RecordPoint.Connectors.SDK.Databases;

namespace RecordPoint.Connectors.SDK.Test.Mock.Databases
{
    public class MockConnectorDBContext : ConnectorDbContext
    {

        public MockConnectorDBContext(DbContextOptions<ConnectorDbContext> options)
            : base(options)
        { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            optionsBuilder.ConfigureWarnings(warnings => warnings.Ignore(InMemoryEventId.TransactionIgnoredWarning));
        }
    }
}
