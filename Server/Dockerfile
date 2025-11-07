# Use build platform for SDK operations to avoid QEMU issues
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG TARGETARCH
WORKDIR /src

# Copy project files
COPY ["Brewery.Core/Brewery.Core.csproj", "Brewery.Core/"]
COPY ["Brewery.Server.Core/Brewery.Server.Core.csproj", "Brewery.Server.Core/"]
COPY ["Brewery.ServiceAdapter/Brewery.ServiceAdapter.csproj", "Brewery.ServiceAdapter/"]
COPY ["Brewery.Server.Logic.RaspberryPi/Brewery.Server.Logic.RaspberryPi.csproj", "Brewery.Server.Logic.RaspberryPi/"]
COPY ["Brewery.Server.Logic/Brewery.Server.Logic.csproj", "Brewery.Server.Logic/"]
COPY ["Brewery.Server/Brewery.Server.csproj", "Brewery.Server/"]

# Restore dependencies for target architecture
RUN dotnet restore "Brewery.Server/Brewery.Server.csproj" -a $TARGETARCH

# Copy source code
COPY . .

# Build for target architecture
WORKDIR "/src/Brewery.Server"
RUN dotnet build "Brewery.Server.csproj" -c Release -a $TARGETARCH -o /app/build --no-restore

# Publish
FROM build AS publish
ARG TARGETARCH
RUN dotnet publish "Brewery.Server.csproj" -c Release -a $TARGETARCH -o /app/publish --no-restore /p:UseAppHost=false

# Runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
EXPOSE 8800

# Copy published files
COPY --from=publish /app/publish .

# For Raspberry Pi GPIO access
RUN apt-get update && apt-get install -y --no-install-recommends \
    libgpiod2 \
    && rm -rf /var/lib/apt/lists/*

ENTRYPOINT ["dotnet", "Brewery.Server.dll"]
