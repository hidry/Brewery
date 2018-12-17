using System;

namespace Brewery.Server.Core.Models
{
    public class MashStep
    {
        public double Temperature { get; set; }
        public int Rast { get; set; }
        public bool Mixer { get; set; }
        public bool Alert { get; set; }
        public string ElapsedTime { get { return $"{Elapsed.Hours.ToString("00")}:{Elapsed.Minutes.ToString("00")}:{Elapsed.Seconds.ToString("00")}"; } }
        public string Step { get; set; }
        public bool Active { get; set; }
        public string Guid { get; set; }
        public TimeSpan Elapsed { get; set; }
    }
}