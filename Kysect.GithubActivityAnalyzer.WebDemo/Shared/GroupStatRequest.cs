using System.Collections.Generic;

namespace Kysect.GithubActivityAnalyzer.WebDemo.Shared
{
   public class GroupStatRequest
   {
        public string GroupName { get; set; }
        public List<string> Usernames { get; set; }

        public GroupStatRequest(){ }

        public GroupStatRequest(string groupName)
        {
            GroupName = groupName;
        }
   }
}
