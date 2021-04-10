using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Kysect.GithubActivityAnalyzer.Services;

namespace Kysect.GithubActivityAnalyzer.WebDemo.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudyGroupController : Controller
    {
        private GithubActivityProvider _provider = new GithubActivityProvider();

        [HttpPost]
        public StudyGroup Get(string[] usernames)
        {
            StudyGroup newGroup = new StudyGroup(usernames[0]);
            for (int i = 1; i < usernames.Length; i++)
            {
                newGroup.Students.Add(new Student(usernames[i], _provider));
            }
            return newGroup;
        }
    }
}
