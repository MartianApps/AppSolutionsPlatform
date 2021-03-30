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
    public class AddLayoutCommand : AbstractBaseCommand
    {
        private IConsoleOutputService _outputService;
        private string _projectFilePath;
        private string _parentSubPath;
        private string _layoutName;
        private string _fileContent;

        public static AddLayoutCommand Create(IConsoleOutputService outputService, string projectFilePath, string parentSubPath, string layoutName)
        {
            var cmd = new AddLayoutCommand();
            cmd._outputService = outputService;
            cmd._projectFilePath = projectFilePath;
            cmd._parentSubPath = parentSubPath;
            cmd._layoutName = layoutName;

            return cmd;
        }

        private string FileName
        {
            get
            {
                return $"{_layoutName}.{Constants.ProjectItemFileExtensions.Layout}";
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
            var page = new Layout
            {
                Id = Guid.NewGuid(),
                Name = _layoutName
            };

            // Page erstellen
            System.Xml.Serialization.XmlSerializer ser = new System.Xml.Serialization.XmlSerializer(typeof(Layout));

            using (StreamWriter sr = new StreamWriter(FilePath))
            {
                ser.Serialize(sr, page);
            }

            _outputService.PushInfo($"Layout '{Path.Combine(_parentSubPath, FileName)}' created");
        }

        public override void Undo()
        {
            _fileContent = File.ReadAllText(FilePath);

            File.Delete(FilePath);

            _outputService.PushInfo($"UNDO Layout '{Path.Combine(_parentSubPath, FileName)}' created");
        }

        public override void Redo()
        {
            File.WriteAllText(FilePath, _fileContent);
            
            _outputService.PushInfo($"REDO Layout '{Path.Combine(_parentSubPath, FileName)}' created");
        }
    }
}
