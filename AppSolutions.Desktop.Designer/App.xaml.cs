using AppSolutions.Desktop.Designer.Extensions;
using AppSolutions.Desktop.Designer.Services;
using Autofac;
using Autofac.Core;
using NLog;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;
using Telerik.Windows.Controls;

namespace AppSolutions.Desktop.Designer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// https://github.com/oriches/Simple.Wpf.DataGrid/tree/master/Simple.Wpf.DataGrid/Views/Views
    /// </summary>
    public partial class App : Application
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        private readonly CompositeDisposable _disposable;

        public App()
        {
            StyleManager.ApplicationTheme = new VisualStudio2019Theme();

            AppDomain.CurrentDomain.UnhandledException += CurrentDomainOnUnhandledException;
            Current.DispatcherUnhandledException += DispatcherOnUnhandledException;
            TaskScheduler.UnobservedTaskException += TaskSchedulerOnUnobservedTaskException;

            _disposable = new CompositeDisposable();
        }

        protected override void OnStartup(StartupEventArgs args)
        {
            using (AppSolutions.Desktop.Designer.Helpers.Duration.Measure(Logger, "OnStartup - " + GetType().Name))
            {
                Logger.Info("Starting");

                // ReSharper disable once RedundantToStringCallForValueType
                var dispatcherMessage =
                    $"Dispatcher managed thread identifier = {Thread.CurrentThread.ManagedThreadId.ToString()}";

                Logger.Info(dispatcherMessage);
                Debug.WriteLine(dispatcherMessage);

                Logger.Info($"WPF rendering capability (tier) = {(RenderCapability.Tier / 0x10000).ToString()}");
                RenderCapability.TierChanged += (s, a) =>
                    Logger.Info($"WPF rendering capability (tier) = {(RenderCapability.Tier / 0x10000).ToString()}");

                base.OnStartup(args);

                BootStrapper.Start();

                var gestureService = BootStrapper.Resolve<IGestureService>();

                AppSolutions.Desktop.Designer.Extensions.ObservableExtensions.GestureService = gestureService;

                // Load the application settings asynchronously
                //LoadSettingsAsync(schedulerService)
                //    .Wait();

                var window = BootStrapper.Resolve<MainWindow>();

                window.Closed += HandleClosed;
                Current.Exit += HandleExit;

                // Let's go...
                window.Show();


#if DEBUG
                ObserveUiFreeze()
                    .DisposeWith(_disposable);
#endif
                ObserveCultureChanges()
                    .DisposeWith(_disposable);

                Logger.Info("Started");
            }
        }

        private void HandleClosed(object sender, EventArgs e)
        {
            _disposable.Dispose();
            BootStrapper.Stop();
        }

        private static void HandleExit(object sender, ExitEventArgs e)
        {
            Logger.Info("Bye Bye!");
            LogManager.Flush();
        }

        private static IDisposable ObserveCultureChanges()
        {
            return CultureService.CultureChanged
                .Subscribe(x =>
                {
                    Current.Windows
                        .Cast<Window>()
                        .ForEach(y => y.InvalidateVisual());
                });
        }

        private static IDisposable ObserveUiFreeze()
        {
            var timer = new DispatcherTimer(DispatcherPriority.Normal)
            {
                Interval = Constants.UI.Diagnostics.UiFreezeTimer
            };

            var previous = DateTime.Now;
            timer.Tick += (sender, args) =>
            {
                var current = DateTime.Now;
                var delta = current - previous;
                previous = current;

                if (delta > Constants.UI.Diagnostics.UiFreeze)
                {
                    var message =
                        $"UI Freeze = {delta.TotalMilliseconds.ToString(CultureInfo.InvariantCulture)} ms";
                    Debug.WriteLine(message);
                }
            };

            timer.Start();
            return Disposable.Create(() => timer.Stop());
        }

        private static void CurrentDomainOnUnhandledException(object sender, UnhandledExceptionEventArgs args)
        {
            Logger.Info("Unhandled app domain exception");
            HandleException(args.ExceptionObject as Exception);
        }

        private static void DispatcherOnUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs args)
        {
            Logger.Info("Unhandled dispatcher thread exception");
            args.Handled = true;

            HandleException(args.Exception);
        }

        private static void TaskSchedulerOnUnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs args)
        {
            Logger.Info("Unhandled task exception");
            args.SetObserved();

            HandleException(args.Exception.GetBaseException());
        }

        private static void HandleException(Exception exception)
        {
            Logger.Error(exception);

            //BootStrapper.Resolve<ISchedulerService>()
            //    .Dispatcher
            //    .Schedule(exception, (scheduler, state) =>
            //    {
            //        var messageService = BootStrapper.Resolve<IMessageService>();

            //        var parameters = new Parameter[]
            //        {
            //            new NamedParameter("exception", state)
            //        };

            //        var viewModel = BootStrapper.Resolve<IExceptionViewModel>(parameters);

            //        Observable.Return(viewModel)
            //            .SelectMany(x => x.Closed, (x, y) => x)
            //            .Take(1)
            //            .Subscribe(x => x.Dispose());

            //        messageService.Post(Constants.UI.ExceptionTitle, viewModel);

            //        return Disposable.Empty;
            //    });
        }
    }
}
