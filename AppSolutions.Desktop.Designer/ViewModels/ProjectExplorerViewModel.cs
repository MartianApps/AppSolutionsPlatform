using AppSolutions.Desktop.Designer.Helpers;
using AppSolutions.Desktop.Designer.Services;
using AppSolutions.Platform.Models.Projects;
using NLog;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using Telerik.Windows.Controls;

namespace AppSolutions.Desktop.Designer.ViewModels
{
    public class ProjectExplorerViewModel : AbstractBaseViewModel, IProjectExplorerViewModel
    {
        private Logger _logger = LogManager.GetCurrentClassLogger();
        private IProjectService _projectService;

        public ProjectExplorerViewModel(IProjectService projectService)
        {
            _projectService = projectService;
            _projectService.ProjectOpened += ProjectService_ProjectOpened;
        }

        private void ProjectService_ProjectOpened(Project project)
        {
            Items.Clear();
            Items.Add(new ProjectItemViewModel { Title = $"Project '{project.Name}'", Type = ProjectItemType.Project });
            foreach (var m in project.Modules)
            {
                Items.Add(new ProjectItemViewModel { Title = m.Name, Type = ProjectItemType.Module });
            }
            ProjectName = project.Name;
            ProjectIsLoaded = true;

            OnPropertyChanged(nameof(ProjectIsLoaded));
        }

        public string ProjectName { get; set; }

        public ObservableCollection<IProjectItemViewModel> Items { get; set; } = new ObservableCollection<IProjectItemViewModel>();

        public bool CreateNewProjectButtonIsEnabled
        {
            get
            {
                return !string.IsNullOrEmpty(ProjectName);
            }
        }

        public bool ProjectIsLoaded { get; set; }

        #region Commands

        //public DelegateCommand CreateNewProjectCommand { get; set; }

        #endregion Commands

        public void CreateNewProject(string name, string folder)
        {
            try
            {
                var projectFilePath = _projectService.CreateEmptyProject(name, folder);
                _projectService.LoadProjectFromDisk(projectFilePath);
                
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
        }
    }
}
