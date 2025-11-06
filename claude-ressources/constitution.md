# Brewery Project Constitution

This document outlines the core principles and standards for the Brewery control system project.

## 1. Code Quality Standards

### Frontend (Angular/TypeScript)
- **Type Safety**: Leverage TypeScript's strong typing. Avoid `any` types where possible.
- **Component Design**: Follow Angular best practices with single responsibility components.
- **Naming Conventions**: Use clear, descriptive names (e.g., `BoilingPlateComponent`, `MashStepService`).
- **Code Organization**: Keep related functionality together (services, models, components).
- **Dependency Injection**: Use Angular's DI system consistently for loose coupling.
- **RxJS Best Practices**: Properly manage subscriptions to prevent memory leaks (use `takeUntil`, `async` pipe).
- **Error Handling**: Implement proper error handling for HTTP requests and asynchronous operations.

### Backend (C#/UWP)
- **SOLID Principles**: Follow SOLID design principles, especially Single Responsibility and Dependency Inversion.
- **Interface-Based Design**: Continue the pattern of defining interfaces in `Brewery.Core` for contracts.
- **Naming Conventions**: Follow C# conventions (PascalCase for public members, camelCase for private).
- **Async/Await**: Use async/await patterns for I/O operations and long-running tasks.
- **Hardware Abstraction**: Maintain clear separation between hardware interfaces and implementation.
- **Resource Management**: Properly dispose of GPIO pins, file handles, and other resources.

### General
- **Documentation**: Write clear comments for complex logic, especially hardware interactions.
- **Git Practices**: Write meaningful commit messages describing the "why" not just the "what".
- **Code Reviews**: Review changes for safety implications in brewery control logic.

## 2. Testing Standards

### Frontend Testing
- **Unit Tests**: Write unit tests for services and components with business logic.
- **Test Coverage**: Focus on critical paths (mash step calculations, API communication, timer logic).
- **E2E Tests**: Maintain Protractor e2e tests for critical user workflows.
- **Mock Services**: Use mock services for testing without backend dependencies.
- **Test Data**: Use realistic brewing data in tests (temperatures, durations, etc.).

### Backend Testing
- **Mock Hardware**: Use `Brewery.Server.Logic.RaspberryPiMock` for testing without physical hardware.
- **API Testing**: Test REST endpoints with various inputs and edge cases.
- **Safety Testing**: Verify temperature limits, timing constraints, and failsafe mechanisms.
- **Integration Testing**: Test full workflows from API to hardware abstraction layer.

### Testing Priorities
1. **Safety-Critical**: Temperature control, power management, alerts
2. **Data Integrity**: Mash step CRUD operations, state persistence
3. **User Interface**: Real-time updates, control responsiveness
4. **Hardware Integration**: Sensor readings, GPIO control accuracy

## 3. User Experience Consistency

### Interface Design
- **Real-Time Feedback**: Maintain 5-second polling interval for timely status updates.
- **Visual Clarity**: Use clear status indicators (colors, icons) for boiling plate states.
- **Responsive Design**: Ensure the Angular Material UI works across different screen sizes.
- **Error Messages**: Provide clear, actionable error messages to users.
- **Loading States**: Show appropriate loading indicators during API calls.

### Control Flow
- **Predictable Behavior**: Controls should respond immediately with visual feedback.
- **Confirmation Dialogs**: Require confirmation for potentially dangerous operations.
- **State Consistency**: Ensure UI reflects actual hardware state accurately.
- **Alert System**: Maintain consistent alert behavior for completed mash steps.

### Accessibility
- **Clear Labels**: Use descriptive labels for all controls and inputs.
- **Keyboard Navigation**: Support keyboard navigation where applicable.
- **Color Independence**: Don't rely solely on color to convey information.

## 4. Performance Requirements

### Frontend Performance
- **Efficient Rendering**: Optimize change detection and minimize unnecessary re-renders.
- **API Call Optimization**: Use appropriate polling intervals, avoid excessive requests.
- **Bundle Size**: Keep the Angular bundle size reasonable for Raspberry Pi hosting.
- **Memory Management**: Prevent memory leaks from unsubscribed observables.
- **Grid Performance**: Optimize ag-grid configuration for mash step table rendering.

### Backend Performance
- **Response Times**: API endpoints should respond within 200ms for control operations.
- **Temperature Polling**: Maintain efficient sensor reading without excessive CPU usage.
- **Concurrent Requests**: Handle multiple simultaneous API requests gracefully.
- **Resource Usage**: Keep memory and CPU usage low for Raspberry Pi constraints.
- **Startup Time**: Minimize UWP application startup time.

### Hardware Constraints
- **Target Platform**: Raspberry Pi (ARM architecture, limited resources).
- **Network Latency**: Account for local network latency in timing calculations.
- **Sensor Accuracy**: Respect 1-Wire sensor polling limitations and accuracy.
- **GPIO Timing**: Ensure precise timing for hardware control signals.

## 5. Safety and Reliability

### Critical Considerations
- **Temperature Safety**: Implement safeguards for maximum temperature limits.
- **Power Control**: Ensure safe power on/off sequences for boiling plates.
- **Failsafe Mechanisms**: Implement timeout protection for heating operations.
- **State Persistence**: Save brewing state to handle unexpected shutdowns.
- **Error Recovery**: Gracefully handle sensor failures or communication errors.

### Operational Reliability
- **State Machine**: Use clear state transitions for brewing process control.
- **Validation**: Validate all user inputs for temperature and timing values.
- **Logging**: Implement comprehensive logging for debugging and audit trails.
- **Monitoring**: Track system health (sensor status, API availability, etc.).

## 6. Architecture Principles

### Separation of Concerns
- **Frontend**: Presentation logic only, delegate business logic to backend.
- **Backend Core**: Define interfaces and contracts.
- **Backend Logic**: Implement business rules and hardware control.
- **Service Adapter**: Handle HTTP communication and serialization.

### Extensibility
- **New Sensors**: Design to easily add new temperature sensors or GPIO controls.
- **New Features**: Structure code to accommodate future brewing features.
- **Configuration**: Use configuration files for environment-specific settings.
- **Plugin Architecture**: Consider plugin-like patterns for hardware modules.

### Maintainability
- **Consistent Patterns**: Follow established patterns in the codebase.
- **Minimal Dependencies**: Keep external dependencies minimal and well-justified.
- **Version Control**: Use meaningful branches and clear commit history.
- **Documentation**: Update documentation when making architectural changes.

## 7. Development Workflow

### Branch Strategy
- **Feature Branches**: Use descriptive branch names (e.g., `feature/mash-step-timer`).
- **Development Branch**: Merge completed features to `dev` branch for testing.
- **Main Branch**: Keep main branch stable with tested, production-ready code.

### Code Integration
- **Small Commits**: Make atomic commits that address single concerns.
- **Build Verification**: Ensure both frontend and backend build without errors.
- **Cross-Platform**: Test on Windows and Raspberry Pi when hardware-related.
- **Breaking Changes**: Document breaking changes in API or data models.

---

*This constitution is a living document and should be updated as the project evolves.*
