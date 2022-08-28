using System;
using System.Collections.Generic;
using System.Linq;

namespace Kysect.GithubActivityAnalyzer.ProfileActivityParsing.Models
{
    public class MonthlyStatistics
    {
        public DateTime Month { get; set; }
        public List<MemberMonthlyActivity> DetailedStat { get; set; }
        public MemberMonthlyActivity MinValueMember { get; set; }
        public MemberMonthlyActivity MaxValueMember { get; set; }
        public double AverageValue { get; set; }
        public int TotalContributions { get; set; }


        public MonthlyStatistics(DateTime date, List<MemberMonthlyActivity> detailedStat)
        {
            DetailedStat = detailedStat;
            Month = date;

            MinValueMember = DetailedStat
                .OrderBy(a => a.MonthlyContributions)
                .First();

            MaxValueMember = DetailedStat
                .OrderBy(a => a.MonthlyContributions)
                .Last();

            AverageValue = DetailedStat.Average(a => a.MonthlyContributions);

            TotalContributions = DetailedStat.Sum(a => a.MonthlyContributions);
        }

        public MonthlyStatistics()
        {
        }
    }

    public class MemberMonthlyActivity
    {
        public string Username { get; set; }

        public int MonthlyContributions { get; set; }

        public MemberMonthlyActivity()
        {
        }

        public MemberMonthlyActivity(string username, int monthlyContributions)
        {
            Username = username;
            MonthlyContributions = monthlyContributions;
        }
    }
}
