using Brewery.Core.Contracts;
using Brewery.Core.Models;
using System;

namespace Brewery.RaspberryPi.Modules
{
    public abstract class BoilingPlateModule : IBoilingPlateModule
    {
        private readonly GpioModule _gpioModule;
        private DateTime? _startTime;

        public BoilingPlateModule(int gpioNumber)
        {
            _gpioModule = new GpioModule(gpioNumber);
        }

        public BoilingPlateModel PowerOn()
        {
            if (_startTime == null) _startTime = DateTime.Now;
            else if ((DateTime.Now - _startTime).Value.Minutes >= 30) PowerOff();

            _gpioModule.Power(true);
            return new BoilingPlateModel() { Status = true };
        }

        public BoilingPlateModel PowerOff()
        {
            _startTime = null;

            _gpioModule.Power(false);
            return new BoilingPlateModel() { Status = false };
        }
    }
}