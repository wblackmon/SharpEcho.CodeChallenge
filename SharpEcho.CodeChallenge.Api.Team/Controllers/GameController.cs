using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharpEcho.CodeChallenge.Api.Team.DTOs;
using SharpEcho.CodeChallenge.Api.Team.Entities;
using SharpEcho.CodeChallenge.Data;
using System.Security.Cryptography.X509Certificates;

namespace SharpEcho.CodeChallenge.Api.Team.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : GenericController<Game>
    {
        public GameController(IRepository repository) : base(repository)
        {
        }

        [HttpPost("RecordGame")]
        public virtual ActionResult<GameDTO> RecordGame(GameDTO gameDTO)
        {
            var game = new Game
            {
                HomeTeamId = gameDTO.HomeTeamId,
                AwayTeamId = gameDTO.AwayTeamId,
                Date = gameDTO.Date,
                WinningTeamId = gameDTO.WinningTeamId
            };
            Repository.Insert(game);

            return Ok(gameDTO);
        }
    }
}
