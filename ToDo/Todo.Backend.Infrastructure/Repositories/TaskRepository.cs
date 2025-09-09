using Microsoft.EntityFrameworkCore;
using Todo.Backend.Domain.Entities;
using Todo.Backend.Infrastructure.Data;

namespace Todo.Backend.Infrastructure.Repositories;

public class TaskRepository : ITaskRepository
{
    private readonly AppDbContext _context;

    public TaskRepository(AppDbContext context) => _context = context;

    public async Task<TaskItem> AddAsync(TaskItem task, CancellationToken ct = default)
    {
        _context.Tasks.Add(task);
        await _context.SaveChangesAsync(ct);
        return task;
    }

    public async Task<IReadOnlyList<TaskItem>> GetAllAsync(CancellationToken ct = default)
    {
        return await _context.Tasks.OrderBy(t => t.CreatedUtc).ToListAsync(ct);
    }

    public async Task<TaskItem?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        return await _context.Tasks.FirstOrDefaultAsync(t => t.Id == id, ct);
    }

    public async Task UpdateAsync(TaskItem task, CancellationToken ct = default)
    {
        _context.Tasks.Update(task);
        await _context.SaveChangesAsync(ct);
    }
}