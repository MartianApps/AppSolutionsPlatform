using AppSolutions.Desktop.Designer.Helpers;
using AppSolutions.Desktop.Designer.Services.ProjectService.Commands;
using AppSolutions.Platform.Models.Projects;
using AppSolutions.Platform.Services.Commands;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AppSolutions.Desktop.Designer.Services.ProjectService
{
    public class ProjectServiceImpl : IProjectService
    {
        private Logger _logger = LogManager.GetCurrentClassLogger();
        private Project _project;
        private string _projectFilePath;
        private IConsoleOutputService _outputService;
        private IMessageService _messageService;
        private CommandProcessor _cmdProcessor = new CommandProcessor();

        public ProjectServiceImpl(IConsoleOutputService outputService, IMessageService messageService)
        {
            _outputService = outputService;
            _messageService = messageService;
        }

        public Project LoadedProject => _project;

        public string ProjectFilePath => _projectFilePath;

        public event ProjectOpenedDelegate ProjectOpened;

        public event ProjectItemChangedDelegate ProjectItemChanged;

        public void RenameModule(string currentName)
        {
            _messageService.GetValueFromUser("Rename Module", "new name of module", currentName, DoRenameModule);
        }

        public void RenameFolder(string parentSubPath, string folderName, string newFolderName)
        {
            // Darf noch nicht existieren
            var folderPath = Path.Combine(Path.GetDirectoryName(_projectFilePath), parentSubPath, newFolderName);
            if (Directory.Exists(folderPath))
            {
                throw new ArgumentException($"A folder named '{newFolderName}' already exists in this directory!");
            }

            _cmdProcessor.Execute(RenameFolderCommand.Create(_outputService, _projectFilePath, parentSubPath, folderName, newFolderName));

            ProjectItemChanged?.Invoke(new ProjectItemChangedArgs
            {
                ItemType = ProjectItemType.Folder,
                Change = ProjectItemChange.Rename,
                ItemName = newFolderName,
                ItemNameOld = folderName,
                ParentSubPath = parentSubPath
            });
        }

        private void DoRenameModule(string currentName, string newName)
        {
            _cmdProcessor.Execute(RenameModuleCommand.Create(_outputService, _projectFilePath, currentName, newName, LoadedProject, SaveProject));

            ProjectItemChanged?.Invoke(new ProjectItemChangedArgs 
            {
                ItemType = ProjectItemType.Module,
                Change = ProjectItemChange.Rename,
                ItemName = newName,
                ItemNameOld = currentName
            });
        }

        public void AddFolder(string parentSubPath, string newFolderName)
        {
            // Darf noch nicht existieren
            var folderPath = Path.Combine(Path.GetDirectoryName(_projectFilePath), parentSubPath, newFolderName);
            if (Directory.Exists(folderPath))
            {
                throw new ArgumentException($"A folder named '{newFolderName}' already exists in this directory!");
            }
            
            _cmdProcessor.Execute(AddFolderCommand.Create(_outputService, _projectFilePath, parentSubPath, newFolderName));

            ProjectItemChanged?.Invoke(new ProjectItemChangedArgs
            {
                ItemType = ProjectItemType.Folder,
                Change = ProjectItemChange.Create,
                ItemName = newFolderName,
                ParentSubPath = parentSubPath
            });
        }

        public void AddPage(string parentSubPath, string pageName)
        {
            var fileName = $"{pageName}.{Constants.ProjectItemFileExtensions.Page}";
            var filePath = Path.Combine(Path.GetDirectoryName(_projectFilePath), parentSubPath, fileName);
            if (File.Exists(filePath))
            {
                throw new ArgumentException($"A page named '{fileName}' already exists in this directory!");
            }

            _cmdProcessor.Execute(AddPageCommand.Create(_outputService, _projectFilePath, parentSubPath, pageName));

            ProjectItemChanged?.Invoke(new ProjectItemChangedArgs
            {
                ItemType = ProjectItemType.Page,
                Change = ProjectItemChange.Create,
                ItemName = pageName,
                ParentSubPath = parentSubPath
            });
        }

        public void AddWorkflow(string parentSubPath, string workflowName)
        {
            var fileName = $"{workflowName}.{Constants.ProjectItemFileExtensions.Workflow}";
            var filePath = Path.Combine(Path.GetDirectoryName(_projectFilePath), parentSubPath, fileName);
            if (File.Exists(filePath))
            {
                throw new ArgumentException($"A workflow named '{fileName}' already exists in this directory!");
            }

            _cmdProcessor.Execute(AddWorkflowCommand.Create(_outputService, _projectFilePath, parentSubPath, workflowName));

            ProjectItemChanged?.Invoke(new ProjectItemChangedArgs
            {
                ItemType = ProjectItemType.Workflow,
                Change = ProjectItemChange.Create,
                ItemName = workflowName,
                ParentSubPath = parentSubPath
            });
        }

        public void DeleteDocument(string parentSubPath, ProjectItemType type, string documentName)
        {
            var fileName = ""; 
            if (type == ProjectItemType.Page)
            {
                fileName = $"{documentName}.{Constants.ProjectItemFileExtensions.Page}";
            }
            if (type == ProjectItemType.Workflow)
            {
                fileName = $"{documentName}.{Constants.ProjectItemFileExtensions.Workflow}";
            }
            var filePath = Path.Combine(Path.GetDirectoryName(_projectFilePath), parentSubPath, fileName);

            if (!File.Exists(filePath))
            {
                throw new ArgumentException($"A {type.ToString()} named '{fileName}' does not exist in this directory and cannot be deleted!");
            }

            _cmdProcessor.Execute(DeleteDocumentCommand.Create(_outputService, _projectFilePath, parentSubPath, fileName));

            ProjectItemChanged?.Invoke(new ProjectItemChangedArgs
            {
                ItemType = type,
                Change = ProjectItemChange.Delete,
                ItemName = documentName,
                ParentSubPath = parentSubPath
            });
        }

        public void LoadProjectFromDisk(string projectFilePath)
        {
            _projectFilePath = projectFilePath;

            System.Xml.Serialization.XmlSerializer ser = new System.Xml.Serialization.XmlSerializer(typeof(Project));

            using (StreamReader sr = new StreamReader(_projectFilePath))
            {
                _project = (Project)ser.Deserialize(sr);
            }

            UserDataManager.Instance.AddUsedProject(_project.Id, _projectFilePath, _project.Name);

            ProjectOpened?.Invoke(_project);

            _cmdProcessor = new CommandProcessor();

            _outputService.PushInfo($"Project '{_project.Name}' loaded from:");
            _outputService.PushInfo(_projectFilePath);
        }

        public string CreateEmptyProject(string name, string folder)
        {
            var projectFilePath = Path.Combine(folder, $"{name}.maproj");

            Directory.CreateDirectory(folder);
            var project = new Project
            {
                Id = Guid.NewGuid(),
                Name = name
            };
            project.Modules.Add(new ProjectModule { Name = "DefaultModule" });
            Directory.CreateDirectory(Path.Combine(folder, "DefaultModule"));

            SaveProject(projectFilePath, project);

            _outputService.PushInfo($"New Project '{_project.Name}' created:");
            _outputService.PushInfo(projectFilePath);

            return projectFilePath;
        }

        private void SaveProject(string projectFilePath, Project project)
        {
            System.Xml.Serialization.XmlSerializer ser = new System.Xml.Serialization.XmlSerializer(typeof(Project));

            using (StreamWriter sr = new StreamWriter(projectFilePath))
            {
                ser.Serialize(sr, project);
            }
        }
    }
}
