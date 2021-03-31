using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Kysect.GithubActivityAnalyzer.Models;

namespace Kysect.GithubActivityAnalyzer.Group
{
    public class Student
    {
        public string Username { get; set; }
        public ActivityInfo ActivityInfo { get; set; }

        private GithubActivityProvider newProvider = new GithubActivityProvider();

        public int TotalContributions => ActivityInfo.Total;

        public Student(string username)
        {
            Username = username;
            ActivityInfo = newProvider.GetActivityInfo(username).Result;
        }

        public int GetActivityForPeriod(DateTime from, DateTime to)
        {
            return ActivityInfo.GetActivityForPeriod(from, to);
        }

        public double GetAverageMonthActivity()
        {
            return ActivityInfo.PerMonthActivity().Average(c => c.Count);
        }

        public double GetMovingAverage(DateTime from, DateTime to)
        {
            return (from days in ActivityInfo.Contributions where days.Date < to && days.Date > @from select days.Count).ToList().Average();
        }
    }
}
