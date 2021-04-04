using AppSolutions.Desktop.Designer.Services;
using AppSolutions.Desktop.Designer.UI.DocumentControls.Layouting.Widgets;
using AppSolutions.Desktop.Designer.ViewModels.DocumentControls.Layouting;
using AppSolutions.Desktop.Designer.ViewModels.DocumentControls.Layouting.Widgets;
using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AppSolutions.Desktop.Designer.UI.DocumentControls.Layouting
{
    /// <summary>
    /// Interaktionslogik für LayoutEditorCanvas.xaml
    /// </summary>
    public partial class LayoutEditorCanvas : UserControl
    {
        public LayoutEditorCanvas()
        {
            InitializeComponent();

            DataContextChanged += LayoutEditorCanvas_DataContextChanged;
        }

        private void LayoutEditorCanvas_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (ViewModel != null)
            {
                Canvas.DataContext = ViewModel.LayoutingCanvas;
                MasterGrid.DataContext = ViewModel.LayoutingCanvas;

                ViewModel.PropertyChanged += ViewModel_PropertyChanged;

                ViewModel.LayoutingItemDragStart += ViewModel_LayoutingItemDragStart;
                ViewModel.LayoutingItemDragStop += ViewModel_LayoutingItemDragStop;

                ConstructUI();
            }
        }

        private void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ILayoutingDocumentViewModel.LayoutingCanvas))
            {
                Canvas.DataContext = ViewModel.LayoutingCanvas;
                MasterGrid.DataContext = ViewModel.LayoutingCanvas;

                ConstructUI();
            }
        }

        private void ConstructUI()
        {
            MasterGrid.Children.Clear();
            if (ViewModel.LayoutingCanvas.Container != null)
            {
                var widget = BootStrapper.Resolve<ContainerWidget>();
                widget.DataContext = ViewModel.LayoutingCanvas.Container;

                MasterGrid.Children.Add(widget);
            }
        }

        private void ActivateDropZonesRecursive(object control, Action<DropZone> action)
        {
            if (control is Panel)
            {
                foreach (var c in ((Panel)control).Children)
                {
                    if (c is DropZone)
                    {
                        action((DropZone)c);
                    }
                    else
                    {
                        ActivateDropZonesRecursive(c, action);
                    }
                }
            }
            if (control is UserControl)
            {
                if (control is DropZone)
                {
                    action((DropZone)control);
                }
                else if (((UserControl)control).Content != null)
                {
                    ActivateDropZonesRecursive(((UserControl)control).Content, action);
                }
            }
        }

        private void ViewModel_LayoutingItemDragStop()
        {
            //var output = BootStrapper.Resolve<IConsoleOutputService>();
            //output.PushInfo("Canvas:DragStop");
            ActivateDropZonesRecursive(MasterGrid, (dz) => { dz.DraggingIsInactive(); });
        }

        private void ViewModel_LayoutingItemDragStart()
        {
            //var output = BootStrapper.Resolve<IConsoleOutputService>();
            //output.PushInfo("Canvas:DragStart");
            ActivateDropZonesRecursive(MasterGrid, (dz) => { dz.DraggingIsActive(); });
        }

         private ILayoutingDocumentViewModel ViewModel => (ILayoutingDocumentViewModel)DataContext;
    }
}
