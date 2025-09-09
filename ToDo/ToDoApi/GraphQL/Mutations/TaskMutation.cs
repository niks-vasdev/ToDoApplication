using Todo.Backend.Application.Abstractions;
using Todo.Backend.Application.Dtos;
using Todo.Backend.Application.Validation;
using HotChocolate;
using FluentValidation;

namespace TaskManagerApi.GraphQL.Mutations;

public class TaskMutation
{
    // GraphQL mutation: createTask
    [GraphQLName("createTask")]
    public async Task<TaskDto> CreateTask(
        CreateTaskInput input, 
        [Service] ITaskService service,
        [Service] CreateTaskInputValidator validator,
        CancellationToken ct = default)
    {
        await validator.ValidateAndThrowAsync(input, ct);
        return await service.CreateAsync(input, ct);
    }

    // GraphQL mutation: updateTaskStatus
    [GraphQLName("updateTaskStatus")]
    public async Task<TaskDto?> UpdateTaskStatus(
        UpdateTaskStatusInput input, 
        [Service] ITaskService service,
        [Service] UpdateTaskStatusInputValidator validator,
        CancellationToken ct = default)
    {
        await validator.ValidateAndThrowAsync(input, ct);
        return await service.UpdateStatusAsync(input, ct);
    }
}
    