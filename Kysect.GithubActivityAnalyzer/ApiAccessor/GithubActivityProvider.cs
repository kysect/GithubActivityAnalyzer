using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Kysect.GithubActivityAnalyzer.ApiAccessor.ApiResponses;

namespace Kysect.GithubActivityAnalyzer.ApiAccessor
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

        public List<(string Username, ActivityInfo Activity)> GetActivityInfo(IReadOnlyCollection<string> usernames, bool isParallel, DateTime? from = null, DateTime? to = null)
        {
            if (!isParallel)
            {
                return usernames
                    .Select(username => (username, GetActivityInfo(username, @from, to).Result))
                    .ToList();
            }

            return usernames
                .AsParallel()
                .Select(username => (username, GetActivityInfo(username, @from, to).Result))
                .ToList();
        }

        /// <summary>
        /// Sometimes result is empty. We will try to resend request to ensure that we cannot fetch data.
        /// </summary>
        /// <param name="usernames"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public List<(string Username, ActivityInfo Activity)> GetInfoWithRetry(IReadOnlyCollection<string> usernames, DateTime? from = null, DateTime? to = null)
        {
            int tryCount = 5;

            List<(string Username, ActivityInfo Activity)> result = new List<(string Username, ActivityInfo Activity)>();
            HashSet<string> processedUsers = new HashSet<string>();

            Debug.Print($"Start processing: {usernames.Count}");
            for (int i = 0; i < tryCount && result.Count != usernames.Count; i++)
            {
                if (i != 0)
                {
                    Debug.Print($"Elements for processing left: {usernames.Count - result.Count}");
                    Thread.Sleep(500);
                }

                List<(string Username, ActivityInfo Result)> localResult = usernames
                    .Where(u => !processedUsers.Contains(u))
                    .AsParallel()
                    .Select(username => (username, GetActivityInfo(username, @from, to).Result))
                    .Where(r => r.Result.Total > 0)
                    .ToList();

                result.AddRange(localResult);
                localResult.ForEach(r => processedUsers.Add(r.Username));
            }

            return result;
        }
    }
}
