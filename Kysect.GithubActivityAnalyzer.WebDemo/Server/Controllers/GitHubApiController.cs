using System.Text.Json;
using Kysect.GithubActivityAnalyzer.ApiAccessor;
using Kysect.GithubActivityAnalyzer.ApiAccessor.ApiResponses;
using Microsoft.AspNetCore.Mvc;

namespace Kysect.GithubActivityAnalyzer.WebDemo.Server.Controllers
{
  [ApiController]
  [Route("[controller]")]
    public class GitHubApiController : Controller
    {
        private GithubActivityProvider provider = new GithubActivityProvider();

        [HttpGet]
        public ActivityInfo Get(string username)
        {
            return provider.GetActivityInfo(username).Result;
        }
    }
}
