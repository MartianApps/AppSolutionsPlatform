using AppSolutions.Platform.Models.Commands;
using AppSolutions.Platform.Services.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSolutions.Desktop.Designer.ViewModels
{
    public abstract class AbstractCommandableViewModel : AbstractBaseViewModel
    {
        protected ICommandProcessor _cmdProcessor;

        public AbstractCommandableViewModel(ICommandProcessor cmdProcessor)
        {
            _cmdProcessor = cmdProcessor;
        }

        protected void Execute(PropertyChangeCommand cmd)
        {
            _cmdProcessor.Execute(cmd);
            cmd.CommandExecuted += Cmd_Action;
            cmd.CommandRedone += Cmd_Action;
            cmd.CommandUndone += Cmd_Action;
            OnPropertyChanged(cmd.PropertyName);
        }

        private void Cmd_Action(ICommand cmd)
        {
            OnPropertyChanged(((PropertyChangeCommand)cmd).PropertyName);
        }
    }
}
