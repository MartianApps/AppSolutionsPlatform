using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Telerik.Windows.Controls;

namespace AppSolutions.Desktop.Designer.ViewModels.DocumentControls.Layouting
{
    public interface ILayoutingToolboxItemViewModel : ITransientViewModel
    {
        LayoutWidgetType Type { get; set; }

        string SvgIcon { get; }

        string Name { get; }

        DelegateCommand MouseMoveCommand { get; set; }

        public Visual DocumentBaseVisual { get; set; }

        void OnDragStart();

        void OnDragStop();
    }
}
