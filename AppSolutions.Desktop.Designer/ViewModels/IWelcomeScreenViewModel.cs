using AppSolutions.Desktop.Designer.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Telerik.Windows.Controls;

namespace AppSolutions.Desktop.Designer.ViewModels
{
    public interface IWelcomeScreenViewModel: IViewModel
    {
        ObservableCollection<ILastUsedProjectViewModel> LastUsedProjects { get; }
    }

    public interface ILastUsedProjectViewModel : ITransientViewModel
    {
        DelegateCommand OpenProjectCommand { get; set; }

        Guid Id { get; }

        DateTime LastUsedDate { get; }

        string Name { get; }

        string Path { get; }

        string FormattedDateTime { get; }
    }
}
