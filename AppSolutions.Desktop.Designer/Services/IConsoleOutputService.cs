using System;
using System.Collections.Generic;
using System.Text;

namespace AppSolutions.Desktop.Designer.Services
{
    public delegate void OutputContentChangedDelegate();

    public interface IConsoleOutputService: IService
    {
        event OutputContentChangedDelegate OutputContentChanged;

        void PushInfo(string message);

        void PushError(string message);

        void PushError(Exception e);

        string Content { get; }
    }
}
