using Kysect.GithubUtils;
using Microsoft.AspNetCore.Mvc;


namespace Kysect.GithubActivityAnalyzer.WebDemo.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DetailedTeamStatController : Controller
    {
        private readonly GithubActivityProvider _parser = new GithubActivityProvider();

         /*[HttpPost]
       public IEnumerable<GroupInfo> GetGroupInfos(DetaiedStatRequest request)
        {
            List<GroupInfo> stats = new DetailedStats(request.StudentList, _provider).GetDetailedStat(request.From, request.To);
            return stats;
        }*/
    }
}
