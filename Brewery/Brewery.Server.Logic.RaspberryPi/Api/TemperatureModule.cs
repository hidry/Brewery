using Brewery.Server.Core.Api;
using Rinsen.IoT.OneWire;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Brewery.Server.Logic.RaspberryPi.Api
{
    public class TemperatureModule : ITemperatureModule, IDisposable
    {
        private readonly object _locker = new object();
        private readonly IEnumerable<DS18B20> _devices;
        private readonly OneWireDeviceHandler _handler;

        public TemperatureModule()
        {
            try
            {
                _handler = new OneWireDeviceHandler(false, false);
                _devices = _handler.OneWireDevices.GetDevices<DS18B20>();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }            
        }

        public double GetCurrenTemperature(string oneWireAddressString)
        {
            lock(_locker)
            {
                var device = _devices?.FirstOrDefault(d => d.OneWireAddressString == oneWireAddressString);
                return device == null ? 0 : device.GetTemperature();
            }
        }

        #region IDisposable Support
        private bool disposedValue = false; // Dient zur Erkennung redundanter Aufrufe.

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: verwalteten Zustand (verwaltete Objekte) entsorgen.
                    _handler.Dispose();
                }

                // TODO: nicht verwaltete Ressourcen (nicht verwaltete Objekte) freigeben und Finalizer weiter unten überschreiben.
                // TODO: große Felder auf Null setzen.

                disposedValue = true;
            }
        }

        // TODO: Finalizer nur überschreiben, wenn Dispose(bool disposing) weiter oben Code für die Freigabe nicht verwalteter Ressourcen enthält.
        // ~TemperatureModule() {
        //   // Ändern Sie diesen Code nicht. Fügen Sie Bereinigungscode in Dispose(bool disposing) weiter oben ein.
        //   Dispose(false);
        // }

        // Dieser Code wird hinzugefügt, um das Dispose-Muster richtig zu implementieren.
        public void Dispose()
        {
            // Ändern Sie diesen Code nicht. Fügen Sie Bereinigungscode in Dispose(bool disposing) weiter oben ein.
            Dispose(true);
            // TODO: Auskommentierung der folgenden Zeile aufheben, wenn der Finalizer weiter oben überschrieben wird.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}