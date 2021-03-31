using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Kysect.GithubActivityAnalyzer.Models;
using Newtonsoft.Json;

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

            var activityInfo = JsonConvert.DeserializeObject<ActivityInfo>(response);
            var contributionsList = activityInfo.Contributions.Where(element => element.Date <= DateTime.Now).ToList();

            activityInfo.Contributions = contributionsList.ToArray();
            return activityInfo;
        }
    }
}