using Microsoft.AspNetCore.Mvc;

namespace SharpEcho.CodeChallenge.Web.Controllers
{
    public class GamesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
