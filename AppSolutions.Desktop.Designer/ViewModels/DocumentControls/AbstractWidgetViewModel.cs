using AppSolutions.Desktop.Designer.ViewModels.DocumentControls.Layouting;
using AppSolutions.Platform.Models.Commands;
using AppSolutions.Platform.Models.Projects;
using AppSolutions.Platform.Services.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Telerik.Windows.Controls;

namespace AppSolutions.Desktop.Designer.ViewModels.DocumentControls
{
    public interface IWidgetViewModel : ITransientViewModel
    {
        Guid Id { get; set; }

        string Name { get; set; }

        LayoutWidgetType Type { get; set; }

        DelegateCommand MouseDownCommand { get; set; }
    }

    public abstract class AbstractWidgetViewModel<TModel>: AbstractCommandableViewModel where TModel : AbstractWidget 
    {
        protected IDocumentEventing _docEventing;
        protected TModel _model;

        public AbstractWidgetViewModel(LayoutWidgetType type, IDocumentEventing docEventing, ICommandProcessor cmdProcessor, TModel model) : base(cmdProcessor)
        {
            _docEventing = docEventing;
            _model = model;
            Id = model.Id;
            Type = type;

            MouseDownCommand = new DelegateCommand(Canvas_MouseDown);
        }

        [Browsable(false)]
        public DelegateCommand MouseDownCommand { get; set; }

        [Browsable(false)]
        public Guid Id { get; set; }

        [Browsable(false)]
        public LayoutWidgetType Type { get; set; }

        [Description("Name of layouting element.")]
        [Display(Name = "Name", GroupName = "Common", Order = 1)]
        public string Name
        {
            get
            {
                return _model.Name;
            }
            set
            {
                Execute(PropertyChangeCommand.Create(nameof(Layout.Name), value, _model));
            }
        }

        private void Canvas_MouseDown(object obj)
        {
            var e = obj as MouseButtonEventArgs;
            e.Handled = true;
            _docEventing.OnMouseDown(this);
        }
    }
}
