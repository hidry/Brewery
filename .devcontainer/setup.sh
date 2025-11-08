#!/bin/bash

set -e

# Enable error reporting
trap 'echo "❌ Error occurred in setup at line $LINENO. Command: $BASH_COMMAND"' ERR

echo "🍺 Setting up Brewery development environment..."
echo "📝 Logging setup process for debugging..."

# Install Angular CLI globally
echo "📦 Installing Angular CLI..."
if npm install -g @angular/cli; then
  echo "✅ Angular CLI installed successfully"
else
  echo "❌ Failed to install Angular CLI"
  exit 1
fi

# Restore .NET dependencies
echo "🔧 Restoring .NET dependencies..."
if cd Server && dotnet restore && cd ..; then
  echo "✅ .NET dependencies restored"
else
  echo "❌ Failed to restore .NET dependencies"
  exit 1
fi

# Install frontend dependencies
echo "📦 Installing frontend dependencies..."
echo "ℹ️  Using --legacy-peer-deps due to angular-in-memory-web-api compatibility"
if cd WebApp && npm install --legacy-peer-deps && cd ..; then
  echo "✅ Frontend dependencies installed"
else
  echo "❌ Failed to install frontend dependencies"
  exit 1
fi

# Set permissions for any scripts
chmod +x Server/*.sh 2>/dev/null || true

# Setup custom .bashrc for development
echo "🔧 Configuring bash environment..."
if ! grep -q "# Brewery Development Environment" ~/.bashrc; then
  echo "" >> ~/.bashrc
  cat .devcontainer/.bashrc >> ~/.bashrc
  echo "✅ Bash environment configured"
else
  echo "✅ Bash environment already configured"
fi

echo ""
echo "✅ Development environment setup complete!"
echo ""
echo "🐛 Debug Mode Configuration:"
echo "  - .NET processes run with hot reload (dotnet watch)"
echo "  - Angular runs with source maps and verbose logging"
echo "  - All builds use Debug configuration"
echo ""
echo "Quick Start Commands:"
echo "  Backend:  backend      (or: cd Server/Brewery.ServerMock && dotnet watch run --configuration Debug -- 8800)"
echo "  Frontend: frontend     (or: cd WebApp && ng serve --host 0.0.0.0 --configuration development --source-map)"
echo ""
echo "Convenient aliases:"
echo "  backend, frontend, build-backend, build-frontend, test-backend, test-frontend"
echo ""
echo "🚀 Happy brewing!"
echo "💡 Tip: Restart your terminal or run 'source ~/.bashrc' to activate the aliases"
echo "💡 Tip: Use F5 in VS Code to start debugging with breakpoints"
echo "💡 Note: Backend runs on port 8800 to match Angular dev server expectations"
