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

        public async Task<ActivityInfo> GetActivityInfo(string username)
        {
            string response = await _client.GetStringAsync(Url + username);
            var activityInfo = JsonSerializer.Deserialize<ActivityInfo>(response, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            activityInfo.Contributions = activityInfo.Contributions.Where(element => element.Date <= DateTime.Now).ToArray();
            return activityInfo;
        }

        public async Task<ActivityInfo> GetActivityInfo(string username, DateTime from, DateTime to)
        {
            string response = await _client.GetStringAsync(Url + username);

            var activityInfo = JsonSerializer.Deserialize<ActivityInfo>(response, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            activityInfo.Contributions = activityInfo.Contributions.Where(element => element.Date <= to && element.Date >= from).ToArray();
            return activityInfo;
        }

        public ParallelQuery<Student> GetStudentListInfo(List<string> usernames)
        {
            var result = from user in usernames.AsParallel()
                          select new Student(user, this);
                
            return result;
        }
    }
}