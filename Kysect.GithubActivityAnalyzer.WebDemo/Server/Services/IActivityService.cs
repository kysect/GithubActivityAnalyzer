using Kysect.GithubActivityAnalyzer.ApiAccessor.ApiResponses;
using System.Threading.Tasks;

namespace Kysect.GithubActivityAnalyzer.WebDemo.Server.Services
{
    public interface IActivityService
    {
        Task<ActivityInfo> GetActivityInfoFromGithub(string username);
        ActivityInfo GetActivityInfoFromDB(string username);
    }
}
