using AppSolutions.Desktop.Designer.Helpers;
using AppSolutions.Desktop.Designer.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Telerik.Windows.Controls;

namespace AppSolutions.Desktop.Designer.ViewModels
{
    public class WelcomeScreenViewModel : AbstractBaseViewModel, IWelcomeScreenViewModel
    {
        private IProjectService _projectService;

        public WelcomeScreenViewModel(IProjectService projectService)
        {
            _projectService = projectService;

            foreach (var p in UserDataManager.Instance.LastUsedProjects.OrderBy(o => o.LastUsedDate))
            {
                var vm = new UsedProjectViewModel(p);
                vm.OpenProject += UsedProject_OpenProject;
                LastUsedProjects.Add(vm);
            }
        }

        private void UsedProject_OpenProject(UsedProject usedProject)
        {
            _projectService.LoadProjectFromDisk(usedProject.Path);
        }

        public ObservableCollection<ILastUsedProjectViewModel> LastUsedProjects { get; } = new ObservableCollection<ILastUsedProjectViewModel>();        
    }

    public delegate void OpenProjectDelegate(UsedProject usedProject);

    public class UsedProjectViewModel : AbstractBaseViewModel, ILastUsedProjectViewModel
    {
        private UsedProject _usedProject;

        public UsedProjectViewModel(UsedProject usedProject)
        {
            _usedProject = usedProject;

            OpenProjectCommand = new DelegateCommand((o) => { OnOpenProject(); });
        }

        public event OpenProjectDelegate OpenProject;

        private void OnOpenProject()
        {
            OpenProject?.Invoke(_usedProject);
        }

        public DelegateCommand OpenProjectCommand { get; set; }

        public Guid Id => _usedProject.Id;

        public DateTime LastUsedDate => _usedProject.LastUsedDate;

        public string Name => $"{_usedProject.Name}.maproj";

        public string Path => _usedProject.Path;

        public string FormattedDateTime
        {
            get
            {
                return LastUsedDate.ToShortDateString() + " " + LastUsedDate.ToShortTimeString();
            }
        }
    }
}
