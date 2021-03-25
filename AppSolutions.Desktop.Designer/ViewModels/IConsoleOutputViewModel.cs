using System;
using System.Collections.Generic;
using System.Text;

namespace AppSolutions.Desktop.Designer.ViewModels
{
    public interface IConsoleOutputViewModel: IViewModel
    {
        string Text { get; }
    }
}
