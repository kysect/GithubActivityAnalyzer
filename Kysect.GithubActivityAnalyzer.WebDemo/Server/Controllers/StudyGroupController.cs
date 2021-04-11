using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
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
        public StudyGroup Get(ControllerGroupInfo info)
        {
            StudyGroup newGroup = new StudyGroup(info.GroupName);
            for (int i = 0; i < info.usernames.Count; i++)
            {
                newGroup.Students.Add(new Student(info.usernames[i], _provider));
            }
            return newGroup;
        }
    }
}
