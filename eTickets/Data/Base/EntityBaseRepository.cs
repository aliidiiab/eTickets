using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace eTickets.Data.Base
{
    public class EntityBaseRepository<T> : IEntityBaseRepository<T> where T : class, IEntityBase, new()
    {
        private readonly AppDbContext context;
        public EntityBaseRepository(AppDbContext _appDbContext)
        {
            context = _appDbContext;
        }
        public async Task AddAsync(T entity)
        {
            await context.Set<T>().AddAsync(entity);
            await context.SaveChangesAsync();
        }
        

        public async Task DeleteAsync(int id)
        {
            var newentity = await GetByIdAsync(id);
            EntityEntry entityEntry = context.Entry<T>(newentity);
            entityEntry.State = EntityState.Deleted;
            await context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()=> await context.Set<T>().ToListAsync();

        public async Task<IEnumerable<T>> GetAllAsync(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = context.Set<T>();
            query = includeProperties.Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
            return await query.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)=> await context.Set<T>().FirstOrDefaultAsync(i => i.Id == id);
        

        public async Task UpdateAsync(int id, T newentity)
        {
            EntityEntry entityEntry = context.Entry<T>(newentity);
            entityEntry.State=EntityState.Modified;
            await context.SaveChangesAsync();

        }
    }
}
