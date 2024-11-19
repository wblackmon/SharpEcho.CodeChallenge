using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SharpEcho.CodeChallenge.Web.DTOs;

namespace SharpEcho.CodeChallenge.Web.Services
{
    public class ApiServiceClient : IApiServiceClient
    {
        private readonly HttpClient _httpClient;

        public ApiServiceClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        
        public async Task DeleteAllRecordsAsync()
        {
            var response = await _httpClient.DeleteAsync("Games/DeleteAll");
            response.EnsureSuccessStatusCode();
        }

        public async Task<TeamDTO> AddTeamAsync(string teamName)
        {
            var teamDTO = new TeamDTO { Name = teamName };
            var content = new StringContent(JsonConvert.SerializeObject(teamDTO), System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("Teams/AddTeam", content);
            response.EnsureSuccessStatusCode();
            var jsonResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<TeamDTO>(jsonResponse);
        }

        public async Task RecordMatchAsync(GameDTO gameDTO)
        {
            var content = new StringContent(JsonConvert.SerializeObject(gameDTO), System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("Games/Post", content);
            response.EnsureSuccessStatusCode();
        }

        public async Task<WinLossDTO> GetWinLossRecordAsync(string firstTeamName, string secondTeamName)
        {
            var response = await _httpClient.GetAsync($"Games/GetWinLossRecord?firstTeamName={firstTeamName}&secondTeamName={secondTeamName}");
            response.EnsureSuccessStatusCode();
            var jsonResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<WinLossDTO>(jsonResponse);
        }
    }
}