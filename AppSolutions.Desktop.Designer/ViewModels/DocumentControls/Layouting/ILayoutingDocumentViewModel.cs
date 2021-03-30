using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using Telerik.Windows.Controls;

namespace AppSolutions.Desktop.Designer.ViewModels.DocumentControls.Layouting
{
    public interface ILayoutingDocumentViewModel: IViewModel
    {
        ObservableCollection<ILayoutingToolboxItemViewModel> ToolboxItems { get; set; }

        void CreateToolItems(Visual documentBaseVisual);

        event LayoutingItemDragStartDelegate LayoutingItemDragStart;

        event LayoutingItemDragStopDelegate LayoutingItemDragStop;
    }

}
