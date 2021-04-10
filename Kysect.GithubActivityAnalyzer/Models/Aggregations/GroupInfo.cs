using System.Collections.Generic;
using Kysect.GithubActivityAnalyzer.Services;

namespace Kysect.GithubActivityAnalyzer.Models.Aggregations
{
    public class GroupInfo
    {
        public StudyGroup Group { get; set; }
        public List<MonthlyStatistics> Statistics { get; set; }

        public GroupInfo(StudyGroup group, List<MonthlyStatistics> statisticsList)
        {
            Group = group;
            Statistics = statisticsList;
        }

        public GroupInfo()
        {
        }
    }
}
