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
            await context.Set<T>().AddAsync(entity);
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
            SetExpressionAndIncludeProperties(dbSet, expression, includeProperties);
            return await dbSet.FirstOrDefaultAsync();
        }

        private void SetExpressionAndIncludeProperties(IQueryable<T> dbSet, Expression<Func<T, bool>> expression, string[] includeProperties)
        {
            if (expression != null)
                dbSet.Where(expression);
            if (includeProperties != null)
                foreach (var includeProp in includeProperties)
                    dbSet.Include(includeProp);
        }
    }
}
