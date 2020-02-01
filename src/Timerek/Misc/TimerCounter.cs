using System;
using System.Collections.Generic;
using System.Windows.Threading;

namespace Timerek.Misc
{
    public class TimerCounter : IDisposable
    {
        private readonly List<Action> _actions = new List<Action>();
        //http://www.pzielinski.com/?p=1528

        private readonly DispatcherTimer _timer1 = new DispatcherTimer();

        public void Dispose()
        {
            ReleaseUnmanagedResources();
            GC.SuppressFinalize(this);
        }

        public void Start()
        {
            _timer1.Interval = TimeSpan.FromMilliseconds(1000 - DateTime.Now.Millisecond);
            _timer1.IsEnabled = true;
            _timer1.Tick += UpdateDisplayedTime;
        }

        private void UpdateDisplayedTime(object sender, EventArgs e)
        {
            foreach (var action in _actions) action();
            _timer1.Interval = TimeSpan.FromMilliseconds(1000 - DateTime.Now.Millisecond);
        }

        public void AddAction(Action action)
        {
            _actions.Add(action);
        }

        private void ReleaseUnmanagedResources()
        {
            _timer1.Stop();
            _timer1.Tick -= UpdateDisplayedTime;
        }

        ~TimerCounter()
        {
            ReleaseUnmanagedResources();
        }
    }
}