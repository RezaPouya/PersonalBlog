using PersonalBlog.Domain.Commons.Base;

namespace PersonalBlog.Domain.Commons;

public interface IRepository<TEntity> where TEntity : EntityBase
{
    void Add(TEntity entity);
    void Update(TEntity entity);
    void SetUpdatedAt(TEntity entity);
    void Delete(TEntity entity);

    Task<TEntity?> FindByIdAsync(int id);
    Task<TEntity?> GetByIdAsync(int id);
    Task<bool> IsExistsByIdAsync(int id);

    IQueryable<TEntity> Query(bool asNoTracking = true);
}
