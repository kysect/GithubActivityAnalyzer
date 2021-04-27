using Microsoft.AspNetCore.Mvc;
using Kysect.GithubActivityAnalyzer.ApiAccessor;


namespace Kysect.GithubActivityAnalyzer.WebDemo.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DetailedGroupStatController : Controller
    {
        private readonly GithubActivityProvider _provider = new GithubActivityProvider();

         /*[HttpPost]
       public IEnumerable<GroupInfo> GetGroupInfos(DetaiedStatRequest request)
        {
            List<GroupInfo> stats = new DetailedStats(request.StudentList, _provider).GetDetailedStat(request.From, request.To);
            return stats;
        }*/
    }
}
