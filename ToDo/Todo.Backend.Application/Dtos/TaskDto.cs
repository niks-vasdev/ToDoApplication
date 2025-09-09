using Todo.Backend.Domain.Enums;

namespace Todo.Backend.Application.Dtos;

public sealed record TaskDto(
    Guid Id,
    string Title,
    string? Description,
    Todo.Backend.Domain.Enums.TaskStatus Status,
    DateTime CreatedUtc,
    DateTime? CompletedUtc
);
