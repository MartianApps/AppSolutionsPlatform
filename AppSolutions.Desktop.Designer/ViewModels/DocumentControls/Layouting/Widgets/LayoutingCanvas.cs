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
using System.Windows.Input;
using Telerik.Windows.Controls;

namespace AppSolutions.Desktop.Designer.ViewModels.DocumentControls.Layouting.Widgets
{
    public interface ILayoutingCanvas : IWidgetViewModel
    {
        double DesignHeight { get; set; }

        double DesignWidth { get; set; }

        IContainerViewModel Container { get; set; }
    }

    public class LayoutingCanvas: AbstractWidgetViewModel<Layout>, ILayoutingCanvas
    {
        private Layout _layout;

        public LayoutingCanvas(IDocumentEventing docEventing, ICommandProcessor cmdProcessor) 
            : this(docEventing, cmdProcessor, new Layout { DesignHeight=400, DesignWidth=600, Container = new ContainerWidget() })
        {
        }

        public LayoutingCanvas(IDocumentEventing docEventing, ICommandProcessor cmdProcessor, Layout layout)
            : base(LayoutWidgetType.Layout, docEventing, cmdProcessor, layout)
        {
            _layout = layout;
            Container = new ContainerViewModel(docEventing, cmdProcessor, layout.Container);
        }

        [Browsable(false)]
        public IContainerViewModel Container { get; set; }


        [Description("Canvas height at design time.")]
        [Display(Name = "Design Height", GroupName = "Designer", Order = 2)]
        public double DesignHeight 
        {
            get
            {
                return _layout.DesignHeight;
            }
            set
            {
                Execute(PropertyChangeCommand.Create(nameof(Layout.DesignHeight), value, _layout));
            }
        }

        [Description("Canvas width at design time.")]
        [Display(Name = "Design Width", GroupName = "Designer", Order = 2)]
        public double DesignWidth 
        {
            get
            {
                return _layout.DesignWidth;
            }
            set
            {
                Execute(PropertyChangeCommand.Create(nameof(Layout.DesignWidth), value, _layout));
            }
        }
    }
}
