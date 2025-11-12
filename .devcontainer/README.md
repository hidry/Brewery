# Brewery Codespaces Configuration

This directory contains the configuration for GitHub Codespaces, allowing you to develop the Brewery project in a fully configured cloud environment.

## What's Included

The devcontainer provides:

- **.NET 10 Preview SDK** - For backend development (with .NET 9 also available)
- **Node.js 20** - For Angular frontend development
- **Angular CLI** - Pre-installed globally
- **Docker-in-Docker** - For building and testing containers
- **VS Code Extensions** - Pre-configured for C# and Angular development
- **Debug Mode Enabled** - All processes run in debug mode with hot reload and source maps

## Getting Started

### Opening in Codespaces

1. Navigate to the [Brewery repository](https://github.com/hidry/Brewery)
2. Click the green **Code** button
3. Select **Codespaces** tab
4. Click **Create codespace on [branch]**

The environment will automatically set up all dependencies using the `setup.sh` script.

## Development Workflow

### Debug Mode

**All processes in the devcontainer run in debug mode by default:**

- **.NET Backend**: Uses `dotnet watch run --configuration Debug` for hot reload
- **Angular Frontend**: Uses `--source-map` and `--verbose` for detailed debugging
- **Environment Variables**: `ASPNETCORE_ENVIRONMENT=Development`, `NODE_ENV=development`

### Backend (Mock Server - No Hardware Required)

```bash
# Run the mock server (recommended for development)
# Runs with debug configuration and hot reload enabled
backend

# Or manually:
cd Server/Brewery.ServerMock
dotnet watch run --configuration Debug -- 8800
```

The API will be available at `http://localhost:8800`

**Debug Features:**
- Hot reload on code changes
- Detailed logging
- Debug symbols included
- Ready for debugger attachment

### Frontend (Angular)

```bash
# Run the Angular dev server
# Runs with source maps and verbose logging
frontend

# Or manually:
cd WebApp
ng serve --host 0.0.0.0 --configuration development --source-map --verbose
```

The frontend will be available at `http://localhost:4200`

**Debug Features:**
- Source maps enabled for debugging TypeScript
- Verbose logging for troubleshooting
- Development configuration with detailed error messages

### Building

```bash
# Build backend
build-backend

# Build frontend
build-frontend
```

### Testing

```bash
# Test backend
test-backend

# Test frontend
test-frontend
```

## Debugging with VS Code

The devcontainer includes pre-configured launch configurations for debugging:

### Starting the Debugger

1. **Press F5** or click the Run and Debug icon in VS Code
2. Select one of the available configurations:
   - `ServerMock (.NET)` - Debug the mock backend server
   - `Server (.NET)` - Debug the main backend server
   - `WebApp (Chrome)` - Debug the Angular frontend
   - `Full Stack (ServerMock + WebApp)` - Debug both simultaneously
   - `Full Stack (Server + WebApp)` - Debug main server + frontend

### Attaching to Running Processes

If you started processes using the aliases, you can attach the debugger:

1. Start the process: `backend` or `frontend`
2. Press F5 and select `Attach to Server (.NET)` or `Attach to ServerMock (.NET)`
3. Select the running process from the list

### Setting Breakpoints

- Click in the gutter next to line numbers to set breakpoints
- Breakpoints work in both C# and TypeScript code
- Use conditional breakpoints for more control (right-click on breakpoint)

## Helpful Aliases

The environment includes several aliases for common tasks (all run in debug mode):

- `backend` - Start mock backend server with hot reload and debug symbols
- `backend-main` - Start main backend server with hot reload and debug symbols
- `frontend` - Start Angular dev server with source maps and verbose logging
- `build-backend` - Build .NET solution in debug configuration
- `build-frontend` - Build Angular app with source maps
- `test-backend` - Run .NET tests with verbose output
- `test-frontend` - Run Angular tests

## Docker Development

The devcontainer includes Docker-in-Docker support:

```bash
# Build the server image
cd Server
docker build -t brewery-server:latest .

# Run with docker-compose
docker-compose up
```

## Port Forwarding

The following ports are automatically forwarded:

- **8800** - Backend API
- **4200** - Angular Dev Server

GitHub Codespaces will notify you when services start on these ports.

## Limitations

Since Codespaces runs in the cloud without physical hardware access:

- **Use the mock server** (`Brewery.ServerMock`) for development
- GPIO operations will not work
- Temperature sensors will return mock data
- Physical relays cannot be controlled

For hardware testing, deploy to an actual Raspberry Pi (see `Server/DEPLOYMENT.md`).

## Troubleshooting

### Dependencies not installed

If dependencies are missing, run the setup script manually:

```bash
bash .devcontainer/setup.sh
```

### Peer Dependency Warnings

The project uses `angular-in-memory-web-api` which doesn't have a version compatible with Angular 19. The setup script uses `--legacy-peer-deps` to work around this. This is safe for development purposes.

If you need to manually install frontend dependencies:

```bash
cd WebApp
npm install --legacy-peer-deps
```

### Port conflicts

If ports 8800 or 4200 are already in use, you can change them:

```bash
# Backend with custom port
cd Server/Brewery.ServerMock
dotnet run -- 9000

# Frontend with custom port
cd WebApp
ng serve --port 4201
```

## Additional Resources

- [Main README](../README.md) - Project documentation
- [Deployment Guide](../Server/DEPLOYMENT.md) - Raspberry Pi deployment
- [GitHub Actions](../GITHUB_ACTIONS.md) - CI/CD documentation
