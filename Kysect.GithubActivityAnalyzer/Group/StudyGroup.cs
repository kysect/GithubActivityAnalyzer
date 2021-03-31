using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Kysect.GithubActivityAnalyzer.Group
{
   public class StudyGroup
   {
        public string GroupName { get; set; }
        public List<Student> Students { get; set; }

        public int TotalContributions
        {
            get => (from student in Students select student.TotalContributions).ToArray().Sum();

            private set => TotalContributions = (from student in Students select student.TotalContributions).ToArray().Sum();
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

        public void AddStudents(params string[] username)
        {
            for (int i = 0; i < username.Length; i++)
            {
                Students.Add(new Student(username[i]));
            }
        }

        public int GetMinValue()
        {
           return (from student in Students select student.TotalContributions).ToList().Min();
        }
        public int GetMaxValue()
        {
           return (from student in Students select student.TotalContributions).ToList().Max();
        }

        public Student GetMinValueStudent()
        {
            return Students.FirstOrDefault(s => s.TotalContributions == GetMinValue());
        }
        public Student GetMaxValueStudent()
        {
            return Students.FirstOrDefault(s => s.TotalContributions == GetMaxValue());
        }
        public int GetAverageValue()
        {
            return Convert.ToInt32((from student in Students select student.TotalContributions).ToList().Average()) ;
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

        public int GetAverageMonthActivity()
        {
            List<int> averegeInts = Students.Select(student => Convert.ToInt32(student.ActivityInfo.PerMonthActivity().Average(c => c.Count))).ToList();
            return Convert.ToInt32(averegeInts.Average());
        }

   }
}
