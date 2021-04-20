using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Kysect.GithubActivityAnalyzer.Models.Aggregations;
using Kysect.GithubActivityAnalyzer.Services;
using Kysect.GithubActivityAnalyzer.WebDemo.Shared;

namespace Kysect.GithubActivityAnalyzer.WebDemo.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudyGroupController : Controller
    {
        private GithubActivityProvider _provider = new GithubActivityProvider();

        [HttpPost]
        public StudyGroupResponse GetStudyGroup(GroupStatRequest info)
        {
            return new StudyGroupResponse(new StudyGroup(info.GroupName, info.usernames, _provider)); 
        }
    }
}
