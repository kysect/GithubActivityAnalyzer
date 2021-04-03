using System;
using System.Collections.Generic;
using System.Linq;
using Kysect.GithubActivityAnalyzer.Group;

namespace Kysect.GithubActivityAnalyzer.DetailedStats
{
    
   public class DetailedStats
   {
       public Dictionary<string, StudyGroup> Groups;

       public DetailedStats(List<StudentInfo> studentInfos, GithubActivityProvider provider)
       {
           Groups = studentInfos
               .GroupBy(c => c.NumberOfGroup)
               .ToDictionary(c => c.Key, k => new StudyGroup(k.Key, studentInfos
                   .Where(c => c.NumberOfGroup == k.Key )
                   .Select(c => new Student(c.Username, provider))
                   .ToList()));
       }

       public List<GroupInfo> GetDetailedStat(DateTime fromDate)
       {
           List<GroupInfo> stats = new List<GroupInfo>();

           DateTime from = fromDate;
           foreach (var group in Groups)
           {
               var groupMonthPair = new GroupInfo(group.Value, new List<MonthlyStatistics>());
               for (DateTime to = from.AddMonths(1); to <= DateTime.Now; to = from.AddMonths(1))
               {
                   var detailedStat = @group.Value.Students
                       .Select(student => (student, student.GetActivityForPeriod(@from, to)))
                       .ToList();

                   var monthStat = new MonthlyStatistics(from, detailedStat);
                   groupMonthPair.Statistics.Add(monthStat);

                    from = to;
               }
               from = fromDate;
               stats.Add(groupMonthPair);
           }
           return stats;
       }
   }
}
