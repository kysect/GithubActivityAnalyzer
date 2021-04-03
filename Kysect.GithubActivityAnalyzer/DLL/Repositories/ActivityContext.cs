using System;
using System.Collections.Generic;
using System.Text;
using Kysect.GithubActivityAnalyzer.DLL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Kysect.GithubActivityAnalyzer.DLL.Repositories
{
    public class ActivityContext : DbContext
    {
        public DbSet<UserCash> UserCash { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite("Data Source=D:\\LABS\\Projects\\ActivityParser\\myDB.db");
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserCash>()
                .HasKey(b => b.Username)
                .HasName("PrimaryKey_Username");
        }
    }
}
