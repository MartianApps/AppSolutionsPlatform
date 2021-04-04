using System;
using System.Collections.Generic;
using System.Text;

namespace AppSolutions.Platform.Models.Commands
{
    public interface ICommandEventing
    {
        void OnCommandExecuted();

        void OnCommandUndone();

        void OnCommandRedone();
    }
}
