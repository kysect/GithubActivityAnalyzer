using Kysect.GithubActivityAnalyzer.ApiAccessor;
using Kysect.GithubActivityAnalyzer.ApiAccessor.ApiResponses;
using Kysect.GithubActivityAnalyzer.Data.Repositories;
using System.Threading.Tasks;

namespace Kysect.GithubActivityAnalyzer.WebDemo.Server.Services
{
    public class ActivityService : IActivityService
    {
        private readonly GithubActivityProvider _githubActivityProvider;
        private readonly UserCacheRepository _userСacheRepository;
        public ActivityService(UserCacheRepository userСacheRepository, GithubActivityProvider githubActivityProvider)
        {
            this._userСacheRepository = userСacheRepository;
            this._githubActivityProvider = githubActivityProvider;
        }

        public async Task<ActivityInfo> GetActivityInfoFromGithub(string username)
        {
            return await _githubActivityProvider.GetActivityInfo(username);
        }

        public ActivityInfo GetActivityInfoFromDB(string username)
        {
            return _userСacheRepository.GetActivityFromUserCash(_userСacheRepository.FindByUsername(username));
        }
    }
}
