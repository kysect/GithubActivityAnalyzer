using System;
using System.Collections.Generic;
using System.Linq;
using Kysect.GithubActivityAnalyzer.Aggregators.Models;
using Kysect.GithubActivityAnalyzer.ProfileActivityParsing.Models;

namespace Kysect.GithubActivityAnalyzer.ProfileActivityParsing
{
    public class Team
    {
        public string TeamName { get; set; }
        public List<UserProfileActivity> Members { get; set; }
        public List<MonthlyStatistics> Statistics => GetDetailedStat();
        public int TotalContributions => TotalActivity();

        public Team()
        {
        }

        public Team(string teamName)
        {
            TeamName = teamName;
            Members = new List<UserProfileActivity>();
        }

        public Team(string teamName, List<UserProfileActivity> members)
        {
            TeamName = teamName;
            Members = members;
        }

        public Team(string teamName, List<string> members, ProfileActivityParser parser)
        {
            TeamName = teamName;
            Members = new List<UserProfileActivity>();
            foreach ((string username, ActivityInfo activity) in parser.GetActivityInfo(members, true))
            {
                Members.Add(new UserProfileActivity(username, activity));
            }
        }

        public static List<Team> CreateFromUserList(List<UserWithTag> users, ProfileActivityParser parser)
        {
            Dictionary<string, ActivityInfo> mapToActivity = parser.GetActivityInfo(users.Select(u => u.Username).ToList(), true);

            List<UserWithTagAndActivity> userWithTagAndActivities = users
                .Where(u => mapToActivity.ContainsKey(u.Username))
                .Select(u => u.AddActivity(mapToActivity[u.Username]))
                .ToList();

            List<UserWithTag> withoutActivity = users
                .Where(u => !mapToActivity.ContainsKey(u.Username))
                .ToList();

            return userWithTagAndActivities
                .ToLookup(user => user.Tag, user => user)
                .Select(group => new Team(group.Key, group.Select(u => new UserProfileActivity(u.Username, u.Activity)).ToList()))
                .ToList();
        }

        private int TotalActivity()
        {
            return Members
                .Select(k => k.TotalContributions)
                .Sum();
        }

        public void AddMembers(ProfileActivityParser parser, bool isParallel, params string[] usernames)
        {
            var listInfo = parser.GetActivityInfo(usernames, isParallel);
            foreach (var item in listInfo)
            {
                Members.Add(new UserProfileActivity(item.Key, item.Value));
            }
        }

        public UserProfileActivity GetMinValueMember(DateTime? from = null, DateTime? to = null)
        {
            from ??= DateTime.MinValue;
            to ??= DateTime.Now;

            return Members
                .OrderBy(k => k.ActivityInfo.GetActivityForPeriod(from.GetValueOrDefault(), to.GetValueOrDefault()))
                .Last();

        }
        public UserProfileActivity GetMaxValueMember(DateTime? from = null, DateTime? to = null)
        {
            from ??= DateTime.MinValue;
            to ??= DateTime.Now;

            return Members
                .OrderBy(k => k.ActivityInfo.GetActivityForPeriod(from.GetValueOrDefault(), to.GetValueOrDefault()))
                .First();

        }
        public double GetAverageValue(DateTime? from = null, DateTime? to = null)
        {
            from ??= DateTime.MinValue;
            to ??= DateTime.Now;

            return Members
                .Select(k => k.ActivityInfo.GetActivityForPeriod(from.GetValueOrDefault(), to.GetValueOrDefault()))
                .Average();
        }

        public Dictionary<string, int> GetShortInfo()
        {
            Dictionary<string, int> usersContributions = new Dictionary<string, int>();

            foreach (var member in Members)
            {
                usersContributions.Add(member.Username, member.TotalContributions);
            }
            return usersContributions;
        }

        public int GetActivityForPeriod(DateTime from, DateTime to)
        {
            return Members
                .Select(member => member.ActivityInfo.GetActivityForPeriod(@from, to))
                .Sum();
        }

        public double GetAverageMonthActivity()
        {
            return Members
                .Select(member => Convert.ToInt32(member.ActivityInfo.PerMonthActivity()
                .Average(c => c.Count)))
                .ToList()
                .Average();
        }

        public double GetMovingAverage(DateTime from, DateTime to)
        {
            return Members
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
                var detailedStat = Members
                    .Select(member => new MemberMonthlyActivity(member.Username, member.GetActivityForPeriod(from, to)))
                    .ToList();
                var monthStat = new MonthlyStatistics(from, detailedStat);
                statistics.Add(monthStat);
                from = to;
            }
            return statistics;
        }
    }
}
