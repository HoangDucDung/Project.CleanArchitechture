using Microsoft.AspNetCore.Mvc;
using Project.Controller.Base.Controller;
using Project.Libs.DependencyInjection;

namespace Project.Controller.Notification.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController(ILazyloadProvider lazyloadProvider) : BaseController(lazyloadProvider)
    {
        [HttpGet("mongo/insert")]
        public IActionResult InsertData()
        {
            return Ok("Pong");
        }
    }
}
