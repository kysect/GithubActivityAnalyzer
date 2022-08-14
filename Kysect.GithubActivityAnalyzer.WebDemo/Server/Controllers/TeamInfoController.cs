using Microsoft.AspNetCore.Mvc;
using Kysect.GithubActivityAnalyzer.Aggregators.Models;
using Kysect.GithubActivityAnalyzer.ProfileActivityParsing;

namespace Kysect.GithubActivityAnalyzer.WebDemo.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TeamInfoController : Controller
    {
        private readonly ProfileActivityParser _parser = new ProfileActivityParser();

        [HttpPost("GetTeamInfo")]
        public TeamResponse GetTeamInfo(Shared.Team info)
        {
            return new TeamResponse(new Team(info.TeamName, info.Usernames, _parser)); 
        }
    }
}
