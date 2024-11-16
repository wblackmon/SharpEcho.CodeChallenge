﻿namespace SharpEcho.CodeChallenge.Api.Team.DTOs
{
    public class WinLossDTO
    {
        public string WinningTeam { get; set; }
        public string LosingTeam { get; set; }
        public int WinningTeamWins { get; set; }
        public int LosingTeamWins { get; set; }
    }
}