namespace Brewery.Server.Core.Models
{
    public class MashStep
    {
        public double Temperature { get; set; }
        public int Rast { get; set; }
        public bool Mixer { get; set; }
        public bool Alert { get; set; }
        public string ElapsedTime { get; set; }
        public string Step { get; set; }
        public bool Active { get; set; }
    }
}