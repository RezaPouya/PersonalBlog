using PersonalBlog.Domain.Entities.Categories;
using PersonalBlog.Domain.Entities.Categories.Dtos;
using PersonalBlog.Utilities.Dtos;
using Utilities.Dtos;

namespace Infrastructure.Persistence.SqlServer.Repositories
{
    public class CategoryRepository(AppDbContext dbContext) : RepositoryBase<Category>(dbContext), ICategoryRepository
    {

        public async Task<List<IdTitleDto<int>>> GetListForLookupAsync(CancellationToken cancellationToken)
        {
            List<IdTitleDto<int>> results = await base.DbContext.Categories.AsNoTracking().
                Select(p => new IdTitleDto<int> { Id = p.Id, Title = p.Title })
                .OrderBy(p => p.Title)
                .ToListAsync(cancellationToken);

            return results;
        }

        public async Task<GridDataSourceResult<CategoryGridDto>> GetCategoryGridAsync(GridDataSourceRequest request,
            CancellationToken cancellationToken)
        {
            var query = base.DbContext.Categories.AsNoTracking()
                .Select(c => new CategoryGridDto
                {
                    Id = c.Id,
                    CreatedAt = c.CreatedAt,
                    UpdatedAt = c.UpdatedAt,
                    Title = c.Title,
                    IsInEnglish = c.IsInEnglish,
                    PostsCount = c.Posts.Count()
                });

            var result = await query.ToDataSourceResult(request, cancellationToken);

            return result;
        }

        public async Task<CategoryDot?> GetCategoryInfoByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await base.DbContext.Categories.AsNoTracking().Where(p => p.Id == id).Select(p =>
                new CategoryDot
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
