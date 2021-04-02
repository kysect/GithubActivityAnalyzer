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

       public struct MonthlyStatistics
       {
           public string month;
           public (Student,int) minValueStudent;
           public (Student, int) maxValueStudent;
           public double AverageValue;
           public int TotalContributions;
           public Dictionary<Student, int> DetailedStat;
       }

       public DetailedStats(List<(string,string)> studentsList, GithubActivityProvider provider)//номер группы, гитхаб
       {
           Groups = new Dictionary<string, StudyGroup>();
           foreach (var pair in studentsList)
           {
               if (!Groups.ContainsKey(pair.Item1))
               {
                   Groups.Add(pair.Item1, new StudyGroup(pair.Item1));
                   Groups[pair.Item1].Students.Add(new Student(pair.Item2, provider));
               }
               else
               {
                   Groups[pair.Item1].Students.Add(new Student(pair.Item2, provider));
               }
           }
       }

       public List<(StudyGroup, List<MonthlyStatistics>)> GetDetailedStat(DateTime inputDate)
       {
           List<(StudyGroup, List<MonthlyStatistics>)> stats = new List<(StudyGroup, List<MonthlyStatistics>)>();

           DateTime from = inputDate;
           DateTime to = from.AddMonths(1);

           foreach (var group in Groups)
           {
               var Pair = (group.Value, new List<MonthlyStatistics>());
               while (to <= DateTime.Now)
               {
                    var monthStat = new MonthlyStatistics()
                    {
                           DetailedStat = new Dictionary<Student, int>()
                    }; 

                    foreach (var student in group.Value.Students) 
                    {
                        monthStat.DetailedStat.Add(student, student.GetActivityForPeriod(from, to));
                    }

                    monthStat.AverageValue = monthStat.DetailedStat.Average(a => a.Value);

                    monthStat.TotalContributions = monthStat.DetailedStat.Sum(a => a.Value);

                    monthStat.month = from.Month.ToString() +"."+ from.Year.ToString();
                    
                    monthStat.minValueStudent = (monthStat.DetailedStat.
                            OrderBy(a => a.Value).
                            Last().Key,
                        monthStat.DetailedStat.
                            OrderBy(a => a.Value)
                            .Last().Value);

                    monthStat.maxValueStudent = (monthStat.DetailedStat
                            .OrderBy(a => a.Value)
                            .First().Key,
                        monthStat.DetailedStat
                            .OrderBy(a => a.Value)
                            .First().Value);
                    Pair.Item2.Add(monthStat);
                    from = to;
                    to = to.AddMonths(1);
               }

               from = inputDate;
               to = from.AddMonths(1);
               stats.Add(Pair);
           }
           return stats;
       }
   }
}
