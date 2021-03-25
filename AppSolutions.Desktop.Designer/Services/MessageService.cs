using AppSolutions.Desktop.Designer.UI;
using AppSolutions.Desktop.Designer.ViewModels;
using Autofac;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using Telerik.Windows.Controls;

namespace AppSolutions.Desktop.Designer.Services
{
    public class MessageService : IMessageService
    {
        private ILifetimeScope _scope;
        private Action<string, string> _valueCallback;
        private Action<string> _valueCallbackShort;
        private string _currentValue;

        public MessageService(ILifetimeScope scope)
        {
            _scope = scope;
        }

        public void Post(string header, IMessageViewModel viewModel)
        {
            var window = _scope.Resolve<MessageBoxWindow>();
            window.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
            window.Header = header;
            window.DataContext = viewModel;
            window.ShowDialog();
        }

        public void GetValueFromUser(string header, string text, Action<string> callback)
        {
            _valueCallbackShort = callback;
            _valueCallback = null;
            _currentValue = null;

            RadWindow.Prompt(new DialogParameters
            {
                Header = header,
                Content = text,
                Closed = new EventHandler<WindowClosedEventArgs>(OnPromptClosed),
                Owner = Application.Current.MainWindow,
                DefaultPromptResultValue = ""
            });
        }

        public void GetValueFromUser(string header, string text, string currentValue, Action<string, string> callback)
        {
            _valueCallback = callback;
            _valueCallbackShort = null;
            _currentValue = currentValue;

            RadWindow.Prompt(new DialogParameters
            {
                Header = header,
                Content = text,
                Closed = new EventHandler<WindowClosedEventArgs>(OnPromptClosed),
                Owner = Application.Current.MainWindow,
                DefaultPromptResultValue = currentValue
            });
        }

        private void OnPromptClosed(object sender, WindowClosedEventArgs e)
        {
            if (e.PromptResult != null && e.PromptResult != string.Empty)
            {
                if (_valueCallback != null)
                {
                    _valueCallback(_currentValue, e.PromptResult);
                }
                if (_valueCallbackShort != null)
                {
                    _valueCallbackShort(e.PromptResult);
                }
            }
        }
    }
}
