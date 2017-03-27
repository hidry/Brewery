using Brewery.Core.Contracts;

namespace Brewery.RaspberryPi.Modules
{
    public class TemperatureControl2Module : TemperatureControlModule, ITemperatureControl2Module
    {
        public TemperatureControl2Module(IBoilingPlate2Module boilingPlate2Module) : base(boilingPlate2Module)
        {
        }
    }
}