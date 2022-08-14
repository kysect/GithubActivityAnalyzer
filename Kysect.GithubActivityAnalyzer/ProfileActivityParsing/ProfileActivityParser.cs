using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Kysect.GithubActivityAnalyzer.ProfileActivityParsing.Models;

namespace Kysect.GithubActivityAnalyzer.ProfileActivityParsing
{
    public class ProfileActivityParser
    {
        private const string Url = "https://github-contributions.now.sh/api/v1/";

        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _jsonSerializerOptions;

        public ProfileActivityParser() : this(new HttpClient())
        {
        }

        public ProfileActivityParser(HttpClient client)
        {
            _client = client;
            _jsonSerializerOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<ActivityInfo> GetActivityInfo(string username, DateTime? from = null, DateTime? to = null)
        {
            ActivityInfo info = await GetActivityInfo(username);
            return info.FilterValues(from, to);
        }

        public async Task<ActivityInfo> GetActivityInfo(string username)
        {
            return await _client.GetFromJsonAsync<ActivityInfo>(Url + username, _jsonSerializerOptions);
        }

        public Dictionary<string, ActivityInfo> GetActivityInfo(IReadOnlyCollection<string> usernames, bool isParallel, DateTime? from = null, DateTime? to = null)
        {
            if (!isParallel)
            {
                return usernames
                    .ToDictionary(username => username, username => GetActivityInfo(username, @from, to).Result);
            }

            return usernames
                .AsParallel()
                .ToDictionary(username => username, username => GetActivityInfo(username, @from, to).Result);
        }

        /// <summary>
        /// Sometimes result is empty. We will try to resend request to ensure that we cannot fetch data.
        /// </summary>
        /// <param name="usernames"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public Dictionary<string, ActivityInfo> GetInfoWithRetry(IReadOnlyCollection<string> usernames, DateTime? from = null, DateTime? to = null)
        {
            int tryCount = 5;

            Dictionary<string, ActivityInfo> result = new Dictionary<string, ActivityInfo>();

            Debug.Print($"Start processing: {usernames.Count}");
            for (int i = 0; i < tryCount && result.Count != usernames.Count; i++)
            {
                if (i != 0)
                {
                    Debug.Print($"Elements for processing left: {usernames.Count - result.Count}");
                    Thread.Sleep(2000);
                }

                List<(string Username, ActivityInfo Result)> localResult = usernames
                    .Where(u => !result.ContainsKey(u))
                    .AsParallel()
                    .Select(username => (username, GetActivityInfo(username, @from, to).Result))
                    .Where(r => r.Result.Total > 0)
                    .ToList();

                localResult.ForEach(r => result[r.Username] = r.Result);
            }

            return result;
        }
    }
}
