using Kysect.GithubActivityAnalyzer.ApiAccessor.ApiResponses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kysect.GithubActivityAnalyzer.WebDemo.Server.Services
{
    public interface IActivityService
    {
        public Task<ActivityInfo> GetActivityInfoFromGithub(string username);
        public ActivityInfo GetActivityInfoFromDB(string username);
    }
}
