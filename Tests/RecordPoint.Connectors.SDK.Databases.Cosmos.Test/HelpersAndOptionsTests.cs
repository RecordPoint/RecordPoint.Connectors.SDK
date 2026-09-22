#nullable enable
using System;
using Moq;
using RecordPoint.Connectors.SDK.Databases.Cosmos.Helpers;
using RecordPoint.Connectors.SDK.Databases.Cosmos.Manager;
using RecordPoint.Connectors.SDK.Databases.Cosmos.SemephoreLock;
using RecordPoint.Connectors.SDK.Toggles;
using Xunit;

namespace RecordPoint.Connectors.SDK.Databases.Cosmos.Test
{
    public class CosmosEndpointHelperTests
    {
        [Fact]
        public void BuildCosmosAccountEndpoint_UsesDocumentsDomain_WhenDedicatedGatewayToggleOff()
        {
            var toggle = new Mock<IToggleProvider>();
            toggle.Setup(t => t.GetToggleBool("Tmp-UseCosmosDedicatedGateway", false)).Returns(false);

            var endpoint = CosmosEndpointHelper.BuildCosmosAccountEndpoint(toggle.Object, "myaccount");

            Assert.Equal("https://myaccount.documents.azure.com/", endpoint);
        }

        [Fact]
        public void BuildCosmosAccountEndpoint_UsesDedicatedGatewayDomain_WhenToggleOn()
        {
            var toggle = new Mock<IToggleProvider>();
            toggle.Setup(t => t.GetToggleBool("Tmp-UseCosmosDedicatedGateway", false)).Returns(true);

            var endpoint = CosmosEndpointHelper.BuildCosmosAccountEndpoint(toggle.Object, "myaccount");

            Assert.Equal("https://myaccount.sqlx.cosmos.azure.com/", endpoint);
        }

        [Fact]
        public void BuildCosmosAccountEndpoint_HandlesNullAccountName()
        {
            var toggle = new Mock<IToggleProvider>();
            toggle.Setup(t => t.GetToggleBool(It.IsAny<string>(), It.IsAny<bool>())).Returns(false);

            var endpoint = CosmosEndpointHelper.BuildCosmosAccountEndpoint(toggle.Object, null);

            Assert.Equal("https://.documents.azure.com/", endpoint);
        }
    }

    public class ConnectorToggleExtensionsTests
    {
        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void UseCosmosDedicatedGateway_DelegatesToToggleProvider_WithDefaultFalse(bool value)
        {
            var toggle = new Mock<IToggleProvider>();
            toggle.Setup(t => t.GetToggleBool("Tmp-UseCosmosDedicatedGateway", false)).Returns(value);

            var result = toggle.Object.UseCosmosDedicatedGateway();

            Assert.Equal(value, result);
            toggle.Verify(t => t.GetToggleBool("Tmp-UseCosmosDedicatedGateway", false), Times.Once);
        }
    }

    public class CosmosDbConnectorDatabaseOptionsTests
    {
        [Fact]
        public void Defaults_AreAsExpected()
        {
            var options = new CosmosDbConnectorDatabaseOptions();

            Assert.Equal("connector-db", options.DatabaseName);
            Assert.Equal(CosmosDbConnectorDatabaseOptions.DEFAULT_DATABASE_NAME, options.DatabaseName);
            Assert.Equal(string.Empty, options.ConnectionString);
            Assert.Equal(string.Empty, options.CosmosDbAccountName);
            Assert.True(options.UseCamelCaseNamingPolicy);
            Assert.False(options.UseGateWayConnectionMode);
            Assert.Null(options.TlsVersion);
            Assert.False(options.UseDirectReads);
            Assert.Equal("CosmosDbConnectorDatabase", CosmosDbConnectorDatabaseOptions.SECTION_NAME);
        }

        [Fact]
        public void Properties_AreSettable()
        {
            var options = new CosmosDbConnectorDatabaseOptions
            {
                DatabaseName = "db",
                ConnectionString = "cs",
                CosmosDbAccountName = "acct",
                UseCamelCaseNamingPolicy = false,
                UseGateWayConnectionMode = true,
                TlsVersion = "Tls12",
                UseDirectReads = true
            };

            Assert.Equal("db", options.DatabaseName);
            Assert.Equal("cs", options.ConnectionString);
            Assert.Equal("acct", options.CosmosDbAccountName);
            Assert.False(options.UseCamelCaseNamingPolicy);
            Assert.True(options.UseGateWayConnectionMode);
            Assert.Equal("Tls12", options.TlsVersion);
            Assert.True(options.UseDirectReads);
        }
    }

    public class BaseCosmosDbItemTests
    {
        [Fact]
        public void Defaults_TtlIsMinusOne_AndNullableFieldsNull()
        {
            var item = new BaseCosmosDbItem();

            Assert.Equal(-1, item.Ttl);
            Assert.Null(item.ETag);
            Assert.Null(item.LastModifiedCosmosTimeStamp);
        }

        [Fact]
        public void Properties_AreSettable()
        {
            var ts = DateTime.UtcNow;
            var item = new BaseCosmosDbItem
            {
                Id = "the-id",
                Ttl = 120,
                ETag = "etag",
                LastModifiedCosmosTimeStamp = ts
            };

            Assert.Equal("the-id", item.Id);
            Assert.Equal(120, item.Ttl);
            Assert.Equal("etag", item.ETag);
            Assert.Equal(ts, item.LastModifiedCosmosTimeStamp);
        }
    }

    public class SemaphoreLockCosmosDbItemTests
    {
        [Fact]
        public void ContainerName_Constant_IsSemaphorelock()
        {
            Assert.Equal("semaphorelock", SemaphoreLockCosmosDbItem.COSMOS_DB_CONTAINER_NAME);
        }

        [Fact]
        public void InheritsBaseCosmosDbItem_AndStoresLockExpiry()
        {
            var expiry = DateTimeOffset.Now.AddMinutes(5);
            var item = new SemaphoreLockCosmosDbItem { Id = "k", LockExpiry = expiry, Ttl = 300 };

            Assert.IsAssignableFrom<BaseCosmosDbItem>(item);
            Assert.Equal(expiry, item.LockExpiry);
            Assert.Equal("k", item.Id);
            Assert.Equal(300, item.Ttl);
        }
    }
}
