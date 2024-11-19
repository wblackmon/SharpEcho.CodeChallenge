using Microsoft.AspNetCore.Mvc;
using SharpEcho.CodeChallenge.Web.Services;
using System.Threading.Tasks;

namespace SharpEcho.CodeChallenge.Web.Controllers
{
    public class TeamsController : Controller
    {
        private readonly IApiServiceClient _apiServiceClient;

        public TeamsController(IApiServiceClient apiServiceClient)
        {
            _apiServiceClient = apiServiceClient;
        }

        public async Task<IActionResult> AddTeams()
        {
            await _apiServiceClient.AddTeamAsync("Dallas Cowboys");
            await _apiServiceClient.AddTeamAsync("Atlanta Falcons");
            return RedirectToAction("Index");
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}