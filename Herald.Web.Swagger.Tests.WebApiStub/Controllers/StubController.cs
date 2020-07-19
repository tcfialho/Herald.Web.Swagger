
using Microsoft.AspNetCore.Mvc;

namespace Herald.Web.Swagger.Tests.WebApiStub.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StubController : ControllerBase
    {
        public StubController()
        {
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }
    }
}