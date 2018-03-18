using System;
using System.Linq;
using System.Threading.Tasks;

namespace Faculty.Core.Data
{
    public interface IRepository<TEntity>
    {
        IQueryable<TEntity> Entities { get; }

        Task<TEntity> AddAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task DeleteAsync(object id);
        Task<TEntity> GetByIdAsync(object id);
    }
}
