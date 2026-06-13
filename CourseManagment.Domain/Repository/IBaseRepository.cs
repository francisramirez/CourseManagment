
using CourseManagment.Domain.Result;

namespace CourseManagment.Domain.Repository
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task<TEntity?> GetByIdAsync(int id);
        Task<IReadOnlyList<TEntity>> GetAllAsync();
        Task<OperationResult> AddAsync(TEntity entity);
        Task<OperationResult> AddRangeAsync(IEnumerable<TEntity> entities);
        Task<OperationResult> Update(TEntity entity);
        Task<OperationResult> Remove(TEntity entity);
    }
}
