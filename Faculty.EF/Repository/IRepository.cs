using Faculty.EFCore.Domain;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Faculty.EFCore.Repository
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        IQueryable<TEntity> Entities { get; }

        Task<TEntity> AddAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task DeleteAsync(Guid id);
        Task<TEntity> GetByIdAsync(Guid id);
    }
}
