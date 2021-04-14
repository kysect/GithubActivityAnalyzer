using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Kysect.GithubActivityAnalyzer.Models.Aggregations;
using Kysect.GithubActivityAnalyzer.Services;
using Kysect.GithubActivityAnalyzer.WebDemo.Shared;

namespace Kysect.GithubActivityAnalyzer.WebDemo.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DetailedGroupStatController : Controller
    {
        private GithubActivityProvider _provider = new GithubActivityProvider();

         /*[HttpPost]
       public IEnumerable<GroupInfo> GetGroupInfos(DetaiedStatRequest request)
        {
            List<GroupInfo> stats = new DetailedStats(request.StudentList, _provider).GetDetailedStat(request.From, request.To);
            return stats;
        }*/
    }
}
