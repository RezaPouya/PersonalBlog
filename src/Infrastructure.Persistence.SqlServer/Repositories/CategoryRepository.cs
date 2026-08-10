using Infrastructure.Persistence.SqlServer.DbExtensions;
using PersonalBlog.Domain.Entities.Categories;
using PersonalBlog.Domain.Entities.Categories.Dtos;
using PersonalBlog.Utilities.Dtos;

namespace Infrastructure.Persistence.SqlServer.Repositories
{
    public class CategoryRepository(AppDbContext dbContext) : RepositoryBase<Category>(dbContext), ICategoryRepository
    {
        public async Task<GridDataSourceResult<CategoryGridDbDto>> GetCategoryGridAsync(GridDataSourceRequest request, CancellationToken cancellationToken)
        {
            var query = base.DbContext.Categories.AsNoTracking()
                .Select(c => new CategoryGridDbDto
                {
                    Id = c.Id,
                    CreatedAt = c.CreatedAt,
                    UpdatedAt = c.UpdatedAt,
                    Title = c.Title,
                    IsInEnglish = c.IsInEnglish,
                    PostsCount = c.Posts.Count // اینجا فقط Count محاسبه می‌شود، بدون Include
                });

            var result = await query.ToDataSourceResult(request, cancellationToken);

            return result;
        }

        public async Task<CategoryDbDto?> GetCategoryInfoByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await base.DbContext.Categories.AsNoTracking().Where(p => p.Id == id).Select(p =>
                new CategoryDbDto
                {
                    Id = p.Id,
                    CreatedAt = p.CreatedAt,
                    Description = p.Description,
                    IsInEnglish = p.IsInEnglish,
                    PostsCount = p.Posts.Count(),
                    Slug = p.Slug,
                    TinyUrl = p.TinyUrl,
                    Title = p.Title,
                    UpdatedAt = p.UpdatedAt,
                }).FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<bool> IsExistsByTitleAsync(string title, CancellationToken cancellationToken)
        {
            return await base.DbContext.Categories.AsNoTracking().Where(p => p.Title == title).AnyAsync(cancellationToken);
        }
    }
}
