using AppSolutions.Desktop.Designer.Helpers;
using AppSolutions.Desktop.Designer.Services;
using NLog;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppSolutions.Desktop.Designer.ViewModels
{
    public class MainViewModel : AbstractBaseViewModel, IMainViewModel
    {
        private Logger _logger = LogManager.GetCurrentClassLogger();
        private IProjectService _projectService;

        public MainViewModel(IProjectService projectService)
        {
            _projectService = projectService;
            _projectService.ProjectOpened += ProjectService_ProjectOpened;
        }

        private void ProjectService_ProjectOpened(Platform.Models.Projects.Project project)
        {
            WelcomScreenIsActive = false;
        }

        #region IMainViewModel

        public bool WelcomScreenIsActive { get; set; } = true;

        #endregion IMainViewModel
    }
}
