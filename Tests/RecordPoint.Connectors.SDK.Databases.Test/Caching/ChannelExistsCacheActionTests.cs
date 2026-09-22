using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RecordPoint.Connectors.SDK.Caching;
using RecordPoint.Connectors.SDK.Content;
using RecordPoint.Connectors.SDK.Databases.Caching;
using RecordPoint.Connectors.SDK.Test;
using RecordPoint.Connectors.SDK.Test.Mock.Databases;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace RecordPoint.Connectors.SDK.Databases.Test.Caching;

/// <summary>
/// SUT for ChannelExistsCacheAction Tests
/// </summary>
public class ChannelExistsCacheActionSUT : CommonSutBase
{
    protected override IHostBuilder CreateSutBuilder()
        => base
            .CreateSutBuilder()           
            .UseInMemoryCache<ChannelExistsCacheAction, bool>()
            .UseMockConnectorDatabase();

    public override async Task StopSUTAsync()
    {  
        await base.StopSUTAsync();
    }
}


public class ChannelExistsCacheActionTests : CommonTestBase<ChannelExistsCacheActionSUT>
{   

    [Fact]
    public async Task ExecuteAsync_ReturnsTrue_WhenChannelExists()
    {
        // Arrange
        await StartSutAsync();

        var databaseClient = Services.GetRequiredService<IConnectorDatabaseClient>();
        using var dbContext = databaseClient.CreateDbContext();      
        var connectorId = Guid.NewGuid().ToString();
        var externalId = Guid.NewGuid().ToString();
        var context = CreateContext(connectorId, externalId);
        var channel = new ChannelModel { ConnectorId = connectorId, ExternalId = externalId };

        dbContext.Channels.Add(channel);
        dbContext.SaveChanges();

        var action = Services.GetRequiredService<ICacheAction<bool>>();

        // Act
        var result = await action.ExecuteAsync(context);

        // Assert
        Assert.True(result.CacheItem);
        Assert.True(result.Expires > DateTimeOffset.UtcNow.AddMinutes(59));
        Assert.True(result.Expires <= DateTimeOffset.UtcNow.AddMinutes(61));
    }

    [Fact]
    public async Task ExecuteAsync_ReturnsFalse_WhenChannelDoesNotExist()
    {
        // Arrange
        await StartSutAsync();

        var connectorId = Guid.NewGuid().ToString();
        var externalId = Guid.NewGuid().ToString();
        var context = CreateContext(connectorId, externalId);
        var action = Services.GetRequiredService<ICacheAction<bool>>();

        // Act
        var result = await action.ExecuteAsync(context);

        // Assert
        Assert.False(result.CacheItem);
        Assert.True(result.Expires > DateTimeOffset.UtcNow.AddMinutes(59));
        Assert.True(result.Expires <= DateTimeOffset.UtcNow.AddMinutes(61));
    }

    [Fact]
    public async Task ExecuteAsync_ReturnsFalse_WhenThereIsMissingContext()
    {
        // Arrange
        await StartSutAsync();

        var context = new CacheActionContext();
        var action = Services.GetRequiredService<ICacheAction<bool>>();

        // Act
        var result = await action.ExecuteAsync(context);

        // Assert
        Assert.False(result.CacheItem);
        Assert.True(result.Expires > DateTimeOffset.UtcNow.AddMinutes(59));
        Assert.True(result.Expires <= DateTimeOffset.UtcNow.AddMinutes(61));
    }

    private static CacheActionContext CreateContext(string connectorId, string externalId)
    {
        return new CacheActionContext
        {
            Properties = new Dictionary<string, object>
                {
                    { "ConnectorId", connectorId },
                    { "ExternalId", externalId }
                }
        };
    }
}