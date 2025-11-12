# Start with the official .NET 9.0 devcontainer image as base
# This ensures all devcontainer tooling is properly configured
FROM mcr.microsoft.com/devcontainers/dotnet:9.0

# Install .NET 10 Preview SDK
RUN wget https://dot.net/v1/dotnet-install.sh -O dotnet-install.sh \
    && chmod +x dotnet-install.sh \
    && ./dotnet-install.sh --channel 10.0 --install-dir /usr/share/dotnet \
    && rm dotnet-install.sh

# Verify .NET 10 is installed
RUN dotnet --list-sdks

# Set environment variables for .NET
ENV DOTNET_ROOT=/usr/share/dotnet
ENV PATH=$PATH:/usr/share/dotnet
