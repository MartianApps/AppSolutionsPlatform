using AppSolutions.Desktop.Designer.ViewModels.DocumentControls.Layouting.Widgets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSolutions.Desktop.Designer.ViewModels.ToolWindows.Properties
{
    public interface IPropertiesToolWindowViewModel: IViewModel
    {
        object EditableViewModel { get; }

        void UpdateEditableViewModel(object editableViewModel);
    }
}
