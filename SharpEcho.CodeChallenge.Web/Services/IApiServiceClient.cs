using SharpEcho.CodeChallenge.Web.DTOs;
using System.Threading.Tasks;

namespace SharpEcho.CodeChallenge.Web.Services
{
    public interface IApiServiceClient
    {
        Task<TeamDTO> AddTeam(string id);
        Task RecordGame(GameDTO gameDTO);
        Task<WinLossDTO> GetWinLossRecord(string homeTeamName, string awayTeamName);
    }
}
