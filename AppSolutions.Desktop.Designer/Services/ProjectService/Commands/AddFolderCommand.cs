using AppSolutions.Platform.Models.Commands;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSolutions.Desktop.Designer.Services.ProjectService.Commands
{
    public class AddFolderCommand : AbstractBaseCommand
    {
        private IConsoleOutputService _outputService;
        private string _projectFilePath;
        private string _parentSubPath;
        private string _newFolderName;

        public static ICommand Create(IConsoleOutputService outputService, string projectFilePath, string parentSubPath, string newFolderName)
        {
            var cmd = new AddFolderCommand();
            cmd._outputService = outputService;
            cmd._projectFilePath = projectFilePath;
            cmd._parentSubPath = parentSubPath;
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

        public override void Execute()
        {
            // Ordner erstellen
            Directory.CreateDirectory(NewFolderPath);

            _outputService.PushInfo($"Folder '{Path.Combine(_parentSubPath, _newFolderName)}' created");
        }

        public override void Undo()
        {
            Directory.Delete(NewFolderPath);

            _outputService.PushInfo($"UNDO Folder '{Path.Combine(_parentSubPath, _newFolderName)}' created");
        }
    }
}
