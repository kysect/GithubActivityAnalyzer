using System.Collections.Generic;
using System.Linq;
using Kysect.GithubActivityAnalyzer.ProfileActivityParsing;

namespace Kysect.GithubActivityAnalyzer.Aggregators.Models
{
    public class TeamResponse
    {
        public string TeamName { get; set; }
        public List<ShortMemberInfo> Members { get; set; }
        public List<MonthlyStatistics> DetailedStatisticsList { get; set; }

        public int TotalContributions { get; set; }

        public TeamResponse() { }
        public TeamResponse(Team team)
        {
            TeamName = team.TeamName;
            TotalContributions = team.TotalContributions;
            Members = team.Members.Select(a => new ShortMemberInfo(a.Username, a.TotalContributions)).ToList();
            DetailedStatisticsList = team.Statistics;
        }
    }

    public class ShortMemberInfo
    {
        public string Username { get; set; }

        public int TotalContributions { get; set; }

        public ShortMemberInfo() { } 
        public ShortMemberInfo(string name, int totalContributions)
        {
            Username = name;
            TotalContributions = totalContributions;
        }
    }
}
