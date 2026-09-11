using PersonalBlog.Domain.Commons.Base;

namespace PersonalBlog.Domain.Commons;

public interface IRepository<TEntity> where TEntity : EntityBase
{
    void Add(TEntity entity);
    void Update(TEntity entity);
    void SetUpdatedAt(TEntity entity);
    void Delete(TEntity entity);

    Task<TEntity?> FindByIdAsync(long id);
    Task<TEntity?> GetByIdAsync(long id);
    Task<bool> IsExistsByIdAsync(long id);

    IQueryable<TEntity> Query(bool asNoTracking = true);
}
