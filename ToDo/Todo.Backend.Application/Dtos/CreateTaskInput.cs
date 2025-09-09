namespace Todo.Backend.Application.Dtos;

public sealed record CreateTaskInput(string Title, string? Description);
