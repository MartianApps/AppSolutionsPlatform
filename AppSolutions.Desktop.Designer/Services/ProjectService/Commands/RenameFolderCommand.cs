using AppSolutions.Platform.Models.Commands;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSolutions.Desktop.Designer.Services.ProjectService.Commands
{
    public class RenameFolderCommand : AbstractBaseCommand
    {
        private IConsoleOutputService _outputService;
        private string _projectFilePath;
        private string _parentSubPath;
        private string _newFolderName;
        private string _folderName;

        public static ICommand Create(IConsoleOutputService outputService, string projectFilePath, string parentSubPath, string folderName, string newFolderName)
        {
            var cmd = new RenameFolderCommand();
            cmd._outputService = outputService;
            cmd._projectFilePath = projectFilePath;
            cmd._parentSubPath = parentSubPath;
            cmd._folderName = folderName;
            cmd._newFolderName = newFolderName;

            return cmd;
        }

        private string NewFolderPath
        {
            get
            {
                return Path.Combine(Path.GetDirectoryName(_projectFilePath), _parentSubPath, _newFolderName);
            }
        }

        private string FolderPath
        {
            get
            {
                return Path.Combine(Path.GetDirectoryName(_projectFilePath), _parentSubPath, _folderName);
            }
        }

        public override void Execute()
        {
            // Ordner umbenennen
            Directory.Move(FolderPath, NewFolderPath);

            _outputService.PushInfo($"Folder '{Path.Combine(_parentSubPath, _folderName)}' renamed to '{Path.Combine(_parentSubPath, _newFolderName)}'");
        }

        public override void Undo()
        {
            // Ordner umbenennen zurücknehmen
            Directory.Move(NewFolderPath, FolderPath);

            _outputService.PushInfo($"UNDO Folder '{Path.Combine(_parentSubPath, _folderName)}' renamed to '{Path.Combine(_parentSubPath, _newFolderName)}'");
        }
    }
}
