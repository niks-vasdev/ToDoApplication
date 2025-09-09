@echo off
echo Starting ToDo Application in Development Mode...
echo.

echo Starting Backend API...
start "Backend API" cmd /k "cd /d ToDo\ToDoApi && dotnet run"

echo Waiting for backend to start...
timeout /t 5 /nobreak > nul

echo Starting Frontend...
start "Frontend" cmd /k "cd /d task-assignment-frontend\task-assignment && npm run dev"

echo.
echo Both services are starting...
echo Backend will be available at: http://localhost:5262/graphql
echo Frontend will be available at: http://localhost:5173
echo.
echo Press any key to exit...
pause > nul