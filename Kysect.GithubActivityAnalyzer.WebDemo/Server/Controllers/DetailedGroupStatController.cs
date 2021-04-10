using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Kysect.GithubActivityAnalyzer.Models.Aggregations;

namespace Kysect.GithubActivityAnalyzer.WebDemo.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DetailedGroupStatController : Controller
    {
        private GithubActivityProvider _provider = new GithubActivityProvider();

        [HttpPost]
        public IEnumerable<GroupInfo> PostGroupInfos(List<StudentInfo> infos)
        {
            List<GroupInfo> stats = new DetailedStats(infos, _provider).GetDetailedStat(new DateTime(2020, 09, 1));
            return stats;
        }
    }
}
