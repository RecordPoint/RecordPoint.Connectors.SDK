#nullable enable
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using RecordPoint.Connectors.SDK.Databases;
using Xunit;

namespace RecordPoint.Connectors.SDK.Databases.AzureSql.Test
{
    /// <summary>
    /// Exercises the EF Core migration classes offline (no live database). Building each
    /// migration's Up/Down operations and target model, plus the model snapshot, invokes the
    /// generated migration code without ever opening a connection.
    /// </summary>
    public class MigrationsTests
    {
        private const string SqlServerProvider = "Microsoft.EntityFrameworkCore.SqlServer";

        private static AzureSqlConnectorDbContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<ConnectorDbContext>()
                .UseSqlServer(TestFactory.UnreachableConnectionString)
                .Options;
            return new AzureSqlConnectorDbContext(options, AzureSqlConnectorDbContext.DEFAULT_DB_SCHEMA_NAME);
        }

        [Fact]
        public void MigrationsAssembly_ContainsExpectedMigrations()
        {
            using var context = CreateContext();
            var migrationsAssembly = context.GetService<IMigrationsAssembly>();

            var names = migrationsAssembly.Migrations.Keys.ToList();

            Assert.Contains(names, n => n.EndsWith("InitialCreate"));
            Assert.Contains(names, n => n.EndsWith("ConnectorConfigUpdate"));
        }

        [Fact]
        public void EachMigration_BuildsUpDownAndTargetModel()
        {
            using var context = CreateContext();
            var migrationsAssembly = context.GetService<IMigrationsAssembly>();

            foreach (var migrationType in migrationsAssembly.Migrations.Values)
            {
                var migration = migrationsAssembly.CreateMigration(migrationType, SqlServerProvider);

                // Accessing these properties runs the generated Up(), Down() and
                // BuildTargetModel() methods.
                Assert.NotEmpty(migration.UpOperations);
                Assert.NotEmpty(migration.DownOperations);
                Assert.NotNull(migration.TargetModel);
            }
        }

        [Fact]
        public void ModelSnapshot_BuildsModel()
        {
            using var context = CreateContext();
            var migrationsAssembly = context.GetService<IMigrationsAssembly>();

            var snapshot = migrationsAssembly.ModelSnapshot;

            Assert.NotNull(snapshot);
            // Accessing the model runs the generated BuildModel() method.
            Assert.NotNull(snapshot!.Model);
            Assert.NotEmpty(snapshot.Model.GetEntityTypes());
        }
    }
}
