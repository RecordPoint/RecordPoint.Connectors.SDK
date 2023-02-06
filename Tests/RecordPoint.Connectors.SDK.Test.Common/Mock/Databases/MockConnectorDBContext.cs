using Microsoft.EntityFrameworkCore;
using RecordPoint.Connectors.SDK.Databases;
using Microsoft.EntityFrameworkCore.Diagnostics;

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
