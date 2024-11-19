using Microsoft.AspNetCore.Mvc;
using SharpEcho.CodeChallenge.Web.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using SharpEcho.CodeChallenge.Web.DTOs;
using System;

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
        public async Task<IActionResult> AddTeams()
        {
            try
            {
                var team1 = await _apiServiceClient.AddTeamAsync("Dallas Cowboys");
                var team2 = await _apiServiceClient.AddTeamAsync("Atlanta Falcons");

                ViewBag.AddedTeams = new List<TeamDTO> { team1, team2 };
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
            }

            return View("Index");
        }

        [HttpPost]
        public async Task<IActionResult> RecordMatches()
        {
            try
            {
                var matches = new List<GameDTO>();
                for (int i = 0; i < 28; i++)
                {
                    var game = new GameDTO
                    {
                        WinningTeamName = i < 17 ? "Dallas Cowboys" : "Atlanta Falcons",
                        LosingTeamName = i < 17 ? "Atlanta Falcons" : "Dallas Cowboys",
                        Date = DateTime.Now
                    };
                    await _apiServiceClient.RecordMatchAsync(game);
                    matches.Add(game);
                }

                ViewBag.RecordedMatches = matches;
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
            }

            return View("Index");
        }

        [HttpPost]
        public async Task<IActionResult> GetWinLossRecord()
        {
            try
            {
                var winLossRecord = await _apiServiceClient.GetWinLossRecordAsync("Dallas Cowboys", "Atlanta Falcons");
                ViewBag.WinLossRecord = winLossRecord;
            }
            catch (Exception ex)
            {
                ViewBag.ErrorMessage = ex.Message;
            }

            return View("Index");
        }
    }
}