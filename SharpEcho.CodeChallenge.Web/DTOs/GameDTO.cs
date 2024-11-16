using System;

namespace SharpEcho.CodeChallenge.Web.DTOs
{
    public class GameDTO
    {
        public long Id { get; set; }
        public long HomeTeamId { get; set; }
        public long AwayTeamId { get; set; }
        public DateTime Date { get; set; }
        public long WinningTeamId { get; set; }
    }
}
