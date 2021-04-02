using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kysect.GithubActivityAnalyzer.Group;

namespace Kysect.GithubActivityAnalyzer.DetailedStats
{
    public class MonthlyStatistics
    {
        public string Month { get; }
        public (Student, int) MinValueStudent { get; }
        public (Student, int) MaxValueStudent { get; }
        public double AverageValue { get; }
        public int TotalContributions { get; }
        public List<(Student, int)> DetailedStat { get; }

        public MonthlyStatistics(DateTime date, List<(Student, int)> detailedStat)
        {
            Month = date.Month.ToString() + "." + date.Year.ToString();//сделать красиво :)

                MinValueStudent = (DetailedStat
                .OrderBy(a => a.Item2)
                .First());

                 MaxValueStudent = (DetailedStat
                .OrderBy(a => a.Item2)
                .Last());

            AverageValue = DetailedStat.Average(a => a.Item2);

            TotalContributions = DetailedStat.Sum(a => a.Item2);
        }
    }
}
