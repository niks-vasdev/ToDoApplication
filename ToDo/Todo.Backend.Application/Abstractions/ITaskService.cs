using Todo.Backend.Application.Dtos;

namespace Todo.Backend.Application.Abstractions;

public interface ITaskService
{
    Task<TaskDto> CreateAsync(CreateTaskInput input, CancellationToken ct = default);
    Task<IReadOnlyList<TaskDto>> GetAllAsync(CancellationToken ct = default);
    Task<TaskDto?> UpdateStatusAsync(UpdateTaskStatusInput input, CancellationToken ct = default);
}
