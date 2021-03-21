using System;
using System.Collections.Generic;
using System.Text;

namespace AppSolutions.Platform.Models.Commands
{
    public interface ICommand
    {
        Guid Id { get; }

        void Execute();

        void Undo();

        void Redo();

        bool IsMergableWith(ICommand command);

        void MergeWith(ICommand command);

        bool IsCompound { get; }

        bool CanBeCompoundedWith(ICommand command);
    }
}
