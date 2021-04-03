using System;
using System.Globalization;

namespace Kysect.GithubActivityAnalyzer.Models
{
    public class ContributionsInfo
    { 
        public DateTime Date { get; set; }
        public int Count { get; set; }

        public ContributionsInfo(string dateAsString, int count)
        {
            Count = count;
            this.Date = DateTime.Parse(dateAsString, CultureInfo.InvariantCulture);
        }

        public ContributionsInfo()
        {
        }
    }
}