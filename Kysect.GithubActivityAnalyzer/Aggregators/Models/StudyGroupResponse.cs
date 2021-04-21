using System.Collections.Generic;
using System.Linq;

namespace Kysect.GithubActivityAnalyzer.Aggregators.Models
{
    public class StudyGroupResponse
    {
        public string GroupName { get; set; }
        public List<ShortStudentInfo> Students { get; set; }
        public List<MonthlyStatistics> DetailedStatisticsList { get; set; }

        public int TotalContributions { get; set; }

        public StudyGroupResponse() { }
        public StudyGroupResponse(StudyGroup group)
        {
            GroupName = group.GroupName;
            TotalContributions = group.TotalContributions;
            Students = group.Students.Select(a => new ShortStudentInfo(a.Username, a.TotalContributions)).ToList();
            DetailedStatisticsList = group.Statistics;
        }
    }

    public class ShortStudentInfo
    {
        public string Username { get; set; }

        public int TotalContributions { get; set; }

        public ShortStudentInfo() { } 
        public ShortStudentInfo(string name, int totalContributions)
        {
            Username = name;
            TotalContributions = totalContributions;
        }
    }
}
