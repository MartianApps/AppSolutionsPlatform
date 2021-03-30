using AppSolutions.Desktop.Designer.Services;
using AppSolutions.Desktop.Designer.UI.DocumentControls.Layouting.Widgets;
using AppSolutions.Desktop.Designer.ViewModels.DocumentControls.Layouting;
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

            var grid = new Grid 
            { 
                Height = 400,
                Width = 600
            };
            //grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(0, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });

            var dropZone = new DropZone();

            Grid.SetRow(dropZone, 0);
            //Grid.SetColumn(dropZone, 0);
            grid.Children.Add(dropZone);

            Canvas.Children.Add(grid);
        }

        private void LayoutEditorCanvas_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (ViewModel != null)
            {
                ViewModel.LayoutingItemDragStart += ViewModel_LayoutingItemDragStart;
                ViewModel.LayoutingItemDragStop += ViewModel_LayoutingItemDragStop;
            }
        }

        private void ActivateDropZonesRecursive(Panel panel, Action<DropZone> action)
        {
            foreach (var c in panel.Children)
            {
                if (c is DropZone)
                {
                    action((DropZone)c);
                }
                else
                {
                    if (c is Panel)
                    {
                        ActivateDropZonesRecursive((Panel)c, action);
                    }
                }
            }
        }

        private void ViewModel_LayoutingItemDragStop()
        {
            var output = BootStrapper.Resolve<IConsoleOutputService>();
            output.PushInfo("Canvas:DragStop");
            ActivateDropZonesRecursive(Canvas, (dz) => { dz.DraggingIsInactive(); });
        }

        private void ViewModel_LayoutingItemDragStart()
        {
            var output = BootStrapper.Resolve<IConsoleOutputService>();
            output.PushInfo("Canvas:DragStart");
            ActivateDropZonesRecursive(Canvas, (dz) => { dz.DraggingIsActive(); });
        }

        private ILayoutingDocumentViewModel ViewModel => (ILayoutingDocumentViewModel)DataContext;
    }
}
