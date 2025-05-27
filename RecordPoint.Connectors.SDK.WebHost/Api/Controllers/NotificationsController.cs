using Microsoft.AspNetCore.Mvc;
using RecordPoint.Connectors.SDK.Client.Models;
using RecordPoint.Connectors.SDK.Context;
using RecordPoint.Connectors.SDK.Notifications.Webhook;
using RecordPoint.Connectors.SDK.Observability;
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
        /// Initialises the Controller
        /// </summary>
        /// <param name="observabilityScope"></param>
        /// <param name="serviceProvider"></param>
        /// <param name="systemContext"></param>
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
        /// Endpoint that returns a 200 (OK) response to Records365 ping requests
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Ping()
        {
            return new StatusCodeResult((int)HttpStatusCode.OK);
        }

        // POST: Notifications
        /// <summary>
        /// Endpoint for receiving Connector notifications from Records365
        /// </summary>
        /// <param name="notification"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ConnectorNotificationModel notification)
        {
            using var systemScope = _observabilityScope.BeginSystemScope(_systemContext);

            var webhookOperation = _serviceProvider.GetRequiredService<WebhookOperation>();
            await webhookOperation.RunAsync(notification, CancellationToken.None);

            return Ok();
        }
    }
}
