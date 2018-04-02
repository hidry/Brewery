using System;

namespace Brewery.Core.Contracts
{
    public class TemperatureChangedEventArgs : EventArgs
    {
        public TemperatureChangedEventArgs(double t)
        {
            Temperature = t;
        }
        public double Temperature { get; }
    }

    public class HeatingStatusChangedEventArgs : EventArgs
    {
        public HeatingStatusChangedEventArgs(bool h)
        {
            Heating = h;
        }

        public bool Heating { get; }
    }

    public interface IDevicesService
    {
        event EventHandler<TemperatureChangedEventArgs> Temperature1ChangedEvent;
        event EventHandler<TemperatureChangedEventArgs> Temperature2ChangedEvent;

        event EventHandler<HeatingStatusChangedEventArgs> HeatingStatus1ChangedEvent;
        event EventHandler<HeatingStatusChangedEventArgs> HeatingStatus2ChangedEvent;

        void RefreshDeviceStatus();
    }
}