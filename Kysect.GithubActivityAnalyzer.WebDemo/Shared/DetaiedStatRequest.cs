using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kysect.GithubActivityAnalyzer.Models.Aggregations;

namespace Kysect.GithubActivityAnalyzer.WebDemo.Shared
{
    public class DetaiedStatRequest
    {
        public List<StudentInfo> StudentList { get; set; }
        public DateTime To { get; set; }
        public DateTime From { get; set; }

    }
}
