using Kysect.GithubActivityAnalyzer.WebDemo.Server.Services;
using Kysect.GithubActivityAnalyzer.WebDemo.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kysect.GithubActivityAnalyzer.WebDemo.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DBTeamController : Controller
    {
        private readonly ITeamService _teamService;
        public DBTeamController(ITeamService teamService)
        {
            _teamService = teamService;
        }

        [HttpPost("addTeam")]
        public void AddTeam(Team newTeam)
        {
            _teamService.AddTeam(newTeam);
        }

        [HttpPost("deleteTeam")]
        public void DeleteTeam(string teamName)
        {
            _teamService.DeleteTeam(teamName);
        }  

        [HttpPost("deleteMember")]
        public void DeleteMember(string username)
        {
            _teamService.DeleteMember(username);
        }

        ///TO DO: to realize
        [HttpPost("updateMember")]
        public void UpdateMember()
        {
        }
        ///TO DO: to realize
        [HttpGet("GetAllTeams")]
        public void GetAllTeams()
        {
        }

        [HttpPost("GetTeam")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Team))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetTeam(Team teamName)
        {

            if (!_teamService.TryGetTeam(teamName, out var team))
            {
                return NotFound();
            }

            return Ok(team);
        }
    }
}
