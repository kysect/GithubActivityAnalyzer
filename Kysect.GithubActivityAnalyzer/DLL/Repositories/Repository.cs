using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Kysect.GithubActivityAnalyzer.DLL.Entities;
using System.Data.Entity;
using System.Linq;

namespace Kysect.GithubActivityAnalyzer.DLL.Repositories
{
   public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        DbContext _context;
        DbSet<BaseEntity> _dbSet;

        public Repository(DbContext context, DbSet<BaseEntity> set)
        {
            _context = context;
            _dbSet = set;
        }

        public BaseEntity Create(BaseEntity item)
        {
            _dbSet.Add(item);
            _context.SaveChanges();
            return item;
        }
        public IQueryable<BaseEntity> Get() 
        {
            return _dbSet;
        }
        public void Update(BaseEntity item)
        {
            _context.Entry(item).State = EntityState.Modified;
            _context.SaveChanges();
        }
        public void Delete(BaseEntity item) 
        {
            _dbSet.Remove(item);
            _context.SaveChanges();
        }
    }
}
