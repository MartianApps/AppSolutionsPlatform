using NLog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Reactive.Disposables;
using System.Text;
using System.Threading;

namespace AppSolutions.Desktop.Designer.Helpers
{
    public sealed class Duration : IDisposable
    {
        private readonly string _context;
        private readonly Logger _logger;
        private readonly Stopwatch _stopwatch;

        private Duration(Logger logger, string context)
        {
            _context = context;
            _stopwatch = new Stopwatch();
            _logger = logger;

            _stopwatch.Start();
        }

        public void Dispose()
        {
            _stopwatch.Stop();

            var parameters = new object[]
            {
                _context,
                _stopwatch.ElapsedMilliseconds.ToString(),
                _stopwatch.ElapsedTicks.ToString(),
                Thread.CurrentThread.ManagedThreadId.ToString()
            };

            var message = string.Format("{0}, duration={1}ms, ticks={2}, thread_id={3}", parameters);

            Debug.WriteLine(message);
            _logger.Debug(message);
        }

        public static IDisposable Measure(Logger logger, string context)
        {
            if (!logger.IsDebugEnabled) return Disposable.Empty;

            return new Duration(logger, context);
        }

        public static IDisposable Measure(Logger logger, string context, object[] args)
        {
            if (!logger.IsDebugEnabled) return Disposable.Empty;

            if (args != null) context = string.Format(CultureInfo.InvariantCulture, context, args);

            return new Duration(logger, context);
        }
    }
}
