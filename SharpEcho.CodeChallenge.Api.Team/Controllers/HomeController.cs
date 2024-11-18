using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace SharpEcho.CodeChallenge.Api.Team.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Welcome to SharpEcho Code Challenge API");
        }
    }
}