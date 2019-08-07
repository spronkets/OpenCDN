using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OpenCdn.WebApi.Controllers
{
    [ApiController]
    public class PingController : ControllerBase
    {
        [AllowAnonymous]
        [HttpGet, Route("api/ping")]
        public ActionResult<string> Get()
        {
            return Ok("Pong!");
        }
    }
}
