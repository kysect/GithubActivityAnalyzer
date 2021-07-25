using System.Linq;

namespace Kysect.GithubActivityAnalyzer.Extensions.Data.Repositories
{
    public interface IRepository<TEntity>
    {
        TEntity Create(TEntity item);
        IQueryable<TEntity> Get();
        IQueryable<TEntity> GetAll();
        void Update(TEntity item);
        void Delete(TEntity item);
    }
}
