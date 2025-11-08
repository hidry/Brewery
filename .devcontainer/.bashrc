# Brewery Development Environment

# Source default bashrc if it exists
if [ -f /etc/bash.bashrc ]; then
    . /etc/bash.bashrc
fi

# Helpful aliases for Brewery development (with debug mode enabled)
alias backend="cd /workspaces/Brewery/Server/Brewery.ServerMock && dotnet watch run --configuration Debug -- 8800"
alias backend-main="cd /workspaces/Brewery/Server/Brewery.Server && dotnet watch run --configuration Debug"
alias frontend="cd /workspaces/Brewery/WebApp && ng serve --host 0.0.0.0 --configuration development --source-map --verbose"
alias build-backend="cd /workspaces/Brewery/Server && dotnet build --configuration Debug"
alias build-frontend="cd /workspaces/Brewery/WebApp && ng build --configuration development --source-map"
alias test-backend="cd /workspaces/Brewery/Server && dotnet test --configuration Debug --verbosity normal"
alias test-frontend="cd /workspaces/Brewery/WebApp && npm test"

# Set PS1 prompt with brewery emoji
export PS1="\[\e[32m\]🍺 \[\e[34m\]\w\[\e[0m\] $ "

# Welcome message
echo ""
echo "🍺 Welcome to Brewery Development Environment!"
echo "🐛 Debug mode enabled with hot reload and source maps"
echo ""
echo "Quick Commands:"
echo "  backend          - Run mock backend server (debug mode with hot reload)"
echo "  backend-main     - Run main backend server (debug mode, requires hardware/privileged mode)"
echo "  frontend         - Run Angular dev server (debug mode with source maps)"
echo "  build-backend    - Build .NET solution (debug configuration)"
echo "  build-frontend   - Build Angular app (development with source maps)"
echo "  test-backend     - Run .NET tests (debug mode, verbose)"
echo "  test-frontend    - Run Angular tests"
echo ""
echo "💡 Tip: Use VS Code's debugger (F5) to attach to running processes"
echo ""
