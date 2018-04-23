using System;

namespace Brewery.UI.Core.Contracts
{
    public interface ITimer
    {
        void AddEvent(string eventName, EventHandler<object> tick);
        void RemoveEvent(string eventName, EventHandler<object> tick);
    }
}