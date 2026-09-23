#nullable enable
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.SqlClient;
using RecordPoint.Connectors.SDK.Databases;
using Xunit;

namespace RecordPoint.Connectors.SDK.Databases.AzureSql.Test
{
    public class AzureSqlConnectorDbProviderTests
    {
        [Fact]
        public void Constructor_NullConnectionString_Throws()
        {
            var ex = Assert.Throws<RequiredValueNullException>(() =>
                TestFactory.CreateProvider(TestFactory.Options(connectionString: null)));

            Assert.Contains("ConnectionString", ex.Message);
        }

        [Fact]
        public void GetConnectionString_ReturnsConfiguredConnectionString()
        {
            const string connectionString = "Server=host;Database=Db;Encrypt=False";
            var provider = TestFactory.CreateProvider(TestFactory.Options(connectionString: connectionString));

            Assert.Equal(connectionString, provider.GetConnectionString());
        }

        [Fact]
        public void GetDatabaseName_ExtractsInitialCatalog()
        {
            var provider = TestFactory.CreateProvider(
                TestFactory.Options(connectionString: "Server=host;Database=MyCatalog;Encrypt=False"));

            Assert.Equal("MyCatalog", provider.CallGetDatabaseName());
        }

        [Fact]
        public void GetDatabaseName_NoCatalog_ReturnsEmpty()
        {
            var provider = TestFactory.CreateProvider(
                TestFactory.Options(connectionString: "Server=host;Encrypt=False"));

            Assert.Equal(string.Empty, provider.CallGetDatabaseName());
        }

        [Fact]
        public void GetAdminConnectionString_NoAdminCredentials_LeavesConnectionUnchanged()
        {
            const string connectionString = "Server=host;Database=Db;Encrypt=False";
            var provider = TestFactory.CreateProvider(
                TestFactory.Options(connectionString: connectionString));

            var admin = provider.CallGetAdminConnectionString();
            var builder = new SqlConnectionStringBuilder(admin);

            Assert.Equal(string.Empty, builder.UserID);
            Assert.Equal(string.Empty, builder.Password);
            Assert.Equal("Db", builder.InitialCatalog);
        }

        [Fact]
        public void GetAdminConnectionString_WithUsernameOnly_SetsUserId()
        {
            var provider = TestFactory.CreateProvider(
                TestFactory.Options(
                    connectionString: "Server=host;Database=Db;Encrypt=False",
                    adminUsername: "mi-identity"));

            var builder = new SqlConnectionStringBuilder(provider.CallGetAdminConnectionString());

            Assert.Equal("mi-identity", builder.UserID);
            Assert.Equal(string.Empty, builder.Password);
        }

        [Fact]
        public void GetAdminConnectionString_WithUsernameAndPassword_SetsBoth()
        {
            var provider = TestFactory.CreateProvider(
                TestFactory.Options(
                    connectionString: "Server=host;Database=Db;Encrypt=False",
                    adminUsername: "sa",
                    adminPassword: "P@ssw0rd"));

            var builder = new SqlConnectionStringBuilder(provider.CallGetAdminConnectionString());

            Assert.Equal("sa", builder.UserID);
            Assert.Equal("P@ssw0rd", builder.Password);
        }

        [Fact]
        public void GetAdminConnectionString_WithPasswordOnly_SetsPassword()
        {
            var provider = TestFactory.CreateProvider(
                TestFactory.Options(
                    connectionString: "Server=host;Database=Db;User Id=existing;Encrypt=False",
                    adminPassword: "pw"));

            var builder = new SqlConnectionStringBuilder(provider.CallGetAdminConnectionString());

            Assert.Equal("existing", builder.UserID);
            Assert.Equal("pw", builder.Password);
        }

        [Fact]
        public void GetAdminConnectionString_IsCached()
        {
            var provider = TestFactory.CreateProvider();

            var first = provider.CallGetAdminConnectionString();
            var second = provider.CallGetAdminConnectionString();

            Assert.Same(first, second);
        }

        [Fact]
        public void CreateDbContext_ReturnsAzureSqlContext()
        {
            var provider = TestFactory.CreateProvider();

            using var context = provider.CreateDbContext();

            Assert.IsType<AzureSqlConnectorDbContext>(context);
        }

        [Fact]
        public void CreateDbAdminContext_ReturnsAzureSqlContext()
        {
            var provider = TestFactory.CreateProvider();

            using var context = provider.CallCreateDbAdminContext();

            Assert.IsType<AzureSqlConnectorDbContext>(context);
        }

        [Fact]
        public void GetContextOptionsBuilder_ConfiguresSqlServer()
        {
            var provider = TestFactory.CreateProvider();

            var builder = provider.CallGetContextOptionsBuilder();

            Assert.True(builder.Options.Extensions.Any());
            Assert.Contains(
                builder.Options.Extensions,
                e => e.GetType().Name.Contains("SqlServer"));
        }

        [Fact]
        public void GetAdminContextOptionsBuilder_ConfiguresSqlServer()
        {
            var provider = TestFactory.CreateProvider();

            var builder = provider.CallGetAdminContextOptionsBuilder();

            Assert.True(builder.Options.Extensions.Any());
            Assert.Contains(
                builder.Options.Extensions,
                e => e.GetType().Name.Contains("SqlServer"));
        }

        [Fact]
        public void GetSqlDatabaseScript_LoadsEmbeddedScriptAndSubstitutesParameters()
        {
            var provider = TestFactory.CreateProvider();

            var script = provider.CallGetSqlDatabaseScript(
                "AzureSqlSchemaCreate.sql",
                new Dictionary<string, string> { ["SchemaName"] = "connector" });

            Assert.Contains("CREATE SCHEMA connector", script);
            Assert.DoesNotContain("{SchemaName}", script);
        }

        [Fact]
        public void GetSqlDatabaseScript_UnknownScript_Throws()
        {
            var provider = TestFactory.CreateProvider();

            Assert.ThrowsAny<System.Exception>(() =>
                provider.CallGetSqlDatabaseScript(
                    "DoesNotExist.sql",
                    new Dictionary<string, string>()));
        }
    }
}
