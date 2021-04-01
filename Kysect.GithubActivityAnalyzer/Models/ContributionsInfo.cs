namespace Kysect.GithubActivityAnalyzer.Models
{
    public class ContributionsInfo
    {
        public string Date { get; set; }
        public int Count { get; set; }

        public ContributionsInfo(string date, int count)
        {
            Date = date;
            Count = count;
        }
        public ContributionsInfo() { }
    }
}