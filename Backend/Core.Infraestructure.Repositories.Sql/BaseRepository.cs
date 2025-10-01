using Core.Application.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Core.Infraestructure.Repositories.Sql
{
    public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        public DbContext Context { get; private set; }
        public DbSet<TEntity> Repository { get; private set; }

        public BaseRepository(DbContext context)
        {
            Context = context;
            Repository = Context.Set<TEntity>();
        }

        public object Add(TEntity entity)
        {
            Repository.Add(entity);
            Context.SaveChanges();

            return (object)Context.Entry(entity).Property("Id").CurrentValue;
        }

        public async Task<object> AddAsync(TEntity entity)
        {
            await Repository.AddAsync(entity);
            await Context.SaveChangesAsync();

            return (object)Context.Entry(entity).Property("Id").CurrentValue;
        }

        public long Count(Expression<Func<TEntity, bool>> filter)
        {
            return Convert.ToInt64(Repository.Count(filter));
        }

        public async Task<long> CountAsync(Expression<Func<TEntity, bool>> filter)
        {
            return Convert.ToInt64(await Repository.CountAsync(filter));
        }

        public List<TEntity> FindAll()
        {
            return [.. Repository];
        }

        public async Task<List<TEntity>> FindAllAsync()
        {
            return await Repository.ToListAsync();
        }

        public TEntity FindOne(params object[] keyValues)
        {
            return Repository.Find(keyValues);
        }

        public async Task<TEntity> FindOneAsync(params object[] keyValues)
        {
            return await Repository.FindAsync(keyValues);
        }

        public virtual bool Remove(params object[] keyValues)
        {
            TEntity entity = FindOne(keyValues);

            if (entity == null)
                return false;

            Repository.Remove(entity);
            return Context.SaveChanges() > 0;
        }

        public virtual async Task<bool> RemoveAsync(Guid id, CancellationToken cancellationToken = default)
        {
            // Buscar directamente por el Guid
            TEntity entity = await Repository.FindAsync(id, cancellationToken);
            if (entity == null)
                return false;

            Repository.Remove(entity);
            return await Context.SaveChangesAsync(cancellationToken) > 0;
        }

        public void Update(object id, TEntity entity)
        {
            TEntity foundEntity = FindOne(id);

            if(foundEntity!=null)
            {
                Repository.Update(entity);
                Context.SaveChanges();
            }
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return Context.SaveChangesAsync(cancellationToken);
        }

         
         
    }
}
