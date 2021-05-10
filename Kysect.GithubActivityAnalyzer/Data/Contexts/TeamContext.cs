using Microsoft.EntityFrameworkCore;
using Kysect.GithubActivityAnalyzer.Data.Entities;

namespace Kysect.GithubActivityAnalyzer.Data.Contexts
{
    public class TeamContext : DbContext
    {
        public DbSet<Member> Team { get; set; }
        public TeamContext(DbContextOptions<TeamContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Member>()
               .HasKey(b => b.Username)
               .HasName("PrimaryKey_Username");
        }
    }
}
