using Microsoft.AspNetCore.Mvc;
using RecordPoint.Connectors.SDK.Status;

namespace RecordPoint.Connectors.SDK.WebHost.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private readonly IStatusManager _statusManager;

        public StatusController(IStatusManager statusManager)
        {
            _statusManager = statusManager;
        }

        [HttpGet]
        public async Task<List<StatusModel>> Get()
        {
            return await _statusManager.GetStatusModelAsync(CancellationToken.None);
        }
    }
}
