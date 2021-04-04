using AppSolutions.Desktop.Designer.ViewModels.DocumentControls.Layouting.Widgets;
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

namespace AppSolutions.Desktop.Designer.UI.DocumentControls.Layouting.Widgets
{
    /// <summary>
    /// Interaktionslogik für LayoutingCanvasGrid.xaml
    /// </summary>
    public partial class ContainerWidget : UserControl, IView
    {
        public ContainerWidget()
        {
            InitializeComponent();

            DataContextChanged += ContainerWidget_DataContextChanged;
        }

        private void ContainerWidget_DataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            GenerateUI();
        }

        private void GenerateUI()
        {
            _grid.RowDefinitions.Clear();
            _grid.Children.Clear();

            if (ViewModel != null && ViewModel.Children != null && ViewModel.Children.Count > 0)
            {
                var rowCounter = 0;
                var orderedList = ViewModel.Children.OrderBy(o => o.Row).ToList();
                for (int i=0; i < orderedList.Count; i++)
                {
                    // Drop Zone
                    AppendDropZone(orderedList[i].Row, rowCounter);
                    rowCounter++;

                    // Widget
                    if (orderedList[i].WidgetViewModel is ILabelWidgetViewModel)
                    {
                        AppendControl<LabelWidgetControl>(orderedList[i].WidgetViewModel, rowCounter);
                    }
                    if (orderedList[i].WidgetViewModel is ITextWidgetViewModel)
                    {
                        AppendControl<TextWidgetControl>(orderedList[i].WidgetViewModel, rowCounter);
                    }

                    rowCounter++;
                }

                // abschließende Drop Zone
                AppendFinalDropZone(rowCounter);
            }
            else
            {
                // Keine definierten Kinder? Dann eine Dropzone einrichten
                AppendFinalDropZone(0);
            }
        }

        private void AppendControl<TControl>(object viewModel, int rowCounter) where TControl: UserControl
        {
            _grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) });
            var control = BootStrapper.Resolve<TControl>();
            control.DataContext = viewModel;
            Grid.SetRow(control, rowCounter);
            _grid.Children.Add(control);
        }

        private void AppendFinalDropZone(int rowCounter)
        {
            AppendDropZone(-1, rowCounter, GridUnitType.Star);
        }

        private void AppendDropZone(int insertRow, int rowCounter, GridUnitType rowConsumption = GridUnitType.Pixel)
        {
            if (rowConsumption == GridUnitType.Star)
            {
                _grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, rowConsumption) });
            }
            else
            {
                _grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(20, GridUnitType.Pixel) });
            }
            var dropZone = BootStrapper.Resolve<DropZone>();
            dropZone.ViewModel.InsertRow = insertRow;
            dropZone.WidgetDropped += DropZone_WidgetDropped;
            Grid.SetRow(dropZone, rowCounter);
            _grid.Children.Add(dropZone);
        }

        //public ContainerWidget(IContainerViewModel viewModel)
        //{
        //    InitializeComponent();

        //    DataContext = viewModel;

        //    _grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });

        //    var dropZone = BootStrapper.Resolve<DropZone>();
        //    dropZone.WidgetDropped += DropZone_WidgetDropped;
        //    Grid.SetRow(dropZone, 0);
        //    _grid.Children.Add(dropZone);
        //}

        private IContainerViewModel ViewModel => (IContainerViewModel)DataContext;


        private void DropZone_WidgetDropped(DropZone d, ViewModels.DocumentControls.Layouting.LayoutWidgetType type)
        {
            ViewModel.InsertWidget(d.ViewModel.InsertRow, type);

            GenerateUI();

            //for (int i = 0; i < _grid.Children.Count; i++)
            //{
            //    if (_grid.Children[i] == d)
            //    {
            //        // Dropzone entfernen
            //        _grid.Children.RemoveAt(i);
            //        // Widget einfügen
            //        switch (type)
            //        {
            //            case ViewModels.DocumentControls.Layouting.LayoutWidgetType.Container:
            //                _grid.Children.Insert(i, BootStrapper.Resolve<ContainerWidget>());
            //                break;
            //            default:
            //                break;
            //        }
            //        // Davor eine Dropzone einfügen (falls es noch keine ist)
            //        var previous = i - 1;
            //        var current = i;
            //        if (previous < 0)
            //        {
            //            previous = 0;
            //        }
            //        if (!(_grid.Children[previous] is DropZone))
            //        {
            //            var dropZone = BootStrapper.Resolve<DropZone>();
            //            _grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });

            //            dropZone.WidgetDropped += DropZone_WidgetDropped;
            //            _grid.Children.Insert(previous, dropZone);
            //            current = previous + 1;
            //        }
            //        var next = current + 1;
            //        if (_grid.Children.Count > next)
            //        {
            //            if (!(_grid.Children[next] is DropZone))
            //            {
            //                var dropZone = BootStrapper.Resolve<DropZone>();
            //                _grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });

            //                dropZone.WidgetDropped += DropZone_WidgetDropped;
            //                _grid.Children.Insert(next, dropZone);
            //            }
            //        }
            //        else
            //        {
            //            var dropZone = BootStrapper.Resolve<DropZone>();
            //            _grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });

            //            dropZone.WidgetDropped += DropZone_WidgetDropped;
            //            _grid.Children.Add(dropZone);
            //        }

            //        break;
            //    }
            //}

            //for (int i = 0; i < _grid.Children.Count; i++)
            //{
            //    Grid.SetRow(_grid.Children[i], i);
            //}
        }

        //public LayoutingCanvasGrid(ILayoutingCanvasGrid viewModel)
        //{
        //    InitializeComponent();

        //    DataContext = viewModel;

        //    _grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });

        //    var dropZone = new DropZone();

        //    Grid.SetRow(dropZone, 0);
        //    _grid.Children.Add(dropZone);
        //}
    }
}
