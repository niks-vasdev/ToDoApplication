using FluentValidation;
using Todo.Backend.Application.Dtos;

namespace Todo.Backend.Application.Validation;

public class CreateTaskInputValidator : AbstractValidator<CreateTaskInput>
{
    public CreateTaskInputValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty()
            .MaximumLength(200);

        RuleFor(x => x.Description)
            .MaximumLength(1000)
            .When(x => !string.IsNullOrEmpty(x.Description));
    }
}