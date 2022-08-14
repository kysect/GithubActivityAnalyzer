using System;
using System.Collections.Generic;
using System.Linq;

namespace Kysect.GithubActivityAnalyzer.ProfileActivityParsing.Models
{
    public class ActivityInfo
    {
        //TODO: if we use this - we need to add filter for this value
        //public YearActivityInfo[] Years { get; set; }
        public ContributionsInfo[] Contributions { get; set; }

        public int Total => PerMonthActivity().Sum(a => a.Count);

        public List<ContributionsInfo> PerMonthActivity()
        { 
            return Contributions
                .GroupBy(c => c.Date.Month.ToString()+ "." + c.Date.Year.ToString())
                .Select(c => new ContributionsInfo(c.Key, c.Sum(_ => _.Count)))
                .ToList();
        }

        public ActivityInfo FilterValues(DateTime? from, DateTime? to)
        {
            IEnumerable<ContributionsInfo> result = Contributions;
            if (from is not null)
                result = result.Where(a => a.Date >= from);
            if (to is not null)
                result = result.Where(a => a.Date <= to);

            return new ActivityInfo
            {
                Contributions = result.ToArray()
            };
        }

        public int GetActivityForPeriod(DateTime from, DateTime to)
        {
            List<int> elements = (from element in Contributions where element.Date <= to && element.Date >= @from select element.Count).ToList();

            return elements.Sum();
        }
    }
}