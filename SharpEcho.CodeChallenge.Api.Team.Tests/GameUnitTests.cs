using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SharpEcho.CodeChallenge.Data;
using System;

namespace SharpEcho.CodeChallenge.Api.Team.Tests
{
    [TestClass]
    public class GameUnitTests
    {
        private static IConfiguration Configuration => new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        private static IRepository Repository => new GenericRepository(Configuration.GetConnectionString("SharpEcho"));

        [TestMethod]
        public void RecordGame_ShouldCreateGame()
        {

            Assert.Inconclusive("Not implemented");
        }

        [TestMethod]
        public void GetWinLossRecord_ShouldReturnCorrectRecord()
        {
            Assert.Inconclusive("Not implemented");
        }
    }
}
