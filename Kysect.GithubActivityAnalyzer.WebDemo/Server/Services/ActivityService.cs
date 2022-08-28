using Kysect.GithubActivityAnalyzer.Extensions.Data.Repositories;
using System.Threading.Tasks;
using Kysect.GithubActivityAnalyzer.ProfileActivityParsing;
using Kysect.GithubUtils;

namespace Kysect.GithubActivityAnalyzer.WebDemo.Server.Services
{
    public class ActivityService : IActivityService
    {
        private readonly GithubActivityProvider _profileActivityParser;
        private readonly UserCacheRepository _userСacheRepository;
        public ActivityService(UserCacheRepository userСacheRepository, GithubActivityProvider profileActivityParser)
        {
            _userСacheRepository = userСacheRepository;
            _profileActivityParser = profileActivityParser;
        }

        public Task<ActivityInfo> GetActivityInfoFromGithub(string username)
        {
            return Task.FromResult(_profileActivityParser.GetActivityInfo(username));
        }

        public ActivityInfo GetActivityInfoFromDB(string username)
        {
            return _userСacheRepository.GetActivityFromUserCash(_userСacheRepository.FindByUsername(username));
        }
    }
}
