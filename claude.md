# Claude AI Configuration for Brewery Project

This file provides guidance for Claude AI when working with the Brewery control system repository.

## Project Overview

The Brewery project is an automated brewing control system designed to run on Raspberry Pi. It consists of:

- **Frontend**: Angular 7 web application (TypeScript) providing the user interface
- **Backend**: C# UWP REST API server controlling brewing hardware
- **Hardware**: Raspberry Pi with GPIO controls for boiling plates, mixers, valves, and 1-Wire temperature sensors

## Repository Structure

```
Brewery/
├── WebApp/              # Angular frontend application
│   ├── src/app/         # Components, services, models
│   ├── e2e/             # End-to-end tests
│   └── package.json     # Node dependencies
│
├── Server/              # C# backend application
│   ├── Brewery.Server/                    # Main UWP app
│   ├── Brewery.Server.Core/               # Core models and interfaces
│   ├── Brewery.Server.Logic/              # Business logic and REST API
│   ├── Brewery.Server.Logic.RaspberryPi/  # Hardware integration
│   └── Brewery.ServiceAdapter/            # HTTP service layer
│
└── claude-ressources/   # Claude AI configuration and guidelines
    └── constitution.md  # Project principles and standards
```

## Resources and Guidelines

**Primary Reference**: See [`claude-ressources/constitution.md`](./claude-ressources/constitution.md) for comprehensive guidelines on:

1. **Code Quality Standards** - Frontend and backend coding practices
2. **Testing Standards** - Unit, integration, and E2E testing requirements
3. **User Experience Consistency** - UI/UX design principles
4. **Performance Requirements** - Optimization for Raspberry Pi platform
5. **Safety and Reliability** - Critical considerations for brewery hardware control
6. **Architecture Principles** - Separation of concerns and extensibility
7. **Development Workflow** - Git practices and integration guidelines

## Key Technologies

### Frontend Stack
- **Framework**: Angular 7
- **Language**: TypeScript 3.1
- **UI Library**: Angular Material 7
- **Data Grid**: ag-grid
- **Reactive**: RxJS 6
- **Testing**: Karma, Jasmine, Protractor

### Backend Stack
- **Platform**: C# / .NET (UWP)
- **API Framework**: Restup.Webserver (REST API on port 8800)
- **DI Container**: MVVM Light SimpleIoC
- **Hardware**: Raspberry Pi GPIO, 1-Wire sensors

## Working with This Repository

### When Adding Features
1. Review the constitution for architectural patterns
2. Follow existing code organization and naming conventions
3. Consider safety implications for hardware control features
4. Add appropriate tests (unit tests for frontend, mock hardware for backend)
5. Update documentation if adding new APIs or major features

### When Fixing Bugs
1. Identify if the issue is frontend, backend, or hardware-related
2. Check for similar patterns in the codebase
3. Consider safety and state consistency implications
4. Add regression tests to prevent recurrence

### When Reviewing Code
1. Verify adherence to code quality standards in constitution.md
2. Check for proper error handling and resource management
3. Ensure type safety (TypeScript) and null safety (C#)
4. Validate that changes don't compromise safety mechanisms

## Important Considerations

### Safety-Critical Areas
- Temperature control logic (`/Server/Brewery.Server.Logic/Api/Controller/BoilingPlate*Controller.cs`)
- Power management for heating elements
- Mash step state machines
- Alert and timeout mechanisms

### Real-Time Requirements
- Frontend polls backend every 5 seconds for status updates
- Temperature readings should be current and accurate
- UI must reflect actual hardware state reliably

### Hardware Abstraction
- All hardware access goes through interfaces defined in `Brewery.Core`
- Use `Brewery.Server.Logic.RaspberryPiMock` for testing without physical hardware
- Respect GPIO timing and 1-Wire sensor polling constraints

## Development Environment

### Frontend Development
```bash
cd WebApp
npm install
ng serve              # Development server
npm test              # Run unit tests
npm run e2e           # Run e2e tests
ng build --prod       # Production build
```

### Backend Development
- Open `Server/Brewery.Server.sln` in Visual Studio
- Set `Brewery.Server` or `Brewery.ServerMock` as startup project
- Build target: UWP for ARM (Raspberry Pi) or x64/x86 (development)

## API Endpoint Structure

The REST API runs on port 8800 with the following main endpoints:

- `/api/boilingPlate1/*` - Control and status for boiling plate 1
- `/api/boilingPlate2/*` - Control and status for boiling plate 2
- `/api/mixer/*` - Mixer/stirrer control
- `/api/piezo/*` - Piezo valve control
- `/api/mashSteps/*` - Mash step CRUD operations
- `/api/status/*` - General system status

## Getting Help

For detailed guidance on:
- **Code quality expectations** → See constitution.md section 1
- **Testing requirements** → See constitution.md section 2
- **UI/UX guidelines** → See constitution.md section 3
- **Performance targets** → See constitution.md section 4
- **Safety protocols** → See constitution.md section 5

---

**Note**: This configuration helps Claude AI understand the project context and follow established patterns when assisting with development tasks.
