using FluentValidation;
using Todo.Backend.Application.Dtos;

namespace Todo.Backend.Application.Validation;

public class UpdateTaskStatusInputValidator : AbstractValidator<UpdateTaskStatusInput>
{
    public UpdateTaskStatusInputValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty();

        RuleFor(x => x.Status)
            .IsInEnum();
    }
}