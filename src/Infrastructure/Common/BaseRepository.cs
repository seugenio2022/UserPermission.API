using Microsoft.EntityFrameworkCore;
using UserPermission.API.Domain.Interfaces;
using UserPermission.API.Infrastructure.Persistence;

namespace UserPermission.API.Infrastructure.Common
{
    public abstract class BaseRepository<TEntity> : IRepositoryWrite<TEntity> where TEntity : class
    {
        private readonly UserPermissionDbContext context;
        protected BaseRepository(UserPermissionDbContext context)
        {
            this.context = context;
        }
        public async Task AddAsync(TEntity entity)
        {
            await context.Set<TEntity>().AddAsync(entity);
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await context.Set<TEntity>().FindAsync(id);
            context.Set<TEntity>().Remove(entity);
        }

        public async Task<TEntity> GetAsync(Guid id)
        {
            return await context.Set<TEntity>().FindAsync(id);
        }


        public void Update(TEntity entity)
        {
            context.Entry(entity).State = EntityState.Modified;
        }
    }
}
