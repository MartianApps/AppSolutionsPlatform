using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace AppSolutions.Desktop.Designer.ViewModels
{
    public interface IProjectExplorerViewModel: IViewModel
    {
        string ProjectName { get; set; }

        ObservableCollection<IProjectItemViewModel> Items { get; set; }

        bool CreateNewProjectButtonIsEnabled { get; }

        bool ProjectIsLoaded { get; }

        void CreateNewProject(string name, string folder);
    }

    public interface IProjectItemViewModel: ITransientViewModel
    {
        string Title { get; set; }

        ProjectItemType Type { get; set; }

        ObservableCollection<IProjectItemViewModel> SubItems { get; set; }
    }

    public enum ProjectItemType
    {
        Project,
        Module,
    }
}
