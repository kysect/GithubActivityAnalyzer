using System;
using System.Collections.Generic;
using System.Linq;
using Kysect.GithubActivityAnalyzer.Group;

namespace Kysect.GithubActivityAnalyzer.DetailedStats
{
    public class MonthlyStatistics
    {
        public DateTime Month { get; }
        public List<(Student, int)> DetailedStat { get; }
        public (Student, int) MinValueStudent { get; }
        public (Student, int) MaxValueStudent { get; }
        public double AverageValue { get; }
        public int TotalContributions { get; }
       

        public MonthlyStatistics(DateTime date, List<(Student, int)> detailedStat)
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
    }
}
