using AppSolutions.Desktop.Designer.ViewModels.DocumentControls.Layouting.Widgets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSolutions.Desktop.Designer.ViewModels.ToolWindows.Properties
{
    public class PropertiesToolWindowViewModelImpl : AbstractBaseViewModel, IPropertiesToolWindowViewModel
    {
        public object EditableViewModel { get; private set; }

        public void UpdateEditableViewModel(object editableViewModel)
        {
            EditableViewModel = editableViewModel;
            OnPropertyChanged(nameof(EditableViewModel));
        }
    }
}
