using AppSolutions.Desktop.Designer.Extensions;
using AppSolutions.Desktop.Designer.Helpers;
using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace AppSolutions.Desktop.Designer.Services
{
    public sealed class GesturesService : DisposableObject, IGestureService
    {
        private readonly DispatcherTimer _timer;

        private bool _isBusy;

        public GesturesService()
        {
            using (Helpers.Duration.Measure(Logger, "Constructor - " + GetType().Name))
            {
                _timer = new DispatcherTimer(TimeSpan.Zero, DispatcherPriority.ApplicationIdle, TimerCallback,
                    Application.Current.Dispatcher);
                _timer.Stop();
            }

            Disposable.Create(() => _timer.Stop())
                .DisposeWith(this);
        }

        public void SetBusy()
        {
            SetBusyState(true);
        }

        private void SetBusyState(bool busy)
        {
            if (busy != _isBusy)
            {
                _isBusy = busy;
                Mouse.OverrideCursor = busy ? Cursors.Wait : null;

                if (_isBusy) _timer.Start();
            }
        }

        private void TimerCallback(object sender, EventArgs e)
        {
            SetBusyState(false);
            _timer.Stop();
        }
    }
}
