using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kysect.GithubActivityAnalyzer.DLL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Kysect.GithubActivityAnalyzer.DLL.Repositories
{
    public class UserCashRepository 
    {
        DbContext _context;
        DbSet<UserCash> _dbSet;

        public UserCashRepository(DbContext context, DbSet<UserCash> set)
        {
            _context = context;
            _dbSet = set;
        }

        public IQueryable<UserCash> GetAll()
        {
            return _dbSet;
        }

        public UserCash GetByUsername(string username)
        {
            return _dbSet.Find(username);
        }

        public UserCash Create(UserCash item)
        {
            _dbSet.Add(item);
            _context.SaveChanges();
            return item;
        }
        public void Update(UserCash item)
        {
            DeleteByUsername(item.Username);
            Create(item);
        }
        public void Delete(UserCash item)
        {
            _dbSet.Remove(item);
            _context.SaveChanges();
        }
        public void DeleteByUsername(string username)
        {
            Delete(GetByUsername(username));
        }
    }
}
