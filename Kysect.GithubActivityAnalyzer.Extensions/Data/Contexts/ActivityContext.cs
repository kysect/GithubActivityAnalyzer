using Kysect.GithubActivityAnalyzer.Extensions.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Kysect.GithubActivityAnalyzer.Extensions.Data.Contexts
{
    public class ActivityContext : DbContext
    {
        public ActivityContext(DbContextOptions<ActivityContext> options) : base(options) { }
        public DbSet<UserСache> UserСache { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
