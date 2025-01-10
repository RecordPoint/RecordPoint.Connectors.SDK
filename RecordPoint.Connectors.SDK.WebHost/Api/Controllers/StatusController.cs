using Microsoft.AspNetCore.Mvc;
using RecordPoint.Connectors.SDK.Status;

namespace RecordPoint.Connectors.SDK.WebHost.Controllers
{
    /// <summary>
    /// 
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
        ///
        /// </summary>
        /// <param name="statusManager"></param>
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
