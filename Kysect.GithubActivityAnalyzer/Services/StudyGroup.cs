using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kysect.GithubActivityAnalyzer.Services
{
   public class StudyGroup
   {
        public string GroupName { get; set; }
        public List<Student> Students { get; set; }

        public int TotalContributions => TotalActivity();

        public StudyGroup(string groupName)
        {
            GroupName = groupName;
            Students = new List<Student>();
        }

        public StudyGroup(string groupName, List<Student> students)
        {
            GroupName = groupName;
            Students = students;
        }

        private int TotalActivity()
        {
            return Students
                .Select(k => k.TotalContributions)
                .Sum();
        }

        public void AddStudents( GithubActivityProvider provider, bool isParallel, params string[] usernames)
        {
            var listInfo = provider.GetStudentListInfo(usernames, isParallel);
            foreach (var item in listInfo)
            {
                Students.Add(item);
            }
        }
        
        public Student GetMinValueStudent(DateTime? from = null, DateTime? to = null)
        {
            from = from ?? DateTime.MinValue;
            to = to ?? DateTime.Now;

            return Students
                .OrderBy(k => k.ActivityInfo.GetActivityForPeriod(from.GetValueOrDefault(), to.GetValueOrDefault()))
                .Last();

        }
        public Student GetMaxValueStudent(DateTime? from = null, DateTime? to = null)
        {
            from = from ?? DateTime.MinValue;
            to = to ?? DateTime.Now;

            return Students
                .OrderBy(k => k.ActivityInfo.GetActivityForPeriod(from.GetValueOrDefault(), to.GetValueOrDefault()))
                .First();

        }
        public double GetAverageValue(DateTime? from = null, DateTime? to = null)
        {
            from = from ?? DateTime.MinValue;
            to = to ?? DateTime.Now;
            
            return Students
                .Select(k => k.ActivityInfo.GetActivityForPeriod(from.GetValueOrDefault(), to.GetValueOrDefault()))
                .Average();
        }

        public Dictionary<string, int> GetShortInfo()
        {
            Dictionary<string, int> usersContributions = new Dictionary<string, int>();

            foreach (var student in Students)
            {
                usersContributions.Add(student.Username, student.TotalContributions);
            }
            return usersContributions;
        }
        public int GetActivityForPeriod(DateTime from, DateTime to)
        { 
            return Students
                .Select(student => student.ActivityInfo.GetActivityForPeriod(@from, to))
                .Sum();
        }

        public double GetAverageMonthActivity()
        {
            return Students
                .Select(student => Convert.ToInt32(student.ActivityInfo.PerMonthActivity()
                    .Average(c => c.Count)))
                .ToList()
                .Average();
        }

        public double GetMovingAverage(DateTime from, DateTime to)
        {
            return Students
                .Select(s => s.GetMovingAverage(from, to))
                .Average();
        }
   }
}
