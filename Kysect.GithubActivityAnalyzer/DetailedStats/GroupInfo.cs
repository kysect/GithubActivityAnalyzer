using System;
using System.Collections.Generic;
using System.Text;
using Kysect.GithubActivityAnalyzer.Group;

namespace Kysect.GithubActivityAnalyzer.DetailedStats
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
    }
}
