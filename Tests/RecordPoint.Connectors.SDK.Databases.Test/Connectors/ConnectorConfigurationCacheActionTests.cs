using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using RecordPoint.Connectors.SDK.Caching;
using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.Connectors;
using RecordPoint.Connectors.SDK.Test;
using RecordPoint.Connectors.SDK.Test.Mock.Databases;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace RecordPoint.Connectors.SDK.Databases.Test.Connectors;

/// <summary>
/// SUT for ConnectorConfigurationCacheAction tests
/// </summary>
public class ConnectorConfigurationCacheActionSUT : CommonSutBase
{
    public ConnectorOptions ConnectorOptions { get; set; } = new() { ConnectorConfigurationCacheTtl = 300 };

    protected override IHostBuilder CreateSutBuilder()
        => base
            .CreateSutBuilder()
            .UseInMemoryCache<ConnectorConfigurationCacheAction, ConnectorConfigurationModel>()
            .UseMockConnectorDatabase()
            .ConfigureServices(svcs =>
                svcs.AddSingleton(Options.Create(ConnectorOptions)));
}

/// <summary>
/// Tests for <see cref="ConnectorConfigurationCacheAction"/>
/// </summary>
public class ConnectorConfigurationCacheActionTests : CommonTestBase<ConnectorConfigurationCacheActionSUT>
{
    [Fact]
    public async Task ExecuteAsync_ReturnsConfiguration_WhenConnectorExists()
    {
        // Arrange
        await StartSutAsync();

        var databaseClient = Services.GetRequiredService<IConnectorDatabaseClient>();
        using var dbContext = databaseClient.CreateDbContext();
        var connectorId = Guid.NewGuid().ToString();
        var connector = new ConnectorConfigurationModel
        {
            ConnectorId = connectorId,
            ConnectorTypeId = Guid.NewGuid().ToString(),
            TenantId = Guid.NewGuid().ToString(),
            DisplayName = "Test Connector",
            Status = "Enabled"
        };
        dbContext.Connectors.Add(connector);
        dbContext.SaveChanges();

        var action = Services.GetRequiredService<ICacheAction<ConnectorConfigurationModel>>();
        var context = CreateContext(connectorId);

        // Act
        var result = await action.ExecuteAsync(context);

        // Assert
        Assert.NotNull(result.CacheItem);
        Assert.Equal(connectorId, result.CacheItem.ConnectorId);
        Assert.True(result.Expires > DateTimeOffset.UtcNow.AddSeconds(299));
        Assert.True(result.Expires <= DateTimeOffset.UtcNow.AddSeconds(301));
    }

    [Fact]
    public async Task ExecuteAsync_ReturnsNullCacheItem_WhenConnectorDoesNotExist()
    {
        // Arrange
        await StartSutAsync();

        var connectorId = Guid.NewGuid().ToString();
        var action = Services.GetRequiredService<ICacheAction<ConnectorConfigurationModel>>();
        var context = CreateContext(connectorId);

        // Act
        var result = await action.ExecuteAsync(context);

        // Assert
        Assert.Null(result.CacheItem);
        Assert.True(result.Expires > DateTimeOffset.UtcNow.AddSeconds(299));
        Assert.True(result.Expires <= DateTimeOffset.UtcNow.AddSeconds(301));
    }

    [Fact]
    public async Task ExecuteAsync_ReturnsNullCacheItem_WhenContextIsMissing()
    {
        // Arrange
        await StartSutAsync();

        var action = Services.GetRequiredService<ICacheAction<ConnectorConfigurationModel>>();
        var context = new CacheActionContext();

        // Act
        var result = await action.ExecuteAsync(context);

        // Assert
        Assert.Null(result.CacheItem);
        Assert.Null(result.Expires);
    }

    [Fact]
    public async Task ExecuteAsync_ThrowsInvalidOperationException_WhenTtlIsZero()
    {
        // Arrange
        SUT.ConnectorOptions.ConnectorConfigurationCacheTtl = 0;
        await StartSutAsync();

        var connectorId = Guid.NewGuid().ToString();
        var action = Services.GetRequiredService<ICacheAction<ConnectorConfigurationModel>>();
        var context = CreateContext(connectorId);

        // Act & Assert
        await Assert.ThrowsAsync<InvalidOperationException>(() => action.ExecuteAsync(context));
    }

    private static CacheActionContext CreateContext(string connectorId)
        => new()
        {
            Properties = new Dictionary<string, object>
            {
                { DatabaseConnectorConfigurationManager.CONNECTOR_ID_DIMENSION, connectorId }
            }
        };
}
