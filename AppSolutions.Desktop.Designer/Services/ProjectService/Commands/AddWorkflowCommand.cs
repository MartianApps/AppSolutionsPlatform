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
    public class AddWorkflowCommand : AbstractBaseCommand
    {
        private IConsoleOutputService _outputService;
        private string _projectFilePath;
        private string _parentSubPath;
        private string _workflowName;
        private string _fileContent;

        public static ICommand Create(IConsoleOutputService outputService, string projectFilePath, string parentSubPath, string workflowName)
        {
            var cmd = new AddWorkflowCommand();
            cmd._outputService = outputService;
            cmd._projectFilePath = projectFilePath;
            cmd._parentSubPath = parentSubPath;
            cmd._workflowName = workflowName;

            return cmd;
        }

        private string FileName
        {
            get
            {
                return $"{_workflowName}.{Constants.ProjectItemFileExtensions.Workflow}";
            }
        }

        private string FilePath
        {
            get
            {
                return Path.Combine(Path.GetDirectoryName(_projectFilePath), _parentSubPath, FileName);
            }
        }

        public override void Execute()
        {
            var workflow = new Workflow
            {
                Id = Guid.NewGuid(),
                Name = _workflowName
            };

            // Page erstellen
            System.Xml.Serialization.XmlSerializer ser = new System.Xml.Serialization.XmlSerializer(typeof(Workflow));

            using (StreamWriter sr = new StreamWriter(FilePath))
            {
                ser.Serialize(sr, workflow);
            }

            _outputService.PushInfo($"Workflow '{Path.Combine(_parentSubPath, FileName)}' created");
        }

        public override void Undo()
        {
            _fileContent = File.ReadAllText(FilePath);

            File.Delete(FilePath);

            _outputService.PushInfo($"UNDO Workflow '{Path.Combine(_parentSubPath, FileName)}' created");
        }

        public override void Redo()
        {
            File.WriteAllText(FilePath, _fileContent);
            
            _outputService.PushInfo($"REDO Workflow '{Path.Combine(_parentSubPath, FileName)}' created");
        }
    }
}
