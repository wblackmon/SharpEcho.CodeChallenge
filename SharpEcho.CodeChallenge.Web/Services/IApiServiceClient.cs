using System.Threading.Tasks;
using SharpEcho.CodeChallenge.Web.DTOs;

namespace SharpEcho.CodeChallenge.Web.Services
{
    public interface IApiServiceClient
    {
        Task<TeamDTO> AddTeamAsync(string teamName);
        Task RecordMatchAsync(GameDTO gameDTO);
        Task<WinLossDTO> GetWinLossRecordAsync(string firstTeamName, string secondTeamName);
    }
}


