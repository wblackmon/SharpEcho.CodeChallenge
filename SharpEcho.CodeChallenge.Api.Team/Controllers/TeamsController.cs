using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SharpEcho.CodeChallenge.Api.Team.DTOs;
using SharpEcho.CodeChallenge.Data;
using static Dapper.SqlMapper;

namespace SharpEcho.CodeChallenge.Api.Team.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeamsController : GenericController<Entities.Team>
    {
        public TeamsController(IRepository repository) : base(repository)
        {
        }

        [HttpGet("GetTeamByName")]
        public virtual ActionResult<TeamDTO> GetTeamByName(string name)
        {
            var result = Repository.Query<Entities.Team>("SELECT * FROM Team WHERE Name = @Name", new { Name = name });
            if (result != null && result.Count() > 0)
            {
                var teamDTO = new TeamDTO
                {
                    Id = result.First().Id,
                    Name = result.First().Name
                };
                return teamDTO;
            }
            return NotFound();
        }

        [HttpPost("AddTeam")]
        public virtual ActionResult<TeamDTO> AddTeam(TeamDTO teamDTO)
        {
            var team = new Entities.Team
            {
                Name = teamDTO.Name 
            };
            var teamId = Repository.Insert(team);
            var createdTeamDTO = new TeamDTO
            {
                Id = teamId,
                Name = team.Name
            };

            return CreatedAtAction(nameof(GetTeamByName), new { name = teamDTO.Name }, teamDTO);
        }

        [HttpDelete("{id}")]
        public override ActionResult<Entities.Team> Delete(long id)
        {
            var team = Repository.Get<Entities.Team>(id);
            if (team == null)
            {
                return NotFound();
            }
            Repository.Delete<Entities.Team>(id);
            return Ok(team);
        }
    }
}
