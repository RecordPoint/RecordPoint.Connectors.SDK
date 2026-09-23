#nullable enable
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Moq;
using RecordPoint.Connectors.SDK.Abstractions.Content;
using RecordPoint.Connectors.SDK.Client;
using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.Configuration;
using RecordPoint.Connectors.SDK.Connectors;
using RecordPoint.Connectors.SDK.Content;
using RecordPoint.Connectors.SDK.ContentManager;
using RecordPoint.Connectors.SDK.Notifications.Handlers;
using RecordPoint.Connectors.SDK.Notifications.Webhook;
using RecordPoint.Connectors.SDK.Observability.Null;
using RecordPoint.Connectors.SDK.Test.Mock.Context;
using RecordPoint.Connectors.SDK.Time;
using RecordPoint.Connectors.SDK.Toggles.Null;
using RecordPoint.Connectors.SDK.Work;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace RecordPoint.Connectors.SDK.Notifications.Test;

public class BuilderExtensionsTests
{
    private sealed class FakeContentRegistrationRequestAction : IContentRegistrationRequestAction
    {
        public Task<List<Channel>> GetChannelsFromRequestAsync(ConnectorConfigModel connectorConfiguration, ContentRegistrationRequest contentRegistrationRequest, CancellationToken cancellationToken)
            => Task.FromResult(new List<Channel>());
    }

    private sealed class FakeConnectorSecretAction : IConnectorSecretAction
    {
        public Task SaveSecretsAsync(ConnectorConfigModel connectorConfiguration, IList<ConnectorSecret> secrets)
            => Task.CompletedTask;
    }

    private static IHost BuildHost(System.Func<IHostBuilder, IHostBuilder> configureNotifications)
    {
        var builder = new HostBuilder()
            .UseSystemTime()
            .UseMockSystemContext("builder-test")
            .UseNullToggleProvider()
            .UseNullTelemetryTracking();

        configureNotifications(builder);

        // Register (or override) dependencies required to resolve handlers / managers.
        builder.ConfigureServices(services =>
        {
            services.AddSingleton(Mock.Of<IConnectorConfigurationManager>());
            services.AddSingleton(Mock.Of<IWorkQueueClient>());
            services.AddSingleton(Mock.Of<IManagedWorkFactory>());
            services.AddSingleton(Mock.Of<IR365ConfigurationClient>());
            services.AddSingleton(Mock.Of<INotificationApiManager>());
            services.AddSingleton(Mock.Of<IR365NotificationClient>());
        });

        return builder.Build();
    }

    private static List<INotificationStrategy> GetStrategies(IHost host)
        => host.Services.GetServices<INotificationStrategy>().ToList();

    [Fact]
    public void UseConnectorConfigHandlers_RegistersBaseHandlers()
    {
        using var host = BuildHost(b => b.UseConnectorConfigHandlers());
        var strategies = GetStrategies(host);

        Assert.Contains(strategies, s => s is ConnectorConfigCreatedHandler);
        Assert.Contains(strategies, s => s is ConnectorConfigUpdatedHandler);
        Assert.Contains(strategies, s => s is ConnectorConfigDeletedHandler);
        Assert.Contains(strategies, s => s is ItemDestroyedHandler);
        Assert.Contains(strategies, s => s is PingHandler);
        Assert.DoesNotContain(strategies, s => s is ContentRegistrationHandler);
        Assert.DoesNotContain(strategies, s => s is ConnectorSecretHandler);
    }

    [Fact]
    public void UseNotificationHandlers_RegistersAllHandlersAndActions()
    {
        using var host = BuildHost(b => b.UseNotificationHandlers<FakeContentRegistrationRequestAction, FakeConnectorSecretAction>());
        var strategies = GetStrategies(host);

        Assert.Contains(strategies, s => s is ContentRegistrationHandler);
        Assert.Contains(strategies, s => s is ConnectorSecretHandler);
        Assert.IsType<FakeContentRegistrationRequestAction>(host.Services.GetRequiredService<IContentRegistrationRequestAction>());
        Assert.IsType<FakeConnectorSecretAction>(host.Services.GetRequiredService<IConnectorSecretAction>());
    }

    [Fact]
    public void UseConnectorSecretHandler_RegistersSecretHandler()
    {
        using var host = BuildHost(b => b.UseConnectorSecretHandler<FakeConnectorSecretAction>());
        var strategies = GetStrategies(host);

        Assert.Contains(strategies, s => s is ConnectorSecretHandler);
        Assert.IsType<FakeConnectorSecretAction>(host.Services.GetRequiredService<IConnectorSecretAction>());
    }

    [Fact]
    public void UseContentRegistrationHandler_RegistersContentRegistrationHandler()
    {
        using var host = BuildHost(b => b.UseContentRegistrationHandler<FakeContentRegistrationRequestAction>());
        var strategies = GetStrategies(host);

        Assert.Contains(strategies, s => s is ContentRegistrationHandler);
        Assert.IsType<FakeContentRegistrationRequestAction>(host.Services.GetRequiredService<IContentRegistrationRequestAction>());
    }

    [Fact]
    public void UseWebhookNotifications_RegistersPushManager()
    {
        using var host = BuildHost(b => b.UseWebhookNotifications());
        Assert.IsType<PushNotificationManager>(host.Services.GetRequiredService<INotificationManager>());
        Assert.NotNull(host.Services.GetRequiredService<WebhookOperation>());
    }

    [Fact]
    public void UseWebhookNotifications_Generic_RegistersPushManagerAndContentHandler()
    {
        using var host = BuildHost(b => b.UseWebhookNotifications<FakeContentRegistrationRequestAction>());
        Assert.IsType<PushNotificationManager>(host.Services.GetRequiredService<INotificationManager>());
        Assert.Contains(GetStrategies(host), s => s is ContentRegistrationHandler);
    }

    [Fact]
    public void UseWebhookNotifications_GenericWithSecret_RegistersAllHandlers()
    {
        using var host = BuildHost(b => b.UseWebhookNotifications<FakeContentRegistrationRequestAction, FakeConnectorSecretAction>());
        Assert.IsType<PushNotificationManager>(host.Services.GetRequiredService<INotificationManager>());
        var strategies = GetStrategies(host);
        Assert.Contains(strategies, s => s is ContentRegistrationHandler);
        Assert.Contains(strategies, s => s is ConnectorSecretHandler);
    }

    [Fact]
    public void UsePolledNotifications_RegistersPullManager()
    {
        using var host = BuildHost(b => b.UsePolledNotifications());
        Assert.IsType<PullNotificationManager>(host.Services.GetRequiredService<INotificationManager>());
        Assert.NotNull(host.Services.GetRequiredService<PollNotificationsOperation>());
    }

    [Fact]
    public void UsePolledNotifications_Generic_RegistersPullManagerAndContentHandler()
    {
        using var host = BuildHost(b => b.UsePolledNotifications<FakeContentRegistrationRequestAction>());
        Assert.IsType<PullNotificationManager>(host.Services.GetRequiredService<INotificationManager>());
        Assert.Contains(GetStrategies(host), s => s is ContentRegistrationHandler);
    }

    [Fact]
    public void UsePolledNotifications_GenericWithSecret_RegistersAllHandlers()
    {
        using var host = BuildHost(b => b.UsePolledNotifications<FakeContentRegistrationRequestAction, FakeConnectorSecretAction>());
        Assert.IsType<PullNotificationManager>(host.Services.GetRequiredService<INotificationManager>());
        var strategies = GetStrategies(host);
        Assert.Contains(strategies, s => s is ContentRegistrationHandler);
        Assert.Contains(strategies, s => s is ConnectorSecretHandler);
    }

    [Fact]
    public void UseNotifications_Generic_UsesWebhookByDefault()
    {
        using var host = BuildHost(b => b.UseNotifications<FakeContentRegistrationRequestAction>());
        Assert.IsType<PushNotificationManager>(host.Services.GetRequiredService<INotificationManager>());
    }

    [Fact]
    public void UseNotifications_GenericWithSecret_UsesWebhookByDefault()
    {
        using var host = BuildHost(b => b.UseNotifications<FakeContentRegistrationRequestAction, FakeConnectorSecretAction>());
        Assert.IsType<PushNotificationManager>(host.Services.GetRequiredService<INotificationManager>());
    }

    [Fact]
    public void UseAsyncNotificationOperation_RegistersQueueableWork()
    {
        using var host = BuildHost(b => b.UseWebhookNotifications().UseAsyncNotificationOperation());
        using var scope = host.Services.CreateScope();
        var queueableWork = scope.ServiceProvider.GetServices<IQueueableWork>().ToList();
        Assert.Contains(queueableWork, w => w is AsyncNotificationOperation);
    }
}
