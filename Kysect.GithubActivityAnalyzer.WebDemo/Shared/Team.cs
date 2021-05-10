using System.Collections.Generic;

namespace Kysect.GithubActivityAnalyzer.WebDemo.Shared
{
   public class Team
   {
        public string TeamName { get; set; }
        public List<string> Usernames { get; set; }

        public Team(){ }

        public Team(string teamName)
        {
            TeamName = teamName;
        }
   }
}
