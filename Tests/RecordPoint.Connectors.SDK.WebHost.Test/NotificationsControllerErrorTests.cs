#nullable enable
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.Context;
using RecordPoint.Connectors.SDK.Notifications;
using RecordPoint.Connectors.SDK.Notifications.Webhook;
using RecordPoint.Connectors.SDK.Observability;
using RecordPoint.Connectors.SDK.Providers;
using RecordPoint.Connectors.SDK.Work;
using RecordPoint.Connectors.SDK.WebHost.Controllers;
using System.Net;
using Xunit;

namespace RecordPoint.Connectors.SDK.WebHost.Test
{
    /// <summary>
    /// Covers the internal-server-error branch of <see cref="NotificationsController.Post"/> that
    /// occurs when the webhook operation neither completes successfully nor records an exception.
    /// The real <see cref="WebhookOperation"/> always completes or fails-with-exception, so this
    /// branch is exercised with a controllable test double.
    /// </summary>
    public class NotificationsControllerErrorTests
    {
        private readonly Mock<IObservabilityScope> _observabilityScope = new();
        private readonly Mock<ISystemContext> _systemContext = new();

        public NotificationsControllerErrorTests()
        {
            _observabilityScope
                .Setup(scope => scope.BeginScope(It.IsAny<Dimensions?>(), It.IsAny<Measures?>()))
                .Returns(Mock.Of<IDisposable>());
        }

        [Fact]
        public async Task Post_WhenOperationNotCompleteWithoutException_ReturnsInternalServerError()
        {
            var operation = new FakeWebhookOperation(WorkResultType.Abandoned, exception: null);
            var services = new ServiceCollection();
            services.AddSingleton<WebhookOperation>(operation);
            var controller = new NotificationsController(
                _observabilityScope.Object,
                services.BuildServiceProvider(),
                _systemContext.Object);

            var result = await controller.Post(new ConnectorNotificationModel { NotificationType = "SomeType" });

            var statusResult = Assert.IsType<StatusCodeResult>(result);
            Assert.Equal((int)HttpStatusCode.InternalServerError, statusResult.StatusCode);
            Assert.True(operation.RunCalled);
        }

        private sealed class FakeWebhookOperation : WebhookOperation
        {
            private readonly WorkResultType _resultType;
            private readonly Exception? _exception;

            public bool RunCalled { get; private set; }

            public FakeWebhookOperation(WorkResultType resultType, Exception? exception)
                : base(
                    Mock.Of<IServiceProvider>(),
                    Mock.Of<INotificationManager>(),
                    Mock.Of<IWorkQueueClient>(),
                    Mock.Of<IObservabilityScope>(),
                    Mock.Of<ITelemetryTracker>(),
                    Mock.Of<IDateTimeProvider>())
            {
                _resultType = resultType;
                _exception = exception;
            }

            public override Task RunAsync(object parameter, CancellationToken cancellationToken)
            {
                RunCalled = true;
                ResultType = _resultType;
                if (_exception != null)
                {
                    Exception = _exception;
                }
                return Task.CompletedTask;
            }
        }
    }
}

