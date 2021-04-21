using Microsoft.EntityFrameworkCore;
using System.Linq;
using Kysect.GithubActivityAnalyzer.Data.Entities;

namespace Kysect.GithubActivityAnalyzer.Data.Repositories
{
    public class StudentsGroupRepository : IRepository<StudentsGroup>
    {
        readonly DbContext _context;
        readonly DbSet<StudentsGroup> _dbSet;

        public StudentsGroupRepository(DbContext context, DbSet<StudentsGroup> set)
        {
            _context = context;
            _dbSet = set;
        }

        public StudentsGroup Create(StudentsGroup item)
        {
            _dbSet.Add(item);
            _context.SaveChanges();
            return item;
        }
        public IQueryable<StudentsGroup> Get()
        {
            return _dbSet;
        }
        public void Update(StudentsGroup item)
        {
            DeleteByUsername(item.Username);
            Create(item);
        }
        public void Delete(StudentsGroup item)
        {
            _dbSet.Remove(item);
            _context.SaveChanges();
        }
        public void DeleteByUsername(string username)
        {
            Delete(FindByUsername(username));
        }
        public IQueryable<StudentsGroup> GetAll()
        {
            return _dbSet;
        }
        public StudentsGroup FindByUsername(string username)
        {
            return _dbSet.Find(username);
        }
        public IQueryable<StudentsGroup> GetAllByGroup(string studyGroup)
        {
            return _dbSet.Where(p => p.StudyGroup == studyGroup); ;
        }

    }
}
