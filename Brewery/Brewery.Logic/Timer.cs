using Brewery.UI.Core.Contracts;
using System;
using System.Collections.Generic;
using Windows.UI.Xaml;

namespace Brewery.UI.Logic
{
    public class Timer : ITimer
    {
        private DispatcherTimer _dispatcherTimer;
        private readonly Dictionary<string, EventHandler<object>> _eventHandlers = new Dictionary<string, EventHandler<object>>();
        public Timer()
        {
            InitTimer();
        }
        public void AddEvent(string eventName, EventHandler<object> tick)
        {
            _eventHandlers.Add(eventName, tick);
            Restart();
        }
        public void RemoveEvent(string eventName, EventHandler<object> tick)
        {
            _eventHandlers.Remove(eventName);
            Restart();
        }
        private void Restart()
        {
            _dispatcherTimer.Stop();
            InitTimer();
            foreach (var e in _eventHandlers)
            {
                _dispatcherTimer.Tick += e.Value;
            }
        }
        private void InitTimer()
        {
            _dispatcherTimer = new DispatcherTimer {Interval = new TimeSpan(0, 0, 0, 1)};
            _dispatcherTimer.Start();
        }
    }
}