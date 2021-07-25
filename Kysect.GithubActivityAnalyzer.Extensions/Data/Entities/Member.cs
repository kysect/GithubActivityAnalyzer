using System.ComponentModel.DataAnnotations;

namespace Kysect.GithubActivityAnalyzer.Extensions.Data.Entities
{
    public class Member
    {
        [Key]
        public string Username { get; set; }
        public string Team { get; set; }
    }
}
