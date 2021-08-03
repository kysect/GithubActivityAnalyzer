using Microsoft.AspNetCore.Mvc;
using Kysect.GithubActivityAnalyzer.Aggregators.Models;
using Kysect.GithubActivityAnalyzer.ApiAccessor;

namespace Kysect.GithubActivityAnalyzer.WebDemo.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TeamController : Controller
    {
        private readonly GithubActivityProvider _provider = new GithubActivityProvider();

        [HttpPost]
        public TeamResponse GetStudyGroup(Shared.Team info)
        {
            return new TeamResponse(new Aggregators.Team(info.TeamName, info.Usernames, _provider)); 
        }
    }
}
