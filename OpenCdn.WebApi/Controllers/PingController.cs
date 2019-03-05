using Microsoft.AspNetCore.Mvc;

namespace OpenCdn.WebApi.Controllers
{
    [Route("api/ping")]
    [ApiController]
    public class PingController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public ActionResult<dynamic> Get()
        {
            return Ok(new { Data = "Pong!" });
        }
    }
}
