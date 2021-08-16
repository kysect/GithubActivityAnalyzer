using Microsoft.EntityFrameworkCore;
using Kysect.GithubActivityAnalyzer.Extensions.Data.Entities;

namespace Kysect.GithubActivityAnalyzer.Extensions.Data.Contexts
{
    public class TeamContext : DbContext
    {
        public DbSet<Member> Team { get; set; }
        public TeamContext(DbContextOptions<TeamContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
