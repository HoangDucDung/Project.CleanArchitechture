using Microsoft.AspNetCore.Mvc;
using Project.Controller.Base.Controller;

namespace Project.Controller.Auth.Controllers
{
    public class AuthController : BaseController
    {
        /// <summary>
        /// Test Authen
        /// </summary>
        /// <param name="Id">GuiId Data</param>
        /// <returns></returns>
        [HttpGet("test-auth")]
        public Task<string> TestAuth(Guid? Id)
        {
            return Task.FromResult("Xin chào");
        }

        /// <summary>
        /// Test Post Authen
        /// </summary>
        /// <param name="Id">GuiId Data</param>
        /// <returns></returns>
        [HttpPost("test-post")]
        public Task<string> TestPostAuth([FromBody] TestModel? Id)
        {
            return Task.FromResult("Xin chào");
        }
    }

    public class TestModel
    {
        public Guid? Id { get; set; }
        public string? Name { get; set; }
    }
}
