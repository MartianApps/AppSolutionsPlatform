using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace AppSolutions.Desktop.Designer.ViewModels
{
    public class ProjectItemViewModel : AbstractBaseViewModel, IProjectItemViewModel
    {
        public string Title { get; set; }

        public ProjectItemType Type { get; set; }

        public ObservableCollection<IProjectItemViewModel> SubItems { get; set; } = new ObservableCollection<IProjectItemViewModel>();
    }


}
