using System;
using System.Collections.Generic;
using System.Linq;
using Kysect.GithubActivityAnalyzer.Services;

namespace Kysect.GithubActivityAnalyzer.Models.Aggregations
{
    public class MonthlyStatistics
    {
        public DateTime Month { get; set; }
        public List<(string, int)> DetailedStat { get; set; }
        public (string, int) MinValueStudent { get; set; }
        public (string, int) MaxValueStudent { get; set; }
        public double AverageValue { get; set; }
        public int TotalContributions { get; set; }


        public MonthlyStatistics(DateTime date, List<(string, int)> detailedStat)
        {
            DetailedStat = detailedStat;
            Month = date;

                MinValueStudent = DetailedStat
                .OrderBy(a => a.Item2)
                .First();

                 MaxValueStudent = DetailedStat
                .OrderBy(a => a.Item2)
                .Last();

            AverageValue = DetailedStat.Average(a => a.Item2);

            TotalContributions = DetailedStat.Sum(a => a.Item2);
        }

        public MonthlyStatistics()
        {
        }
    }
}
