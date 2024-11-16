using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using SharpEcho.CodeChallenge.Api.Team.Controllers;
using SharpEcho.CodeChallenge.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace SharpEcho.CodeChallenge.Api.Team.Tests
{
    [TestClass]
    public class TeamUnitTests
    {
        IRepository Repository = new GenericRepository(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetConnectionString("SharpEchoDB"));

        [TestMethod]
        public void GetTeamByName_ShouldReturnCorrectTeam()
        {
            var controller = new TeamsController(Repository);

            var team = new Entities.Team
            {
                Name = "Houston Texans"
            };

            var result = controller.GetTeamByName(team.Name).Value;

            if (result == null)
            {
                controller.Post(team);
                result = controller.GetTeamByName(team.Name).Value;
            }

            Assert.AreEqual(team.Name, result.Name);
        }

        [TestMethod]
        public void GetTeamByName_ShouldNotReturnTeam()
        {
            var controller = new TeamsController(Repository);

            var team = new Entities.Team
            {
                Name = Guid.NewGuid().ToString()
            };

            var result = controller.GetTeamByName(team.Name).Value;

            Assert.IsNull(result);
        }

        [TestMethod]
        public void Post_ShouldCreateTeam()
        {
            var controller = new TeamsController(Repository);
            var team = new Entities.Team
            {
                Name = Guid.NewGuid().ToString() // Generate a unique team name using a GUID
            };

            var result = controller.Post(team);
            if (result.Result is ConflictObjectResult conflictResult)
            {
                Assert.AreEqual(409, (int)conflictResult.StatusCode);
            }
            else
            {
                var createdTeam = (Entities.Team)((CreatedAtActionResult)result.Result).Value;
                Assert.AreEqual(team.Name, createdTeam.Name);
                Assert.IsTrue(createdTeam.Id > 0); // Verify that the ID is set
            }
        }

        [TestMethod]
        public void Delete_ShouldDeleteTeam()
        {
            var controller = new TeamsController(Repository);
            var team = new Entities.Team
            {
                Name = Guid.NewGuid().ToString() // Generate a unique team name using a GUID
            };

            var result = controller.Post(team);
            var teamId = ((Entities.Team)((CreatedAtActionResult)result.Result).Value).Id;

            controller.Delete(teamId);
            var deletedTeam = controller.Get(teamId).Value;
            Assert.IsNull(deletedTeam);
        }


    }
}
