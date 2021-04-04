using AppSolutions.Desktop.Designer.Services;
using AppSolutions.Desktop.Designer.ViewModels.DocumentControls.Layouting;
using AppSolutions.Desktop.Designer.ViewModels.DocumentControls.Layouting.Widgets;
using AppSolutions.Platform.Models.Projects;
using AppSolutions.Platform.Services.Commands;
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
    public class LayoutingDocumentViewModel : AbstractBaseViewModel, ILayoutingDocumentViewModel, ICommandProcessor
    {
        private IProjectService _projectService;
        private ICommandProcessor _cmdProcessor = new CommandProcessor();

        public LayoutingDocumentViewModel(IProjectService projectService)
        {
            _projectService = projectService;
            LayoutingCanvas = new LayoutingCanvas(this, this);
        }

        public ObservableCollection<ILayoutingToolboxItemViewModel> ToolboxItems { get; set; } = new ObservableCollection<ILayoutingToolboxItemViewModel>();

        public ILayoutingCanvas LayoutingCanvas { get; set; }

        public event LayoutingItemDragStartDelegate LayoutingItemDragStart;

        public event LayoutingItemDragStopDelegate LayoutingItemDragStop;

        public void LoadDocument(ProjectItemType type, string documentPath)
        {
            var layout = _projectService.LoadLayoutDocument(documentPath);

            _cmdProcessor.Clear();

            LayoutingCanvas = new LayoutingCanvas(this, this, layout);
            OnPropertyChanged(nameof(LayoutingCanvas));
        }

        public void CreateToolItems(Visual documentBaseVisual)
        {
            if (ToolboxItems.Count > 0)
            {
                return;
            }
            AddToolboxItem(documentBaseVisual, LayoutWidgetType.Label, "/Resources/Svg/border-none-solid.svg", "Label");
            AddToolboxItem(documentBaseVisual, LayoutWidgetType.Text, "/Resources/Svg/border-none-solid.svg", "Text");

            AddToolboxItem(documentBaseVisual, LayoutWidgetType.Container       , "/Resources/Svg/border-none-solid.svg", "Container");
            AddToolboxItem(documentBaseVisual, LayoutWidgetType.ScrollContainer , "/Resources/Svg/border-none-solid.svg", "Scroll Container");
        }

        private void AddToolboxItem(Visual documentBaseVisual, LayoutWidgetType type, string svgIcon, string caption)
        {
            var item = new LayoutingToolboxItemViewModel
            {
                Type = type,
                SvgIcon = svgIcon,
                Name = caption,
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

        #region IDocumentEventing

        public void OnMouseDown(IViewModel viewModel)
        {
            ViewModelMouseDown?.Invoke(viewModel);
        }

        public event ViewModelMouseDownDelegate ViewModelMouseDown;

        #endregion IDocumentEventing

        #region ICommandProcessor

        public void Execute(Platform.Models.Commands.ICommand command)
        {
            _cmdProcessor.Execute(command);
        }

        public void Undo()
        {
            _cmdProcessor.Undo();
        }

        public void Redo()
        {
            _cmdProcessor.Redo();
        }

        public void Clear()
        {
            _cmdProcessor.Clear();
        }

        #endregion ICommandProcessor
    }
}
