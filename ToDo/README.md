# Todo API - GraphQL Backend

A simple TODO list backend API built with ASP.NET Core 8.0, GraphQL (HotChocolate), and SQLite following Clean Architecture principles.

## 🚀 Features

- ✅ **GraphQL API** with queries and mutations
- ✅ **SQLite Database** with Entity Framework Core
- ✅ **Clean Architecture** (Domain, Application, Infrastructure layers)
- ✅ **Input Validation** with FluentValidation
- ✅ **Error Handling** with custom GraphQL error filters
- ✅ **Docker Support** for containerized deployment
- ✅ **Built-in GraphQL Playground** for testing

## 🏗️ Project Structure

```
├── ToDoApi/                        # Web API Layer (Presentation)
│   ├── GraphQL/
│   │   ├── Mutations/              # GraphQL mutations
│   │   │   └── TaskMutation.cs
│   │   ├── Queries/                # GraphQL queries
│   │   │   └── TaskQuery.cs
│   │   ├── Types/                  # GraphQL type definitions
│   │   └── GraphQLErrorFilter.cs   # Error handling
│   ├── Program.cs                  # Application startup
│   ├── appsettings.json           # Configuration
│   └── TodoApi.csproj             # Project file
│
├── Todo.Backend.Domain/            # Domain Layer (Core Business Logic)
│   ├── Entities/
│   │   └── Entities.cs            # TaskItem entity
│   └── Enums/
│       └── TaskStatus.cs          # Task status enum
│
├── Todo.Backend.Application/       # Application Layer (Use Cases)
│   ├── Abstractions/
│   │   └── ITaskService.cs        # Service interface
│   ├── Dtos/                      # Data transfer objects
│   │   ├── CreateTaskInput.cs
│   │   ├── TaskDto.cs
│   │   └── UpdateTaskStatusInput.cs
│   └── Validation/                # Input validators
│       ├── CreateTaskInputValidator.cs
│       └── UpdateTaskStatusInputValidator.cs
│
└── Todo.Backend.Infrastructure/    # Infrastructure Layer (External Concerns)
    ├── Data/
    │   └── AppDbContext.cs        # Database context
    ├── Repositories/
    │   ├── ITaskRepository.cs     # Repository interface
    │   └── TaskRepository.cs      # Repository implementation
    ├── Services/
    │   └── TaskService.cs         # Service implementation
    └── DependencyInjection.cs    # DI configuration
```

## 🛠️ Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Docker](https://www.docker.com/get-started) (optional)

## 🚀 Quick Start

### 1. Clone the Repository
```bash
git clone <repository-url>
cd ToDo
```

### 2. Run the API
```bash
cd ToDoApi
dotnet run
```

### 3. Access GraphQL
- **API Endpoint**: `http://localhost:5262/graphql`
- **GraphQL Playground**: `http://localhost:5262/graphql` (Development only)

## 📊 Database

### SQLite Configuration
- **File**: `todo.db` (auto-created in ToDoApi folder)
- **Connection String**: `Data Source=todo.db`
- **Auto-Migration**: Database is created automatically on startup

### Switch to In-Memory Database
Update `appsettings.json`:
```json
{
  "UseInMemoryProvider": true
}
```

## 🔧 GraphQL Operations

### Create a New Task
```graphql
mutation {
  createTask(input: {
    title: "Learn GraphQL"
    description: "Study GraphQL with HotChocolate"
  }) {
    id
    title
    description
    status
    createdUtc
    completedUtc
  }
}
```

### Get All Tasks
```graphql
query {
  getAllTasks {
    id
    title
    description
    status
    createdUtc
    completedUtc
  }
}
```

### Update Task Status
```graphql
mutation {
  updateTaskStatus(input: {
    id: "your-task-id-here"
    status: COMPLETED
  }) {
    id
    title
    status
    completedUtc
  }
}
```

## 📋 API Schema

### Task Entity
```csharp
public sealed class TaskItem
{
    public Guid Id { get; private set; }
    public string Title { get; private set; }
    public string? Description { get; private set; }
    public TaskStatus Status { get; private set; }  // PENDING | COMPLETED
    public DateTime CreatedUtc { get; private set; }
    public DateTime? CompletedUtc { get; private set; }
}
```

### Input Types
```csharp
// Create Task Input
public sealed record CreateTaskInput(string Title, string? Description);

// Update Status Input
public sealed record UpdateTaskStatusInput(Guid Id, TaskStatus Status);
```

### Task Status Enum
```csharp
public enum TaskStatus
{
    Pending = 0,
    Completed = 1
}
```

## 🐳 Docker Deployment

### Build and Run with Docker
```bash
docker build -t todo-api .
docker run -p 5262:80 todo-api
```

Access the API at: `http://localhost:5262/graphql`

## ⚙️ Configuration

### appsettings.json
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=todo.db"
  },
  "UseInMemoryProvider": false
}
```

## 🧪 Development

### Build the Solution
```bash
dotnet build ToDo.sln
```

### Run the API
```bash
cd ToDoApi
dotnet run
```

### Clean Build Artifacts
```bash
dotnet clean
```

## 🛡️ Validation Rules

### Create Task Validation
- **Title**: Required, max 200 characters
- **Description**: Optional, max 1000 characters

### Update Status Validation
- **Id**: Required, valid GUID
- **Status**: Required, valid enum value (PENDING | COMPLETED)

## 🚨 Error Handling

The API includes comprehensive error handling:
- **Validation Errors**: Detailed field-level validation messages via FluentValidation
- **Argument Errors**: Clear error messages for invalid arguments
- **GraphQL Errors**: Structured error responses with proper error codes via GraphQLErrorFilter

## 📦 Dependencies

### Main Packages
- **HotChocolate.AspNetCore** (15.1.10) - GraphQL server
- **HotChocolate.Data** (15.1.10) - GraphQL data extensions
- **HotChocolate.Types** (15.1.10) - GraphQL type system
- **Microsoft.EntityFrameworkCore.Sqlite** (8.0.8) - Database provider
- **FluentValidation** (12.0.0) - Input validation
- **FluentValidation.AspNetCore** (11.3.1) - ASP.NET Core integration

## 🔍 Database Tools

### View Database Contents
1. **DB Browser for SQLite**: [Download here](https://sqlitebrowser.org/)
2. **SQLite CLI**: `sqlite3 todo.db`
3. **VS Code Extension**: SQLite Viewer

### Sample Database Queries
```sql
-- View all tasks
SELECT * FROM Tasks;

-- Count tasks by status
SELECT Status, COUNT(*) FROM Tasks GROUP BY Status;
```

## 🆘 Troubleshooting

### Common Issues

**Build Errors**
```bash
# Clean and rebuild
dotnet clean && dotnet build
```

**Port Already in Use**
- Change the port in `Properties/launchSettings.json`
- Or kill the process using the port

**Database Connection Issues**
- Ensure the SQLite file has proper permissions
- Check the connection string in `appsettings.json`

### Getting Help
- Check console output for detailed error messages
- Review the GraphQL schema in the playground for API documentation

## 🎯 Architecture Benefits

- **Clean Architecture**: Clear separation of concerns across layers
- **Domain-Driven Design**: Rich domain entities with business logic
- **CQRS Pattern**: Separate read/write operations via GraphQL queries/mutations
- **Dependency Injection**: Loose coupling and testability
- **Validation**: Input validation at the application boundary

## 📝 License

This project is for educational/demonstration purposes.

---

**Built with ❤️ using ASP.NET Core 8.0 and GraphQL**