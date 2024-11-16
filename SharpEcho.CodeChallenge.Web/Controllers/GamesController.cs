using Microsoft.AspNetCore.Mvc;

namespace SharpEcho.CodeChallenge.Web.Controllers
{
    public class GameController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
