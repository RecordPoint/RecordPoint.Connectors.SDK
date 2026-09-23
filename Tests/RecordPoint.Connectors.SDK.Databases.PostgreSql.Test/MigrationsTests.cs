#nullable enable
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using RecordPoint.Connectors.SDK.Databases;
using System;
using System.Linq;
using Xunit;

namespace RecordPoint.Connectors.SDK.Databases.PostgreSql.Test
{
    /// <summary>
    /// Exercises the EF Core migrations offline. <see cref="IMigrator.GenerateScript"/>
    /// runs each migration's Up/Down methods and builds their target models without
    /// requiring a live PostgreSql instance.
    /// </summary>
    public class MigrationsTests
    {
        private static PostgreSqlConnectorDbContext CreateContext()
        {
            var options = new DbContextOptionsBuilder<ConnectorDbContext>()
                .UseNpgsql("Host=localhost;Database=test;Username=u;Password=p")
                .Options;
            return new PostgreSqlConnectorDbContext(options, PostgreSqlConnectorDbContext.DEFAULT_DB_SCHEMA_NAME);
        }

        [Fact]
        public void Context_HasBothMigrations()
        {
            using var context = CreateContext();

            var migrations = context.Database.GetMigrations().ToList();

            Assert.Contains(migrations, m => m.EndsWith("InitialCreate"));
            Assert.Contains(migrations, m => m.EndsWith("ConnectorConfigUpdate"));
        }

        [Fact]
        public void GenerateScript_Up_RunsAllMigrationUpMethods()
        {
            using var context = CreateContext();
            var migrator = context.Database.GetService<IMigrator>();

            var script = migrator.GenerateScript();

            Assert.False(string.IsNullOrWhiteSpace(script));
            // The connector schema table should be created by the migrations.
            Assert.Contains("Connector", script);
        }

        [Fact]
        public void GenerateScript_Down_RunsAllMigrationDownMethods()
        {
            using var context = CreateContext();
            var migrator = context.Database.GetService<IMigrator>();
            var lastMigration = context.Database.GetMigrations().Last();

            var script = migrator.GenerateScript(
                fromMigration: lastMigration,
                toMigration: Migration.InitialDatabase);

            Assert.False(string.IsNullOrWhiteSpace(script));
        }

        [Fact]
        public void ModelSnapshot_BuildModel_ProducesModel()
        {
            var snapshotType = typeof(PostgreSqlConnectorDbProvider).Assembly
                .GetType("RecordPoint.Connectors.SDK.Databases.PostgreSql.Migrations.PostgreSqlConnectorDbContextModelSnapshot");
            Assert.NotNull(snapshotType);

            var snapshot = (ModelSnapshot)Activator.CreateInstance(snapshotType!, nonPublic: true)!;

            // Accessing Model triggers the generated BuildModel(...) override.
            IModel model = snapshot.Model;

            Assert.NotNull(model);
            Assert.NotEmpty(model.GetEntityTypes());
        }
    }
}
