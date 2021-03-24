using AppSolutions.Desktop.Designer.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace AppSolutions.Desktop.Designer.Converter
{
    public class MessageTypeToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((MessageType)value)
            {
                case MessageType.Exception:
                    return "Red";
                case MessageType.Info:
                    return "Blue";
                case MessageType.Warning:
                    return "Yelow";
                default:
                    return "Blue";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
