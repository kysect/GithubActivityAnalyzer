using System;
using System.Collections.Generic;
using System.Globalization;
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
                .GroupBy(c => c.Date.Substring(0, 7))
                .Select(c => new ContributionsInfo(c.Key, c.Sum(_ => _.Count)))
                .ToList();
        }

        public int GetActivityForPeriod(DateTime from, DateTime to)
        {
            return Contributions
                .Select(c => (Date: DateTime.Parse(c.Date, CultureInfo.InvariantCulture), c.Count))
                .Where(c => c.Date >= from && c.Date <= to)
                .Sum(c => c.Count);
        }
    }
}