#nullable enable
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.Notifications.Handlers;
using RecordPoint.Connectors.SDK.Requests;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace RecordPoint.Connectors.SDK.Notifications.Test;

public class ConnectorRequestBuilderExtensionsTests
{
    [Fact]
    public void EnablingConnectorRequestsRegistersTheNotificationStrategyAndTheCallbackClient()
    {
        var services = Capture(builder => builder.UseConnectorRequestHandlers());

        Assert.Single(services, d =>
            d.ServiceType == typeof(INotificationStrategy) &&
            d.ImplementationType == typeof(ConnectorRequestHandler));

        Assert.Single(services, d =>
            d.ServiceType == typeof(IConnectorRequestCallbackClient) &&
            d.ImplementationType == typeof(ConnectorRequestCallbackClient));
    }

    [Fact]
    public void EnablingConnectorRequestsTwiceStillRegistersOneStrategy()
    {
        // The manager keys strategies by notification type, so a second one throws at startup.
        var services = Capture(builder => builder
            .UseConnectorRequestHandlers()
            .UseConnectorRequestHandlers());

        Assert.Single(services, d =>
            d.ServiceType == typeof(INotificationStrategy) &&
            d.ImplementationType == typeof(ConnectorRequestHandler));
    }

    [Fact]
    public void AddingAHandlerAlsoEnablesConnectorRequests()
    {
        // So one call is enough, and requests cannot arrive with nothing to dispatch them.
        var services = Capture(builder => builder.AddConnectorRequestHandler<StubHandler>());

        Assert.Single(services, d =>
            d.ServiceType == typeof(INotificationStrategy) &&
            d.ImplementationType == typeof(ConnectorRequestHandler));

        Assert.Single(services, d => d.ServiceType == typeof(IConnectorRequestHandler));
    }

    [Fact]
    public void SeveralHandlersAreAllRegistered()
    {
        var services = Capture(builder => builder
            .AddConnectorRequestHandler<StubHandler>()
            .AddConnectorRequestHandler<OtherStubHandler>());

        Assert.Equal(2, services.Count(d => d.ServiceType == typeof(IConnectorRequestHandler)));
    }

    [Fact]
    public void RegistrationDoesNotDependOnHowNotificationsArrive()
    {
        // Both the webhook and polled managers resolve INotificationStrategy, so neither needs changing.
        var services = Capture(builder => builder.UseConnectorRequestHandlers());

        var strategy = Assert.Single(services, d =>
            d.ImplementationType == typeof(ConnectorRequestHandler));

        Assert.Equal(typeof(INotificationStrategy), strategy.ServiceType);
        Assert.Equal(ServiceLifetime.Singleton, strategy.Lifetime);
    }

    private static IServiceCollection Capture(System.Func<IHostBuilder, IHostBuilder> configure)
    {
        IServiceCollection? captured = null;

        var builder = new HostBuilder();
        configure(builder);
        builder.ConfigureServices(services => captured = services);
        builder.Build();

        Assert.NotNull(captured);
        return captured!;
    }

    private sealed class StubHandler : IConnectorRequestHandler
    {
        public string RequestType => ConnectorRequestTypes.Echo;

        public Task<ConnectorRequestResponse> HandleAsync(ConnectorConfigModel connectorConfiguration, ConnectorRequestEnvelope request, CancellationToken cancellationToken)
            => Task.FromResult(ConnectorRequestResponse.Ok(request.CorrelationId));
    }

    private sealed class OtherStubHandler : IConnectorRequestHandler
    {
        public string RequestType => "SomethingElse";

        public Task<ConnectorRequestResponse> HandleAsync(ConnectorConfigModel connectorConfiguration, ConnectorRequestEnvelope request, CancellationToken cancellationToken)
            => Task.FromResult(ConnectorRequestResponse.Ok(request.CorrelationId));
    }
}
