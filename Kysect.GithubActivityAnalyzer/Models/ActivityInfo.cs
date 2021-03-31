using System;
using System.Collections.Generic;
using System.Linq;

namespace Kysect.GithubActivityAnalyzer.Models
{
    public class ActivityInfo
    {
        public YearActivityInfo[] Years { get; set; }
        public ContributionsInfo[] Contributions { get; set; }

        public int Total => PerMonthActivity().Sum(a => a.Count);

        public List<ContributionsInfo> PerMonthActivity()
        { 
            return Contributions
                .GroupBy(c => c.Date.Month.ToString()+ "." + c.Date.Year.ToString())
                .Select(c => new ContributionsInfo(c.Key, c.Sum(_ => _.Count)))
                .ToList();
        }

        public int GetActivityForPeriod(DateTime from, DateTime to)
        {
            List<int> elements = (from element in Contributions where element.Date <= to && element.Date >= @from select element.Count).ToList();

            return elements.Sum();
        }
    }
}