using Microsoft.AspNetCore.Mvc;

namespace SharpEcho.CodeChallenge.Api.Team.Controllers
{
    public class GamesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
