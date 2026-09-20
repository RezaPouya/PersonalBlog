using FluentValidation;
using PersonalBlog.Domain.Commons;
using PersonalBlog.Domain.Entities.Projects;
using PersonalBlog.Domain.Exceptions;

namespace AppServices.Admin.Projects.Delete;

public class DeleteProjectCommandHandler(
    IValidator<DeleteProjectCommand> validator,
    IProjectRepository projectRepository,
    IUnitOfWork unitOfWork) : ICommandHandler<DeleteProjectCommand, int>
{
    public async Task<int> Handle(DeleteProjectCommand input, CancellationToken cancellationToken)
    {
        var validationResult = validator.Validate(input);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var project = await projectRepository.FindByIdAsync(input.Id, cancellationToken);
        if (project is null)
            throw new BusinessException("پروژه با این شناسه یافت نشد.");

        projectRepository.Delete(project);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return project.Id;
    }
}
