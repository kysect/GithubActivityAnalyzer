using System;
using System.Collections.Generic;
using System.Linq;
using Kysect.GithubActivityAnalyzer.Aggregators.Models;
using Kysect.GithubActivityAnalyzer.ApiAccessor;
using Kysect.GithubActivityAnalyzer.ApiAccessor.ApiResponses;

namespace Kysect.GithubActivityAnalyzer.Aggregators
{
    public class StudyGroup
    {
        public string GroupName { get; set; }
        public List<Student> Students { get; set; }
        public List<MonthlyStatistics> Statistics => GetDetailedStat();
        public int TotalContributions => TotalActivity();

        public StudyGroup()
        {
        }

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

        public StudyGroup(string groupName, List<string> students, GithubActivityProvider provider)
        {
            GroupName = groupName;
            Students = new List<Student>();
            foreach ((string username, ActivityInfo activity) in provider.GetActivityInfo(students, true))
            {
                Students.Add(new Student(username, activity));
            }
        }

        private int TotalActivity()
        {
            return Students
                .Select(k => k.TotalContributions)
                .Sum();
        }

        public void AddStudents(GithubActivityProvider provider, bool isParallel, params string[] usernames)
        {
            var listInfo = provider.GetActivityInfo(usernames, isParallel);
            foreach (var item in listInfo)
            {
                Students.Add(new Student(item.Username, item.Activity));
            }
        }

        public Student GetMinValueStudent(DateTime? from = null, DateTime? to = null)
        {
            from ??= DateTime.MinValue;
            to ??= DateTime.Now;

            return Students
                .OrderBy(k => k.ActivityInfo.GetActivityForPeriod(from.GetValueOrDefault(), to.GetValueOrDefault()))
                .Last();

        }
        public Student GetMaxValueStudent(DateTime? from = null, DateTime? to = null)
        {
            from ??= DateTime.MinValue;
            to ??= DateTime.Now;

            return Students
                .OrderBy(k => k.ActivityInfo.GetActivityForPeriod(from.GetValueOrDefault(), to.GetValueOrDefault()))
                .First();

        }
        public double GetAverageValue(DateTime? from = null, DateTime? to = null)
        {
            from ??= DateTime.MinValue;
            to ??= DateTime.Now;

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

        public List<MonthlyStatistics> GetDetailedStat(DateTime? fromDate = null, DateTime? endTime = null)
        {
            var statistics = new List<MonthlyStatistics>();
            //TODO: fix
            DateTime from = fromDate ?? new DateTime(2020, 09, 01);
            endTime = endTime ?? DateTime.Now;
            for (DateTime to = from.AddMonths(1); from <= endTime || from.Month == endTime.Value.Month; to = from.AddMonths(1))
            {
                var detailedStat = this.Students
                    .Select(student => new StudentMonthlyActivity(student.Username, student.GetActivityForPeriod(from, to)))
                    .ToList();
                var monthStat = new MonthlyStatistics(from, detailedStat);
                statistics.Add(monthStat);
                from = to;
            }
            return statistics;
        }
    }
}
