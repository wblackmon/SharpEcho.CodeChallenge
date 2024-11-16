using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using SharpEcho.CodeChallenge.Api.Team.Controllers;
using SharpEcho.CodeChallenge.Data;
using Microsoft.Extensions.Configuration;

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
    }
}
