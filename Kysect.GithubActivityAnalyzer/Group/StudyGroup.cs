using System;
using System.Collections.Generic;
using System.Linq;

namespace Kysect.GithubActivityAnalyzer.Group
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
            return (from student in Students select student.TotalContributions).ToArray().Sum();
        }

        public void AddStudents(params string[] username)
        {
            for (int i = 0; i < username.Length; i++)
            {
                Students.Add(new Student(username[i]));
            }
        }
        //+по интервалам

        public Student GetMinValueStudent(DateTime? from = null, DateTime? to = null)
        {
            DateTime minValue = DateTime.MaxValue;
            foreach (var days in from student in Students from days in student.ActivityInfo.Contributions where days.Date < minValue select days)
            {
                minValue = days.Date;
            }

            from = from ?? minValue;
            to = to ?? DateTime.Now;

                return Students.FirstOrDefault(s =>
                    s.ActivityInfo.GetActivityForPeriod(from.GetValueOrDefault(), to.GetValueOrDefault()) ==
                    Students.Select(k => k.ActivityInfo.GetActivityForPeriod(from.GetValueOrDefault(), to.GetValueOrDefault())).ToList().Min());
            
        }
        public Student GetMaxValueStudent(DateTime? from = null, DateTime? to = null)
        {
            DateTime minValue = DateTime.MaxValue;
            foreach (var days in from student in Students from days in student.ActivityInfo.Contributions where days.Date < minValue select days)
            {
                minValue = days.Date;
            }

            from = from ?? minValue;
            to = to ?? DateTime.Now;

            return Students.FirstOrDefault(s =>
                s.ActivityInfo.GetActivityForPeriod(from.GetValueOrDefault(), to.GetValueOrDefault()) ==
                Students.Select(k => k.ActivityInfo.GetActivityForPeriod(from.GetValueOrDefault(), to.GetValueOrDefault())).ToList().Max());
        }
        public double GetAverageValue(DateTime? from = null, DateTime? to = null)
        {
            DateTime minValue = DateTime.MaxValue;
            foreach (var days in from student in Students from days in student.ActivityInfo.Contributions where days.Date < minValue select days)
            {
                minValue = days.Date;
            }

            from = from ?? minValue;
            to = to ?? DateTime.Now;
            
            return Students.Select(k => k.ActivityInfo.GetActivityForPeriod(from.GetValueOrDefault(), to.GetValueOrDefault())).ToList().Average();
        }

        public Dictionary<string, int> GetShortInfo()
        {
            Dictionary<string, int> UsersContributions = new Dictionary<string, int>();

            foreach (var student in Students)
            {
                UsersContributions.Add(student.Username, student.TotalContributions);
            }

            return UsersContributions;
        }

        public int GetActivityForPeriod(DateTime from, DateTime to)
        { 
            return Students.Select(student => student.ActivityInfo.GetActivityForPeriod(@from, to)).ToList().Sum();
        }

        public double GetAverageMonthActivity()
        {
            List<int> averegeInts = Students.Select(student => Convert.ToInt32(student.ActivityInfo.PerMonthActivity().Average(c => c.Count))).ToList();
            return averegeInts.Average();
        }

        public double GetMovingAverage(DateTime from, DateTime to)
        {
            return Students.Select(s => s.GetMovingAverage(from, to)).ToList().Average();
        }

    }
}
