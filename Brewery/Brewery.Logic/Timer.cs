using Brewery.Core.Contracts;
using System;
using Windows.UI.Xaml;

namespace Brewery.Logic
{
    public class Timer : ITimer
    {
        private readonly DispatcherTimer _dispatcherTimer;
        public Timer()
        {
            _dispatcherTimer = new DispatcherTimer();
            _dispatcherTimer.Interval = new TimeSpan(0, 0, 0, 1);
            _dispatcherTimer.Start();
        }
        public void AddEvent(EventHandler<object> tick)
        {
            _dispatcherTimer.Tick += tick;
        }
        public void RemoveEvent(EventHandler<object> tick)
        {
            _dispatcherTimer.Tick -= tick;
        }
    }
}