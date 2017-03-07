using Brewery.Core.Models;

namespace Brewery.Core.Contracts
{
    public interface IDateTimeModule
    {
        DateTimeModel GetCurrentDateTime();
    }
}
