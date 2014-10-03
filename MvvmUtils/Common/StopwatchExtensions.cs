using System;
using System.Diagnostics;

namespace Pellared.Common
{
    public static class StopwatchExtensions
    {
        public static IDisposable Measurement(this Stopwatch stopwatch)
        {
            stopwatch.Restart();
            var stopper = new Disposer(stopwatch.Stop);
            return stopper;
        }

        public static IDisposable Continue(this Stopwatch stopwatch)
        {
            stopwatch.Start();
            var stopper = new Disposer(stopwatch.Stop);
            return stopper;
        }
    }
}