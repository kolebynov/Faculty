using Faculty.EFCore.Data;
using Faculty.EFCore.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Faculty.EFCore.Repository
{
    public class EFRepository<TEntity> : IRepository<TEntity> 
        where TEntity : BaseEntity
    {
        protected DbSet<TEntity> DbSet => DbContext.Set<TEntity>();
        protected DbContext DbContext { get; }

        public IQueryable<TEntity> Entities => DbSet;

        public EFRepository(FacultyContext dbContext)
        {
            DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task DeleteAsync(Guid id)
        {
            TEntity entity = DbSet.Find(id);
            if (entity != null)
            {
                DbSet.Remove(entity);
                await DbContext.SaveChangesAsync();
            }
        }

        public async Task UpdateAsync(TEntity entity)
        {
            TEntity foundEntity;
            if (entity.Id != Guid.Empty && (foundEntity = DbSet.Find(entity.Id)) != null)
            {
                CopyProperties(foundEntity, entity);
                DbSet.Update(foundEntity);
            }
            else
            {
                if (entity.Id == Guid.Empty)
                {
                    entity.Id = Guid.NewGuid();
                }

                DbSet.Add(entity);
            }
            await DbContext.SaveChangesAsync();
        }

        public async Task<TEntity> GetByIdAsync(Guid id) =>
            await DbSet.FindAsync(id);

        private void CopyProperties(TEntity destEntity, TEntity srcEntity)
        {
            foreach (PropertyInfo property in typeof(TEntity).GetProperties())
            {
                property.SetValue(destEntity, property.GetValue(srcEntity));
            }
        }
    }
}
