using HotChocolate.Types;
using Todo.Backend.Application.Dtos;
using Todo.Backend.Domain.Enums;

namespace TaskManagerApi.GraphQL.Types;

public class TaskType : ObjectType<TaskDto>
{
    protected override void Configure(IObjectTypeDescriptor<TaskDto> descriptor)
    {
        descriptor.Field(t => t.Id).Type<NonNullType<UuidType>>();
        descriptor.Field(t => t.Title).Type<NonNullType<StringType>>();
        descriptor.Field(t => t.Description).Type<StringType>();
        descriptor.Field(t => t.Status).Type<NonNullType<EnumType<Todo.Backend.Domain.Enums.TaskStatus>>>();
        descriptor.Field(t => t.CreatedUtc).Type<NonNullType<DateTimeType>>();
        descriptor.Field(t => t.CompletedUtc).Type<DateTimeType>();
    }
}
