using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Kysect.GithubActivityAnalyzer.Models;

namespace Kysect.GithubActivityAnalyzer.Group
{
    public class Student
    {
        public string Username { get; set; }
        public ActivityInfo ActivityInfo { get; set; }

        public int TotalContributions
        {
            get => ActivityInfo.Total;

            set => TotalContributions = ActivityInfo.Total;
        }

        public Student(string username)
        {
            Username = username;
            GithubActivityProvider newProvider = new GithubActivityProvider();
            ActivityInfo = newProvider.GetActivityInfo(username).Result;
        }

        public int GetActivityForPeriod(DateTime from, DateTime to)
        {
            return ActivityInfo.GetActivityForPeriod(from, to);
        }

        public int GetAverageMonthActivity()
        {
            return Convert.ToInt32(ActivityInfo.PerMonthActivity().Average(c => c.Count));
        }
    }
}
