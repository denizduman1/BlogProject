using BlogProject.Shared.Data.Abstract;
using BlogProject.Shared.Entities.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BlogProject.Shared.Data.Concrete.EntityFramework
{
    public class EfEntityRepositoryBase<T> : IEntityRepository<T>
        where T: class,IEntity,new()
    {
        private readonly DbContext _context;
        public EfEntityRepositoryBase(DbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().AnyAsync(predicate);
        }

        public async Task<int> CountAsync(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().CountAsync(predicate);
        }

        public async Task DeleteAsync(T entity)
        {
            await Task.Run(()=>_context.Set<T>().Remove(entity));
        }

        public async Task<IList<T>> GetAllAsync(Expression<Func<T, bool>> predicate = null, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> entites = _context.Set<T>();
            if (predicate != null)
            {
                entites.Where(predicate);
            }
            if (includeProperties.Any())
            {
                foreach (var includeProperty in includeProperties)
                {
                    entites.Include(includeProperty); // Linq -> include
                }
            }
            return await entites.ToListAsync();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> entity = _context.Set<T>();
            if (predicate != null)
            {
                entity.Where(predicate);
            }
            if (includeProperties.Any())
            {
                foreach (var includeProperty in includeProperties)
                {
                    entity.Include(includeProperty); // Linq -> include
                }   
            }
            return await entity.SingleOrDefaultAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            await Task.Run(()=>_context.Set<T>().Update(entity));
        }
    }
}
