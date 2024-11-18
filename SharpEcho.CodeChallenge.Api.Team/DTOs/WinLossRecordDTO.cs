namespace SharpEcho.CodeChallenge.Api.Team.DTOs
{
    public class WinLossRecordDTO
    {
        public string WinningTeamName { get; set; }
        public string LosingTeamName { get; set; }
        public int WinningTeamWins { get; set; }
        public int LosingTeamWins { get; set; }
    }
}
