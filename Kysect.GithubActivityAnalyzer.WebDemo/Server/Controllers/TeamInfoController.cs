﻿using Microsoft.AspNetCore.Mvc;
using Kysect.GithubActivityAnalyzer.ProfileActivityParsing;
using Kysect.GithubActivityAnalyzer.ProfileActivityParsing.Models;
using Kysect.GithubUtils;

namespace Kysect.GithubActivityAnalyzer.WebDemo.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TeamInfoController : Controller
    {
        private readonly GithubActivityProvider _parser = new GithubActivityProvider();

        [HttpPost("GetTeamInfo")]
        public TeamResponse GetTeamInfo(Shared.Team info)
        {
            return new TeamResponse(new Team(info.TeamName, info.Usernames, _parser)); 
        }
    }
}
