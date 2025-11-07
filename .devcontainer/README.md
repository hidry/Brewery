# Brewery Codespaces Configuration

This directory contains the configuration for GitHub Codespaces, allowing you to develop the Brewery project in a fully configured cloud environment.

## What's Included

The devcontainer provides:

- **.NET 8 SDK** - For backend development
- **Node.js 20** - For Angular frontend development
- **Angular CLI** - Pre-installed globally
- **Docker-in-Docker** - For building and testing containers
- **VS Code Extensions** - Pre-configured for C# and Angular development

## Getting Started

### Opening in Codespaces

1. Navigate to the [Brewery repository](https://github.com/hidry/Brewery)
2. Click the green **Code** button
3. Select **Codespaces** tab
4. Click **Create codespace on [branch]**

The environment will automatically set up all dependencies using the `setup.sh` script.

## Development Workflow

### Backend (Mock Server - No Hardware Required)

```bash
# Run the mock server (recommended for development)
backend

# Or manually:
cd Server/Brewery.ServerMock
dotnet run
```

The API will be available at `http://localhost:8800`

### Frontend (Angular)

```bash
# Run the Angular dev server
frontend

# Or manually:
cd WebApp
ng serve --host 0.0.0.0
```

The frontend will be available at `http://localhost:4200`

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

## Helpful Aliases

The environment includes several aliases for common tasks:

- `backend` - Start mock backend server
- `backend-main` - Start main backend server
- `frontend` - Start Angular dev server
- `build-backend` - Build .NET solution
- `build-frontend` - Build Angular app
- `test-backend` - Run .NET tests
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

### Port conflicts

If ports 8800 or 4200 are already in use, you can change them:

```bash
# Backend with custom port
cd Server/Brewery.ServerMock
dotnet run --urls "http://localhost:8801"

# Frontend with custom port
cd WebApp
ng serve --port 4201
```

## Additional Resources

- [Main README](../README.md) - Project documentation
- [Deployment Guide](../Server/DEPLOYMENT.md) - Raspberry Pi deployment
- [GitHub Actions](../GITHUB_ACTIONS.md) - CI/CD documentation
