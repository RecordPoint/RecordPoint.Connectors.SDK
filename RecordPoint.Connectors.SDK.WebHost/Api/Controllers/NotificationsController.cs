using Microsoft.AspNetCore.Mvc;
using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.Context;
using RecordPoint.Connectors.SDK.Notifications;
using RecordPoint.Connectors.SDK.Notifications.Webhook;
using RecordPoint.Connectors.SDK.Observability;
using RecordPoint.Connectors.SDK.Work;
using System.Net;

namespace RecordPoint.Connectors.SDK.WebHost.Controllers
{
    /// <summary>
    /// Notifications Controller for receiving webhook requests from Records365
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly IObservabilityScope _observabilityScope;
        private readonly IServiceProvider _serviceProvider;
        private readonly ISystemContext _systemContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationsController"/> class.
        /// </summary>
        /// <param name="observabilityScope">Scope provider for correlating telemetry.</param>
        /// <param name="serviceProvider">Service provider used to resolve notification operations.</param>
        /// <param name="systemContext">System context for the current connector instance.</param>
        public NotificationsController(
            IObservabilityScope observabilityScope,
            IServiceProvider serviceProvider,
            ISystemContext systemContext)
        {
            _observabilityScope = observabilityScope;
            _serviceProvider = serviceProvider;
            _systemContext = systemContext;
        }

        // GET: Notifications
        /// <summary>
        /// Returns a 200 (OK) response to Records365 ping requests.
        /// </summary>
        /// <returns>An OK status response.</returns>
        [HttpGet]
        public IActionResult Ping()
        {
            return new StatusCodeResult((int)HttpStatusCode.OK);
        }

        // POST: Notifications
        /// <summary>
        /// Receives connector notifications from Records365.
        /// </summary>
        /// <param name="notification">The notification payload posted by Records365.</param>
        /// <returns>An HTTP response indicating the processing outcome.</returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ConnectorNotificationModel notification)
        {
            using var systemScope = _observabilityScope.BeginSystemScope(_systemContext);

            var webhookOperation = _serviceProvider.GetRequiredService<WebhookOperation>();
            await webhookOperation.RunAsync(notification, CancellationToken.None);
            
            if(!webhookOperation.ResultType.Equals(WorkResultType.Complete))
            {
                if (webhookOperation.Exception != null) 
                {
                    throw webhookOperation.Exception;
                }

                return StatusCode((int)HttpStatusCode.InternalServerError);
            }

            if (AsyncNotifications.NotificationTypes.Contains(notification.NotificationType))
            {
                return Accepted();
            }

            return Ok();
        }
    }
}
