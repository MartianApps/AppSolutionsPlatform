using AppSolutions.Desktop.Designer.Helpers;
using AppSolutions.Desktop.Designer.Services;
using AppSolutions.Platform.Models.Projects;
using NLog;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using Telerik.Windows.Controls;

namespace AppSolutions.Desktop.Designer.ViewModels
{
    public class ProjectExplorerViewModel : AbstractBaseViewModel, IProjectExplorerViewModel
    {
        private Logger _logger = LogManager.GetCurrentClassLogger();
        private IProjectService _projectService;
        private IMessageService _messageService;

        public ProjectExplorerViewModel(IProjectService projectService, IMessageService messageService)
        {
            _messageService = messageService;

            _projectService = projectService;
            _projectService.ProjectOpened += ProjectService_ProjectOpened;
            _projectService.ProjectItemChanged += ProjectService_ProjectItemChanged;

            RenameCommand = new DelegateCommand((o) => { OnRenameSelectedItem(); });
            AddFolderCommand = new DelegateCommand((o) => { OnAddFolder(); });
            AddPageCommand = new DelegateCommand((o) => { OnAddPage(); });
            AddWorkflowCommand = new DelegateCommand((o) => { OnAddWorkflow(); });
            DeleteCommand = new DelegateCommand((o) => { OnDeleteSelectedItem(); });
        }

        private void ProjectService_ProjectItemChanged(ProjectItemChangedArgs args)
        {
            if (args.ItemType == ProjectItemType.Module && args.Change == ProjectItemChange.Rename)
            {
                foreach (var i in Items)
                {
                    if (i.Title == args.ItemNameOld)
                    {
                        i.Title = args.ItemName;
                    }
                }
            }
            if (args.ItemType == ProjectItemType.Folder && args.Change == ProjectItemChange.Create)
            {
                var parentItem = GetItemRecursive(Items, args.ParentSubPath.Split('\\'), 0);
                var folderItem = new ProjectItemViewModel
                {
                    Id = Guid.NewGuid(),
                    Type = ProjectItemType.Folder,
                    Title = args.ItemName,
                    Parent = parentItem,
                };
                InsertFolderAlphabetically(parentItem.SubItems, folderItem);
            }
            if (args.ItemType == ProjectItemType.Folder && args.Change == ProjectItemChange.Rename)
            {
                var parentItem = GetItemRecursive(Items, args.ParentSubPath.Split('\\'), 0);
                var folderItem = parentItem.SubItems.FirstOrDefault(o => o.Title == args.ItemNameOld);
                parentItem.SubItems.Remove(folderItem);
                folderItem.Title = args.ItemName;
                InsertFolderAlphabetically(parentItem.SubItems, folderItem);
            }
            if ((args.ItemType == ProjectItemType.Page || args.ItemType == ProjectItemType.Workflow) && args.Change == ProjectItemChange.Create)
            {
                var parentItem = GetItemRecursive(Items, args.ParentSubPath.Split('\\'), 0);
                var documentItem = new ProjectItemViewModel
                {
                    Id = Guid.NewGuid(),
                    Type = args.ItemType,
                    Title = args.ItemName,
                    Parent = parentItem,
                };
                InsertDocumentAlphabetically(parentItem.SubItems, documentItem);
            }
            if ((args.ItemType == ProjectItemType.Page || args.ItemType == ProjectItemType.Workflow) && args.Change == ProjectItemChange.Delete)
            {
                var parentItem = GetItemRecursive(Items, args.ParentSubPath.Split('\\'), 0);
                var documentItem = parentItem.SubItems.FirstOrDefault(o => o.Title == args.ItemName && o.Type == args.ItemType);
                parentItem.SubItems.Remove(documentItem);
            }
        }

        private void InsertFolderAlphabetically(ObservableCollection<IProjectItemViewModel> items, IProjectItemViewModel folderItem)
        {
            var insertIndex = -1;
            for (int i = 0; i < items.Count; i++)
            {
                var item = items[i];
                if (item.Type != ProjectItemType.Folder && item.Type != ProjectItemType.Module)
                {
                    insertIndex = i;
                    break;
                }
                if (string.Compare(item.Title, folderItem.Title, StringComparison.CurrentCulture) > 0)
                {
                    insertIndex = i;
                    break;
                }
            }
            if (insertIndex == -1)
            {
                items.Add(folderItem);
            }
            else
            {
                items.Insert(insertIndex, folderItem);
            }
        }

        private void InsertDocumentAlphabetically(ObservableCollection<IProjectItemViewModel> items, IProjectItemViewModel documentItem)
        {
            var insertIndex = -1;
            for (int i = 0; i < items.Count; i++)
            {
                var item = items[i];
                if (item.Type == ProjectItemType.Folder || item.Type == ProjectItemType.Module)
                {
                    continue;
                }
                if (string.Compare(item.Title, documentItem.Title, StringComparison.CurrentCulture) > 0)
                {
                    insertIndex = i;
                    break;
                }
            }
            if (insertIndex == -1)
            {
                items.Add(documentItem);
            }
            else
            {
                items.Insert(insertIndex, documentItem);
            }
        }

        private IProjectItemViewModel GetItemRecursive(ObservableCollection<IProjectItemViewModel> items, string[] path, int index)
        {
            var title = path[index];
            foreach (var i in items)
            {
                if (i.Title == title)
                {
                    if (index == path.Length-1)
                    {
                        return i;
                    }
                    else if (i.Type == ProjectItemType.Module || i.Type == ProjectItemType.Folder)
                    {
                        return GetItemRecursive(i.SubItems, path, index + 1);
                    }
                }
            }
            return null;
        }

        private void ProjectService_ProjectOpened(Project project)
        {
            Items.Clear();
            Items.Add(new ProjectItemViewModel { Id = project.Id, Title = $"Project '{project.Name}'", Type = ProjectItemType.Project });
            foreach (var m in project.Modules)
            {
                var moduleItem = new ProjectItemViewModel { Id = Guid.NewGuid(), Title = m.Name, Type = ProjectItemType.Module };
                Items.Add(moduleItem);
                RecursiveAppendProjectItems(moduleItem, Path.GetDirectoryName(_projectService.ProjectFilePath));
            }
            ProjectName = project.Name;
            ProjectIsLoaded = true;

            OnPropertyChanged(nameof(ProjectIsLoaded));
        }

        private void RecursiveAppendProjectItems(ProjectItemViewModel currentItem, string parentPath)
        {
            var currentPath = Path.Combine(parentPath, currentItem.Title);

            if (currentItem.SubItems == null)
            {
                currentItem.SubItems = new ObservableCollection<IProjectItemViewModel>();
            }

            // Alle Ordner darstellen
            foreach (var dirName in Directory.GetDirectories(currentPath).Select(dir => {
                var info = new DirectoryInfo(dir);
                return info.Name;
            }).OrderBy(dirName => dirName))
            {
                var folderItem = new ProjectItemViewModel
                {
                    Id = Guid.NewGuid(),
                    Type = ProjectItemType.Folder,
                    Title = dirName,
                    Parent = currentItem,
                };
                currentItem.SubItems.Add(folderItem);
                RecursiveAppendProjectItems(folderItem, currentPath);
            }

            // Alle Dateien die zum Projekt gehören
            foreach (var file in Directory.GetFiles(currentPath).Select(o => Path.GetFileName(o)).OrderBy(fileName => fileName))
            {
                if (file.EndsWith(Constants.ProjectItemFileExtensions.Page))
                {
                    currentItem.SubItems.Add(new ProjectItemViewModel 
                    {
                        Id = Guid.NewGuid(),
                        Type = ProjectItemType.Page,
                        Title = file.Split('.')[0],
                        Parent = currentItem,
                    });
                }
                if (file.EndsWith(Constants.ProjectItemFileExtensions.Workflow))
                {
                    currentItem.SubItems.Add(new ProjectItemViewModel
                    {
                        Id = Guid.NewGuid(),
                        Type = ProjectItemType.Workflow,
                        Title = file.Split('.')[0],
                        Parent = currentItem,
                    });
                }
            }
        }

        public string ProjectName { get; set; }

        public ObservableCollection<IProjectItemViewModel> Items { get; set; } = new ObservableCollection<IProjectItemViewModel>();

        public IProjectItemViewModel SelectedItem { get; set; }

        public bool CreateNewProjectButtonIsEnabled
        {
            get
            {
                return !string.IsNullOrEmpty(ProjectName);
            }
        }

        public bool ProjectIsLoaded { get; set; }

        #region Commands

        public DelegateCommand RenameCommand { get; set; }

        public DelegateCommand AddFolderCommand { get; set; }

        public DelegateCommand AddPageCommand { get; set; }

        public DelegateCommand AddWorkflowCommand { get; set; }

        public DelegateCommand DeleteCommand { get; set; }

        #endregion Commands

        #region ContextMenu Handlers

        private void OnDeleteSelectedItem()
        {
            if (SelectedItem.Type == ProjectItemType.Page || SelectedItem.Type == ProjectItemType.Workflow)
            {
                _projectService.DeleteDocument(SelectedItem.ParentSubPath, SelectedItem.Type, SelectedItem.Title);
            }
        }

        private void OnRenameSelectedItem()
        {
            if (SelectedItem.Type == ProjectItemType.Module)
            {
                _projectService.RenameModule(SelectedItem.Title);
            }
            if (SelectedItem.Type == ProjectItemType.Folder)
            {
                _messageService.GetValueFromUser("Rename Folder", "new name of folder", SelectedItem.Title, (folderName, newFolderName) => 
                {
                    _projectService.RenameFolder(SelectedItem.ParentSubPath, folderName, newFolderName);
                });
            }
        }

        private void OnAddFolder()
        {
            _messageService.GetValueFromUser("Add Folder", "Specify name of new folder", (newFolderName) => 
            {
                _projectService.AddFolder(SelectedItem.FullSubPath, newFolderName);
            });            
        }

        private void OnAddPage()
        {
            _messageService.GetValueFromUser("Add Page", "Specify name of new page", (newPageName) =>
            {
                _projectService.AddPage(SelectedItem.FullSubPath, newPageName);
            });
        }

        private void OnAddWorkflow()
        {
            _messageService.GetValueFromUser("Add Workflow", "Specify name of new workflow", (newWorkflowName) =>
            {
                _projectService.AddWorkflow(SelectedItem.FullSubPath, newWorkflowName);
            });
        }

        #endregion ContextMenu Handlers

        public void CreateNewProject(string name, string folder)
        {
            var projectFilePath = _projectService.CreateEmptyProject(name, folder);
            _projectService.LoadProjectFromDisk(projectFilePath);
        }
        
    }
}
