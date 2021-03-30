using AppSolutions.Desktop.Designer.Services;
using AppSolutions.Desktop.Designer.ViewModels.DocumentControls.Layouting;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using Telerik.Windows.Controls;

namespace AppSolutions.Desktop.Designer.ViewModels
{
    public class LayoutingDocumentViewModel : AbstractBaseViewModel, ILayoutingDocumentViewModel
    {
        public LayoutingDocumentViewModel()
        {            
        }

        public ObservableCollection<ILayoutingToolboxItemViewModel> ToolboxItems { get; set; } = new ObservableCollection<ILayoutingToolboxItemViewModel>();

        public event LayoutingItemDragStartDelegate LayoutingItemDragStart;

        public event LayoutingItemDragStopDelegate LayoutingItemDragStop;

        public void CreateToolItems(Visual documentBaseVisual)
        {
            if (ToolboxItems.Count > 0)
            {
                return;
            }
            var item = new LayoutingToolboxItemViewModel
            {
                SvgIcon = "/Resources/Svg/border-none-solid.svg",
                Name = "Container",
                DocumentBaseVisual = documentBaseVisual,
            };
            item.LayoutingItemDragStart += Item_LayoutingItemDragStart;
            item.LayoutingItemDragStop += Item_LayoutingItemDragStop;
            ToolboxItems.Add(item);

            item = new LayoutingToolboxItemViewModel
            {
                SvgIcon = "/Resources/Svg/border-none-solid.svg",
                Name = "Scroll Container",
                DocumentBaseVisual = documentBaseVisual
            };
            item.LayoutingItemDragStart += Item_LayoutingItemDragStart;
            item.LayoutingItemDragStop += Item_LayoutingItemDragStop;
            ToolboxItems.Add(item);
        }

        private void Item_LayoutingItemDragStop()
        {
            LayoutingItemDragStop?.Invoke();
        }

        private void Item_LayoutingItemDragStart()
        {
            LayoutingItemDragStart?.Invoke();
        }
    }
}
