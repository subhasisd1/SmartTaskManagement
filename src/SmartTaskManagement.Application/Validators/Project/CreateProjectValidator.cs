using FluentValidation;
using SmartTaskManagement.Application.DTOs.Project;

namespace SmartTaskManagement.Application.Validators.Project;

public class CreateProjectValidator : AbstractValidator<CreateProjectDto>
{
    public CreateProjectValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(100);

        RuleFor(x => x.Description)
            .NotEmpty()
            .MaximumLength(500);
    }
}