using FluentValidation;
using PersonalBlog.Domain.Commons;
using PersonalBlog.Domain.Entities.Projects;
using PersonalBlog.Domain.Exceptions;

namespace AppServices.Admin.Projects.Update;

public class UpdateProjectCommandHandler(
    IValidator<UpdateProjectCommand> validator,
    IProjectRepository projectRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<UpdateProjectCommand, int>
{
    public async Task<int> Handle(UpdateProjectCommand input, CancellationToken cancellationToken)
    {
        var validationResult = validator.Validate(input);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        input.Sanitize();

        var project = await projectRepository.FindByIdAsync(input.Id, cancellationToken);
        if (project is null)
            throw new BusinessException("پروژه با این شناسه یافت نشد.");

        var isDuplicated = await projectRepository.IsExistsBySlugAsync(input.Slug, input.Id, cancellationToken);
        if (isDuplicated)
            throw new BusinessException("پروژه‌ای دیگر با همین اسلاگ وجود دارد.");

        project.Title = input.Title;
        project.Slug = input.Slug;
        project.Description = input.Description;
        project.ImageUrl = input.ImageUrl;
        project.LiveUrl = input.LiveUrl;
        project.RepoUrl = input.RepoUrl;
        project.TechnologiesCsv = input.TechnologiesCsv;
        project.OrderInProjects = input.OrderInProjects;
        project.IsFeatured = input.IsFeatured;
        project.IsInEnglish = input.IsInEnglish;

        await unitOfWork.SaveChangesAsync(cancellationToken);

        return project.Id;
    }
}
