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
        public virtual ActionResult<List<GameDTO>> GetGamesByTeamNames(string homeTeamName, string awayTeamName)
        {
            var homeTeam = Repository.Query<Entities.Team>("SELECT * FROM Team WHERE Name = @Name", new { Name = homeTeamName }).SingleOrDefault();
            var awayTeam = Repository.Query<Entities.Team>("SELECT * FROM Team WHERE Name = @Name", new { Name = awayTeamName }).SingleOrDefault();

            if (homeTeam == null || awayTeam == null)
            {
                return NotFound("One or both teams not found.");
            }

            var games = Repository.Query<Game>("SELECT * FROM Game WHERE HomeTeamId = @HomeTeamId AND AwayTeamId = @AwayTeamId", new { HomeTeamId = homeTeam.Id, AwayTeamId = awayTeam.Id });

            var gameDTOs = games.Select(g => new GameDTO
            {
                Id = g.Id,
                HomeTeamId = g.HomeTeamId,
                AwayTeamId = g.AwayTeamId,
                Date = g.Date,
                WinningTeamId = g.WinningTeamId
            });

            return Ok(gameDTOs);
        }

        [HttpGet("GetWinLossRecord")]
        public virtual ActionResult<WinLossDTO> GetWinLossRecord(string firstTeamName, string secondTeamName)
        {
            var firstTeam = Repository.Query<Entities.Team>("SELECT * FROM Team WHERE Name = @Name", new { Name = firstTeamName }).FirstOrDefault();
            var secondTeam = Repository.Query<Entities.Team>("SELECT * FROM Team WHERE Name = @Name", new { Name = secondTeamName }).FirstOrDefault();

            if (firstTeam == null || secondTeam == null)
            {
                return NotFound("One or both teams not found.");
            }

            var firstTeamWins = Repository.Query<Game>("SELECT * FROM Game WHERE HomeTeamId = @FirstTeamId AND AwayTeamId = @SecondTeamId AND WinningTeamId = @FirstTeamId", new { FirstTeamId = firstTeam.Id, SecondTeamId = secondTeam.Id }).Count();
            firstTeamWins += Repository.Query<Game>("SELECT * FROM Game WHERE HomeTeamId = @SecondTeamId AND AwayTeamId = @FirstTeamId AND WinningTeamId = @FirstTeamId", new { FirstTeamId = firstTeam.Id, SecondTeamId = secondTeam.Id }).Count();

            var secondTeamWins = Repository.Query<Game>("SELECT * FROM Game WHERE HomeTeamId = @FirstTeamId AND AwayTeamId = @SecondTeamId AND WinningTeamId = @SecondTeamId", new { FirstTeamId = firstTeam.Id, SecondTeamId = secondTeam.Id }).Count();
            secondTeamWins += Repository.Query<Game>("SELECT * FROM Game WHERE HomeTeamId = @SecondTeamId AND AwayTeamId = @FirstTeamId AND WinningTeamId = @SecondTeamId", new { FirstTeamId = firstTeam.Id, SecondTeamId = secondTeam.Id }).Count();

            var winLossRecord = new WinLossDTO
            {
                WinningTeam = firstTeamWins > secondTeamWins ? firstTeam.Name : secondTeam.Name,
                LosingTeam = firstTeamWins > secondTeamWins ? secondTeam.Name : firstTeam.Name,
                WinningTeamWins = firstTeamWins > secondTeamWins ? firstTeamWins : secondTeamWins,
                LosingTeamWins = firstTeamWins > secondTeamWins ? secondTeamWins : firstTeamWins
            };

            return Ok(winLossRecord);
        }



        [HttpPost("Post")]
        public virtual ActionResult<GameDTO> Post(GameDTO gameDTO)
        {
            var game = new Game
            {
                HomeTeamId = gameDTO.HomeTeamId,
                AwayTeamId = gameDTO.AwayTeamId,
                Date = gameDTO.Date,
                WinningTeamId = gameDTO.WinningTeamId
            };
            game.Id = Repository.Insert(game);

            return Ok(gameDTO);
        }
    }
}
