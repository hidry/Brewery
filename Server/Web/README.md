# Shared Web Directory

This directory contains the compiled Angular frontend application that is served by both `Brewery.Server` and `Brewery.ServerMock`.

## Purpose

This is a **centralized distribution directory** that eliminates the need to manually copy frontend files to multiple locations. Both server projects reference this shared directory in their `.csproj` files and automatically include its contents in their build output.

## How It Works

### Build Process

1. The Angular app is built in `WebApp/dist/brewery/`
2. The deployment script (`WebApp/deploy.js`) copies the build output to this directory
3. Both server projects include files from this directory using MSBuild `Include` directives
4. When the servers are published, the Web files are automatically included

### Building the Frontend

From the `WebApp` directory:

```bash
# Build and deploy in one command (recommended)
npm run build:deploy

# Or separately:
npm run build    # Build to WebApp/dist/brewery
npm run deploy   # Copy to Server/Web
```

### Server Configuration

Both `Brewery.Server.csproj` and `Brewery.ServerMock.csproj` contain:

```xml
<ItemGroup>
  <None Include="..\Web\**\*" Link="Web\%(RecursiveDir)%(Filename)%(Extension)">
    <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
  </None>
</ItemGroup>
```

This configuration:
- **Includes** all files from the parent `Web` directory (`../Web/**/*`)
- **Links** them to appear as `Web/` in the project structure
- **Copies** them to the output directory during build/publish

## Migration Note

**Issue #71**: This simplified structure replaces the previous manual process where files needed to be copied to three separate directories:
- ~~`Server/Brewery.Server/Web/`~~ (removed)
- ~~`Server/Brewery.ServerMock/Web/`~~ (removed)
- `Server/Web/` (centralized location)

## Contents

When built, this directory contains:
- `index.html` - Main application entry point
- `*.js` - Compiled JavaScript bundles
- `*.css` - Compiled stylesheets
- `favicon.ico` - Application icon
- `3rdpartylicenses.txt` - Third-party license information

These files are served as static files by the ASP.NET Core server on port 8800.
