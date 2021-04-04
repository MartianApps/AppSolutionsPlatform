using AppSolutions.Platform.Models.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSolutions.Platform.Services.Commands
{
    public interface ICommandProcessor
    {
        void Clear();

        void Execute(ICommand command);

        void Undo();

        void Redo();
    }
}
