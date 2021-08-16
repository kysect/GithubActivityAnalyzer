using Microsoft.EntityFrameworkCore;
using System.Linq;
using Kysect.GithubActivityAnalyzer.Extensions.Data.Entities;
using Kysect.GithubActivityAnalyzer.Extensions.Data.Contexts;

namespace Kysect.GithubActivityAnalyzer.Extensions.Data.Repositories
{
    public class TeamRepository : IRepository<Member>
    {
        readonly TeamContext _context;
        readonly DbSet<Member> _dbSet;

        public TeamRepository(TeamContext context)
        {
            _context = context;
            _dbSet = context.Team;
        }

        public Member Create(Member item)
        {
            _dbSet.Add(item);
            _context.SaveChanges();
            return item;
        }
        public IQueryable<Member> Get()
        {
            return _dbSet;
        }
        public void Update(Member item)
        {
            DeleteByUsername(item.Username);
            Create(item);
        }
        public void Delete(Member item)
        {
            _dbSet.Remove(item);
            _context.SaveChanges();
        }
        public void DeleteByUsername(string username)
        {
            Delete(FindByUsername(username));
        }
        public IQueryable<Member> GetAll()
        {
            return _dbSet;
        }
        public Member FindByUsername(string username)
        {
            return _dbSet.Find(username);
        }
        public IQueryable<Member> GetAllByTeam(string team)
        {
            return _dbSet.Where(p => p.Team == team);
        }
        public void DeleteByTeam(string team)
        {
            foreach (var member in GetAllByTeam(team))
            {
                Delete(member);
            }
        }
    }
}
