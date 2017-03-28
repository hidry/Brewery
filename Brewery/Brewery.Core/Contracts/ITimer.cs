using System;

namespace Brewery.Core.Contracts
{
    public interface ITimer
    {
        void AddEvent(EventHandler<object> tick);
        void RemoveEvent(EventHandler<object> tick);
    }
}