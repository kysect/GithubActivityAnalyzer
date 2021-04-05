using Kysect.GithubActivityAnalyzer.ResultCache.Entities;
using Microsoft.EntityFrameworkCore;

namespace Kysect.GithubActivityAnalyzer.ResultCache.Repositories
{
    public class ActivityContext : DbContext
    {
        public DbSet<UserСache> UserСache { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source=..\\myDB.db");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserСache>()
                .HasKey(b => b.Username)
                .HasName("PrimaryKey_Username");
        }

        public ActivityContext()
        {
            Database.EnsureCreated();
        }
    }
}
