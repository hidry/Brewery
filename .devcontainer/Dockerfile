# Start with the official .NET 9.0 devcontainer image as base
# This ensures all devcontainer tooling is properly configured
FROM mcr.microsoft.com/devcontainers/dotnet:9.0

# Install .NET 10 SDK (GA)
# Use curl instead of wget for better compatibility and add error handling
RUN curl -sSL https://dot.net/v1/dotnet-install.sh -o dotnet-install.sh \
    && chmod +x dotnet-install.sh \
    && ./dotnet-install.sh --channel 10.0 --install-dir /usr/share/dotnet \
    && rm dotnet-install.sh \
    || echo "Warning: .NET 10 installation failed, continuing with .NET 9"

# Verify installed .NET SDKs
RUN dotnet --list-sdks

# Set environment variables for .NET
ENV DOTNET_ROOT=/usr/share/dotnet
ENV PATH=$PATH:/usr/share/dotnet
