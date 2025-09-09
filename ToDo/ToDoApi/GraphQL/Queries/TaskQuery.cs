using Todo.Backend.Application.Abstractions;
using Todo.Backend.Application.Dtos;
using HotChocolate;

namespace TaskManagerApi.GraphQL.Queries;

public class TaskQuery
{
    // GraphQL resolver: query getAllTasks
    [GraphQLName("getAllTasks")]
    public async Task<IEnumerable<TaskDto>> GetAllTasks([Service] ITaskService service, CancellationToken ct = default) =>
        await service.GetAllAsync(ct);
}
