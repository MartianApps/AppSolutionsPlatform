using AppSolutions.Platform.Models.Commands;
using AppSolutions.Platform.Models.Projects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSolutions.Desktop.Designer.Services.ProjectService.Commands
{
    public class DeleteDocumentCommand : AbstractBaseCommand
    {
        private IConsoleOutputService _outputService;
        private string _projectFilePath;
        private string _parentSubPath;
        private string _fileName;
        private string _fileContent;

        public static ICommand Create(IConsoleOutputService outputService, string projectFilePath, string parentSubPath, string fileName)
        {
            var cmd = new DeleteDocumentCommand();
            cmd._outputService = outputService;
            cmd._projectFilePath = projectFilePath;
            cmd._parentSubPath = parentSubPath;
            cmd._fileName = fileName;

            return cmd;
        }

        private string FilePath
        {
            get
            {
                return Path.Combine(Path.GetDirectoryName(_projectFilePath), _parentSubPath, _fileName);
            }
        }

        public override void Execute()
        {
            _fileContent = File.ReadAllText(FilePath);

            File.Delete(FilePath);

            _outputService.PushInfo($"File '{Path.Combine(_parentSubPath, _fileName)}' deleted");
        }

        public override void Undo()
        {
            File.WriteAllText(FilePath, _fileContent);

            _outputService.PushInfo($"UNDO File '{Path.Combine(_parentSubPath, _fileName)}' deleted");
        }
    }
}
