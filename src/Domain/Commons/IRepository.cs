using PersonalBlog.Domain.Commons.Base;

namespace PersonalBlog.Domain.Commons;

public interface IRepository<TEntity> where TEntity : EntityBase
{
    void Create(TEntity entity);
    void Update(TEntity entity);
    void SetUpdatedAt(TEntity entity);
    void Delete(TEntity entity);

    Task<TEntity?> FindByIdAsync(int id);

    /// <summary>
    /// as not tracking 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<TEntity?> GetByIdAsync(int id);
    Task<bool> IsExistsByIdAsync(int id);

    IQueryable<TEntity> Query(bool asNoTracking = true);
}
