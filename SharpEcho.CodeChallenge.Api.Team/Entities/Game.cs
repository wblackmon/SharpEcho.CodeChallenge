﻿using System;

namespace SharpEcho.CodeChallenge.Api.Team.Entities
{
    public class Game
    {
        public long Id { get; set; }
        public long HomeTeamId { get; set; }
        public long AwayTeamId { get; set; }
        public DateTime Date { get; set; }
        public long WinningTeamId { get; set; }
    }
}
