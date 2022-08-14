using Kysect.GithubActivityAnalyzer.ProfileActivityParsing.Models;

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

        public UserWithTagAndActivity AddActivity(ActivityInfo activity)
        {
            return new UserWithTagAndActivity(Username, Tag, activity);
        }
    }

    public class UserWithTagAndActivity
    {
        public string Username { get; set; }
        public string Tag { get; set; }
        public ActivityInfo Activity { get; set; }

        public UserWithTagAndActivity(string username, string tag, ActivityInfo activity)
        {
            Username = username;
            Tag = tag;
            Activity = activity;
        }
    }
}