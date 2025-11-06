using Brewery.Server.Core.Api;
using Iot.Device.OneWire;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Brewery.Server.Logic.RaspberryPi.Api
{
    public class TemperatureModule : ITemperatureModule, IDisposable
    {
        private readonly object _locker = new object();
        private bool _initialized = false;
        private const string OneWireBasePath = "/sys/bus/w1/devices";

        public TemperatureModule()
        {
            try
            {
                // Check if 1-Wire is available
                _initialized = Directory.Exists(OneWireBasePath);
                if (_initialized)
                {
                    Debug.WriteLine("1-Wire interface initialized");
                }
                else
                {
                    Debug.WriteLine("1-Wire interface not available - /sys/bus/w1/devices not found");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to initialize 1-Wire: {ex}");
                _initialized = false;
            }
        }

        public double GetCurrenTemperature(string oneWireAddressString)
        {
            lock (_locker)
            {
                if (!_initialized)
                {
                    return 0;
                }

                try
                {
                    // Read temperature directly from sysfs
                    // Format: 28-xxxxxxxxxxxx
                    var devicePath = Path.Combine(OneWireBasePath, oneWireAddressString, "w1_slave");
                    
                    if (!File.Exists(devicePath))
                    {
                        Debug.WriteLine($"Temperature sensor not found: {oneWireAddressString}");
                        return 0;
                    }

                    var lines = File.ReadAllLines(devicePath);
                    if (lines.Length < 2)
                    {
                        Debug.WriteLine($"Invalid data from sensor: {oneWireAddressString}");
                        return 0;
                    }

                    // Check CRC
                    if (!lines[0].EndsWith("YES"))
                    {
                        Debug.WriteLine($"CRC check failed for sensor: {oneWireAddressString}");
                        return 0;
                    }

                    // Parse temperature (format: t=23125)
                    var tempPos = lines[1].IndexOf("t=");
                    if (tempPos == -1)
                    {
                        Debug.WriteLine($"Temperature value not found for sensor: {oneWireAddressString}");
                        return 0;
                    }

                    var tempString = lines[1].Substring(tempPos + 2);
                    if (int.TryParse(tempString, out int tempRaw))
                    {
                        // Temperature is in millidegrees Celsius
                        return tempRaw / 1000.0;
                    }

                    Debug.WriteLine($"Failed to parse temperature for sensor: {oneWireAddressString}");
                    return 0;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error reading temperature from {oneWireAddressString}: {ex}");
                    return 0;
                }
            }
        }

        #region IDisposable Support
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
