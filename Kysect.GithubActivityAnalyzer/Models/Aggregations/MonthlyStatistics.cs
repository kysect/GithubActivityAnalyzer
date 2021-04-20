using System;
using System.Collections.Generic;
using System.Linq;
using Kysect.GithubActivityAnalyzer.Services;

namespace Kysect.GithubActivityAnalyzer.Models.Aggregations
{
    public class MonthlyStatistics
    {
        public DateTime Month { get; set; }
        public List<StudentMonthlyActivity> DetailedStat { get; set; }
        public StudentMonthlyActivity MinValueStudent { get; set; }
        public StudentMonthlyActivity MaxValueStudent { get; set; }
        public double AverageValue { get; set; }
        public int TotalContributions { get; set; }


        public MonthlyStatistics(DateTime date, List<StudentMonthlyActivity> detailedStat)
        {
            DetailedStat = detailedStat;
            Month = date;

                MinValueStudent = DetailedStat
                .OrderBy(a => a.MonthlyContributions)
                .First();

                 MaxValueStudent = DetailedStat
                .OrderBy(a => a.MonthlyContributions)
                .Last();

            AverageValue = DetailedStat.Average(a => a.MonthlyContributions);

            TotalContributions = DetailedStat.Sum(a => a.MonthlyContributions);
        }

        public MonthlyStatistics()
        {
        }
    }

    public class StudentMonthlyActivity
    {
        public string Username { get; set; }

        public int MonthlyContributions { get; set; }

        public StudentMonthlyActivity()
        {
        }

        public StudentMonthlyActivity(string username, int monthlyContributions)
        {
            Username = username;
            MonthlyContributions = monthlyContributions;
        }
    }
}
