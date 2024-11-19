using System;

namespace SharpEcho.CodeChallenge.Web.DTOs
{
    public class WinLossDTO
    {
        public string WinningTeamName { get; set; }
        public string LosingTeamName { get; set; }
        public int WinningTeamWins { get; set; }
        public int LosingTeamWins { get; set; }
    }
}
