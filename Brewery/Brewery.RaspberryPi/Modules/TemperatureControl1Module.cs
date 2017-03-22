using Brewery.Core.Contracts;

namespace Brewery.RaspberryPi.Modules
{
    public class TemperatureControl1Module : TemperatureControlModule, ITemperatureControl1Module
    {
        public TemperatureControl1Module(IBoilingPlate1Module boilingPlate1Module) : base(boilingPlate1Module)
        {
        }
    }
}