using System;
using System.Collections.Generic;
using System.Text;

namespace Kysect.GithubActivityAnalyzer.DetailedStats
{
    public class StudentInfo
    {
        public string Username { get; set; }
        public string Groupname { get;set; }

        public StudentInfo(string username, string groupname)
        {
            Username = username;
            Groupname = groupname;
        }
    }
}
