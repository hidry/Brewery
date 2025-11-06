using Brewery.Server.Core.Api;
using Iot.Device.OneWire;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using UnitsNet;

namespace Brewery.Server.Logic.RaspberryPi.Api
{
    public class TemperatureModule : ITemperatureModule, IDisposable
    {
        private readonly object _locker = new object();
        private readonly Dictionary<string, OneWireBus> _buses;
        private bool _initialized = false;

        public TemperatureModule()
        {
            _buses = new Dictionary<string, OneWireBus>();
            try
            {
                // Initialize 1-Wire bus
                foreach (var busId in OneWireBus.EnumerateBusIds())
                {
                    try
                    {
                        var bus = new OneWireBus(busId);
                        _buses.Add(busId, bus);
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine($"Failed to initialize 1-Wire bus {busId}: {ex}");
                    }
                }
                _initialized = _buses.Count > 0;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to enumerate 1-Wire buses: {ex}");
            }
        }

        public double GetCurrenTemperature(string oneWireAddressString)
        {
            lock (_locker)
            {
                if (!_initialized || _buses.Count == 0)
                {
                    return 0;
                }

                try
                {
                    // Try to find the device on any bus
                    foreach (var bus in _buses.Values)
                    {
                        foreach (var deviceAddress in bus.EnumerateDeviceIds())
                        {
                            if (deviceAddress == oneWireAddressString)
                            {
                                // Read temperature from DS18B20
                                var tempRaw = bus.ReadTemperature(deviceAddress);
                                return tempRaw.DegreesCelsius;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error reading temperature from {oneWireAddressString}: {ex}");
                }

                return 0;
            }
        }

        #region IDisposable Support
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    foreach (var bus in _buses.Values)
                    {
                        bus?.Dispose();
                    }
                    _buses.Clear();
                }

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