using System;
using System.Collections.Generic;
using System.Text;

namespace AppSolutions.Desktop.Designer.ViewModels
{
    public class MessageViewModel : AbstractBaseViewModel, IMessageViewModel
    {
        public MessageType Type { get; private set; }

        public string Message { get; private set; }

        public Exception Exception { get; private set; }

        public static IMessageViewModel CreateExceptionMessage(Exception e)
        {
            return new MessageViewModel 
            {
                Type = MessageType.Exception,
                Exception = e,
                Message = e.Message
            };
        }
    }
}
