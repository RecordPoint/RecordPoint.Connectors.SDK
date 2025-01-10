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
        /// Constructor
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="healthCheckManager"></param>
        public HealthController(IServiceProvider serviceProvider, IHealthCheckManager healthCheckManager)
        {
            _serviceProvider = serviceProvider;
            _healthCheckManager = healthCheckManager;
        }

        /// <summary>
        /// Get the health check result
        /// </summary>
        /// <returns></returns>
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
