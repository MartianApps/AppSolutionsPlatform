using AppSolutions.Platform.Models.Projects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Telerik.Windows.Controls;

namespace AppSolutions.Desktop.Designer.ViewModels
{
    public delegate void OpenDocumentDelegate(ProjectItemType type, string documentName, string documentPath);

    public interface IProjectExplorerViewModel: IViewModel
    {
        event OpenDocumentDelegate OpenDocument;

        string ProjectName { get; set; }

        ObservableCollection<IProjectItemViewModel> Items { get; set; }

        IProjectItemViewModel SelectedItem { get; set; }

        bool CreateNewProjectButtonIsEnabled { get; }

        bool ProjectIsLoaded { get; }

        void CreateNewProject(string name, string folder);

        DelegateCommand RenameCommand { get; set; }

        DelegateCommand AddFolderCommand { get; set; }

        DelegateCommand AddPageCommand { get; set; }

        DelegateCommand AddLayoutCommand { get; set; }

        DelegateCommand AddWorkflowCommand { get; set; }

        DelegateCommand DeleteCommand { get; set; }

        DelegateCommand ItemDoubleClickedCommand { get; set; }
    }

    public interface IProjectItemViewModel: ITransientViewModel
    {
        string Title { get; set; }

        public string Icon { get; set; }

        ProjectItemType Type { get; set; }

        bool IsExpanded { get; set; }

        bool IsSelected { get; set; }

        IProjectItemViewModel Parent { get; set; }

        string FullSubPath { get; }

        string ParentSubPath { get; }

        string FileName { get; }

        ObservableCollection<IProjectItemViewModel> SubItems { get; set; }
    }
}
