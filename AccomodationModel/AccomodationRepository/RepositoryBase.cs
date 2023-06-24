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

        public async Task CreateAsync(T entity)
        {
            await context.Set<T>().AddAsync(entity);
        }

        public async Task UpdateAsync(T entity)
        {
            context.Set<T>().Update(entity);
        }

        public async void Delete(T entity)
        {
            context.Set<T>().Remove(entity);
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> expression = null, bool tracked = true, params string[] includeProperties)
        {
            IQueryable<T> dbSet = context.Set<T>();
            if (!tracked)
                dbSet.AsNoTracking();
            SetExpressionAndIncludeProperties(dbSet, expression, includeProperties);

            return await context.Set<T>().ToListAsync();
        }

        public async Task<T> GetByConditionAsync(Expression<Func<T, bool>> expression = null, params string[] includeProperties)
        {
            IQueryable<T> dbSet = context.Set<T>();
            var result = SetExpressionAndIncludeProperties(dbSet, expression, includeProperties);
            return await result.FirstOrDefaultAsync();
        }

        private IQueryable<T> SetExpressionAndIncludeProperties(IQueryable<T> dbSet, Expression<Func<T, bool>> expression, string[] includeProperties)
        {
            IQueryable<T> result = dbSet;
            if (expression != null)
                result = dbSet.Where(expression);
            if (includeProperties != null)
                foreach (var includeProp in includeProperties)
                    result = dbSet.Include(includeProp);
            return result;
        }
    }
}
