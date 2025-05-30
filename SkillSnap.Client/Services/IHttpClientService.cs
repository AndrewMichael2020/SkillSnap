using SkillSnap.Shared;
using System.Net.Http;
using System.Threading.Tasks;

namespace SkillSnap.Client.Services
{
    public interface IHttpClientService
    {
        Task<PortfolioUserDto?> GetPortfolioUserAsync(int id);
        Task<HttpResponseMessage> PostAsync<T>(string url, T data);
        // Add more methods as needed
    }
}
