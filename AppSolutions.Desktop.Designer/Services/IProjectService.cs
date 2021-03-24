using AppSolutions.Platform.Models.Projects;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppSolutions.Desktop.Designer.Services
{
    public delegate void ProjectOpenedDelegate(Project project);

    public interface IProjectService: IService
    {
        Project LoadedProject { get; }

        event ProjectOpenedDelegate ProjectOpened;

        string CreateEmptyProject(string name, string folder);

        void LoadProjectFromDisk(string projectFilePath);
    }
}
