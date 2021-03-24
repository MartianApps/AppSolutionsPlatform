using AppSolutions.Desktop.Designer.Helpers;
using AppSolutions.Platform.Models.Projects;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace AppSolutions.Desktop.Designer.Services
{
    public class ProjectService : IProjectService
    {
        private Logger _logger = LogManager.GetCurrentClassLogger();
        private Project _project;

        public Project LoadedProject => _project;

        public event ProjectOpenedDelegate ProjectOpened;

        public void LoadProjectFromDisk(string projectFilePath)
        {
            try
            {
                System.Xml.Serialization.XmlSerializer ser = new System.Xml.Serialization.XmlSerializer(typeof(Project));

                using (StreamReader sr = new StreamReader(projectFilePath))
                {
                    _project = (Project)ser.Deserialize(sr);
                }

                UserDataManager.Instance.AddUsedProject(_project.Id, projectFilePath, _project.Name);

                ProjectOpened?.Invoke(_project);
            }
            catch (Exception e)
            {
                _logger.Error(e);
            }
        }

        public string CreateEmptyProject(string name, string folder)
        {
            var projectFilePath = Path.Combine(folder, $"{name}.maproj");
            try
            {                

                Directory.CreateDirectory(folder);
                var project = new Project
                {
                    Id = Guid.NewGuid(),
                    Name = name
                };
                project.Modules.Add(new ProjectModule { Name = "DefaultModule" });
                Directory.CreateDirectory(Path.Combine(folder, "DefaultModule"));

                System.Xml.Serialization.XmlSerializer ser = new System.Xml.Serialization.XmlSerializer(typeof(Project));

                using (StreamWriter sr = new StreamWriter(projectFilePath))
                {
                    ser.Serialize(sr, project);
                }
            }
            catch (Exception e)
            {
                _logger.Error(e);
            }
            return projectFilePath;
        }
    }
}
