using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kysect.GithubActivityAnalyzer.Group;

namespace Kysect.GithubActivityAnalyzer.DetailedStats
{
    
   public class DetailedStats
   {
       public Dictionary<string, StudyGroup> Groups;

       public DetailedStats(List<StudentInfo> studentInfos, GithubActivityProvider provider)
       {
           Groups = new Dictionary<string, StudyGroup>();
           foreach (var pair in studentInfos)//убрать колхоз 
           {
               if (!Groups.ContainsKey(pair.Groupname))
               {
                   Groups.Add(pair.Groupname, new StudyGroup(pair.Groupname));
                   Groups[pair.Groupname].Students.Add(new Student(pair.Username, provider));
               }
               else
               {
                   Groups[pair.Groupname].Students.Add(new Student(pair.Username, provider));
               }
           }
       }

       public List<GroupInfo> GetDetailedStat(DateTime fromDate)
       {
           List<GroupInfo> stats = new List<GroupInfo>();

           DateTime from = fromDate;
           DateTime to = from.AddMonths(1);

           foreach (var group in Groups)
           {
               var groupMonthPair = new GroupInfo(group.Value, new List<MonthlyStatistics>());
               while (to <= DateTime.Now)
               {

                   var DetailedStat = new List<(Student, int)>();

                   foreach (var student in group.Value.Students) 
                   {
                       DetailedStat.Add((student, student.GetActivityForPeriod(from, to)));
                   }
                   var monthStat = new MonthlyStatistics(from, DetailedStat);
                  
                    groupMonthPair.Statistics.Add(monthStat);

                    from = to;
                    to = to.AddMonths(1);

               }
               from = fromDate;
               to = from.AddMonths(1);
               stats.Add(groupMonthPair);
           }
           return stats;
       }
   }
}
