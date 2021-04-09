using System.Text.Json;
using Kysect.GithubActivityAnalyzer.Models.ApiResponses;
using Microsoft.AspNetCore.Mvc;

namespace Kysect.GithubActivityAnalyzer.WebDemo.Server.Controllers
{
  [ApiController]
  [Route("[controller]")]
    public class GitHubApiController : Controller
    {
        private GithubActivityProvider provider = new GithubActivityProvider();

        [HttpGet]
        public ActionResult<string> Get(string username)
        {
            return JsonSerializer.Serialize(provider.GetActivityInfo(username).Result);
        }
    }
}
