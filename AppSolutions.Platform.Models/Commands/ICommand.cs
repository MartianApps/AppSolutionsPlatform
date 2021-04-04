using System;
using System.Collections.Generic;
using System.Text;

namespace AppSolutions.Platform.Models.Commands
{
    public delegate void ComamndActionDelegate(ICommand cmd);

    public interface ICommand
    {
        Guid Id { get; }

        Guid ModelGuid { get; }

        void InjectModelGuid(Guid modelGuid);

        void Execute();

        void Undo();

        void Redo();

        bool IsMergableWith(ICommand command);

        void MergeWith(ICommand command);

        bool IsCompound { get; }

        bool CanBeCompoundedWith(ICommand command);

        event ComamndActionDelegate CommandExecuted;

        event ComamndActionDelegate CommandUndone;

        event ComamndActionDelegate CommandRedone;
    }
}
