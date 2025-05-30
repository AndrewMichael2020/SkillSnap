using SkillSnap.Shared;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace SkillSnap.Client.Services
{
    public class HttpClientService : IHttpClientService
    {
        private readonly HttpClient _httpClient;

        public HttpClientService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<PortfolioUserDto?> GetPortfolioUserAsync(int id)
        {
            var url = $"{ApiRoutes.Users}/{id}";
            return await _httpClient.GetFromJsonAsync<PortfolioUserDto>(url);
        }

        public async Task<HttpResponseMessage> PostAsync<T>(string url, T data)
        {
            return await _httpClient.PostAsJsonAsync(url, data);
        }
        // Add more methods as needed
    }
}
