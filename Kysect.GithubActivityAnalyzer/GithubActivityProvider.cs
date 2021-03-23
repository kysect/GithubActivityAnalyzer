using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Serialization;
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
            var jstring = JsonConvert.DeserializeObject<ActivityInfo>(response);
            var contributionsList = jstring.Contributions.ToList();

            List<ContributionsInfo> keyToDelete = jstring.Contributions.Where(element => element.date > DateTime.Now).ToList();
            foreach (var element in keyToDelete)
            {
                contributionsList.Remove(element);
            }

            jstring.Contributions = contributionsList.ToArray();
            return jstring;
        }
    }
}