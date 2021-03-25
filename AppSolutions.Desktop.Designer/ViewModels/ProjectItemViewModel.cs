using AppSolutions.Platform.Models.Projects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;

namespace AppSolutions.Desktop.Designer.ViewModels
{
    public class ProjectItemViewModel : AbstractBaseViewModel, IProjectItemViewModel
    {
        public ProjectItemViewModel()
        {
            PropertyChanged += ProjectItemViewModel_PropertyChanged;
        }

        private void ProjectItemViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(IsExpanded) || e.PropertyName == nameof(Type))
            {
                SetIcon();
                OnPropertyChanged();
            }
        }

        private void SetIcon()
        {
            switch (Type)
            {
                case ProjectItemType.Project:
                    Icon = "Cube";
                    break;
                case ProjectItemType.Module:
                    Icon = "Cubes";
                    break;
                case ProjectItemType.Folder:
                    Icon = IsExpanded ? "FolderOpen" : "Folder";
                    break;
                case ProjectItemType.Workflow:
                    Icon = "Sitemap";
                    break;
                case ProjectItemType.Page:
                    Icon = "FileOutline";
                    break;
                default:
                    Icon = "Question-Circle";
                    break;
            }
        }

        public Guid Id { get; set; }

        public string Icon { get; set; }

        public string Title { get; set; }

        public bool IsExpanded { get; set; }

        public bool IsSelected { get; set; }

        public ProjectItemType Type { get; set; }

        public IProjectItemViewModel Parent { get; set; }

        public ObservableCollection<IProjectItemViewModel> SubItems { get; set; } = new ObservableCollection<IProjectItemViewModel>();

        public string FullSubPath 
        {
            get 
            {
                if (Parent == null)
                {
                    return Title;
                }
                else
                {
                    return Path.Combine(Parent.FullSubPath, Title);
                }
            }
        }

        public string ParentSubPath
        {
            get
            {
                if (Parent == null)
                {
                    return "";
                }
                else
                {
                    return Parent.FullSubPath;
                }
            }
        }
    }


}
