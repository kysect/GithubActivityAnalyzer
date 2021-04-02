using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Kysect.GithubActivityAnalyzer.Models;

namespace Kysect.GithubActivityAnalyzer
{
    public class GithubActivityProvider
    {
        private const string Url = "https://github-contributions.now.sh/api/v1/";

        private readonly HttpClient _client;

        public GithubActivityProvider()
        {
            _client = new HttpClient();
        }

        public async Task<ActivityInfo> GetActivityInfo(string username)
        {
            string response = await _client.GetStringAsync(Url + username);
            return JsonSerializer.Deserialize<ActivityInfo>(response, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
    }
}