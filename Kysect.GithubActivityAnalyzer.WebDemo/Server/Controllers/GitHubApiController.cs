using System.Text.Json;
using Kysect.GithubActivityAnalyzer.ApiAccessor;
using Kysect.GithubActivityAnalyzer.ApiAccessor.ApiResponses;
using Kysect.GithubActivityAnalyzer.WebDemo.Server.Services;
using Microsoft.AspNetCore.Mvc;

namespace Kysect.GithubActivityAnalyzer.WebDemo.Server.Controllers
{
  [ApiController]
  [Route("[controller]")]
    public class GitHubApiController : Controller
    {
        private readonly IActivityService _activityService;
        public GitHubApiController(IActivityService activityService)
        {
            _activityService = activityService;
        }

        [HttpGet("fromGithub")]
        public ActivityInfo OnGetFromGithub(string username)
        {
            return _activityService.GetActivityInfoFromGithub(username).Result;
        }

        [HttpGet("FromDB")]
        public ActivityInfo OnGetFromDB(string username)
        {
            return _activityService.GetActivityInfoFromDB(username);
        }
    }
}
