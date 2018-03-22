using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Faculty.EFCore.Domain;
using Microsoft.EntityFrameworkCore;

namespace Faculty.EFCore.Data
{
    public class EFRepository<TEntity> : IRepository<TEntity> 
        where TEntity : BaseEntity
    {
        private static readonly string ENTITY_NOT_FOUND = "Entity with id {0} not found.";

        protected DbSet<TEntity> DbSet => DbContext.Set<TEntity>();
        protected DbContext DbContext { get; }

        public IQueryable<TEntity> Entities => DbSet;

        public EFRepository(FacultyContext dbContext)
        {
            DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            if (entity.Id == Guid.Empty)
            {
                entity.Id = Guid.NewGuid();
            }
            TEntity addedEntity = DbSet.Add(entity).Entity;
            await DbContext.SaveChangesAsync();
            return addedEntity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            TEntity foundEntity;
            if (entity.Id != Guid.Empty && (foundEntity = DbSet.Find(entity.Id)) != null)
            {
                CopyProperties(foundEntity, entity);
                foundEntity = DbSet.Update(foundEntity).Entity;
            }
            else
            {
                throw new ArgumentException(string.Format(ENTITY_NOT_FOUND, entity.Id), nameof(entity));
            }
            await DbContext.SaveChangesAsync();
            return foundEntity;
        }

        public async Task DeleteAsync(object id)
        {
            TEntity entity = DbSet.Find(id);
            if (entity != null)
            {
                DbSet.Remove(entity);
                await DbContext.SaveChangesAsync();
            }
        }

        public async Task<TEntity> GetByIdAsync(object id) =>
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
