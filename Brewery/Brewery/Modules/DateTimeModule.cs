using System;
using Brewery.Core.Contracts;
using Brewery.Core.Models;

namespace Brewery.Modules
{
    class DateTimeModule : IDateTimeModule
    {
        public DateTimeModel GetCurrentDateTime()
        {
            return new DateTimeModel() {DateTime = DateTime.Now};
        }
    }
}