using AppSolutions.Desktop.Designer.UI;
using AppSolutions.Desktop.Designer.ViewModels;
using Autofac;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppSolutions.Desktop.Designer.Services
{
    public class MessageService : IMessageService
    {
        private ILifetimeScope _scope;

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
    }
}
