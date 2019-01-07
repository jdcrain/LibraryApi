using Microsoft.AspNetCore.Mvc;

namespace LibraryApi.Controllers
{
    [ApiController]
    public class StatusController : ControllerBase
    {
        [HttpGet("/")]
        public ActionResult Get() 
        {
            return Ok(new {status = "ok"});
        }
    }
}