using PersonalBlog.Domain.Commons;
using PersonalBlog.Domain.Commons.Base;

namespace Infrastructure.Persistance.SqlServer;

public class RepositoryBase<TEntity>(AppDbContext dbContext) : IRepository<TEntity>
    where TEntity : EntityBase
{
    protected readonly AppDbContext DbContext = dbContext;

    public void Create(TEntity entity) => DbContext.Set<TEntity>().Add(entity);

    public virtual void Update(TEntity entity) => DbContext.Set<TEntity>().Update(entity);

    public void SetUpdatedAt(TEntity entity)
    {
        DbContext.Entry(entity).Property(e => e.UpdatedAt).IsModified = true;
    }

    public void Delete(TEntity entity) => DbContext.Set<TEntity>().Remove(entity);

    public async Task<TEntity?> FindByIdAsync(int id) => await DbContext.Set<TEntity>().FindAsync(id);

    public async Task<TEntity?> GetByIdAsync(int id) =>
        await DbContext.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);

    public async Task<bool> IsExistsByIdAsync(int id) =>
        await DbContext.Set<TEntity>().AnyAsync(x => x.Id == id);

    public IQueryable<TEntity> Query(bool asNoTracking = true)
    {
        var set = DbContext.Set<TEntity>().AsQueryable();
        return asNoTracking ? set.AsNoTracking() : set;
    }
}
