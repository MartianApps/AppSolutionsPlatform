using AppSolutions.Desktop.Designer.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppSolutions.Desktop.Designer.Services
{
    public interface IMessageService: IService
    {
        void Post(string header, IMessageViewModel viewModel);

        void GetValueFromUser(string header, string text, Action<string> callback);

        void GetValueFromUser(string header, string text, string currentValue, Action<string, string> callback);
    }
}
