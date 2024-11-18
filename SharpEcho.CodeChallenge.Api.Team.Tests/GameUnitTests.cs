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
            // Delete all records in the Game table
            Repository.Query<Game>("DELETE FROM [dbo].[Game]", null);

            // Delete specific records in the Game table
            Repository.Query<Game>("DELETE FROM [dbo].[Game] WHERE WinningTeamId IN (SELECT Id FROM [dbo].[Team] WHERE [Name] IN ('Dallas Cowboys', 'Atlanta Falcons')) AND LosingTeamId IN (SELECT Id FROM [dbo].[Team] WHERE [Name] IN ('Dallas Cowboys', 'Atlanta Falcons'))", null);

            // Delete specific records in the Team table
            Repository.Query<Entities.Team>("DELETE FROM [dbo].[Team] WHERE [Name] IN ('Dallas Cowboys', 'Atlanta Falcons')", null);
        }

        [TestMethod]
        public void RecordGame_ShouldCreateGame()
        {
            var controller = new GamesController(Repository);

            // Create teams
            var winningTeam = new Entities.Team { Name = "Dallas Cowboys" };
            var losingTeam = new Entities.Team { Name = "Atlanta Falcons" };
            winningTeam.Id = Repository.Insert(winningTeam);
            losingTeam.Id = Repository.Insert(losingTeam);

            // Create GameDTO
            var gameDTO = new DTOs.GameDTO
            {
                WinningTeamName = winningTeam.Name,
                LosingTeamName = losingTeam.Name,
                WinningTeamId = winningTeam.Id,
                LosingTeamId = losingTeam.Id,
                Date = DateTime.Now,
            };

            // Record game
            var result = controller.Post(gameDTO).Result as OkObjectResult;
            var createdGameId = (long)result.Value;

            // Verify the gmame was created
            Assert.AreNotEqual(createdGameId, 0);
        }

        [TestMethod]
        public void GetWinLossRecord_ShouldReturnCorrectWinLossRecord()
        {
            var controller = new GamesController(Repository);

            // Create teams
            Entities.Team team1, team2;
            CreateTeamsWithWinsAndLosses(out team1, out team2);

            // Get win-loss record
            var result = controller.GetWinLossRecord(team1.Name, team2.Name).Result as OkObjectResult;
            var winLossRecord = result.Value as WinLossRecordDTO;

            // Verify the win-loss record
            Assert.IsNotNull(winLossRecord);
            Assert.AreEqual("Dallas Cowboys", winLossRecord.WinningTeamName);
            Assert.AreEqual("Atlanta Falcons", winLossRecord.LosingTeamName);
            Assert.AreEqual(17, winLossRecord.WinningTeamWins);
            Assert.AreEqual(11, winLossRecord.LosingTeamWins);
        }

        private void CreateTeamsWithWinsAndLosses(out Entities.Team team1, out Entities.Team team2)
        {
            team1 = new Entities.Team { Name = "Dallas Cowboys" };
            team2 = new Entities.Team { Name = "Atlanta Falcons" };
            team1.Id = Repository.Insert(team1);
            team2.Id = Repository.Insert(team2);

            // Create games if teams are newly created
            if (team1.Id != 0 && team2.Id != 0)
            {
                for (int i = 0; i < 17; i++)
                {
                    var game = new Game
                    {
                        WinningTeamId = team1.Id,
                        LosingTeamId = team2.Id,
                        WinningTeamName = team1.Name,
                        LosingTeamName = team2.Name,
                        Date = DateTime.Now.AddDays(i),
                    };
                    game.Id = Repository.Insert(game);
                }
                for (int i = 0; i < 11; i++)
                {
                    var game = new Game
                    {
                        WinningTeamId = team2.Id,
                        LosingTeamId = team1.Id,
                        WinningTeamName = team2.Name,
                        LosingTeamName = team1.Name,
                        Date = DateTime.Now.AddDays(17 + i)
                    };
                    game.Id = Repository.Insert(game);
                }
            }
        }

        [TestMethod]
        public void GetGamesByTeamNames_ShoulddReturnCorrectGames()
        {
            var controller = new GamesController(Repository);

            // Check if teams exist
            var team1 = Repository.Query<Entities.Team>("SELECT * FROM Team WHERE Name = @Name", new { Name = "Dallas Cowboys" }).SingleOrDefault();
            var team2 = Repository.Query<Entities.Team>("SELECT * FROM Team WHERE Name = @Name", new { Name = "Atlanta Falcons" }).SingleOrDefault();

            // Create teams and games if they do not exist
            if (team1 == null || team2 == null)
            {
                CreateTeamsWithWinsAndLosses(out team1, out team2);
            }

            // Get games by team names
            var result = controller.GetGamesByTeamNames("Dallas Cowboys", "Atlanta Falcons").Result as OkObjectResult;
            var games = result.Value as List<GameDTO>;

            // Verify the games were returned
            Assert.IsNotNull(games);
            Assert.AreNotEqual(0, games.Count);
        }
    }
}
