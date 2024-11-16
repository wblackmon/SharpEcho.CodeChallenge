using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
        public virtual ActionResult<Entities.Team> GetTeamByName(string name)
        {
            var result = Repository.Query<Entities.Team>("SELECT * FROM Team WHERE Name = @Name", new { Name = name });
            if (result != null && result.Count() > 0)
            {
                return result.First();
            }
            return NotFound();
        }

        [HttpPost("Post")]
        public override ActionResult<Entities.Team> Post(Entities.Team team)
        {
            try
            {
                var insertedTeamId = Repository.Insert(team);
                team.Id = insertedTeamId; // Set the ID of the team
                return CreatedAtAction(nameof(Get), new { id = team.Id }, team);
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
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
            return NoContent();
        }
    }
}
