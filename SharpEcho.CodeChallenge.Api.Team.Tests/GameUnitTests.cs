using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpEcho.CodeChallenge.Api.Team.Controllers;
using SharpEcho.CodeChallenge.Api.Team.DTOs;
using SharpEcho.CodeChallenge.Api.Team.Entities;
using SharpEcho.CodeChallenge.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace SharpEcho.CodeChallenge.Api.Team.Tests
{
    [TestClass]
    public class GameUnitTests
    {
        IRepository Repository = new GenericRepository(new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetConnectionString("SharpEchoDB"));

        [TestInitialize]
        public void InitializeDatabase()
        {
            // Delete specific records in the Game table
            Repository.Query<Game>("DELETE FROM [dbo].[Game] WHERE HomeTeamId IN (SELECT Id FROM [dbo].[Team] WHERE [Name] IN ('Dallas Cowboys', 'Atlanta Falcons')) AND AwayTeamId IN (SELECT Id FROM [dbo].[Team] WHERE [Name] IN ('Dallas Cowboys', 'Atlanta Falcons'))", null);

            // Delete specific records in the Team table
            Repository.Query<Entities.Team>("DELETE FROM [dbo].[Team] WHERE [Name] IN ('Dallas Cowboys', 'Atlanta Falcons')", null);
        }

        [TestMethod]
        public void RecordGame_ShouldCreateGame()
        {
            var controller = new GamesController(Repository);

            // Create teams
            var homeTeam = new Entities.Team { Name = Guid.NewGuid().ToString() };
            var awayTeam = new Entities.Team { Name = Guid.NewGuid().ToString() };
            Repository.Insert(homeTeam);
            Repository.Insert(awayTeam);

            // Create GameDTO
            var gameDTO = new DTOs.GameDTO
            {
                HomeTeamId = homeTeam.Id,
                AwayTeamId = awayTeam.Id,
                Date = DateTime.Now,
                WinningTeamId = homeTeam.Id
            };

            // Record game
            var result = controller.Post(gameDTO).Result as OkObjectResult;
            var createdGame = result.Value as GameDTO;

            // Verify the gmame was created
            Assert.IsNotNull(createdGame);
            Assert.AreEqual(gameDTO.HomeTeamId, createdGame.HomeTeamId);
            Assert.AreEqual(gameDTO.AwayTeamId, createdGame.AwayTeamId);
            Assert.AreEqual(gameDTO.Date, createdGame.Date);
            Assert.AreEqual(gameDTO.WinningTeamId, createdGame.WinningTeamId);

        }

        [TestMethod]
        public void GetWinLossRecord_ShoulddReturnCorrectWinLossRecord()
        {
            var controller = new GamesController(Repository);

            // Create teams
            var homeTeam = new Entities.Team { Name = Guid.NewGuid().ToString() };
            var awayTeam = new Entities.Team { Name = Guid.NewGuid().ToString() };
            homeTeam.Id = Repository.Insert(homeTeam);
            awayTeam.Id = Repository.Insert(awayTeam);

            // Create games
            var game1 = new Game
            {
                HomeTeamId = homeTeam.Id,
                AwayTeamId = awayTeam.Id,
                Date = DateTime.Now,
                WinningTeamId = homeTeam.Id
            };
            var game2 = new Game
            {
                HomeTeamId = homeTeam.Id,
                AwayTeamId = awayTeam.Id,
                Date = DateTime.Now.AddDays(1),
                WinningTeamId = awayTeam.Id
            };
            Repository.Insert(game1);
            Repository.Insert(game2);

            // Get games by team names
            var result = controller.GetGamesByTeamNames(homeTeam.Name, awayTeam.Name).Result as OkObjectResult;
            var games = result.Value as IEnumerable<GameDTO>;

            // Verify the games were returned
            Assert.IsNotNull(games);
            Assert.AreEqual(2, games.ToList().Count);
            Assert.IsTrue(games.Any(g => g.Id == game1.Id && game1.WinningTeamId == game1.WinningTeamId));
            Assert.IsTrue(games.Any(g => g.Id == game2.Id && game2.WinningTeamId == game2.WinningTeamId));
        }
    }
}
