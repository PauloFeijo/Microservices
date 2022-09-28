using Microservice.Consumer.Infra.Data.SqlServer.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Threading.Tasks;

namespace Microservice.Consumer.Infra.Data.SqlServer.Repositories
{
    public abstract class BaseRepository<TEntity> where TEntity : class
    {
        private readonly IContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public BaseRepository(IContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        protected async ValueTask<EntityEntry<TEntity>> ExecuteAsync(Func<DbSet<TEntity>, ValueTask<EntityEntry<TEntity>>> func)
        {
            var result = await func(_dbSet);
            await _context.SaveChangesAsync();
            return result;
        }
    }
}
