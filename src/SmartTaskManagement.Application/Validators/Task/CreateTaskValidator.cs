using FluentValidation;

public class CreateTaskValidator : AbstractValidator<CreateTaskDto>
{
    public CreateTaskValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.ProjectId)
            .NotEmpty();

        RuleFor(x => x.EstimatedHours)
            .GreaterThan(0);

        RuleFor(x => x.DueDate)
            .GreaterThan(DateTime.UtcNow);
    }
}