using System.Threading.Tasks;
using Kysect.GithubActivityAnalyzer.ProfileActivityParsing.Models;

namespace Kysect.GithubActivityAnalyzer.WebDemo.Server.Services
{
    public interface IActivityService
    {
        Task<ActivityInfo> GetActivityInfoFromGithub(string username);
        ActivityInfo GetActivityInfoFromDB(string username);
    }
}
