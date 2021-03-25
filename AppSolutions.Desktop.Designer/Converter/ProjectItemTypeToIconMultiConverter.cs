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
    public class ProjectItemTypeToIconMultiConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            var itemType = (ProjectItemType)values[0];
            var isExpanded = (bool)values[1];
            switch (itemType)
            {
                case ProjectItemType.Project:
                    return "Cube";
                case ProjectItemType.Module:
                    return "Cubes";
                case ProjectItemType.Folder:
                    if (isExpanded)
                    { 
                        return "FolderOpen";
                    }
                    else
                    {
                        return "Folder";
                    }
                case ProjectItemType.Workflow:
                    return "Sitemap";
                case ProjectItemType.Page:
                    return "FileOutline";
                default:
                    return "Question-Circle";
            }
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
