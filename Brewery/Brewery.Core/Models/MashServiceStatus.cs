namespace Brewery.Core.Models
{
    public enum ServiceStatus
    {
        Started,
        Paused,
        Stopped,
        Finished
    }

    public class MashServiceStatus
    {
        //todo: addMashSteps!? DataAnnotations for UI???
        public ServiceStatus Status { get; set; } = ServiceStatus.Stopped;
        public string Message { get; set; }
    }
}