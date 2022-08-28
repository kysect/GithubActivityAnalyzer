using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Kysect.GithubActivityAnalyzer.Extensions.Data.Contexts;
using Kysect.GithubActivityAnalyzer.Extensions.Data.Entities;
using Kysect.GithubUtils;
using Microsoft.EntityFrameworkCore;

namespace Kysect.GithubActivityAnalyzer.Extensions.Data.Repositories
{
    public class UserCacheRepository : IRepository<UserСache>
    {
        readonly ActivityContext _context;
        readonly DbSet<UserСache> _dbSet;

        public UserCacheRepository(ActivityContext context)
        {
            _context = context;
            _dbSet = context.UserСache;
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
        public IQueryable<UserСache> Get()
        {
            return _dbSet;
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
        public List<(string Username, ActivityInfo Activity)> GetActivityFromUserCash(IEnumerable<string> usernames, bool isParallel)
        {
            if (!isParallel)
            {
                return usernames
                    .Select(username => (username, GetActivityFromUserCash(FindByUsername(username))))
                    .ToList();
            }

            List<(string, ActivityInfo)> result = usernames
                .AsParallel()
                .Select(username => (username, GetActivityFromUserCash(FindByUsername(username))))
                .ToList();
            return result;
        }
    }
}
