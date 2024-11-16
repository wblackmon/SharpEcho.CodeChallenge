using Microsoft.AspNetCore.Mvc;

namespace SharpEcho.CodeChallenge.Web.Controllers
{
    public class TeamsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
