using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kysect.GithubActivityAnalyzer.WebDemo.Shared
{
   public class GroupStatRequest
   {
        public string GroupName { get; set; }
        public List<string> usernames { get; set; }

        public GroupStatRequest()
        { }

        public GroupStatRequest(string groupName)
        {
            GroupName = groupName;
        }
   }
}
