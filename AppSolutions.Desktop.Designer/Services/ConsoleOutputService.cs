using NLog;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppSolutions.Desktop.Designer.Services
{
    public class ConsoleOutputService : IConsoleOutputService
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private StringBuilder _content = new StringBuilder();

        public event OutputContentChangedDelegate OutputContentChanged;

        public string Content
        {
            get
            {
                return _content.ToString();
            }
        }

        public void PushError(string message)
        {
            Logger.Error(message);
            _content.AppendLine(message);
            OutputContentChanged?.Invoke();
        }

        public void PushError(Exception e)
        {
            Logger.Error(e);
            _content.AppendLine(e.Message);
            _content.Append(e.StackTrace);
            OutputContentChanged?.Invoke();
        }

        public void PushInfo(string message)
        {
            Logger.Info(message);
            _content.AppendLine(message);
            OutputContentChanged?.Invoke();
        }
    }
}
