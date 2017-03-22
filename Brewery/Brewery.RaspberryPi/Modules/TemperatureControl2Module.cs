using Brewery.Core.Contracts;

namespace Brewery.RaspberryPi.Modules
{
    public class TemperatureControl2Module : TemperatureControlModule, ITemperatureControl2Module
    {
        public TemperatureControl2Module(IBoilingPlate1Module boilingPlate1Module) : base(boilingPlate1Module)
        {
        }
    }
}