using Microsoft.UI.Xaml;
using System;

namespace Solari.App.Helpers
{
    /// <summary>
    /// A simple helper clock to be used in the UI thread.
    /// Returns the time every second, formatted as HH:mm.
    /// </summary>
    public class Clock
    {
        private readonly DispatcherTimer Timer;

        public Clock()
        {
            Timer = new() { Interval = new TimeSpan(0, 0, 1) };
        }

        /// <summary>
        /// Start the clock.
        /// </summary>
        /// <param name="callback">Returns the current time, formatted as HH:mm.</param>
        public void Run(Action<string> callback)
        {
            // Initial time.
            callback(DateTime.Now.ToString("HH:mm"));

            //// Update time every second after initial time.
            Timer.Tick += (object sender, object e) =>
                callback(DateTime.Now.ToString("HH:mm"));

            Timer.Start();
        }

        /// <summary>
        /// Stop the clock.
        /// </summary>
        public void Stop()
        {
            Timer.Stop();
        }
    }
}
