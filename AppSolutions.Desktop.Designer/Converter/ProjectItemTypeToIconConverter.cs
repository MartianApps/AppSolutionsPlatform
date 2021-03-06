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
    public class ProjectItemTypeToIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var type = (value as ProjectItemViewModel).Type;
            var isExpanded = (value as ProjectItemViewModel).IsExpanded;
            switch (type)
            {
                case ProjectItemType.Project:
                    return "Cube";
                case ProjectItemType.Module:
                    return "Cubes";
                case ProjectItemType.Folder:
                    return isExpanded ? "FolderOpen" : "Folder";
                case ProjectItemType.Workflow:
                    return "Sitemap";
                case ProjectItemType.Page:
                    return "FileOutline";
                default:
                    return "Question-Circle";
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
