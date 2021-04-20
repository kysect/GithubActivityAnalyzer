namespace Kysect.GithubActivityAnalyzer.Aggregators.Models
{
    public class UserWithTag
    {
        public UserWithTag(string username, string tag)
        {
            Username = username;
            Tag = tag;
        }

        public string Username { get; set; }
        public string Tag { get; set; }
    }
}