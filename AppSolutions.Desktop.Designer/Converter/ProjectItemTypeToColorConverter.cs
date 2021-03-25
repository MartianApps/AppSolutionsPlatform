using AppSolutions.Desktop.Designer.ViewModels;
using AppSolutions.Platform.Models.Projects;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows;
using System.Windows.Data;

namespace AppSolutions.Desktop.Designer.Converter
{
    public class ProjectItemTypeToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((ProjectItemType)value)
            {
                case ProjectItemType.Project:
                    return "DarkRed";
                case ProjectItemType.Module:
                    return "Black";
                case ProjectItemType.Folder:
                    return "DarkGoldenrod";
                case ProjectItemType.Workflow:
                    return "SkyBlue";
                case ProjectItemType.Page:
                    return "Black";
                default:
                    return "Red";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
