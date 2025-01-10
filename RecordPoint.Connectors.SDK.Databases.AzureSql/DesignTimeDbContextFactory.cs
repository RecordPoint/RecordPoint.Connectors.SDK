﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace RecordPoint.Connectors.SDK.Databases.AzureSql
{
    /// <summary>
    /// The design time db context factory.
    /// </summary>
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ConnectorDbContext>
    {
        /// <summary>
        /// Get context options builder.
        /// </summary>
        /// <returns><![CDATA[DbContextOptionsBuilder<ConnectorDbContext>]]></returns>
        public static DbContextOptionsBuilder<ConnectorDbContext> GetContextOptionsBuilder() => new DbContextOptionsBuilder<ConnectorDbContext>()
                .UseSqlServer("Data Source=connector.db");

        /// <summary>
        /// Creates db context.
        /// </summary>
        /// <param name="args">The args.</param>
        /// <returns>A ConnectorDbContext</returns>
        public ConnectorDbContext CreateDbContext(string[] args) => new AzureSqlConnectorDbContext(GetContextOptionsBuilder().Options, AzureSqlConnectorDbContext.DEFAULT_DB_SCHEMA_NAME);
    }
}
