# Brewery Development Environment

# Source default bashrc if it exists
if [ -f /etc/bash.bashrc ]; then
    . /etc/bash.bashrc
fi

# Helpful aliases for Brewery development
alias backend="cd /workspaces/Brewery/Server/Brewery.ServerMock && dotnet run"
alias backend-main="cd /workspaces/Brewery/Server/Brewery.Server && dotnet run"
alias frontend="cd /workspaces/Brewery/WebApp && ng serve --host 0.0.0.0"
alias build-backend="cd /workspaces/Brewery/Server && dotnet build"
alias build-frontend="cd /workspaces/Brewery/WebApp && ng build"
alias test-backend="cd /workspaces/Brewery/Server && dotnet test"
alias test-frontend="cd /workspaces/Brewery/WebApp && npm test"

# Set PS1 prompt with brewery emoji
export PS1="\[\e[32m\]🍺 \[\e[34m\]\w\[\e[0m\] $ "

# Welcome message
echo ""
echo "🍺 Welcome to Brewery Development Environment!"
echo ""
echo "Quick Commands:"
echo "  backend          - Run mock backend server"
echo "  backend-main     - Run main backend server (requires hardware/privileged mode)"
echo "  frontend         - Run Angular dev server"
echo "  build-backend    - Build .NET solution"
echo "  build-frontend   - Build Angular app"
echo "  test-backend     - Run .NET tests"
echo "  test-frontend    - Run Angular tests"
echo ""
