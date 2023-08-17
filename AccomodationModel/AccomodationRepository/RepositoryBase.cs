using AccomodationModel.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;


namespace AccomodationModel.AccomodationRepository
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected readonly AccomodationContext context;

        public RepositoryBase(AccomodationContext context)
        {
            this.context = context;
        }

        public virtual async Task CreateAsync(T entity)
        {
            await context.Set<T>().AddAsync(entity);
        }

        public virtual async Task UpdateAsync(T entity)
        {
            context.Set<T>().Update(entity);
        }

        public virtual async void Delete(T entity)
        {
            context.Set<T>().Remove(entity);
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> expression = null, bool tracked = true, params string[] includeProperties)
        {
            IQueryable<T> dbSet = context.Set<T>();
            if (!tracked)
                dbSet.AsNoTracking();
            dbSet = SetExpressionAndIncludeProperties(dbSet, expression, includeProperties);

            return await dbSet.ToListAsync();
        }

        public virtual async Task<T> GetByConditionAsync(Expression<Func<T, bool>> expression = null, params string[] includeProperties)
        {
            IQueryable<T> dbSet = context.Set<T>();
            var result = SetExpressionAndIncludeProperties(dbSet, expression, includeProperties);
            return await result.FirstOrDefaultAsync();
        }

        private IQueryable<T> SetExpressionAndIncludeProperties(IQueryable<T> dbSet, Expression<Func<T, bool>> expression, string[] includeProperties)
        {
            if (expression != null)
                dbSet = dbSet.Where(expression);
            if (includeProperties != null)
                foreach (var includeProp in includeProperties)
                    dbSet = dbSet.Include(includeProp);
            return dbSet;
        }
    }
}
