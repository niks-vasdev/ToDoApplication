using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Todo.Backend.Infrastructure;
using Todo.Backend.Application.Validation;
using HotChocolate.AspNetCore;
using HotChocolate;
using TaskManagerApi.GraphQL.Queries;
using TaskManagerApi.GraphQL.Mutations;
using TaskManagerApi.GraphQL.Types;
using TaskManagerApi.GraphQL;
using FluentValidation;
using FluentValidation.AspNetCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// configuration: Use appsettings.json connection string or fall back to sqlite file
builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Add infrastructure (DbContext, repositories, services)
builder.Services.AddInfrastructure(builder.Configuration);

// Add FluentValidation
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<CreateTaskInputValidator>();
builder.Services.AddScoped<CreateTaskInputValidator>();
builder.Services.AddScoped<UpdateTaskStatusInputValidator>();

// Add GraphQL
builder.Services
    .AddGraphQLServer()
    .AddQueryType<TaskQuery>()
    .AddMutationType<TaskMutation>()
    .AddType<TaskType>()
    .AddFiltering()
    .AddSorting()
    .AddErrorFilter<GraphQLErrorFilter>();

// Add error filter
builder.Services.AddSingleton<GraphQLErrorFilter>();

// JSON options
builder.Services.Configure<Microsoft.AspNetCore.Http.Json.JsonOptions>(opts =>
{
    opts.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});

// Build app
var app = builder.Build();

// Use CORS
app.UseCors();

// Ensure DB exists and run pending migrations (if any)
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<Todo.Backend.Infrastructure.Data.AppDbContext>();
    db.Database.EnsureCreated();
}

// Map GraphQL endpoint with built-in UI
app.MapGraphQL("/graphql").WithOptions(new GraphQLServerOptions
{
    Tool = { Enable = app.Environment.IsDevelopment() }
});

app.Run();
