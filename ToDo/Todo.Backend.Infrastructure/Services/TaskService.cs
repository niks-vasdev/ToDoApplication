using Todo.Backend.Application.Abstractions;
using Todo.Backend.Application.Dtos;
using Todo.Backend.Domain.Entities;
using Todo.Backend.Infrastructure.Repositories;

namespace Todo.Backend.Infrastructure.Services;

public class TaskService : ITaskService
{
    private readonly ITaskRepository _repo;

    public TaskService(ITaskRepository repo) => _repo = repo;

    public async Task<TaskDto> CreateAsync(CreateTaskInput input, CancellationToken ct = default)
    {
        if (input == null) throw new ArgumentNullException(nameof(input));
        var entity = new TaskItem(input.Title, input.Description);
        var created = await _repo.AddAsync(entity, ct);
        return Map(created);
    }

    public async Task<IReadOnlyList<TaskDto>> GetAllAsync(CancellationToken ct = default)
    {
        var items = await _repo.GetAllAsync(ct);
        return items.Select(Map).ToList();
    }

    public async Task<TaskDto?> UpdateStatusAsync(UpdateTaskStatusInput input, CancellationToken ct = default)
    {
        if (input == null) throw new ArgumentNullException(nameof(input));
        var existing = await _repo.GetByIdAsync(input.Id, ct);
        if (existing == null) return null;
        existing.UpdateStatus(input.Status);
        await _repo.UpdateAsync(existing, ct);
        return Map(existing);
    }

    private static TaskDto Map(TaskItem i) => new(i.Id, i.Title, i.Description, i.Status, i.CreatedUtc, i.CompletedUtc);
}
