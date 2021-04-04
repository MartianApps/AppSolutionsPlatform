using AppSolutions.Platform.Models.Commands;
using AppSolutions.Platform.Models.Projects;
using AppSolutions.Platform.Services.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSolutions.Desktop.Designer.ViewModels.DocumentControls.Layouting.Widgets
{
    public interface IContainerViewModel: IWidgetViewModel
    {
        ObservableCollection<ContainerItemViewModel> Children { get; set; }

        void InsertWidget(int insertRow, LayoutWidgetType widgetType);
    }

    public class ContainerItemViewModel: AbstractBaseViewModel, ITransientViewModel
    {
        public int Row { get; set; }

        public IWidgetViewModel WidgetViewModel { get; set; }
    }

    public class ContainerViewModel: AbstractWidgetViewModel<ContainerWidget>, IContainerViewModel
    {
        public ContainerViewModel(IDocumentEventing docEventing, ICommandProcessor cmdProcessor, ContainerWidget model) 
            : base(LayoutWidgetType.Container, docEventing, cmdProcessor, model)
        {
            UpdateViewModels();
        }

        private void UpdateViewModels()
        {
            Children.Clear();
            if (_model.Items != null && _model.Items.Count > 0)
            {
                foreach (var item in _model.Items)
                {
                    var rowItemViewModel = new ContainerItemViewModel
                    {
                        Row = item.Row
                    };
                    if (item.Widget is LabelWidget)
                    {
                        rowItemViewModel.WidgetViewModel = new LabelWidgetViewModel(_docEventing, _cmdProcessor, item.Widget as LabelWidget);
                    }
                    else if (item.Widget is TextWidget)
                    {
                        rowItemViewModel.WidgetViewModel = new TextWidgetViewModel(_docEventing, _cmdProcessor, item.Widget as TextWidget);
                    }
                    else
                    {
                        throw new ArgumentException($"model type {item.Widget.GetType().Name} unknown");
                    }
                    Children.Add(rowItemViewModel);
                }
            }
        }

        public void InsertWidget(int insertRow, LayoutWidgetType widgetType)
        {
            AbstractWidget widgetModel = null;
            switch (widgetType)
            {
                case LayoutWidgetType.Label:
                    widgetModel = new LabelWidget { Id = Guid.NewGuid() };
                    break;
                case LayoutWidgetType.Text:
                    widgetModel = new TextWidget { Id = Guid.NewGuid() };
                    break;
                case LayoutWidgetType.Container:
                    widgetModel = new ContainerWidget { Id = Guid.NewGuid(), Items = new List<ContainerItem>() };
                    break;
                default:
                    throw new ArgumentException($"InsertWidget for widget {widgetType} not implemented");
            }

            if (insertRow == -1)
            {
                _model.Items.Add(new ContainerItem
                {
                    Row = _model.Items.Count + 1,
                    Widget = widgetModel
                });
            }
            else
            {
                foreach (var item in _model.Items.Where(o => o.Row >= insertRow))
                {
                    item.Row = item.Row + 1;
                }
                _model.Items.Insert(insertRow, new ContainerItem
                {
                    Row = insertRow,
                    Widget = widgetModel
                });
            }

            UpdateViewModels();
        }

        public ObservableCollection<ContainerItemViewModel> Children { get; set; } = new ObservableCollection<ContainerItemViewModel>();
    }
}
