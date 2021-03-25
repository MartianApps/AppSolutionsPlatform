using AppSolutions.Desktop.Designer.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppSolutions.Desktop.Designer.ViewModels
{
    public class ConsoleOutputViewModel : AbstractBaseViewModel, IConsoleOutputViewModel
    {
        private IConsoleOutputService _outputService;

        public ConsoleOutputViewModel(IConsoleOutputService outputService)
        {
            _outputService = outputService;

            _outputService.OutputContentChanged += OutputService_OutputContentChanged;
        }

        private void OutputService_OutputContentChanged()
        {
            Text = _outputService.Content;
        }

        public string Text { get; private set; }
    }
}
