@echo off
setlocal

set SCRIPT_DIR=%~dp0

cd /d "%SCRIPT_DIR%"

echo Script is running from: %SCRIPT_DIR%

echo Loading Docker image...
docker load -i ftd-asia-api.tar

echo Running container...
docker run -d -p 8080:80 --name ftd-asia-api-container ftd-asia-api

echo Done. Your API should now be running at http://localhost:8080
pause