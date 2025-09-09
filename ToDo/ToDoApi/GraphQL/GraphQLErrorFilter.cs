using HotChocolate;
using FluentValidation;

namespace TaskManagerApi.GraphQL;

public class GraphQLErrorFilter : IErrorFilter
{
    public IError OnError(IError error)
    {
        if (error.Exception is ValidationException validationException)
        {
            return error.WithMessage($"Validation failed: {string.Join(", ", validationException.Errors.Select(e => e.ErrorMessage))}");
        }
        
        if (error.Exception is ArgumentException argumentException)
        {
            return error.WithMessage($"Invalid argument: {argumentException.Message}");
        }

        return error.Exception != null 
            ? error.WithMessage($"{error.Message}: {error.Exception.Message}")
            : error;
    }
}