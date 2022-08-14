using System;
using System.Linq;
using Kysect.GithubActivityAnalyzer.ProfileActivityParsing.Models;

namespace Kysect.GithubActivityAnalyzer.ProfileActivityParsing
{
    public class UserProfileActivity
    {
        public string Username { get; set; }
        public ActivityInfo ActivityInfo { get; set; }

        public int TotalContributions => ActivityInfo.Total;

        public UserProfileActivity()
        {
        }

        public UserProfileActivity(string username, ActivityInfo activityInfo)
        {
            Username = username;
            ActivityInfo = activityInfo;
        }

        public int GetActivityForPeriod(DateTime from, DateTime to)
        {
            return ActivityInfo.GetActivityForPeriod(from, to);
        }

        public double GetAverageMonthActivity()
        {
            return ActivityInfo.PerMonthActivity()
                .Average(c => c.Count);
        }

        public double GetMovingAverage(DateTime from, DateTime to)
        {
            return ActivityInfo.Contributions
                .Where(k => k.Date <= @to && k.Date >= @from)
                .Average(c => c.Count);
        }
    }
}
