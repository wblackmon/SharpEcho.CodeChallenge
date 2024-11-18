using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharpEcho.CodeChallenge.Api.Team.DTOs;
using SharpEcho.CodeChallenge.Api.Team.Entities;
using SharpEcho.CodeChallenge.Data;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace SharpEcho.CodeChallenge.Api.Team.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : GenericController<Game>
    {
        public GamesController(IRepository repository) : base(repository)
        {
        }

        [HttpGet("GetGamesByTeamNames")]
        public virtual ActionResult<List<GameDTO>> GetGamesByTeamNames(string team1Name, string team2Name)
        {
            var team1 = Repository.Query<Entities.Team>("SELECT * FROM Team WHERE Name = @Name", new { Name = team1Name }).SingleOrDefault();
            var team2 = Repository.Query<Entities.Team>("SELECT * FROM Team WHERE Name = @Name", new { Name = team2Name }).SingleOrDefault();

            if (team1 == null || team2 == null)
            {
                return NotFound("One or both teams not found.");
            }

            var games = Repository.Query<Game>(@"
        SELECT * FROM Game 
        WHERE (WinningTeamId = @Team1Id AND LosingTeamId = @Team2Id) 
           OR (WinningTeamId = @Team2Id AND LosingTeamId = @Team1Id)",
                new { Team1Id = team1.Id, Team2Id = team2.Id });

            var gameDTOs = games.Select(g => new GameDTO
            {
                Id = g.Id,
                WinningTeamName = g.WinningTeamName,
                LosingTeamName = g.LosingTeamName,
                WinningTeamId = g.WinningTeamId,
                LosingTeamId = g.LosingTeamId,
                Date = g.Date
            }).ToList();

            return Ok(gameDTOs);
        }



        [HttpGet("GetWinLossRecord")]
        public virtual ActionResult<WinLossRecordDTO> GetWinLossRecord(string firstTeamName, string secondTeamName)
        {
            var firstTeam = Repository.Query<Entities.Team>("SELECT * FROM Team WHERE Name = @Name", new { Name = firstTeamName }).FirstOrDefault();
            var secondTeam = Repository.Query<Entities.Team>("SELECT * FROM Team WHERE Name = @Name", new { Name = secondTeamName }).FirstOrDefault();

            if (firstTeam == null || secondTeam == null)
            {
                return NotFound("One or both teams not found.");
            }

            var firstTeamWins = Repository.Query<Game>("SELECT * FROM Game WHERE WinningTeamId = @FirstTeamId AND LosingTeamId = @SecondTeamId", new { FirstTeamId = firstTeam.Id, SecondTeamId = secondTeam.Id }).Count();
            var secondTeamWins = Repository.Query<Game>("SELECT * FROM Game WHERE WinningTeamId = @SecondTeamId AND LosingTeamId = @FirstTeamId", new { FirstTeamId = firstTeam.Id, SecondTeamId = secondTeam.Id }).Count();

            var winLossRecord = new WinLossRecordDTO
            {
                WinningTeamName = firstTeamWins > secondTeamWins ? firstTeam.Name : secondTeam.Name,
                LosingTeamName = firstTeamWins > secondTeamWins ? secondTeam.Name : firstTeam.Name,
                WinningTeamWins = firstTeamWins > secondTeamWins ? firstTeamWins : secondTeamWins,
                LosingTeamWins = firstTeamWins > secondTeamWins ? secondTeamWins : firstTeamWins
            };

            return Ok(winLossRecord);
        }



        [HttpPost("Post")]
        public virtual ActionResult<long> Post(GameDTO gameDTO)
        {
            var winningTeam = Repository.Query<Entities.Team>("SELECT * FROM Team WHERE Name = @Name", new { Name = gameDTO.WinningTeamName }).SingleOrDefault();
            var losingTeam = Repository.Query<Entities.Team>("SELECT * FROM Team WHERE Name = @Name", new { Name = gameDTO.LosingTeamName }).SingleOrDefault();

            if (winningTeam == null || losingTeam == null)
            {
                return NotFound("One or both teams not found.");
            }

            var game = new Game
            {
                WinningTeamId = winningTeam.Id,
                LosingTeamId = losingTeam.Id,
                WinningTeamName = winningTeam.Name,
                LosingTeamName = losingTeam.Name,
                Date = gameDTO.Date
            };
            game.Id = Repository.Insert(game);

            return Ok(game.Id);
        }
    }
}
