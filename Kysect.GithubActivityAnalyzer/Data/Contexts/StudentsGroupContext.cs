using Microsoft.EntityFrameworkCore;
using Kysect.GithubActivityAnalyzer.Data.Entities;

namespace Kysect.GithubActivityAnalyzer.Data.Contexts
{
    public class StudentsGroupContext : DbContext
    {
        public DbSet<StudentsGroup> StudentsGroup { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source=..\\StudentsGroup.db");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudentsGroup>()
               .HasKey(b => b.Username)
               .HasName("PrimaryKey_Username");
        }

        public StudentsGroupContext()
        {
            Database.EnsureCreated();
        }
    }
}
