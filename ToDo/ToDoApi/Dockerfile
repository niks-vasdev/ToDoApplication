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