using Kysect.GithubActivityAnalyzer.Extensions.Data.Repositories;
using System.Threading.Tasks;
using Kysect.GithubActivityAnalyzer.ProfileActivityParsing;
using Kysect.GithubActivityAnalyzer.ProfileActivityParsing.Models;

namespace Kysect.GithubActivityAnalyzer.WebDemo.Server.Services
{
    public class ActivityService : IActivityService
    {
        private readonly ProfileActivityParser _profileActivityParser;
        private readonly UserCacheRepository _userСacheRepository;
        public ActivityService(UserCacheRepository userСacheRepository, ProfileActivityParser profileActivityParser)
        {
            this._userСacheRepository = userСacheRepository;
            this._profileActivityParser = profileActivityParser;
        }

        public async Task<ActivityInfo> GetActivityInfoFromGithub(string username)
        {
            return await _profileActivityParser.GetActivityInfo(username);
        }

        public ActivityInfo GetActivityInfoFromDB(string username)
        {
            return _userСacheRepository.GetActivityFromUserCash(_userСacheRepository.FindByUsername(username));
        }
    }
}
