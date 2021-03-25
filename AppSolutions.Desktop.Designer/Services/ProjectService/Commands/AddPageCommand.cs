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
    public class AddPageCommand : AbstractBaseCommand
    {
        private IConsoleOutputService _outputService;
        private string _projectFilePath;
        private string _parentSubPath;
        private string _pageName;
        private string _fileContent;

        public static ICommand Create(IConsoleOutputService outputService, string projectFilePath, string parentSubPath, string pageName)
        {
            var cmd = new AddPageCommand();
            cmd._outputService = outputService;
            cmd._projectFilePath = projectFilePath;
            cmd._parentSubPath = parentSubPath;
            cmd._pageName = pageName;

            return cmd;
        }

        private string FileName
        {
            get
            {
                return $"{_pageName}.{Constants.ProjectItemFileExtensions.Page}";
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
            var page = new Page
            {
                Id = Guid.NewGuid(),
                Name = _pageName
            };

            // Page erstellen
            System.Xml.Serialization.XmlSerializer ser = new System.Xml.Serialization.XmlSerializer(typeof(Page));

            using (StreamWriter sr = new StreamWriter(FilePath))
            {
                ser.Serialize(sr, page);
            }

            _outputService.PushInfo($"Page '{Path.Combine(_parentSubPath, FileName)}' created");
        }

        public override void Undo()
        {
            _fileContent = File.ReadAllText(FilePath);

            File.Delete(FilePath);

            _outputService.PushInfo($"UNDO Page '{Path.Combine(_parentSubPath, FileName)}' created");
        }

        public override void Redo()
        {
            File.WriteAllText(FilePath, _fileContent);
            
            _outputService.PushInfo($"REDO Page '{Path.Combine(_parentSubPath, FileName)}' created");
        }
    }
}
