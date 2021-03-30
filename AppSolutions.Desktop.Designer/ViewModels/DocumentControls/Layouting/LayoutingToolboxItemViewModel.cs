using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Telerik.Windows.Controls;

namespace AppSolutions.Desktop.Designer.ViewModels.DocumentControls.Layouting
{
    public delegate void LayoutingItemDragStartDelegate();
    public delegate void LayoutingItemDragStopDelegate();


    public class LayoutingToolboxItemViewModel: AbstractBaseViewModel, ILayoutingToolboxItemViewModel
    {
        public string SvgIcon { get; set; }

        public string Name { get; set; }

        public DelegateCommand MouseMoveCommand { get; set; }

        public Visual DocumentBaseVisual { get; set; }

        public event LayoutingItemDragStartDelegate LayoutingItemDragStart;

        public event LayoutingItemDragStopDelegate LayoutingItemDragStop;

        public void OnDragStart()
        {
            LayoutingItemDragStart?.Invoke();
        }

        public void OnDragStop()
        {
            LayoutingItemDragStop?.Invoke();
        }
    }
}
