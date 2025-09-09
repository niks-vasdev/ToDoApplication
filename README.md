# ToDo Application

A full-stack task management application built with React (TypeScript) frontend and ASP.NET Core GraphQL backend.

## 🚀 Features

### Frontend (React + TypeScript)
- ✅ **Modern React** with TypeScript and Vite
- ✅ **Adobe React Spectrum** UI components
- ✅ **GraphQL Integration** with Relay
- ✅ **Drag & Drop** task management
- ✅ **Real-time Updates** via GraphQL mutations
- ✅ **Responsive Design** with Tailwind CSS

### Backend (ASP.NET Core + GraphQL)
- ✅ **GraphQL API** with HotChocolate
- ✅ **SQLite Database** with Entity Framework Core
- ✅ **Clean Architecture** (Domain, Application, Infrastructure)
- ✅ **Input Validation** with FluentValidation
- ✅ **CORS Support** for frontend integration
- ✅ **Docker Support** for containerized deployment

## 🏗️ Project Structure

```
ToDoApplication/
├── task-assignment-frontend/          # React Frontend
│   └── task-assignment/
│       ├── src/
│       │   ├── components/            # React components
│       │   │   ├── AddTask.tsx
│       │   │   ├── CompletedTask.tsx
│       │   │   └── PendingTask.tsx
│       │   ├── pages/
│       │   │   └── Task.tsx           # Main task page
│       │   ├── relay/                 # GraphQL Relay setup
│       │   ├── interfaces/            # TypeScript interfaces
│       │   └── style/                 # CSS styles
│       ├── Dockerfile                 # Frontend Docker config
│       ├── docker-compose.yml         # Frontend compose
│       └── package.json
│
└── ToDo/                              # .NET Backend
    ├── ToDoApi/                       # Web API Layer
    │   ├── GraphQL/
    │   │   ├── Mutations/             # GraphQL mutations
    │   │   ├── Queries/               # GraphQL queries
    │   │   └── Types/                 # GraphQL types
    │   ├── Program.cs                 # Application startup
    │   └── Dockerfile                 # Backend Docker config
    │
    ├── Todo.Backend.Domain/           # Domain Layer
    │   ├── Entities/                  # Domain entities
    │   └── Enums/                     # Domain enums
    │
    ├── Todo.Backend.Application/      # Application Layer
    │   ├── Abstractions/              # Service interfaces
    │   ├── Dtos/                      # Data transfer objects
    │   └── Validation/                # Input validators
    │
    ├── Todo.Backend.Infrastructure/   # Infrastructure Layer
    │   ├── Data/                      # Database context
    │   ├── Repositories/              # Data repositories
    │   └── Services/                  # Service implementations
    │
    └── docker-compose.yml             # Backend compose
```

## 🛠️ Prerequisites

- [Node.js 18+](https://nodejs.org/)
- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Docker](https://www.docker.com/get-started) (for containerized deployment)

## 🚀 Quick Start

### Option 1: Run with Docker (Recommended)

#### Full Application with Docker Compose
```bash
# Clone the repository
git clone <repository-url>
cd ToDoApplication

# Run both frontend and backend
docker-compose up --build
```

**Access the application:**
- Frontend: http://localhost:3000
- Backend GraphQL: http://localhost:5262/graphql

#### Individual Services

**Backend Only:**
```bash
cd ToDo
docker-compose up --build
# Access: http://localhost:5262/graphql
```

**Frontend Only:**
```bash
cd task-assignment-frontend/task-assignment
docker-compose up --build
# Access: http://localhost:3000
```

### Option 2: Run Locally

#### Quick Start (Windows)
```bash
# Run both services with startup script
.\start-dev.bat
# or
.\start-dev.ps1
```

#### Manual Start

**Backend (.NET API):**
```bash
cd ToDo/ToDoApi
dotnet restore
dotnet run
# Access: http://localhost:5262/graphql
```

**Frontend (React):**
```bash
cd task-assignment-frontend/task-assignment
npm install
npm run dev
# Access: http://localhost:5173
```

## 🐳 Docker Configuration

### Root Docker Compose (Full Stack)
Create `docker-compose.yml` in the root directory:

```yaml
version: '3.8'

services:
  backend:
    build:
      context: ./ToDo
      dockerfile: ToDoApi/Dockerfile
    container_name: todo-backend
    ports:
      - "5262:80"
    environment:
      - ASPNETCORE_ENVIRONMENT=Development
      - ASPNETCORE_URLS=http://+:80
    networks:
      - todo-network

  frontend:
    build:
      context: ./task-assignment-frontend/task-assignment
      dockerfile: Dockerfile
    container_name: todo-frontend
    ports:
      - "3000:80"
    environment:
      - NODE_ENV=production
      - VITE_API_URL=http://localhost:5262
    depends_on:
      - backend
    networks:
      - todo-network

networks:
  todo-network:
    driver: bridge
```

### Individual Dockerfiles

#### Backend Dockerfile (`ToDo/ToDoApi/Dockerfile`)
```dockerfile
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["ToDoApi/TodoApi.csproj", "ToDoApi/"]
COPY ["Todo.Backend.Domain/Todo.Backend.Domain.csproj", "Todo.Backend.Domain/"]
COPY ["Todo.Backend.Application/Todo.Backend.Application.csproj", "Todo.Backend.Application/"]
COPY ["Todo.Backend.Infrastructure/Todo.Backend.Infrastructure.csproj", "Todo.Backend.Infrastructure/"]
RUN dotnet restore "ToDoApi/TodoApi.csproj"
COPY . .
WORKDIR "/src/ToDoApi"
RUN dotnet build "TodoApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TodoApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TodoApi.dll"]
```

#### Frontend Dockerfile (`task-assignment-frontend/task-assignment/Dockerfile`)
```dockerfile
# Build stage
FROM node:18-alpine AS build
WORKDIR /app
COPY package*.json ./
RUN npm ci
COPY . .
RUN npm run relay
RUN npm run build

# Production stage
FROM nginx:alpine
COPY --from=build /app/dist /usr/share/nginx/html
EXPOSE 80
CMD ["nginx", "-g", "daemon off;"]
```

## 📊 Database

### SQLite Configuration
- **File**: `todo.db` (auto-created in ToDoApi folder)
- **Connection String**: `Data Source=todo.db`
- **Auto-Migration**: Database is created automatically on startup

### Database Schema
```sql
CREATE TABLE Tasks (
    Id TEXT PRIMARY KEY,
    Title TEXT NOT NULL,
    Description TEXT,
    Status INTEGER NOT NULL,  -- 0=PENDING, 1=COMPLETED
    CreatedUtc TEXT NOT NULL,
    CompletedUtc TEXT
);
```

## 🔧 GraphQL API

### Available Operations

#### Create Task
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
  }
}
```

#### Get All Tasks
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

#### Update Task Status
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

## 🎨 Frontend Features

### Components
- **AddTask**: Form to create new tasks
- **PendingTask**: Display and manage pending tasks
- **CompletedTask**: Display completed tasks
- **Task**: Main page component

### Technologies
- **React 19** with TypeScript
- **Vite** for build tooling
- **Adobe React Spectrum** for UI components
- **Relay** for GraphQL client
- **Tailwind CSS** for styling

## ⚙️ Configuration

### Backend Configuration (`appsettings.json`)
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

### Frontend Configuration
Update the GraphQL endpoint in `src/relay/Environment.ts`:
```typescript
const environment = new Environment({
  network: Network.create(fetchQuery),
  store: new Store(new RecordSource()),
});

async function fetchQuery(operation: RequestParameters, variables: Variables) {
  const response = await fetch('http://localhost:5262/graphql', {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
    },
    body: JSON.stringify({
      query: operation.text,
      variables,
    }),
  });
  return response.json();
}
```

## 🧪 Development

### Backend Development
```bash
cd ToDo
dotnet build                    # Build solution
dotnet run --project ToDoApi    # Run API
dotnet test                     # Run tests (if any)
```

### Frontend Development
```bash
cd task-assignment-frontend/task-assignment
npm install                     # Install dependencies
npm run dev                     # Start dev server
npm run build                   # Build for production
npm run lint                    # Run linting
npm run relay                   # Generate Relay artifacts
```

### Generate GraphQL Schema
```bash
# From the backend directory
cd ToDo/ToDoApi
dotnet run
# Schema will be available at http://localhost:5262/graphql
```

## 🚨 Troubleshooting

### Common Issues

**CORS Errors**
- Ensure backend CORS is configured for frontend URL
- Check `Program.cs` CORS configuration

**GraphQL Connection Issues**
- Verify backend is running on correct port (5262)
- Check GraphQL endpoint URL in frontend

**Docker Build Issues**
```bash
# Clean Docker cache
docker system prune -a

# Rebuild without cache
docker-compose build --no-cache
```

**Port Conflicts**
- Backend: Change port in `Properties/launchSettings.json`
- Frontend: Change port in `vite.config.ts`

### Database Issues
```bash
# View database contents
sqlite3 ToDo/ToDoApi/todo.db
.tables
SELECT * FROM Tasks;
```

## 📦 Dependencies

### Backend
- HotChocolate.AspNetCore (15.1.10)
- Microsoft.EntityFrameworkCore.Sqlite (8.0.8)
- FluentValidation.AspNetCore (11.3.1)

### Frontend
- React (19.1.1)
- @adobe/react-spectrum (3.44.1)
- react-relay (20.1.1)
- vite (7.1.2)

## 🔒 Security Considerations

- CORS is configured for development (allow any origin)
- For production, configure specific origins
- Add authentication/authorization as needed
- Validate all inputs on both client and server

## 📈 Performance

- SQLite for lightweight database needs
- Relay for efficient GraphQL caching
- Vite for fast frontend builds
- Docker multi-stage builds for optimized images

## 🚀 Deployment

### Production Deployment
1. Update CORS settings for production domains
2. Configure production database connection
3. Set environment variables
4. Use Docker Compose for orchestration

### Environment Variables
```bash
# Backend
ASPNETCORE_ENVIRONMENT=Production
ASPNETCORE_URLS=http://+:80

# Frontend
NODE_ENV=production
VITE_API_URL=https://your-api-domain.com
```

## 📝 License

This project is for educational/demonstration purposes.

---

**Built with ❤️ using React, TypeScript, ASP.NET Core, and GraphQL**
