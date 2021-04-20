using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Kysect.GithubActivityAnalyzer.Models.ApiResponses;

namespace Kysect.GithubActivityAnalyzer.Services
{
    public class GithubActivityProvider
    {
        private const string Url = "https://github-contributions.now.sh/api/v1/";

        private readonly HttpClient _client;

        public GithubActivityProvider()
        {
            _client = new HttpClient();
        }

        public async Task<ActivityInfo> GetActivityInfo(string username, DateTime? from = null, DateTime? to = null)
        {
            string response = await _client.GetStringAsync(Url + username);
            var activityInfo = JsonSerializer.Deserialize<ActivityInfo>(response, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return activityInfo.FilterValues(from, to);
        }

        public List<(string Username, ActivityInfo Activity)> GetActivityInfo(string[] usernames, bool isParallel, DateTime? from = null, DateTime? to = null)
        {
            if (!isParallel)
            {
                return usernames
                    .Select(username => (username, GetActivityInfo(username, @from, to).Result))
                    .ToList();
            }

            List<(string, ActivityInfo)> result = usernames
                .AsParallel()
                .Select(username => (username, GetActivityInfo(username, @from, to).Result))
                .ToList();

            return result;
        }
    }
}
