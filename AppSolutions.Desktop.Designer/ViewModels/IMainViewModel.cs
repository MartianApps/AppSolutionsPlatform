using AppSolutions.Desktop.Designer.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppSolutions.Desktop.Designer.ViewModels
{
    public interface IMainViewModel : IViewModel
    {
        bool WelcomScreenIsActive { get; }
    }
}
