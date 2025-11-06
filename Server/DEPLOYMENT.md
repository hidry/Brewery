# Deployment Guide for Raspberry Pi

## Prerequisites

1. **Raspberry Pi** (3, 4, or 5) with Raspberry Pi OS (64-bit recommended)
2. **.NET 10 Runtime** installed
3. **1-Wire and GPIO** enabled

## Installation Steps

### 1. Install .NET 10 Runtime

```bash
# Download and install .NET 10 SDK
curl -sSL https://dot.net/v1/dotnet-install.sh | bash /dev/stdin --channel 10.0

# Add to PATH
echo 'export DOTNET_ROOT=$HOME/.dotnet' >> ~/.bashrc
echo 'export PATH=$PATH:$HOME/.dotnet' >> ~/.bashrc
source ~/.bashrc

# Verify installation
dotnet --version
```

### 2. Enable 1-Wire and GPIO

Edit `/boot/config.txt`:
```bash
sudo nano /boot/config.txt
```

Add these lines:
```
# Enable 1-Wire for DS18B20 temperature sensors
dtoverlay=w1-gpio,gpiopin=4

# Enable GPIO
gpio=4=ip
```

Reboot:
```bash
sudo reboot
```

### 3. Deploy Application

#### Option A: Manual Deployment

```bash
# Create deployment directory
sudo mkdir -p /opt/brewery
sudo chown $USER:$USER /opt/brewery

# Build and publish (from development machine)
cd Server
dotnet publish Brewery.Server/Brewery.Server.csproj -c Release -o publish

# Copy to Raspberry Pi
scp -r publish/* pi@raspberrypi.local:/opt/brewery/
```

#### Option B: Docker Deployment

```bash
# Build Docker image
docker build -t brewery-server:latest -f Server/Dockerfile Server/

# Run container
docker run -d \
  --name brewery-server \
  --restart unless-stopped \
  -p 8800:8800 \
  --privileged \
  -v /sys:/sys \
  brewery-server:latest
```

### 4. Configure systemd Service (Manual Deployment)

```bash
# Copy service file
sudo cp brewery.service /etc/systemd/system/

# Reload systemd
sudo systemctl daemon-reload

# Enable service to start on boot
sudo systemctl enable brewery.service

# Start service
sudo systemctl start brewery.service

# Check status
sudo systemctl status brewery.service
```

### 5. Verify Installation

```bash
# Check if service is running
sudo systemctl status brewery.service

# View logs
sudo journalctl -u brewery.service -f

# Test API
curl http://localhost:8800/api/status/serverStatus
```

Expected response:
```json
{"message":"Server is up and running..."}
```

## Configuration

### GPIO Pin Assignments

- **Pin 12**: Mixer
- **Pin 21**: Piezo
- Additional pins as configured in your setup

### Temperature Sensor Configuration

Edit `Settings.cs` to configure 1-Wire addresses:
```csharp
public static string TemperatureSensor1OneWireAddress = "28-XXXXXXXXXXXX";
public static string TemperatureSensor2OneWireAddress = "28-XXXXXXXXXXXX";
```

Find your sensor addresses:
```bash
ls /sys/bus/w1/devices/
```

## Troubleshooting

### Service Won't Start

Check logs:
```bash
sudo journalctl -u brewery.service -n 50 --no-pager
```

### GPIO Permission Denied

Add user to gpio group:
```bash
sudo usermod -a -G gpio pi
sudo reboot
```

### 1-Wire Devices Not Found

Verify kernel modules:
```bash
sudo modprobe w1-gpio
sudo modprobe w1-therm
lsmod | grep w1
```

Check if sensors are detected:
```bash
ls /sys/bus/w1/devices/
cat /sys/bus/w1/devices/28-*/temperature
```

### Port 8800 Already in Use

Find process using port:
```bash
sudo lsof -i :8800
```

Kill process or change port in configuration.

## Updating

### Manual Update
```bash
# Stop service
sudo systemctl stop brewery.service

# Backup current version
sudo cp -r /opt/brewery /opt/brewery.backup

# Deploy new version
scp -r publish/* pi@raspberrypi.local:/opt/brewery/

# Start service
sudo systemctl start brewery.service
```

### Docker Update
```bash
# Pull new image
docker pull brewery-server:latest

# Stop and remove old container
docker stop brewery-server
docker rm brewery-server

# Run new container
docker run -d \
  --name brewery-server \
  --restart unless-stopped \
  -p 8800:8800 \
  --privileged \
  -v /sys:/sys \
  brewery-server:latest
```

## Security Considerations

1. **Firewall**: Configure UFW to allow only necessary ports
   ```bash
   sudo ufw allow 8800/tcp
   sudo ufw enable
   ```

2. **HTTPS**: Consider adding reverse proxy (nginx) with SSL
3. **Authentication**: Implement API authentication for production use
4. **Network**: Use isolated network or VPN for remote access

## Performance Tuning

### Optimize for Raspberry Pi

Edit `/opt/brewery/appsettings.json`:
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Warning"
    }
  },
  "Kestrel": {
    "Limits": {
      "MaxConcurrentConnections": 20,
      "MaxConcurrentUpgradedConnections": 20
    }
  }
}
```

### Memory Management

For Raspberry Pi 3 with limited RAM:
```bash
export DOTNET_GCHeapHardLimit=100000000
```

Add to service file under `[Service]` section.

## Monitoring

### System Resources
```bash
# CPU and Memory
htop

# Disk Space
df -h

# Temperature
vcgencmd measure_temp
```

### Application Logs
```bash
# Follow logs in real-time
sudo journalctl -u brewery.service -f

# Last 100 lines
sudo journalctl -u brewery.service -n 100

# Errors only
sudo journalctl -u brewery.service -p err
```

## Backup

### Configuration Backup
```bash
# Create backup script
cat > /home/pi/backup-brewery.sh << 'SCRIPT'
#!/bin/bash
BACKUP_DIR="/home/pi/brewery-backups"
TIMESTAMP=$(date +%Y%m%d_%H%M%S)
mkdir -p $BACKUP_DIR
tar -czf $BACKUP_DIR/brewery-$TIMESTAMP.tar.gz /opt/brewery
find $BACKUP_DIR -name "brewery-*.tar.gz" -mtime +30 -delete
SCRIPT

chmod +x /home/pi/backup-brewery.sh

# Add to crontab for daily backups
(crontab -l 2>/dev/null; echo "0 2 * * * /home/pi/backup-brewery.sh") | crontab -
```

## Support

For issues or questions, refer to:
- Project repository issues
- Migration documentation (MIGRATION.md)
- .NET 10 documentation: https://docs.microsoft.com/dotnet/
