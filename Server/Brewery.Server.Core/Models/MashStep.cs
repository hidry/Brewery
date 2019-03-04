using System;

namespace Brewery.Server.Core.Models
{
    public class MashStep
    {
        public double Temperature { get; set; }
        public int Rast { get; set; }
        public bool Mixer { get; set; }
        public bool Alert { get; set; }
        public string Step { get; set; }
        public string Guid { get; set; }
        public TimeSpan Elapsed { get; set; }
        public int EstimatedTime { get; set; }
    }
}