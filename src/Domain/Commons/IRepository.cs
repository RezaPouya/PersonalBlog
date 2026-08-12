namespace PersonalBlog.Domain.Commons;

public interface IRepository<TEntity> where TEntity : EntityBase
{

    void Create(TEntity entity);
    void Update(TEntity entity);
    void SetUpdatedAt(TEntity entity);
    void Delete(TEntity entity);

    Task<TEntity?> FindByIdAsync(int id, CancellationToken cancellationToken);

    /// <summary>
    /// as not tracking 
    /// </summary>
    Task<TEntity?> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<bool> IsExistsByIdAsync(int id, CancellationToken cancellationToken);

}
