using Microsoft.AspNetCore.Mvc;
using RecordPoint.Connectors.SDK.Status;

namespace RecordPoint.Connectors.SDK.WebHost.Controllers
{
    /// <summary>
    /// API controller for exposing connector status details.
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        /// <summary>
        /// The status manager.
        /// </summary>
        private readonly IStatusManager _statusManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="StatusController"/> class.
        /// </summary>
        /// <param name="statusManager">The status manager used to retrieve status entries.</param>
        public StatusController(IStatusManager statusManager)
        {
            _statusManager = statusManager;
        }

        /// <summary>
        /// Get and return a task of a list of statusmodels.
        /// </summary>
        /// <returns><![CDATA[Task<List<StatusModel>>]]></returns>
        [HttpGet]
        public async Task<List<StatusModel>> Get()
        {
            return await _statusManager.GetStatusModelAsync(CancellationToken.None);
        }
    }
}
