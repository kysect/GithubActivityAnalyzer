using System;
using System.Collections.Generic;
using System.Linq;
using Kysect.GithubActivityAnalyzer.Services;

namespace Kysect.GithubActivityAnalyzer.Models.Aggregations
{
    
   public class DetailedStats
   {
       public Dictionary<string, StudyGroup> Groups { get; }

       public DetailedStats(List<StudentInfo> studentInfos, GithubActivityProvider provider)
       {
           Groups = studentInfos
               .GroupBy(c => c.NumberOfGroup)
               .ToDictionary(c => c.Key, k => new StudyGroup(k.Key, k
                   .Select(c => new Student(c.Username, provider))
                   .ToList()));
       }

       public List<GroupInfo> GetDetailedStat(DateTime fromDate, DateTime? endTime = null)
       {
           List<GroupInfo> stats = new List<GroupInfo>();
           endTime = endTime ?? DateTime.Now;

            DateTime from = fromDate;
           foreach (var group in Groups)
           {
              
               var groupMonthPair = new GroupInfo(group.Value, new List<MonthlyStatistics>());
               for (DateTime to = from.AddMonths(1); from <= endTime || from.Month == endTime.Value.Month ; to = from.AddMonths(1))
               {
                   var detailedStat = group.Value.Students
                       .Select(student => (student, student.GetActivityForPeriod(from, to)))
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
