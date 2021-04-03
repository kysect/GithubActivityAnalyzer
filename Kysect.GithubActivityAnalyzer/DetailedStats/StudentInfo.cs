

namespace Kysect.GithubActivityAnalyzer.DetailedStats
{
    public class StudentInfo
    {
        public string Username { get; set; }
        public string NumberOfGroup { get;set; }

        public StudentInfo(string numberOfGroup, string username)
        { 
            NumberOfGroup = numberOfGroup;
            Username = username;
        }
    }
}
