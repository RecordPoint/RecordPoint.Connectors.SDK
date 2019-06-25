using Moq;
using RecordPoint.Connectors.SDK.Client;
using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.Diagnostics;
using RecordPoint.Connectors.SDK.Exceptions;
using RecordPoint.Connectors.SDK.Notifications;
using RecordPoint.Connectors.SDK.NotificationTasks;
using RecordPoint.Connectors.SDK.Providers;
using RecordPoint.Connectors.SDK.Test.Helpers;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace RecordPoint.Connectors.SDK.Test.NotificationTasks
{
    public class NotificationTaskRunnerTest
    {
        private Mock<INotificationHandler> _handler = new Mock<INotificationHandler>();
        private Mock<INotificationPullManager> _pullManager = new Mock<INotificationPullManager>();
        private NotificationRunnerSettings _settings = new NotificationRunnerSettings(){ NotificationPollIntervalSeconds = 1 };

        public NotificationTaskRunnerTest()
        {
            _pullManager.Setup(x => x.GetAllPendingConnectorNotifications(
                It.IsAny<ApiClientFactorySettings>(), 
                It.IsAny<AuthenticationHelperSettings>(), 
                It.IsAny<string>(), 
                It.IsAny<CancellationToken>())
            ).ReturnsAsync(
                new List<ConnectorNotificationModel>()
            );
        }

        [Fact]
        public async Task NotificationTaskRunner_CanBeRunWithInitialConnectors()
        {
            var sut = GetNotificationTaskRunner();
            var enabledConnectorOne = GetConnectorConfigModel();
            var enabledConnectorTwo = GetConnectorConfigModel();
            var disabledConnector = GetConnectorConfigModel(ConnectorConfigStatus.Disabled);
            var erroredConnector = GetConnectorConfigModel(ConnectorConfigStatus.Error);

            var connectors = new List<ConnectorConfigModel>()
            {
                enabledConnectorOne,
                enabledConnectorTwo,
                erroredConnector,
                disabledConnector
            };

            await RunSUT(sut, async (task) =>
            {
                await task.WaitForVerification(() =>
                {
                    _pullManager.Verify(x => x.GetAllPendingConnectorNotifications(
                        It.IsAny<ApiClientFactorySettings>(), 
                        It.IsAny<AuthenticationHelperSettings>(), 
                        It.Is<string>(y => y == enabledConnectorOne.Id), 
                        It.IsAny<CancellationToken>()
                    ), Times.AtLeastOnce);

                    _pullManager.Verify(x => x.GetAllPendingConnectorNotifications(
                        It.IsAny<ApiClientFactorySettings>(), 
                        It.IsAny<AuthenticationHelperSettings>(), 
                        It.Is<string>(y => y == enabledConnectorTwo.Id), 
                        It.IsAny<CancellationToken>()
                    ), Times.AtLeastOnce);

                    _pullManager.Verify(x => x.GetAllPendingConnectorNotifications(
                        It.IsAny<ApiClientFactorySettings>(), 
                        It.IsAny<AuthenticationHelperSettings>(), 
                        It.Is<string>(y => y == disabledConnector.Id), 
                        It.IsAny<CancellationToken>()
                    ), Times.Never);

                    _pullManager.Verify(x => x.GetAllPendingConnectorNotifications(
                        It.IsAny<ApiClientFactorySettings>(), 
                        It.IsAny<AuthenticationHelperSettings>(), 
                        It.Is<string>(y => y == erroredConnector.Id), 
                        It.IsAny<CancellationToken>()
                    ), Times.Never);
                });
            },
            connectors
            ).ConfigureAwait(false);
        }

        [Fact]
        public async Task NotificationTaskRunner_DoesNotRun_DisabledConnectors()
        {
            var sut = GetNotificationTaskRunner();
            var notificationTaskCount = 0;
            // Set up a factory that counts the amount of times it was called
            sut.NotificationTaskFactory = () =>
            {
                notificationTaskCount++;
                return GetNotificationTask();
            };

            await RunSUT(sut, async (task) =>
            {
                var connector = GetConnectorConfigModel(ConnectorConfigStatus.Disabled);
                sut.UpsertConnectorConfigModel(connector);
                await task.WaitForVerification(() => Assert.True(notificationTaskCount == 0), 5, true);
            }).ConfigureAwait(false);
        }

        [Fact]
        public async Task NotificationTaskRunner_Refreshes_ConnectorConfigAddedOrRemoved()
        {
            var sut = GetNotificationTaskRunner();
            var notificationTaskCount = 0;
            // Set up a factory that counts the amount of times it was called
            sut.NotificationTaskFactory = () =>
            {
                notificationTaskCount++;
                return GetNotificationTask();
            };

            await RunSUT(sut, async (task) =>
            {
                // No connectors to start with - Shouldn't be any tasks after the second we wait before this check
                Assert.True(notificationTaskCount == 0);
                // Add a connector - Should only be one task factory run even after 5s
                var connector = GetConnectorConfigModel();
                sut.UpsertConnectorConfigModel(connector);
                await task.WaitForVerification(() => Assert.True(notificationTaskCount == 1), 5, true);
                // We left the tasks running for a while, would expect multiple hits on the pull manager
                _pullManager.Verify(x => x.GetAllPendingConnectorNotifications(
                    It.IsAny<ApiClientFactorySettings>(), 
                    It.IsAny<AuthenticationHelperSettings>(), 
                    It.Is<string>(y => y == connector.Id), 
                    It.IsAny<CancellationToken>()
                ), Times.AtLeast(3));
                // Remove the connector - Should still only have been one task factory run
                Assert.True(sut.TryRemoveConnectorConfigModel(connector.IdAsGuid));
                await task.WaitForVerification(() => Assert.True(notificationTaskCount == 1), 5, true);
                // Add a different connector - Factory should have been run twice
                connector = GetConnectorConfigModel();
                sut.UpsertConnectorConfigModel(connector);
                await task.WaitForVerification(() => Assert.True(notificationTaskCount == 2));
                // Update the connector - Factory should have been run three times
                sut.UpsertConnectorConfigModel(connector);
                await task.WaitForVerification(() => Assert.True(notificationTaskCount == 3));
                // Add a second connector - Factory should have been run 5 times
                sut.UpsertConnectorConfigModel(GetConnectorConfigModel());
                await task.WaitForVerification(() => Assert.True(notificationTaskCount == 5));
            }).ConfigureAwait(false);
        }

        [Fact(Skip = "Fails intermittently")]        
        public async Task NotificationTaskRunner_RefreshesOnAddAndRemove_Selectively()
        {
            var sut = GetNotificationTaskRunner();
            var notificationTaskCount = 0;
            // Set up a factory that counts the amount of times it was called
            sut.NotificationTaskFactory = () =>
            {
                notificationTaskCount++;
                return GetNotificationTask();
            };

            await RunSUT(sut, async (task) =>
            {
                var connector = GetConnectorConfigModel();
                sut.UpsertConnectorConfigModel(connector);
                await task.WaitForVerification(() => Assert.Equal(1, notificationTaskCount));
                // We would not expect an increment of this number when we add a non-enabled connector
                var disabledConnector = GetConnectorConfigModel(ConnectorConfigStatus.Disabled);
                var erroredConnector = GetConnectorConfigModel(ConnectorConfigStatus.Error);
                sut.UpsertConnectorConfigModel(disabledConnector);
                sut.UpsertConnectorConfigModel(erroredConnector);
                await task.WaitForVerification(() => Assert.Equal(1, notificationTaskCount), 5, true);
                // But we would when we updated the initial connector
                sut.UpsertConnectorConfigModel(connector);
                await task.WaitForVerification(() => Assert.Equal(2, notificationTaskCount));
                // And we would when we added a new connector (We now have two enabled connectors, so increments by 2)
                sut.UpsertConnectorConfigModel(GetConnectorConfigModel());
                await task.WaitForVerification(() => Assert.Equal(4, notificationTaskCount));
                // When we remove one of our enabled connectors, we would expect a refresh
                sut.TryRemoveConnectorConfigModel(connector.IdAsGuid);
                await task.WaitForVerification(() => Assert.Equal(5, notificationTaskCount));
                // ... But we would not when we removed our disabled connectors
                sut.TryRemoveConnectorConfigModel(disabledConnector.IdAsGuid);
                sut.TryRemoveConnectorConfigModel(erroredConnector.IdAsGuid);
                await task.WaitForVerification(() => Assert.Equal(5, notificationTaskCount), 5, true);
            }).ConfigureAwait(false);
        }

        [Fact]
        public async Task NotificationTask_AcknowledgesNotifications()
        {
            var sut = GetNotificationTask();
            var pullManager = new FakeNotificationPullManager();
            sut.NotificationPullManager = pullManager;

            await RunSUT(sut, GetConnectorConfigModel(), async (task) =>
            {
                await task.EnsureIsStillRunning();
                pullManager.AddNotification(new ConnectorNotificationModel(Guid.NewGuid().ToString()));

                await task.WaitForVerification(() => Assert.True(FakeNotificationPullManager.Dictionary.IsEmpty));
            }).ConfigureAwait(false);
        }

        [Fact]
        public async Task NotificationTask_HandlesResourceNotFound()
        {
            var sut = GetNotificationTask();

            _pullManager.Setup(x => x.GetAllPendingConnectorNotifications(
                It.IsAny<ApiClientFactorySettings>(), 
                It.IsAny<AuthenticationHelperSettings>(), 
                It.IsAny<string>(), 
                It.IsAny<CancellationToken>())
            ).ReturnsAsync(
                new List<ConnectorNotificationModel>(){ new ConnectorNotificationModel(Guid.NewGuid().ToString()) } 
            );

            _pullManager.Setup(x => x.AcknowledgeNotification(
                It.IsAny<ApiClientFactorySettings>(), 
                It.IsAny<AuthenticationHelperSettings>(), 
                It.IsAny<ConnectorNotificationAcknowledgeModel>(), 
                It.IsAny<CancellationToken>())
            ).ThrowsAsync(
                new ResourceNotFoundException()
            );

            await RunSUT(sut, GetConnectorConfigModel(), async (task) =>
            {
                await task.WaitForVerification(() =>
                {
                    _pullManager.Verify(x => x.AcknowledgeNotification(
                        It.IsAny<ApiClientFactorySettings>(), 
                        It.IsAny<AuthenticationHelperSettings>(), 
                        It.IsAny<ConnectorNotificationAcknowledgeModel>(), 
                        It.IsAny<CancellationToken>())
                    );
                });

                await task.EnsureIsStillRunning();
            }).ConfigureAwait(false);
        }

        private async Task RunSUT(NotificationTaskRunner sut, Func<Task, Task> code, IEnumerable<ConnectorConfigModel> initialConnectors = null)
        {
            var source = new CancellationTokenSource();
            Task task = null;

            if(initialConnectors == null)
            {
                task = Task.Run(() => sut.Run(source.Token));
            }
            else
            {
                task = Task.Run(() => sut.Run(initialConnectors, source.Token));
            }

            // Give it a second to get started
            await Task.Delay(TimeSpan.FromSeconds(1));

            await code(task);

            source.Cancel();
            await task.AwaitCompleteOrCancel();
        }

        private async Task RunSUT(NotificationTask sut, ConnectorConfigModel model, Func<Task, Task> code)
        {
            var source = new CancellationTokenSource();
            var task = Task.Run(() => sut.RunAsync(model, string.Empty, source.Token));
            // Give it a second to get started
            await Task.Delay(TimeSpan.FromSeconds(1));

            await code(task);

            source.Cancel();
            await task.AwaitCompleteOrCancel();
        }

        private ConnectorConfigModel GetConnectorConfigModel(string status = ConnectorConfigStatus.Enabled)
        {
            return new ConnectorConfigModel()
            {
                Id = Guid.NewGuid().ToString(),
                Status = status
            };
        }

        private NotificationTaskRunner GetNotificationTaskRunner()
        {
            return new NotificationTaskRunner()
            {
                DateTimeProvider = DateTimeProvider.Instance,
                ExceptionHandlerFunction = null,
                Log = new Mock<ILog>().Object,
                NotificationTaskFactory = GetNotificationTaskFactory(),
                RepeatedTaskFailureTimeFunction = null,
                Settings = _settings
            };
        }

        private Func<NotificationTask> GetNotificationTaskFactory()
        {
            return () =>
            {
                return GetNotificationTask();
            };
        }

        private NotificationTask GetNotificationTask()
        {
            return new NotificationTask()
            {
                ApiClientFactorySettings = new ApiClientFactorySettings(),
                AuthenticationHelperSettingsFactory = (cc) => new AuthenticationHelperSettings(),
                Log = new Mock<ILog>().Object,
                NotificationHandler = _handler.Object,
                NotificationPullManager = _pullManager.Object,
                NotificationRunnerSettings = _settings
            };
        }

        private class FakeNotificationPullManager : INotificationPullManager
        {
            public static ConcurrentDictionary<string, ConnectorNotificationModel> Dictionary = new ConcurrentDictionary<string, ConnectorNotificationModel>();

            public void AddNotification(ConnectorNotificationModel model)
            {
                Dictionary.AddOrUpdate(model.Id, model, (k, v) => model);
            }

            public Task AcknowledgeNotification(ApiClientFactorySettings factorySettings, AuthenticationHelperSettings authenticationSettings, ConnectorNotificationAcknowledgeModel acknowledgement, CancellationToken cancellationToken = default(CancellationToken))
            {
                if(!Dictionary.TryRemove(acknowledgement.NotificationId, out var dummy))
                {
                    throw new ResourceNotFoundException();
                }

                return Task.FromResult(0);
            }

            public Task<IList<ConnectorNotificationModel>> GetAllPendingConnectorNotifications(ApiClientFactorySettings factorySettings, AuthenticationHelperSettings authenticationSettings, string connectorConfigId, CancellationToken cancellationToken = default(CancellationToken))
            {
                return Task.FromResult(Dictionary.Values as IList<ConnectorNotificationModel>);
            }
        }
    }
}
