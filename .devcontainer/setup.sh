#!/bin/bash

set -e

echo "🍺 Setting up Brewery development environment..."

# Install Angular CLI globally
echo "📦 Installing Angular CLI..."
npm install -g @angular/cli

# Restore .NET dependencies
echo "🔧 Restoring .NET dependencies..."
cd Server
dotnet restore
cd ..

# Install frontend dependencies
echo "📦 Installing frontend dependencies..."
cd WebApp
npm install
cd ..

# Set permissions for any scripts
chmod +x Server/*.sh 2>/dev/null || true

echo "✅ Development environment setup complete!"
echo ""
echo "Quick Start Commands:"
echo "  Backend:  cd Server/Brewery.ServerMock && dotnet run"
echo "  Frontend: cd WebApp && ng serve"
echo ""
echo "🚀 Happy brewing!"
