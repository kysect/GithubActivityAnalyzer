using System;
using System.Globalization;

namespace Kysect.GithubActivityAnalyzer.Models
{
    public class ContributionsInfo
    {
        public string Date { get; set; }
        public DateTime date;
        public int Count { get; set; }

        public ContributionsInfo(string date, int count)
        {
            Date = date;
            Count = count;
            this.date = DateTime.Parse(date, CultureInfo.InvariantCulture);
        }

        public ContributionsInfo()
        {
        }
    }
}