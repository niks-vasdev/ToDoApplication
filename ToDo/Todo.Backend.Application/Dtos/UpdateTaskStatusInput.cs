using Todo.Backend.Domain.Enums;

namespace Todo.Backend.Application.Dtos;

public sealed record UpdateTaskStatusInput(Guid Id, Todo.Backend.Domain.Enums.TaskStatus Status);
