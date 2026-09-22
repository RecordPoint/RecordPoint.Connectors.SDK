#nullable enable
using Microsoft.Rest;
using Moq;
using Newtonsoft.Json;
using RecordPoint.Connectors.SDK.Abstractions.Content;
using RecordPoint.Connectors.SDK.Client;
using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.Configuration;
using RecordPoint.Connectors.SDK.Notifications.Handlers;
using RecordPoint.Connectors.SDK.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;
using JsonElement = System.Text.Json.JsonElement;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace RecordPoint.Connectors.SDK.Notifications.Test;

/// <summary>The whole connector side of a request. Only the generated API client is stubbed.</summary>
/// <remarks>The wire names come from the Swagger, so only the two that fail silently are asserted.</remarks>
public class ConnectorRequestEndToEndTests
{
    private const string ConnectorApiUrl = "https://connector.example.com/";
    private const string CorrelationId = "the-correlation-id";

    private readonly Guid _connectorConfigId = Guid.NewGuid();
    private readonly Guid _tenantId = Guid.NewGuid();

    private readonly List<ConnectorRequestResponseCallbackModel> _posted = new();
    private readonly List<IDictionary<string, List<string>>> _postedHeaders = new();

    /// <summary>Statuses the client returns before falling back to 202, one per attempt.</summary>
    /// <remarks>Returned, not thrown: the client throws only for a status the Swagger omits.</remarks>
    private readonly Queue<HttpStatusCode> _thenStatuses = new();

    private int _attempts;

    private IConnectorSecretDecryptor _decryptor = Mock.Of<IConnectorSecretDecryptor>();

    [Fact]
    public async Task AnEchoRequestIsAnsweredWithItsOwnPayload()
    {
        var outcome = await Run(new EchoHandler(), NewEnvelope(payload: new { Message = "hello" }));

        Assert.Equal(NotificationOutcomeType.Ok, outcome.OutcomeType);

        var body = Assert.Single(_posted);
        Assert.Equal(nameof(RequestOutcomeType.Ok), body.Outcome);
        Assert.Equal("hello", PayloadOf(body).GetProperty("Message").GetString());
    }

    [Fact]
    public async Task TheCallbackIsAuthenticatedAsTheConnector()
    {
        // Without a token this is a 401, and the answer is lost with the notification acknowledged.
        await Run(new EchoHandler(), NewEnvelope());

        Assert.Equal("Bearer the-token", Assert.Single(_postedHeaders)["Authorization"].Single());
    }

    [Fact]
    public async Task TheAnswerCarriesEveryPropertyThePlatformBinds()
    {
        await Run(new EchoHandler(), NewEnvelope(scope: "table-a"));

        var body = Assert.Single(_posted);

        Assert.Equal(_connectorConfigId, body.ConnectorId);
        Assert.Equal(ConnectorRequestTypes.Echo, body.RequestType);
        Assert.Equal(CorrelationId, body.CorrelationId);
        Assert.Equal("table-a", body.ResponseScope);
        Assert.NotNull(body.Outcome);
        Assert.NotNull(body.Messages);
    }

    [Fact]
    public void TheGeneratedBodyStillSerialisesTheNamesThePlatformNeeds()
    {
        // Both fail silently: connectorId resolves the tenant, and outcome travels as a name.
        var body = new ConnectorRequestResponseCallbackModel
        {
            ConnectorId = _connectorConfigId,
            RequestType = ConnectorRequestTypes.Echo,
            CorrelationId = CorrelationId,
            Outcome = nameof(RequestOutcomeType.Ok)
        };

        var json = JsonSerializer.Deserialize<JsonElement>(JsonConvert.SerializeObject(body));

        Assert.Equal(_connectorConfigId, json.GetProperty("connectorId").GetGuid());
        Assert.Equal("Ok", json.GetProperty("outcome").GetString());
        Assert.False(json.TryGetProperty("connectorConfigId", out _));
    }

    [Fact]
    public async Task APayloadKeepsItsCasingAllTheWayToThePlatform()
    {
        // The client renames its own properties and leaves the payload alone. Read back case-sensitively.
        await Run(new EchoHandler(), NewEnvelope(payload: new { FieldName = "FieldValue" }));

        var json = JsonConvert.SerializeObject(Assert.Single(_posted));

        Assert.Contains("\"FieldName\"", json);
        Assert.DoesNotContain("\"fieldName\"", json);
    }

    [Fact]
    public async Task TheHandlerNeverSeesTheConfigurationIdButTheAnswerCarriesIt()
    {
        // The SDK supplies the configuration id, so a handler cannot answer against the wrong one.
        var handler = new EchoHandler();

        await Run(handler, NewEnvelope());

        Assert.NotNull(handler.Received);
        Assert.Equal(_connectorConfigId, Assert.Single(_posted).ConnectorId);
    }

    [Fact]
    public async Task AnUnsupportedRequestTypeIsStillPostedAsAFailedAnswer()
    {
        var outcome = await Run(new EchoHandler(), NewEnvelope(requestType: "NotSupportedHere"));

        Assert.Equal(NotificationOutcomeType.Ok, outcome.OutcomeType);
        Assert.Equal(nameof(RequestOutcomeType.Failed), Assert.Single(_posted).Outcome);
    }

    [Theory]
    [InlineData(HttpStatusCode.BadRequest)]
    [InlineData(HttpStatusCode.RequestEntityTooLarge)]
    public async Task ACallbackThePlatformRejectsIsReportedAsAFailedNotification(HttpStatusCode statusCode)
    {
        // Declared on the route, so the client returns them. Unchecked, this reported Ok.
        _thenStatuses.Enqueue(statusCode);

        var outcome = await Run(new EchoHandler(), NewEnvelope());

        Assert.Equal(NotificationOutcomeType.Failed, outcome.OutcomeType);
    }

    [Fact]
    public async Task ARejectedCallbackIsNotRetried()
    {
        // 400 means the answer itself is wrong. Sending it again would fail the same way.
        _thenStatuses.Enqueue(HttpStatusCode.BadRequest);

        await Run(new EchoHandler(), NewEnvelope());

        Assert.Equal(1, _attempts);
    }

    [Fact]
    public async Task ACallbackRejectedByATransientFailureIsRetried()
    {
        // Nothing redelivers a connector request, so without this a throttle loses the answer.
        _thenStatuses.Enqueue(HttpStatusCode.ServiceUnavailable);

        var outcome = await Run(new EchoHandler(), NewEnvelope());

        Assert.Equal(NotificationOutcomeType.Ok, outcome.OutcomeType);
        Assert.Equal(2, _attempts);
    }

    [Fact]
    public async Task AHandlerReceivesItsSecretsDecrypted()
    {
        // The envelope's contract. Ciphertext would fail looking like wrong credentials.
        var handler = new SecretReadingHandler();
        _decryptor = new ConnectorSecretDecryptor(ConfigurationClientReturning(EncryptionKeyConfiguration()).Object);

        await Run(handler, NewEnvelope(secrets: new List<ConnectorSecret>
        {
            new() { Field = "ApiKey", Value = Encrypted("the-real-key") }
        }));

        Assert.Equal("the-real-key", Assert.Single(handler.Received!.Secrets).Value);
    }

    [Fact]
    public async Task SecretsThatCannotBeDecryptedAreAnsweredRatherThanThrown()
    {
        // The caller is waiting and nothing redelivers this, so silence would be worse than a reason.
        _decryptor = new ConnectorSecretDecryptor(ConfigurationClientReturning(EncryptionKeyConfiguration()).Object);

        var outcome = await Run(new EchoHandler(), NewEnvelope(secrets: new List<ConnectorSecret>
        {
            new() { Field = "ApiKey", Value = "not-encrypted-at-all" }
        }));

        Assert.Equal(NotificationOutcomeType.Ok, outcome.OutcomeType);
        Assert.Equal(nameof(RequestOutcomeType.Failed), Assert.Single(_posted).Outcome);
    }

    private async Task<NotificationOutcome> Run(IConnectorRequestHandler handler, ConnectorRequestEnvelope envelope)
    {
        var configuration = new R365ConfigurationModel
        {
            ConnectorApiUrl = ConnectorApiUrl,
            ClientId = Guid.NewGuid().ToString(),
            ClientSecret = "the-client-secret",
            Audience = "the-audience"
        };

        var configurationClient = ConfigurationClientReturning(configuration);

        var authenticationProvider = new Mock<IAuthenticationProvider>();
        authenticationProvider
            .Setup(x => x.AcquireTokenAsync(It.IsAny<AuthenticationHelperSettings>()))
            .ReturnsAsync(new AuthenticationResult { AccessTokenType = "Bearer", AccessToken = "the-token" });

        var post = new Mock<IPOST>();
        post.Setup(x => x.ApiNotificationsConnectorRequestCallbackWithHttpMessagesAsync(
                It.IsAny<string>(),
                It.IsAny<ConnectorRequestResponseCallbackModel>(),
                It.IsAny<Dictionary<string, List<string>>>(),
                It.IsAny<CancellationToken>()))
            .Returns<string, ConnectorRequestResponseCallbackModel, Dictionary<string, List<string>>, CancellationToken>(
                (_, body, headers, _) =>
                {
                    _attempts++;
                    _posted.Add(body);
                    _postedHeaders.Add(headers);

                    var status = _thenStatuses.Count > 0 ? _thenStatuses.Dequeue() : HttpStatusCode.Accepted;

                    // 503 is not declared on the route, so the real client throws rather than returns.
                    if (status == HttpStatusCode.ServiceUnavailable)
                    {
                        return Task.FromException<HttpOperationResponse>(HttpFailure(status));
                    }

                    return Task.FromResult(new HttpOperationResponse
                    {
                        Response = new HttpResponseMessage(status) { Content = new StringContent(string.Empty) }
                    });
                });

        var apiClient = new Mock<IApiClient>();
        apiClient.SetupGet(x => x.POST).Returns(post.Object);

        var apiClientFactory = new Mock<IApiClientFactory>();
        apiClientFactory.Setup(x => x.CreateApiClient(It.IsAny<ApiClientFactorySettings>())).Returns(apiClient.Object);
        apiClientFactory
            .Setup(x => x.CreateAuthenticationProvider(It.IsAny<AuthenticationHelperSettings>()))
            .Returns(authenticationProvider.Object);

        var callbackClient = new ConnectorRequestCallbackClient(
            apiClientFactory.Object, configurationClient.Object, null!);

        var sut = new ConnectorRequestHandler(new[] { handler }, callbackClient, _decryptor, null!);

        return await sut.HandleNotificationAsync(CreateNotification(envelope), CancellationToken.None);
    }

    /// <summary>The configuration the callback client and the decryptor both read.</summary>
    private static Mock<IR365ConfigurationClient> ConfigurationClientReturning(R365ConfigurationModel configuration)
    {
        var client = new Mock<IR365ConfigurationClient>();
        client.Setup(x => x.GetR365Configuration(It.IsAny<string>())).Returns(configuration);

        return client;
    }

    /// <summary>The credentials the platform derives its AES key and IV from.</summary>
    private static R365ConfigurationModel EncryptionKeyConfiguration()
    {
        return new R365ConfigurationModel
        {
            ConnectorApiUrl = ConnectorApiUrl,
            ClientId = "00000000-0000-0000-0000-000000000001",
            ClientSecret = "the-client-secret",
            Audience = "the-audience"
        };
    }

    /// <summary>Encrypts a value the way the platform does, so a real decryptor can read it back.</summary>
    private static string Encrypted(string value)
    {
        var configuration = EncryptionKeyConfiguration();

        using var aes = Aes.Create();
        aes.Key = SHA256.HashData(Encoding.ASCII.GetBytes(configuration.ClientSecret));
        aes.IV = MD5.HashData(Encoding.ASCII.GetBytes(configuration.ClientId));

        using var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
        using var stream = new MemoryStream();
        using (var crypto = new CryptoStream(stream, encryptor, CryptoStreamMode.Write))
        using (var writer = new StreamWriter(crypto))
        {
            writer.Write(value);
        }

        return Convert.ToBase64String(stream.ToArray());
    }

    /// <summary>The failure the client throws, carrying the status the retry policy inspects.</summary>
    private static HttpOperationException HttpFailure(HttpStatusCode statusCode)
    {
        using var response = new HttpResponseMessage(statusCode);

        return new HttpOperationException($"The callback failed with {(int)statusCode}.")
        {
            Response = new HttpResponseMessageWrapper(response, string.Empty)
        };
    }

    private static JsonElement PayloadOf(ConnectorRequestResponseCallbackModel body)
    {
        return JsonSerializer.Deserialize<JsonElement>(JsonConvert.SerializeObject(body.Data));
    }

    private static ConnectorRequestEnvelope NewEnvelope(
        string? requestType = null, object? payload = null, string? scope = null, List<ConnectorSecret>? secrets = null)
    {
        return new ConnectorRequestEnvelope
        {
            RequestType = requestType ?? ConnectorRequestTypes.Echo,
            CorrelationId = CorrelationId,
            ResponseScope = scope ?? string.Empty,
            Payload = payload,
            Secrets = secrets ?? new List<ConnectorSecret>()
        };
    }

    private ConnectorNotificationModel CreateNotification(ConnectorRequestEnvelope envelope)
    {
        return new ConnectorNotificationModel
        {
            Id = Guid.NewGuid().ToString(),
            NotificationType = ConnectorRequestHandler.CONNECTOR_REQUEST_NOTIFICATION_TYPE,

            // The platform fills both from one configuration, so separate values send nothing real.
            TenantId = _tenantId.ToString(),
            ConnectorId = _connectorConfigId.ToString(),
            Context = JsonSerializer.Deserialize<JsonElement>(JsonSerializer.Serialize(envelope)),
            ConnectorConfig = new ConnectorConfigModel
            {
                Id = _connectorConfigId.ToString(),
                TenantId = _tenantId.ToString(),
                TenantDomainName = "tenant.example.com"
            }
        };
    }

    /// <summary>Keeps the request it was given, so a test can see what the SDK handed over.</summary>
    private sealed class SecretReadingHandler : IConnectorRequestHandler
    {
        public string RequestType => ConnectorRequestTypes.Echo;

        public ConnectorRequestEnvelope? Received { get; private set; }

        public Task<ConnectorRequestResponse> HandleAsync(ConnectorConfigModel connectorConfiguration, ConnectorRequestEnvelope request, CancellationToken cancellationToken)
        {
            Received = request;

            return Task.FromResult(ConnectorRequestResponse.Ok(request.CorrelationId, "Read the secrets."));
        }
    }

    /// <summary>A reference handler: return the payload unchanged. All a connector author writes.</summary>
    private sealed class EchoHandler : IConnectorRequestHandler
    {
        public string RequestType => ConnectorRequestTypes.Echo;

        public ConnectorRequestEnvelope? Received { get; private set; }

        public Task<ConnectorRequestResponse> HandleAsync(ConnectorConfigModel connectorConfiguration, ConnectorRequestEnvelope request, CancellationToken cancellationToken)
        {
            Received = request;

            return Task.FromResult(ConnectorRequestResponse.Ok(
                request.CorrelationId,
                "Echoed the request payload.",
                request.Payload));
        }
    }
}
