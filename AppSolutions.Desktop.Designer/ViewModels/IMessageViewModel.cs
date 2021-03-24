using System;
using System.Collections.Generic;
using System.Text;

namespace AppSolutions.Desktop.Designer.ViewModels
{
    public interface IMessageViewModel: ITransientViewModel
    {
        MessageType Type { get; }

        string Message { get; }

        Exception Exception { get; }
    }

    public enum MessageType
    {
        Info,
        Exception,
        Warning
    }
}
