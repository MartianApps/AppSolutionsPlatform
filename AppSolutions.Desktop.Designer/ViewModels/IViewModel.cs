using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace AppSolutions.Desktop.Designer.ViewModels
{
    public interface IViewModel : INotifyPropertyChanged, IDisposable
    {
        IDisposable SuspendNotifications();
    }
}
