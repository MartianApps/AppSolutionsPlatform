using AppSolutions.Platform.Models.Commands;
using AppSolutions.Platform.Models.Projects;
using AppSolutions.Platform.Services.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSolutions.Desktop.Designer.ViewModels.DocumentControls.Layouting.Widgets
{
    public interface ILabelWidgetViewModel : IWidgetViewModel
    {
        string Caption { get; set; }
    }

    public class LabelWidgetViewModel : AbstractWidgetViewModel<LabelWidget>, ILabelWidgetViewModel
    {
        public LabelWidgetViewModel(IDocumentEventing docEventing, ICommandProcessor cmdProcessor, LabelWidget model)
            : base(LayoutWidgetType.Label, docEventing, cmdProcessor, model)
        {
        }

        [Description("Text of this element.")]
        [Display(Name = "Caption", GroupName = "General", Order = 2)]
        public string Caption
        {
            get
            {
                return _model.Caption;
            }
            set
            {
                Execute(PropertyChangeCommand.Create(nameof(LabelWidget.Caption), value, _model));
            }
        }
    }
}
