using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kysect.GithubActivityAnalyzer.DLL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Kysect.GithubActivityAnalyzer.DLL.Repositories
{
    public class UserСacheRepository
    {
        DbContext _context;
        DbSet<UserСache> _dbSet;

        public UserСacheRepository(DbContext context, DbSet<UserСache> set)
        {
            _context = context;
            _dbSet = set;
        }

        public IQueryable<UserСache> GetAll()
        {
            return _dbSet;
        }

        public UserСache FindByUsername(string username)
        {
            return _dbSet.Find(username);
        }

        public UserСache Create(UserСache item)
        {
            _dbSet.Add(item);
            _context.SaveChanges();
            return item;
        }
        public void Update(UserСache item)
        {
            DeleteByUsername(item.Username);
            Create(item);
        }
        public void Delete(UserСache item)
        {
            _dbSet.Remove(item);
            _context.SaveChanges();
        }
        public void DeleteByUsername(string username)
        {
            Delete(FindByUsername(username));
        }
    }
}
