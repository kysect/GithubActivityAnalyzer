using System.Linq;
using System.Text.Json;
using Kysect.GithubActivityAnalyzer.Models.ApiResponses;
using Kysect.GithubActivityAnalyzer.ResultCache.Entities;
using Microsoft.EntityFrameworkCore;

namespace Kysect.GithubActivityAnalyzer.ResultCache.Repositories
{
    public class UserСacheRepository
    {
        readonly DbContext _context;
        readonly DbSet<UserСache> _dbSet;

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

        public UserСache ConvertToUserCash(string username, ActivityInfo info)
        {
            var cash = JsonSerializer.Serialize(info);
            return new UserСache() { Username = username, ActivityInfo = cash };
        }

        public ActivityInfo GetActivityFromUserCash(UserСache userCash)
        {
            var activity = JsonSerializer.Deserialize<ActivityInfo>(userCash.ActivityInfo, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            return activity;
        }
    }
}
