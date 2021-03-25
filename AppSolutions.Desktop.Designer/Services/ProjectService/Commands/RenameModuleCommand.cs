using AppSolutions.Platform.Models.Commands;
using AppSolutions.Platform.Models.Projects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AppSolutions.Desktop.Designer.Services.ProjectService.Commands
{
    public class RenameModuleCommand : AbstractBaseCommand
    {
        private IConsoleOutputService _outputService;
        private string _projectFilePath;
        private string _currentName;
        private string _newName;
        private Project _project;
        private Action<string, Project> _saveProjectAction;

        public static ICommand Create(IConsoleOutputService outputService, string projectFilePath, string currentName, string newName, Project project, Action<string,Project> saveProjectAction)
        {
            var cmd = new RenameModuleCommand();
            cmd._project = project;
            cmd._outputService = outputService;
            cmd._projectFilePath = projectFilePath;
            cmd._currentName = currentName;
            cmd._newName = newName;
            cmd._saveProjectAction = saveProjectAction;

            return cmd;
        }

        public override void Execute()
        {
            var rootPath = Path.GetDirectoryName(_projectFilePath);

            // Modul im Projectfile umbenennen
            _project.Modules.FirstOrDefault(o => o.Name == _currentName).Name = _newName;

            // Ordner auf Festplatte umbenennen
            Directory.Move(Path.Combine(rootPath, _currentName), Path.Combine(rootPath, _newName));

            // Speichern der Projektdatei
            _saveProjectAction(_projectFilePath, _project);

            _outputService.PushInfo($"Module '{_currentName}' renamed to '{_newName}'");
        }

        public override void Undo()
        {
            var rootPath = Path.GetDirectoryName(_projectFilePath);

            // Modul im Projectfile umbenennen
            _project.Modules.FirstOrDefault(o => o.Name == _newName).Name = _currentName;

            // Ordner auf Festplatte umbenennen
            Directory.Move(Path.Combine(rootPath, _newName), Path.Combine(rootPath, _currentName));

            // Speichern der Projektdatei
            _saveProjectAction(_projectFilePath, _project);

            _outputService.PushInfo($"UNDO Module '{_currentName}' renamed to '{_newName}'");
        }
    }
}
