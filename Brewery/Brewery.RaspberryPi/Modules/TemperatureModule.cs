using System.Collections.Generic;
using System.Linq;
using Brewery.Core.Contracts;
using Brewery.Core.Models;
using Rinsen.IoT.OneWire;

namespace Brewery.RaspberryPi.Modules
{
    public abstract class TemperatureModule : ITemperatureModule
    {
        private static readonly IEnumerable<DS18B20> _devices;
        private static readonly OneWireDeviceHandler _handler;
        private  readonly string _oneWireAddressString;

        static TemperatureModule()
        {
            _handler = new OneWireDeviceHandler(false, false);
            _devices = _handler.OneWireDevices.GetDevices<DS18B20>();
        }

        public TemperatureModule(string oneWireAddressString)
        {
            _oneWireAddressString = oneWireAddressString;            
        }

        public TemperatureModel GetCurrenTemperature()
        {
            return new TemperatureModel() {Temperature = Device.GetTemperature() };
        }

        private DS18B20 Device => _devices.First(d => d.OneWireAddressString == _oneWireAddressString);

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