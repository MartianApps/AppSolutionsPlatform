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
    public interface ITextWidgetViewModel : IWidgetViewModel
    {
        string Text { get; set; }
    }

    public class TextWidgetViewModel : AbstractWidgetViewModel<TextWidget>, ITextWidgetViewModel
    {
        public TextWidgetViewModel(IDocumentEventing docEventing, ICommandProcessor cmdProcessor, TextWidget model)
            : base(LayoutWidgetType.Text, docEventing, cmdProcessor, model)
        {
        }

        [Description("Text of input element.")]
        [Display(Name = "Text", GroupName = "General", Order = 2)]
        public string Text
        {
            get
            {
                return _model.Text;
            }
            set
            {
                Execute(PropertyChangeCommand.Create(nameof(TextWidget.Text), value, _model));
            }
        }
    }
}
