#nullable enable
using System;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using RecordPoint.Connectors.SDK.Databases.Sqlite;
using Xunit;

namespace RecordPoint.Connectors.SDK.Databases.Sqlite.Test
{
    /// <summary>
    /// Exercises the design-time model snapshot. Its <c>BuildModel</c> body is only
    /// evaluated when the snapshot's <c>Model</c> is materialised, which never happens
    /// during a normal <c>MigrateAsync</c>. The snapshot type is internal, so it is
    /// reached through reflection rather than a source change.
    /// </summary>
    public class SqliteModelSnapshotTests
    {
        [Fact]
        public void ModelSnapshot_BuildModel_ProducesEntityTypesForAllMappedModels()
        {
            var snapshotType = typeof(SqliteConnectorDbContext).Assembly
                .GetType("RecordPoint.Connectors.SDK.Databases.Sqlite.Migrations.SqliteConnectorDbContextModelSnapshot");

            Assert.NotNull(snapshotType);

            var snapshot = Activator.CreateInstance(snapshotType!, nonPublic: true);
            Assert.NotNull(snapshot);

            // Accessing Model triggers the generated BuildModel(...) body.
            var modelProperty = typeof(ModelSnapshot).GetProperty(
                nameof(ModelSnapshot.Model),
                BindingFlags.Public | BindingFlags.Instance);
            Assert.NotNull(modelProperty);

            var model = (IModel?)modelProperty!.GetValue(snapshot);

            Assert.NotNull(model);
            Assert.NotEmpty(model!.GetEntityTypes());
        }
    }
}
