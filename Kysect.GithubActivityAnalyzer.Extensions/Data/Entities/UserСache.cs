using System.ComponentModel.DataAnnotations;

namespace Kysect.GithubActivityAnalyzer.Extensions.Data.Entities
{
    public class UserСache
    {
        [Key]
        public string Username { get; set; }
        public string ActivityInfo { get; set; }
    }
}
