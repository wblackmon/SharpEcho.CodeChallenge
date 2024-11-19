using System;

namespace SharpEcho.CodeChallenge.Web.DTOs
{
    public class GameDTO
    {
        public long Id { get; set; }
        public long WinningTeamId { get; set; }
        public long LosingTeamId { get; set; }
        public string WinningTeamName { get; set; }
        public string LosingTeamName { get; set; }
        public DateTime Date { get; set; }
    }
}
