using System.Linq.Expressions;

namespace AccomodationModel.AccomodationRepository
{
    public interface IRepositoryBase<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> expression = null, bool tracked = true, params string[] includeProperties);
        Task<T> GetByConditionAsync(Expression<Func<T, bool>> expression = null, params string[] includeProperties);
        Task CreateAsync(T entity);
        Task UpdateAsync(T entity);
        void Delete(T entity);
    }
}
