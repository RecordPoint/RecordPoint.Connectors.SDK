#nullable enable
using Moq;
using RecordPoint.Connectors.SDK.Abstractions.Content;
using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.Configuration;
using RecordPoint.Connectors.SDK.Connectors;
using RecordPoint.Connectors.SDK.Content;
using RecordPoint.Connectors.SDK.ContentManager;
using RecordPoint.Connectors.SDK.Notifications.Handlers;
using RecordPoint.Connectors.SDK.Work;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace RecordPoint.Connectors.SDK.Notifications.Test;

public class HandlerTests
{
    private const string ConnectorId = "connector-1";

    private static ConnectorConfigurationModel CreateConnectorData(ConnectorConfigModel config)
    {
        return new ConnectorConfigurationModel
        {
            ConnectorId = config.Id,
            ConnectorTypeId = config.ConnectorTypeId,
            DisplayName = config.DisplayName,
            Status = config.Status,
            TenantId = config.TenantId,
            Data = JsonSerializer.Serialize(config)
        };
    }

    private static ConnectorConfigModel CreateConfig()
    {
        return new ConnectorConfigModel
        {
            Id = ConnectorId,
            TenantId = Guid.NewGuid().ToString(),
            TenantDomainName = "tenant.example.com",
            ConnectorTypeId = Guid.NewGuid().ToString(),
            ConnectorTypeConfigurationId = "type-config-1",
            DisplayName = "Test connector"
        };
    }

    private static object ToJsonElement(object value)
    {
        return JsonSerializer.SerializeToElement(value);
    }

    #region ConnectorConfigCreatedHandler

    [Fact]
    public void ConnectorConfigCreatedHandler_HasExpectedNotificationType()
    {
        var handler = new ConnectorConfigCreatedHandler(Mock.Of<IConnectorConfigurationManager>());
        Assert.Equal(ConnectorConfigCreatedHandler.CONNECTOR_CONFIG_CREATED_NOTIFICATION_TYPE, handler.NotificationType);
        Assert.Equal("ConnectorConfigCreated", handler.NotificationType);
    }

    [Fact]
    public async Task ConnectorConfigCreatedHandler_SetsConnector_AndReturnsOk()
    {
        var manager = new Mock<IConnectorConfigurationManager>();
        var handler = new ConnectorConfigCreatedHandler(manager.Object);
        var notification = new ConnectorNotificationModel { ConnectorConfig = CreateConfig() };

        var outcome = await handler.HandleNotificationAsync(notification, CancellationToken.None);

        Assert.Equal(NotificationOutcomeType.Ok, outcome.OutcomeType);
        manager.Verify(x => x.SetConnectorConfigurationAsync(
            It.Is<ConnectorConfigurationModel>(c => c.ConnectorId == ConnectorId),
            It.IsAny<CancellationToken>()), Times.Once);
    }

    #endregion

    #region ConnectorConfigUpdatedHandler

    [Fact]
    public void ConnectorConfigUpdatedHandler_HasExpectedNotificationType()
    {
        var handler = new ConnectorConfigUpdatedHandler(Mock.Of<IConnectorConfigurationManager>());
        Assert.Equal("ConnectorConfigUpdated", handler.NotificationType);
    }

    [Fact]
    public async Task ConnectorConfigUpdatedHandler_SetsConnector_AndReturnsOk()
    {
        var manager = new Mock<IConnectorConfigurationManager>();
        var handler = new ConnectorConfigUpdatedHandler(manager.Object);
        var notification = new ConnectorNotificationModel { ConnectorConfig = CreateConfig() };

        var outcome = await handler.HandleNotificationAsync(notification, CancellationToken.None);

        Assert.Equal(NotificationOutcomeType.Ok, outcome.OutcomeType);
        manager.Verify(x => x.SetConnectorConfigurationAsync(
            It.Is<ConnectorConfigurationModel>(c => c.ConnectorId == ConnectorId),
            It.IsAny<CancellationToken>()), Times.Once);
    }

    #endregion

    #region ConnectorConfigDeletedHandler

    [Fact]
    public void ConnectorConfigDeletedHandler_HasExpectedNotificationType()
    {
        var handler = new ConnectorConfigDeletedHandler(Mock.Of<IConnectorConfigurationManager>());
        Assert.Equal("ConnectorConfigDeleted", handler.NotificationType);
    }

    [Fact]
    public async Task ConnectorConfigDeletedHandler_DeletesConnector_AndReturnsOk()
    {
        var manager = new Mock<IConnectorConfigurationManager>();
        var handler = new ConnectorConfigDeletedHandler(manager.Object);
        var notification = new ConnectorNotificationModel { ConnectorId = ConnectorId };

        var outcome = await handler.HandleNotificationAsync(notification, CancellationToken.None);

        Assert.Equal(NotificationOutcomeType.Ok, outcome.OutcomeType);
        manager.Verify(x => x.DeleteConnectorConfigurationAsync(ConnectorId, It.IsAny<CancellationToken>()), Times.Once);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public async Task ConnectorConfigDeletedHandler_Throws_WhenConnectorIdMissing(string? connectorId)
    {
        var manager = new Mock<IConnectorConfigurationManager>();
        var handler = new ConnectorConfigDeletedHandler(manager.Object);
        var notification = new ConnectorNotificationModel { ConnectorId = connectorId };

        await Assert.ThrowsAsync<RequiredValueNullException>(() =>
            handler.HandleNotificationAsync(notification, CancellationToken.None));

        manager.Verify(x => x.DeleteConnectorConfigurationAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Never);
    }

    #endregion

    #region PingHandler

    [Fact]
    public void PingHandler_HasExpectedNotificationType()
    {
        var handler = new PingHandler(Mock.Of<IConnectorConfigurationManager>());
        Assert.Equal("Ping", handler.NotificationType);
    }

    [Fact]
    public async Task PingHandler_ReturnsOk()
    {
        var handler = new PingHandler(Mock.Of<IConnectorConfigurationManager>());
        var outcome = await handler.HandleNotificationAsync(new ConnectorNotificationModel(), CancellationToken.None);
        Assert.Equal(NotificationOutcomeType.Ok, outcome.OutcomeType);
    }

    #endregion

    #region ItemDestroyedHandler

    [Fact]
    public void ItemDestroyedHandler_HasExpectedNotificationType()
    {
        var handler = new ItemDestroyedHandler(Mock.Of<IConnectorConfigurationManager>(), Mock.Of<IWorkQueueClient>());
        Assert.Equal("ItemDestroyed", handler.NotificationType);
    }

    [Fact]
    public async Task ItemDestroyedHandler_DisposesRecord_AndReturnsOk()
    {
        var config = CreateConfig();
        var manager = new Mock<IConnectorConfigurationManager>();
        manager.Setup(x => x.GetConnectorConfigurationAsync(ConnectorId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(CreateConnectorData(config));

        var workQueueClient = new Mock<IWorkQueueClient>();
        WorkRequest? submitted = null;
        workQueueClient.Setup(x => x.SubmitWorkAsync(It.IsAny<WorkRequest>(), It.IsAny<CancellationToken>()))
            .Callback<WorkRequest, CancellationToken>((r, _) => submitted = r)
            .Returns(Task.CompletedTask);

        var handler = new ItemDestroyedHandler(manager.Object, workQueueClient.Object);

        var notification = new ConnectorNotificationModel
        {
            ConnectorId = ConnectorId,
            Item = new ItemSubmissionOutputModel
            {
                Author = "author",
                ContentVersion = "v1",
                ExternalId = "ext-1",
                Location = "loc",
                MediaType = "media",
                MimeType = "text/plain",
                SourceCreatedBy = "creator",
                SourceCreatedDate = new DateTime(2024, 1, 1),
                SourceLastModifiedBy = "modifier",
                SourceLastModifiedDate = new DateTime(2024, 2, 2),
                Title = "title",
                ParentExternalId = "parent-1",
                PreviousDisposalBy = "jo.bloggs@example.com",
                SourceProperties = new List<MetaDataModel>
                {
                    new MetaDataModel { Name = "prop1", Type = "String", Value = "value1" }
                }
            }
        };

        var outcome = await handler.HandleNotificationAsync(notification, CancellationToken.None);

        Assert.Equal(NotificationOutcomeType.Ok, outcome.OutcomeType);
        workQueueClient.Verify(x => x.SubmitWorkAsync(It.IsAny<WorkRequest>(), It.IsAny<CancellationToken>()), Times.Once);
        Assert.NotNull(submitted);
        Assert.Equal(config.Id, submitted!.ConnectorConfigId);
        Assert.Contains("ext-1", submitted.Body, StringComparison.Ordinal);
        Assert.Contains("prop1", submitted.Body, StringComparison.Ordinal);
    }

    [Fact]
    public async Task ItemDestroyedHandler_CarriesPreviousDisposalBy()
    {
        var config = CreateConfig();
        var manager = new Mock<IConnectorConfigurationManager>();
        manager.Setup(x => x.GetConnectorConfigurationAsync(ConnectorId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(CreateConnectorData(config));

        var workQueueClient = new Mock<IWorkQueueClient>();
        WorkRequest? submitted = null;
        workQueueClient.Setup(x => x.SubmitWorkAsync(It.IsAny<WorkRequest>(), It.IsAny<CancellationToken>()))
            .Callback<WorkRequest, CancellationToken>((r, _) => submitted = r)
            .Returns(Task.CompletedTask);

        var handler = new ItemDestroyedHandler(manager.Object, workQueueClient.Object);

        var notification = new ConnectorNotificationModel
        {
            ConnectorId = ConnectorId,
            Item = new ItemSubmissionOutputModel
            {
                ExternalId = "ext-1",
                PreviousDisposalBy = "jo.bloggs@example.com",
                SourceProperties = new List<MetaDataModel>()
            }
        };

        await handler.HandleNotificationAsync(notification, CancellationToken.None);

        Assert.NotNull(submitted);
        var queued = JsonSerializer.Deserialize<RecordPoint.Connectors.SDK.Content.Record>(submitted!.Body);
        Assert.NotNull(queued);
        Assert.Equal("jo.bloggs@example.com", queued!.PreviousDisposalBy);
    }

    [Fact]
    public async Task ItemDestroyedHandler_PreviousDisposalByNull_IsCarriedAsNull()
    {
        var config = CreateConfig();
        var manager = new Mock<IConnectorConfigurationManager>();
        manager.Setup(x => x.GetConnectorConfigurationAsync(ConnectorId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(CreateConnectorData(config));

        var workQueueClient = new Mock<IWorkQueueClient>();
        WorkRequest? submitted = null;
        workQueueClient.Setup(x => x.SubmitWorkAsync(It.IsAny<WorkRequest>(), It.IsAny<CancellationToken>()))
            .Callback<WorkRequest, CancellationToken>((r, _) => submitted = r)
            .Returns(Task.CompletedTask);

        var handler = new ItemDestroyedHandler(manager.Object, workQueueClient.Object);

        var notification = new ConnectorNotificationModel
        {
            ConnectorId = ConnectorId,
            Item = new ItemSubmissionOutputModel
            {
                ExternalId = "ext-1",
                SourceProperties = new List<MetaDataModel>()
            }
        };

        await handler.HandleNotificationAsync(notification, CancellationToken.None);

        Assert.NotNull(submitted);
        var queued = JsonSerializer.Deserialize<RecordPoint.Connectors.SDK.Content.Record>(submitted!.Body);
        Assert.NotNull(queued);
        Assert.Null(queued!.PreviousDisposalBy);
    }

    [Fact]
    public async Task ItemDestroyedHandler_HandlesEmptySourceProperties()
    {
        var config = CreateConfig();
        var manager = new Mock<IConnectorConfigurationManager>();
        manager.Setup(x => x.GetConnectorConfigurationAsync(ConnectorId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(CreateConnectorData(config));
        var workQueueClient = new Mock<IWorkQueueClient>();
        var handler = new ItemDestroyedHandler(manager.Object, workQueueClient.Object);

        var notification = new ConnectorNotificationModel
        {
            ConnectorId = ConnectorId,
            Item = new ItemSubmissionOutputModel
            {
                SourceCreatedDate = new DateTime(2024, 1, 1),
                SourceLastModifiedDate = new DateTime(2024, 2, 2),
                SourceProperties = new List<MetaDataModel>()
            }
        };

        var outcome = await handler.HandleNotificationAsync(notification, CancellationToken.None);

        Assert.Equal(NotificationOutcomeType.Ok, outcome.OutcomeType);
        workQueueClient.Verify(x => x.SubmitWorkAsync(It.IsAny<WorkRequest>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    #endregion

    #region ContentRegistrationHandler

    [Fact]
    public void ContentRegistrationHandler_HasExpectedNotificationType()
    {
        var handler = new ContentRegistrationHandler(
            Mock.Of<IConnectorConfigurationManager>(),
            Mock.Of<IManagedWorkFactory>(),
            Mock.Of<IContentRegistrationRequestAction>());
        Assert.Equal("ContentRegistration", handler.NotificationType);
    }

    [Fact]
    public async Task ContentRegistrationHandler_ReturnsFailed_WhenConnectorNotFound()
    {
        var manager = new Mock<IConnectorConfigurationManager>();
        manager.Setup(x => x.GetConnectorConfigurationAsync(ConnectorId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((ConnectorConfigurationModel?)null);

        var handler = new ContentRegistrationHandler(
            manager.Object, Mock.Of<IManagedWorkFactory>(), Mock.Of<IContentRegistrationRequestAction>());

        var notification = new ConnectorNotificationModel
        {
            ConnectorId = ConnectorId,
            Context = ToJsonElement(new List<ContentRegistrationRequest> { new ContentRegistrationRequest() })
        };

        var outcome = await handler.HandleNotificationAsync(notification, CancellationToken.None);

        Assert.Equal(NotificationOutcomeType.Failed, outcome.OutcomeType);
        Assert.Equal("Connector not found", outcome.Reason);
    }

    [Fact]
    public async Task ContentRegistrationHandler_ReturnsFailed_WhenConnectorDisabled()
    {
        var config = CreateConfig();
        var manager = new Mock<IConnectorConfigurationManager>();
        manager.Setup(x => x.GetConnectorConfigurationAsync(ConnectorId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(CreateConnectorData(config));
        manager.Setup(x => x.GetConnectorStatusAsync(ConnectorId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ConnectorFeatureStatus { Enabled = false });

        var handler = new ContentRegistrationHandler(
            manager.Object, Mock.Of<IManagedWorkFactory>(), Mock.Of<IContentRegistrationRequestAction>());

        var notification = new ConnectorNotificationModel
        {
            ConnectorId = ConnectorId,
            Context = ToJsonElement(new List<ContentRegistrationRequest> { new ContentRegistrationRequest() })
        };

        var outcome = await handler.HandleNotificationAsync(notification, CancellationToken.None);

        Assert.Equal(NotificationOutcomeType.Failed, outcome.OutcomeType);
        Assert.Equal("Connector is disabled", outcome.Reason);
    }

    [Fact]
    public async Task ContentRegistrationHandler_StartsOperationsForEachChannel_WithDates()
    {
        var config = CreateConfig();
        var manager = new Mock<IConnectorConfigurationManager>();
        manager.Setup(x => x.GetConnectorConfigurationAsync(ConnectorId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(CreateConnectorData(config));
        manager.Setup(x => x.GetConnectorStatusAsync(ConnectorId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ConnectorFeatureStatus { Enabled = true });

        var channels = new List<Channel>
        {
            new Channel { ExternalId = "chan-1", Title = "Channel 1" },
            new Channel { ExternalId = "chan-2", Title = "Channel 2" }
        };
        var action = new Mock<IContentRegistrationRequestAction>();
        action.Setup(x => x.GetChannelsFromRequestAsync(
                It.IsAny<ConnectorConfigModel>(), It.IsAny<ContentRegistrationRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(channels);

        var workManager = new Mock<IManagedWorkManager>();
        workManager.Setup(x => x.StartAsync(It.IsAny<CancellationToken>(), It.IsAny<DateTimeOffset?>()))
            .Returns(Task.CompletedTask);
        var factory = new Mock<IManagedWorkFactory>();
        factory.Setup(x => x.CreateWork(
                It.IsAny<ConnectorConfigModel>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
            .Returns(workManager.Object);

        var handler = new ContentRegistrationHandler(manager.Object, factory.Object, action.Object);

        var request = new ContentRegistrationRequest
        {
            StartDate = DateTimeOffset.UtcNow.AddDays(-1),
            EndDate = DateTimeOffset.UtcNow
        };
        var notification = new ConnectorNotificationModel
        {
            ConnectorId = ConnectorId,
            Context = ToJsonElement(new List<ContentRegistrationRequest> { request })
        };

        var outcome = await handler.HandleNotificationAsync(notification, CancellationToken.None);

        Assert.Equal(NotificationOutcomeType.Ok, outcome.OutcomeType);
        workManager.Verify(x => x.StartAsync(It.IsAny<CancellationToken>(), It.IsAny<DateTimeOffset?>()), Times.Exactly(2));
    }

    [Fact]
    public async Task ContentRegistrationHandler_DoesNothing_WhenNoChannels()
    {
        var config = CreateConfig();
        var manager = new Mock<IConnectorConfigurationManager>();
        manager.Setup(x => x.GetConnectorConfigurationAsync(ConnectorId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(CreateConnectorData(config));
        manager.Setup(x => x.GetConnectorStatusAsync(ConnectorId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ConnectorFeatureStatus { Enabled = true });

        var action = new Mock<IContentRegistrationRequestAction>();
        action.Setup(x => x.GetChannelsFromRequestAsync(
                It.IsAny<ConnectorConfigModel>(), It.IsAny<ContentRegistrationRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<Channel>());

        var factory = new Mock<IManagedWorkFactory>(MockBehavior.Strict);
        var handler = new ContentRegistrationHandler(manager.Object, factory.Object, action.Object);

        var notification = new ConnectorNotificationModel
        {
            ConnectorId = ConnectorId,
            Context = ToJsonElement(new List<ContentRegistrationRequest> { new ContentRegistrationRequest() })
        };

        var outcome = await handler.HandleNotificationAsync(notification, CancellationToken.None);

        Assert.Equal(NotificationOutcomeType.Ok, outcome.OutcomeType);
        factory.Verify(x => x.CreateWork(
            It.IsAny<ConnectorConfigModel>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Never);
    }

    #endregion

    #region ConnectorSecretHandler

    [Fact]
    public void ConnectorSecretHandler_HasExpectedNotificationType()
    {
        var handler = new ConnectorSecretHandler(
            Mock.Of<IConnectorConfigurationManager>(),
            Mock.Of<IConnectorSecretAction>(),
            Mock.Of<IConnectorSecretDecryptor>());
        Assert.Equal("ConnectorSecret", handler.NotificationType);
    }

    private static string EncryptSecret(string plainText, string clientId, string clientSecret)
    {
        using var aes = Aes.Create();
        aes.Key = SHA256.HashData(Encoding.ASCII.GetBytes(clientSecret));
        aes.IV = MD5.HashData(Encoding.ASCII.GetBytes(clientId));
        using var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
        var plainBytes = Encoding.UTF8.GetBytes(plainText);
        var encrypted = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);
        return Convert.ToBase64String(encrypted);
    }

    [Fact]
    public async Task ConnectorSecretHandler_DecryptsSecrets_AndSaves()
    {
        const string clientId = "client-id-value";
        const string clientSecret = "client-secret-value";
        const string plain = "my-secret-value";

        var config = CreateConfig();
        var r365Config = new R365ConfigurationModel { ClientId = clientId, ClientSecret = clientSecret };

        var configClient = new Mock<IR365ConfigurationClient>();
        configClient.Setup(x => x.GetR365Configuration(config.ConnectorTypeConfigurationId)).Returns(r365Config);

        var manager = new Mock<IConnectorConfigurationManager>();
        manager.Setup(x => x.GetConnectorConfigurationAsync(ConnectorId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(CreateConnectorData(config));

        var secretAction = new Mock<IConnectorSecretAction>();
        IList<ConnectorSecret>? savedSecrets = null;
        secretAction.Setup(x => x.SaveSecretsAsync(It.IsAny<ConnectorConfigModel>(), It.IsAny<IList<ConnectorSecret>>()))
            .Callback<ConnectorConfigModel, IList<ConnectorSecret>>((_, secrets) => savedSecrets = secrets)
            .Returns(Task.CompletedTask);

        var handler = new ConnectorSecretHandler(manager.Object, secretAction.Object, new ConnectorSecretDecryptor(configClient.Object));

        var secretsInput = new List<ConnectorSecret>
        {
            new ConnectorSecret { Field = "field1", Value = EncryptSecret(plain, clientId, clientSecret) }
        };
        var notification = new ConnectorNotificationModel
        {
            ConnectorId = ConnectorId,
            ConnectorConfig = config,
            Context = ToJsonElement(secretsInput)
        };

        var outcome = await handler.HandleNotificationAsync(notification, CancellationToken.None);

        Assert.Equal(NotificationOutcomeType.Ok, outcome.OutcomeType);
        Assert.NotNull(savedSecrets);
        Assert.Single(savedSecrets!);
        Assert.Equal(plain, savedSecrets![0].Value);
    }

    [Fact]
    public async Task ConnectorSecretHandler_Throws_WhenR365ConfigurationMissing()
    {
        var config = CreateConfig();
        var configClient = new Mock<IR365ConfigurationClient>();
        configClient.Setup(x => x.GetR365Configuration(It.IsAny<string>())).Returns((R365ConfigurationModel)null!);

        var handler = new ConnectorSecretHandler(
            Mock.Of<IConnectorConfigurationManager>(), Mock.Of<IConnectorSecretAction>(), new ConnectorSecretDecryptor(configClient.Object));

        var notification = new ConnectorNotificationModel
        {
            ConnectorId = ConnectorId,
            ConnectorConfig = config,
            Context = ToJsonElement(new List<ConnectorSecret> { new ConnectorSecret { Field = "f", Value = "abc" } })
        };

        await Assert.ThrowsAsync<RequiredValueNullException>(() =>
            handler.HandleNotificationAsync(notification, CancellationToken.None));
    }

    [Fact]
    public async Task ConnectorSecretHandler_Throws_WhenClientSecretMissing()
    {
        var config = CreateConfig();
        var configClient = new Mock<IR365ConfigurationClient>();
        configClient.Setup(x => x.GetR365Configuration(It.IsAny<string>()))
            .Returns(new R365ConfigurationModel { ClientId = "id", ClientSecret = null! });

        var handler = new ConnectorSecretHandler(
            Mock.Of<IConnectorConfigurationManager>(), Mock.Of<IConnectorSecretAction>(), new ConnectorSecretDecryptor(configClient.Object));

        var notification = new ConnectorNotificationModel
        {
            ConnectorId = ConnectorId,
            ConnectorConfig = config,
            Context = ToJsonElement(new List<ConnectorSecret> { new ConnectorSecret { Field = "f", Value = "abc" } })
        };

        await Assert.ThrowsAsync<RequiredValueNullException>(() =>
            handler.HandleNotificationAsync(notification, CancellationToken.None));
    }

    #endregion
}
