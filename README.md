# Brewery Control System

An automated brewing control system designed to run on Raspberry Pi, providing precise temperature control, mash step automation, and real-time monitoring through a web-based interface.

## Overview

The Brewery project is a full-stack IoT solution that combines hardware control with modern web technologies to automate the brewing process. It controls heating elements, mixers, valves, and monitors temperatures through 1-Wire DS18B20 sensors, all accessible through an intuitive Angular web application.

## Features

- **Temperature Control**: Precise temperature monitoring and control for boiling plates using PID controllers
- **Mash Step Automation**: Define and execute multi-step mashing processes with automatic progression
- **Real-Time Monitoring**: Web interface with live status updates every 5 seconds
- **Hardware Control**: Direct GPIO control of heating elements, mixers, and piezo valves
- **REST API**: Full-featured API for programmatic control and integration
- **Safety Mechanisms**: Built-in timeouts, alerts, and emergency shutoff capabilities
- **Docker Support**: Containerized deployment for easy setup and portability
- **Mock Mode**: Hardware abstraction layer allows development and testing without physical hardware

## Architecture

### Frontend
- **Framework**: Angular 7
- **Language**: TypeScript 3.1
- **UI Library**: Angular Material 7
- **Data Grid**: ag-grid
- **Reactive Programming**: RxJS 6
- **Testing**: Karma, Jasmine, Protractor

### Backend
- **Platform**: C# / .NET 8
- **API Framework**: ASP.NET Core
- **Dependency Injection**: Microsoft.Extensions.DependencyInjection
- **Hardware**: Raspberry Pi GPIO via System.Device.Gpio
- **Temperature Sensors**: DS18B20 via Iot.Device.Bindings
- **Port**: REST API on port 8800

### Hardware Support
- **GPIO Control**: System.Device.Gpio (v3.2.0) for heating elements, mixers, and valves
- **Temperature Sensors**: 1-Wire DS18B20 sensors via Iot.Device.Bindings (v3.2.0)
- **Platform**: Optimized for Raspberry Pi running Raspberry Pi OS (Linux)

## Project Structure

```
Brewery/
├── WebApp/                                      # Angular frontend application
│   ├── src/app/                                # Components, services, models
│   ├── e2e/                                    # End-to-end tests
│   └── package.json                            # Node dependencies
│
├── Server/                                      # C# backend application
│   ├── Brewery.Server/                         # Main console application (entry point)
│   ├── Brewery.Core/                           # Core models and interfaces
│   ├── Brewery.Server.Core/                    # Server-specific core models
│   ├── Brewery.Server.Logic/                   # Business logic and ASP.NET Core API
│   ├── Brewery.Server.Logic.RaspberryPi/       # Raspberry Pi hardware integration
│   ├── Brewery.Server.Logic.RaspberryPiMock/   # Mock hardware for testing
│   ├── Brewery.ServerMock/                     # Mock server for development
│   ├── Brewery.ServiceAdapter/                 # HTTP service layer
│   ├── Dockerfile                              # Docker containerization
│   ├── DEPLOYMENT.md                           # Deployment instructions
│   └── Brewery.Server.sln                      # Visual Studio solution
│
└── claude-ressources/                           # AI assistant configuration
    └── constitution.md                          # Project principles and standards
```

## Requirements

### Hardware (for production)
- Raspberry Pi (any model with GPIO support)
- DS18B20 temperature sensors with 1-Wire interface
- Relay modules for controlling heating elements
- Power supplies appropriate for your heating elements

### Software
- **.NET 8 SDK** or later
- **Node.js 14+** and npm (for frontend development)
- **Docker** (optional, for containerized deployment)
- **Raspberry Pi OS** (Bookworm or later) for production deployment

## Installation

### Backend Setup

```bash
# Clone the repository
git clone https://github.com/hidry/Brewery.git
cd Brewery

# Build the backend
cd Server
dotnet restore
dotnet build

# Run the server
cd Brewery.Server
dotnet run
```

The REST API will be available at `http://localhost:8800`

### Frontend Setup

```bash
cd WebApp
npm install

# Development server
ng serve

# Production build
ng build --prod
```

The web application will be available at `http://localhost:4200` during development.

### Docker Deployment

```bash
cd Server
docker build -t brewery-server:latest .

# Run with GPIO access (required on Raspberry Pi)
docker run -p 8800:8800 --privileged brewery-server:latest
```

For Docker Compose deployment, see `Server/docker-compose.yml`.

## Configuration

### Enable 1-Wire on Raspberry Pi

Add to `/boot/config.txt`:
```
dtoverlay=w1-gpio
```

Then reboot:
```bash
sudo reboot
```

### GPIO Permissions

Add your user to the gpio group:
```bash
sudo usermod -a -G gpio $USER
```

Or run with elevated privileges if needed.

## API Endpoints

The REST API provides the following main endpoints:

### Status
- `GET /api/status/serverStatus` - Get overall system status

### Boiling Plate 1 (Mash Control)
- `GET /api/boilingPlate1/powerStatus` - Get power status
- `GET /api/boilingPlate1/getCurrentTemperature` - Get current temperature
- `PUT /api/boilingPlate1/acknowledgeMessage` - Acknowledge alerts
- `PUT /api/boilingPlate1/startMashProcess` - Start automated mash process
- `PUT /api/boilingPlate1/stopMashProcess` - Stop mash process

### Boiling Plate 2 (Temperature Control)
- `GET /api/boilingPlate2/powerStatus` - Get power status
- `PUT /api/boilingPlate2/power/{on}` - Turn power on/off
- `GET /api/boilingPlate2/getCurrentTemperature` - Get current temperature
- `PUT /api/boilingPlate2/setTemperature/{temperature}` - Set target temperature
- `GET /api/boilingPlate2/getTemperature` - Get target temperature

### Mash Steps
- `GET /api/mashSteps` - Get all mash steps
- `POST /api/mashSteps` - Create new mash step
- `PUT /api/mashSteps` - Update mash step
- `DELETE /api/mashSteps/{guid}` - Delete mash step
- `GET /api/mashSteps/currentStep` - Get current active step
- `GET /api/mashSteps/totalEstimatedRemainingTime` - Get remaining time

### Hardware Control
- `PUT /api/mixer/power/{power}` - Control mixer on/off
- `PUT /api/piezo/power/{power}` - Control piezo valve on/off

## Development

### Frontend Development

```bash
cd WebApp
npm install
ng serve              # Development server on port 4200
npm test              # Run unit tests
npm run e2e           # Run end-to-end tests
ng build --prod       # Production build
```

### Backend Development

Open `Server/Brewery.Server.sln` in Visual Studio or any .NET-compatible IDE:

```bash
cd Server
dotnet restore
dotnet build
dotnet test           # Run tests (when available)
```

For development without Raspberry Pi hardware, use the mock server:
```bash
cd Server/Brewery.ServerMock
dotnet run
```

## Testing

### Frontend Tests
```bash
cd WebApp
npm test              # Unit tests with Karma
npm run e2e           # E2E tests with Protractor
```

### Backend Tests
```bash
cd Server
dotnet test
```

## Deployment

See [Server/DEPLOYMENT.md](Server/DEPLOYMENT.md) for detailed deployment instructions including:
- Raspberry Pi setup
- Systemd service configuration
- Docker deployment
- Production considerations

## Migration from UWP

This project was successfully migrated from Universal Windows Platform (UWP) to .NET 8. See [MIGRATION.md](MIGRATION.md) for details about:
- Changes in dependencies
- API compatibility
- Performance improvements
- Migration timeline

## Documentation

- [MIGRATION.md](MIGRATION.md) - UWP to .NET 8 migration details
- [GITHUB_ACTIONS.md](GITHUB_ACTIONS.md) - CI/CD pipeline information
- [Server/DEPLOYMENT.md](Server/DEPLOYMENT.md) - Deployment guide
- [claude-ressources/constitution.md](claude-ressources/constitution.md) - Development standards and guidelines
- [claude.md](claude.md) - AI assistant context and guidelines

## Safety Considerations

This system controls heating elements and other hardware. Always:
- Test thoroughly before use with real brewing equipment
- Implement appropriate hardware safety mechanisms (thermal fuses, etc.)
- Monitor the system during operation
- Ensure proper electrical wiring and safety
- Use at your own risk

## Contributing

When contributing to this project:
1. Review [claude-ressources/constitution.md](claude-ressources/constitution.md) for coding standards
2. Follow existing code organization and naming conventions
3. Consider safety implications for hardware control features
4. Add appropriate tests
5. Update documentation for API changes

## Technology Stack Summary

| Component | Technology | Version |
|-----------|-----------|---------|
| Backend Runtime | .NET | 8.0 |
| Backend Framework | ASP.NET Core | 8.0 |
| Frontend Framework | Angular | 7 |
| Language (Frontend) | TypeScript | 3.1 |
| Language (Backend) | C# | Latest |
| GPIO Library | System.Device.Gpio | 3.2.0 |
| Temp Sensors | Iot.Device.Bindings | 3.2.0 |
| DI Container | Microsoft.Extensions.DI | 9.0.0 |
| UI Framework | Angular Material | 7 |

## License

[Specify your license here]

## Acknowledgments

Built for automated brewing control on Raspberry Pi. Migrated from UWP to .NET 8 for better performance and cross-platform support.
