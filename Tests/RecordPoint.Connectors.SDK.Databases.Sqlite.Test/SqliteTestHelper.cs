#nullable enable
using System;
using System.IO;
using Microsoft.Extensions.Options;
using Moq;
using RecordPoint.Connectors.SDK.Context;
using RecordPoint.Connectors.SDK.Databases.Sqlite;

namespace RecordPoint.Connectors.SDK.Databases.Sqlite.Test
{
    /// <summary>
    /// Creates a unique temporary data directory that is removed on <see cref="Dispose"/>,
    /// together with a configured <see cref="SqliteConnectorDatabaseProvider"/> that points at it.
    /// </summary>
    internal sealed class SqliteTestHelper : IDisposable
    {
        public string DataRootPath { get; }

        public SqliteTestHelper()
        {
            DataRootPath = Path.Combine(Path.GetTempPath(), "rp-sqlite-tests", Path.GetRandomFileName());
        }

        /// <summary>
        /// Create a provider whose data directory is this helper's temp path.
        /// </summary>
        public SqliteConnectorDatabaseProvider CreateProvider(string? databaseName = null)
        {
            var systemContext = new Mock<ISystemContext>();
            systemContext.Setup(x => x.GetDataRootPath()).Returns(DataRootPath);

            var databaseOptions = new SqliteConnectorDatabaseOptions();
            if (databaseName != null)
            {
                databaseOptions.DatabaseName = databaseName;
            }

            return new SqliteConnectorDatabaseProvider(systemContext.Object, Options.Create(databaseOptions));
        }

        public void Dispose()
        {
            try
            {
                if (Directory.Exists(DataRootPath))
                {
                    Directory.Delete(DataRootPath, recursive: true);
                }
            }
            catch
            {
                // Best-effort cleanup only.
            }
        }
    }
}
