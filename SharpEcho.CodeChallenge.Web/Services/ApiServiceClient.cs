using Microsoft.Extensions.Configuration;
using SharpEcho.CodeChallenge.Web.DTOs;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SharpEcho.CodeChallenge.Web.Services
{
    public class ApiServiceClient : IApiServiceClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _baseUri;

        public ApiServiceClient(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _baseUri = configuration["ApiSettings:BaseUri"];
        }

        public async Task<TeamDTO> AddTeam(string teamName)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_baseUri);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var team = new TeamDTO { Name = teamName };
            var response = await client.PostAsync("teams", new StringContent(JsonSerializer.Serialize(team), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<TeamDTO>(responseBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task RecordGame(GameDTO gameDTO)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_baseUri);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.PostAsync("games", new StringContent(JsonSerializer.Serialize(gameDTO), Encoding.UTF8, "application/json"));
            response.EnsureSuccessStatusCode();
        }

        public async Task<WinLossDTO> GetWinLossRecord(string homeTeamName, string awayTeamName)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_baseUri);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.GetAsync($"games/GetWinLossRecord?firstTeamName={homeTeamName}&secondTeamName={awayTeamName}");
            response.EnsureSuccessStatusCode();
            var responseBody = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<WinLossDTO>(responseBody, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
    }
}