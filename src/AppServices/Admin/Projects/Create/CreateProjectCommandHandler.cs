using FluentValidation;
using PersonalBlog.Domain.Commons;
using PersonalBlog.Domain.Entities.Projects;
using PersonalBlog.Domain.Exceptions;

namespace AppServices.Admin.Projects.Create;

public class CreateProjectCommandHandler(
    IValidator<CreateProjectCommand> validator,
    IProjectRepository projectRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<CreateProjectCommand, int>
{
    public async Task<int> Handle(CreateProjectCommand input, CancellationToken cancellationToken)
    {
        var validationResult = validator.Validate(input);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        input.Sanitize();

        var isDuplicated = await projectRepository.IsExistsBySlugAsync(input.Slug, null, cancellationToken);
        if (isDuplicated)
            throw new BusinessException("پروژه‌ای با همین اسلاگ وجود دارد.");

        var project = new Project
        {
            Title = input.Title,
            Slug = input.Slug,
            Description = input.Description,
            ImageUrl = input.ImageUrl,
            LiveUrl = input.LiveUrl,
            RepoUrl = input.RepoUrl,
            TechnologiesCsv = input.TechnologiesCsv,
            OrderInProjects = input.OrderInProjects,
            IsFeatured = input.IsFeatured,
            IsInEnglish = input.IsInEnglish
        };

        projectRepository.Create(project);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return project.Id;
    }
}
