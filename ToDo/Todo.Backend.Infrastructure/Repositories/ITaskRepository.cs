using Todo.Backend.Domain.Entities;

namespace Todo.Backend.Infrastructure.Repositories;

public interface ITaskRepository
{
    Task<TaskItem> AddAsync(TaskItem task, CancellationToken ct = default);
    Task<IReadOnlyList<TaskItem>> GetAllAsync(CancellationToken ct = default);
    Task<TaskItem?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task UpdateAsync(TaskItem task, CancellationToken ct = default);
}