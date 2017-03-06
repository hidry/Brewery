using Brewery.Core.Models;

namespace Brewery.Core.Contracts
{
    interface IDateTimeModule
    {
        DateTimeModel GetCurrentDateTime();
    }
}
