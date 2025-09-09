using System;
using Todo.Backend.Domain.Enums;

namespace Todo.Backend.Domain.Entities;

public sealed class TaskItem
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Title { get; private set; }
    public string? Description { get; private set; }
    public Enums.TaskStatus Status { get; private set; } = Enums.TaskStatus.Pending;
    public DateTime CreatedUtc { get; private set; } = DateTime.UtcNow;
    public DateTime? CompletedUtc { get; private set; }

    // EF Core requires a parameterless ctor
    private TaskItem() => Title = string.Empty;

    public TaskItem(string title, string? description = null)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title is required", nameof(title));

        Title = title.Trim();
        Description = string.IsNullOrWhiteSpace(description) ? null : description.Trim();
        Status = Enums.TaskStatus.Pending;
        CreatedUtc = DateTime.UtcNow;
    }

    public void UpdateTitle(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException("Title is required", nameof(title));
        Title = title.Trim();
    }

    public void UpdateDescription(string? description)
    {
        Description = string.IsNullOrWhiteSpace(description) ? null : description.Trim();
    }

    public void UpdateStatus(Enums.TaskStatus status)
    {
        Status = status;
        CompletedUtc = status == Enums.TaskStatus.Completed ? DateTime.UtcNow : null;
    }
}
