using System;

namespace SharpEcho.CodeChallenge.Api.Team.Entities
{
    public class Game
    {
        public long Id { get; set; }
        public long WinningTeamId { get; set; }
        public long LosingTeamId { get; set; }
        public string WinningTeamName { get; set; }
        public string LosingTeamName { get; set; }
        public DateTime Date { get; set; }
    }
}
