#nullable enable
using Moq;
using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.Notifications.Handlers;
using RecordPoint.Connectors.SDK.Requests;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace RecordPoint.Connectors.SDK.Notifications.Test;

public class ConnectorRequestHandlerTests
{
    private const string CorrelationId = "the-correlation-id";

    private readonly Mock<IConnectorRequestCallbackClient> _callbackClient = new();

    [Fact]
    public void TheHandlerAnswersTheConnectorRequestNotification()
    {
        Assert.Equal("ConnectorRequest", CreateSut().NotificationType);
    }

    [Fact]
    public async Task TheRequestReachesTheHandlerRegisteredForItsType()
    {
        var echo = new StubHandler(ConnectorRequestTypes.Echo);
        var other = new StubHandler("SomethingElse");
        var sut = CreateSut(echo, other);

        await sut.HandleNotificationAsync(CreateNotification(NewEnvelope()), CancellationToken.None);

        Assert.NotNull(echo.Received);
        Assert.Null(other.Received);
    }

    [Fact]
    public async Task ARequestTypeInAnotherCasingStillReachesItsHandler()
    {
        // A replayed envelope cannot rely on the platform having normalised the casing.
        var echo = new StubHandler(ConnectorRequestTypes.Echo);
        var sut = CreateSut(echo);

        var envelope = NewEnvelope();
        envelope.RequestType = "eChO";

        await sut.HandleNotificationAsync(CreateNotification(envelope), CancellationToken.None);

        Assert.NotNull(echo.Received);
    }

    [Fact]
    public async Task TheAnswerCarriesTheRequestTypeCorrelationIdAndScopeFromTheRequest()
    {
        // The SDK sets these, not the handler. They are what address the answer.
        var handler = new StubHandler(ConnectorRequestTypes.Echo)
        {
            Answer = new ConnectorRequestResponse { Outcome = RequestOutcomeType.Ok }
        };
        var sut = CreateSut(handler);

        var envelope = NewEnvelope();
        envelope.ResponseScope = "table-a";

        await sut.HandleNotificationAsync(CreateNotification(envelope), CancellationToken.None);

        var sent = CapturedAnswer();
        Assert.Equal(ConnectorRequestTypes.Echo, sent.RequestType);
        Assert.Equal(CorrelationId, sent.CorrelationId);
        Assert.Equal("table-a", sent.ResponseScope);
    }

    [Fact]
    public async Task TheAnswerIsReturnedForTheConfigurationTheRequestWasSentTo()
    {
        var notification = CreateNotification(NewEnvelope());
        var sut = CreateSut(new StubHandler(ConnectorRequestTypes.Echo));

        await sut.HandleNotificationAsync(notification, CancellationToken.None);

        _callbackClient.Verify(x => x.SendAsync(
            notification.ConnectorConfig,
            It.IsAny<ConnectorRequestResponse>(),
            It.IsAny<CancellationToken>()), Times.Once());
    }

    [Fact]
    public async Task AnUnsupportedRequestTypeIsAnsweredRatherThanIgnored()
    {
        // A caller is waiting, and "not supported" beats leaving them polling for nothing.
        var sut = CreateSut(new StubHandler("SomethingElse"));

        var outcome = await sut.HandleNotificationAsync(CreateNotification(NewEnvelope()), CancellationToken.None);

        var sent = CapturedAnswer();
        Assert.Equal(RequestOutcomeType.Failed, sent.Outcome);
        Assert.Contains(ConnectorRequestTypes.Echo, Assert.Single(sent.Messages));
        Assert.Equal(NotificationOutcomeType.Ok, outcome.OutcomeType);
    }

    [Fact]
    public async Task AHandlerThatThrowsStillProducesAnAnswer()
    {
        var handler = new StubHandler(ConnectorRequestTypes.Echo)
        {
            Throw = new InvalidOperationException("Login failed for user sa with password hunter2")
        };
        var sut = CreateSut(handler);

        var outcome = await sut.HandleNotificationAsync(CreateNotification(NewEnvelope()), CancellationToken.None);

        var sent = CapturedAnswer();
        Assert.Equal(RequestOutcomeType.Failed, sent.Outcome);
        Assert.Equal(NotificationOutcomeType.Ok, outcome.OutcomeType);

        // Stored and shown to an administrator, so it must not carry what the source system said.
        Assert.DoesNotContain("hunter2", Assert.Single(sent.Messages));
    }

    [Fact]
    public async Task AHandlerThatReturnsNothingProducesAFailedAnswer()
    {
        var handler = new StubHandler(ConnectorRequestTypes.Echo) { Answer = null };
        var sut = CreateSut(handler);

        await sut.HandleNotificationAsync(CreateNotification(NewEnvelope()), CancellationToken.None);

        Assert.Equal(RequestOutcomeType.Failed, CapturedAnswer().Outcome);
    }

    [Fact]
    public async Task AFailedAnswerIsStillASuccessfullyHandledNotification()
    {
        // Delivered and answered: the caller learns the failure from the answer.
        var handler = new StubHandler(ConnectorRequestTypes.Echo)
        {
            Answer = ConnectorRequestResponse.Failed(CorrelationId, "no")
        };
        var sut = CreateSut(handler);

        var outcome = await sut.HandleNotificationAsync(CreateNotification(NewEnvelope()), CancellationToken.None);

        Assert.Equal(NotificationOutcomeType.Ok, outcome.OutcomeType);
    }

    [Fact]
    public async Task ANotificationWhoseAnswerCannotBeReturnedIsReportedAsFailed()
    {
        // The only failed notification. Nothing redelivers it, so this is a signal not a recovery.
        _callbackClient
            .Setup(x => x.SendAsync(It.IsAny<ConnectorConfigModel>(), It.IsAny<ConnectorRequestResponse>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new InvalidOperationException("the platform was unreachable"));
        var sut = CreateSut(new StubHandler(ConnectorRequestTypes.Echo));

        var outcome = await sut.HandleNotificationAsync(CreateNotification(NewEnvelope()), CancellationToken.None);

        Assert.Equal(NotificationOutcomeType.Failed, outcome.OutcomeType);
    }

    [Fact]
    public async Task AContextThatIsNotARequestIsReportedWithoutAnAnswer()
    {
        // No correlation id, so there is nothing to answer.
        var notification = CreateNotification(NewEnvelope());
        notification.Context = JsonSerializer.Deserialize<JsonElement>("[1,2,3]");
        var sut = CreateSut(new StubHandler(ConnectorRequestTypes.Echo));

        var outcome = await sut.HandleNotificationAsync(notification, CancellationToken.None);

        Assert.Equal(NotificationOutcomeType.Failed, outcome.OutcomeType);
        _callbackClient.Verify(x => x.SendAsync(
            It.IsAny<ConnectorConfigModel>(), It.IsAny<ConnectorRequestResponse>(), It.IsAny<CancellationToken>()),
            Times.Never());
    }

    [Fact]
    public async Task ARequestWithNoTypeIsReportedWithoutAnAnswer()
    {
        var envelope = NewEnvelope();
        envelope.RequestType = "   ";
        var sut = CreateSut(new StubHandler(ConnectorRequestTypes.Echo));

        var outcome = await sut.HandleNotificationAsync(CreateNotification(envelope), CancellationToken.None);

        Assert.Equal(NotificationOutcomeType.Failed, outcome.OutcomeType);
        _callbackClient.Verify(x => x.SendAsync(
            It.IsAny<ConnectorConfigModel>(), It.IsAny<ConnectorRequestResponse>(), It.IsAny<CancellationToken>()),
            Times.Never());
    }

    [Fact]
    public async Task TheLastHandlerRegisteredForARequestTypeWins()
    {
        // So a connector can replace a handler the SDK ships.
        var first = new StubHandler(ConnectorRequestTypes.Echo);
        var second = new StubHandler(ConnectorRequestTypes.Echo);
        var sut = CreateSut(first, second);

        await sut.HandleNotificationAsync(CreateNotification(NewEnvelope()), CancellationToken.None);

        Assert.Null(first.Received);
        Assert.NotNull(second.Received);
    }

    [Fact]
    public async Task ANotificationIsRequired()
    {
        await Assert.ThrowsAsync<ArgumentNullException>(() =>
            CreateSut().HandleNotificationAsync(null!, CancellationToken.None));
    }

    private ConnectorRequestHandler CreateSut(params IConnectorRequestHandler[] handlers)
    {
        return new ConnectorRequestHandler(handlers, _callbackClient.Object, Mock.Of<IConnectorSecretDecryptor>(), null!);
    }

    private ConnectorRequestResponse CapturedAnswer()
    {
        ConnectorRequestResponse? captured = null;
        _callbackClient.Verify(x => x.SendAsync(
            It.IsAny<ConnectorConfigModel>(),
            It.Is<ConnectorRequestResponse>(r => Capture(r, out captured)),
            It.IsAny<CancellationToken>()), Times.Once());

        Assert.NotNull(captured);
        return captured!;
    }

    [Fact]
    public async Task TheHandlerIsGivenTheConfigurationTheNotificationCarried()
    {
        // Without this a handler cannot tell what it is answering about, and an Authenticate-style
        // handler judges the settings as supplied, which may not be stored anywhere yet.
        var handler = new StubHandler(ConnectorRequestTypes.Echo);
        var sut = CreateSut(handler);
        var notification = CreateNotification(NewEnvelope());

        await sut.HandleNotificationAsync(notification, CancellationToken.None);

        Assert.Same(notification.ConnectorConfig, handler.ReceivedConfiguration);
    }

    private static bool Capture(ConnectorRequestResponse response, out ConnectorRequestResponse? captured)
    {
        captured = response;
        return true;
    }

    private static ConnectorRequestEnvelope NewEnvelope()
    {
        return new ConnectorRequestEnvelope
        {
            RequestType = ConnectorRequestTypes.Echo,
            CorrelationId = CorrelationId
        };
    }

    private static ConnectorNotificationModel CreateNotification(ConnectorRequestEnvelope envelope)
    {
        // The platform fills both from one configuration, so separate values send nothing real.
        var connectorConfigId = Guid.NewGuid().ToString();
        var tenantId = Guid.NewGuid().ToString();

        return new ConnectorNotificationModel
        {
            Id = Guid.NewGuid().ToString(),
            NotificationType = ConnectorRequestHandler.CONNECTOR_REQUEST_NOTIFICATION_TYPE,
            TenantId = tenantId,
            ConnectorId = connectorConfigId,
            // Re-read so the handler gets the JsonElement the transport produces, not a plain object.
            Context = JsonSerializer.Deserialize<JsonElement>(JsonSerializer.Serialize(envelope)),
            ConnectorConfig = new ConnectorConfigModel
            {
                Id = connectorConfigId,
                TenantId = tenantId,
                TenantDomainName = "tenant.example.com"
            }
        };
    }

    private sealed class StubHandler : IConnectorRequestHandler
    {
        public StubHandler(string requestType) => RequestType = requestType;

        public string RequestType { get; }

        public ConnectorRequestEnvelope? Received { get; private set; }

        public ConnectorConfigModel? ReceivedConfiguration { get; private set; }

        public ConnectorRequestResponse? Answer { get; set; } = new ConnectorRequestResponse
        {
            Outcome = RequestOutcomeType.Ok
        };

        public Exception? Throw { get; set; }

        public Task<ConnectorRequestResponse> HandleAsync(ConnectorConfigModel connectorConfiguration, ConnectorRequestEnvelope request, CancellationToken cancellationToken)
        {
            Received = request;
            ReceivedConfiguration = connectorConfiguration;

            if (Throw != null)
            {
                throw Throw;
            }

            return Task.FromResult(Answer!);
        }
    }
}
