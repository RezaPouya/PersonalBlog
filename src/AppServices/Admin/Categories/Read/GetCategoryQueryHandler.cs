using AppServices.Base;
using PersonalBlog.Domain.Entities.Categories;
using PersonalBlog.Domain.Entities.Categories.Dtos;
using PersonalBlog.Domain.Exceptions;

namespace AppServices.Admin.Categories.Read;

public class GetCategoryQueryHandler(ICategoryRepository categoryRepository)
    : IQueryHandler<GetCategoryQuery, GetCategoryResult>
{
    public async Task<GetCategoryResult> Invoke(GetCategoryQuery request, CancellationToken cancellationToken)
    {
        CategoryDbDto? record = await categoryRepository.GetCategoryInfoByIdAsync(request.Id, cancellationToken)
            ?? throw new BusinessException("رکوردی با این شناسه یافت نشد");

        return new GetCategoryResult
        {
            Id = record.Id,
            Title = record.Title,
            Description = record.Description,
            Slug = record.Slug,
            TinyUrl = record.TinyUrl,
            IsInEnglish = record.IsInEnglish,
            PostsCount = record.PostsCount,
            CreatedAt = record.CreatedAt,
            UpdatedAt = record.UpdatedAt
        };
    }
}