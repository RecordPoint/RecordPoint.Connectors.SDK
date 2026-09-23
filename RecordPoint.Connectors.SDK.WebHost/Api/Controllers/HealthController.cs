using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecordPoint.Connectors.SDK.Health;

namespace RecordPoint.Connectors.SDK.WebHost.Api.Controllers
{
    /// <summary>
    /// The Health Check controller
    /// </summary>
    [AllowAnonymous]
    [Route("[controller]")]
    [ApiController]
    public class HealthController : ControllerBase
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IHealthCheckManager _healthCheckManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="HealthController"/> class.
        /// </summary>
        /// <param name="serviceProvider">Service provider used to resolve health check actions.</param>
        /// <param name="healthCheckManager">Manager that tracks the current health state.</param>
        public HealthController(IServiceProvider serviceProvider, IHealthCheckManager healthCheckManager)
        {
            _serviceProvider = serviceProvider;
            _healthCheckManager = healthCheckManager;
        }

        /// <summary>
        /// Gets the current health check result snapshot.
        /// </summary>
        /// <returns>The current <see cref="HealthCheckResult"/>.</returns>
        [HttpGet]
        public HealthCheckResult Get()
        {
            return _healthCheckManager.HealthCheckResult;
        }

        /// <summary>
        /// Check if the service is live
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route(nameof(Livez))]
        public async Task<IActionResult> Livez()
        {
            var healthCheckAction = _serviceProvider.GetRequiredService<IHealthCheckLiveAction>();
            var result = await healthCheckAction.CheckIsLiveAsync();
            return result
                ? Ok()
                : StatusCode(503);
        }

        /// <summary>
        /// Check if the service is ready
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route(nameof(Readyz))]
        public async Task<IActionResult> Readyz()
        {
            var healthCheckAction = _serviceProvider.GetRequiredService<IHealthCheckReadyAction>();
            var result = await healthCheckAction.CheckIsReadyAsync();
            return result
                ? Ok()
                : StatusCode(503);
        }

    }
}
