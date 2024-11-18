using Microsoft.AspNetCore.Mvc;
using SharpEcho.CodeChallenge.Web.DTOs;
using SharpEcho.CodeChallenge.Web.Services;
using System.Threading.Tasks;

namespace SharpEcho.CodeChallenge.Web.Controllers
{
    public class GamesController : Controller
    {
        private readonly IApiServiceClient _apiServiceClient;
        public GamesController(IApiServiceClient apiServiceClient)
        {
            _apiServiceClient = apiServiceClient;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RecordGame(GameDTO gameDTO)
        {
            await _apiServiceClient.RecordGame(gameDTO);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> AddTeam(string teamName)
        {
            await _apiServiceClient.AddTeam(teamName);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> GetWinLossRecord(string homeTeamName, string awayTeamName)
        {
            var winLossDTO = await _apiServiceClient.GetWinLossRecord(homeTeamName, awayTeamName);
            return View(winLossDTO);
        }

    }
}
