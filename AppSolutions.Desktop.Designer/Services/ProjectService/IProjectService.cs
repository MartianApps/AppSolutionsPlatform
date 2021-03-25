using AppSolutions.Platform.Models.Projects;
using System;
using System.Collections.Generic;
using System.Text;

namespace AppSolutions.Desktop.Designer.Services
{
    public delegate void ProjectOpenedDelegate(Project project);

    public delegate void ProjectItemChangedDelegate(ProjectItemChangedArgs args);

    public interface IProjectService: IService
    {
        Project LoadedProject { get; }

        string ProjectFilePath { get; }

        event ProjectOpenedDelegate ProjectOpened;

        event ProjectItemChangedDelegate ProjectItemChanged;

        string CreateEmptyProject(string name, string folder);

        void LoadProjectFromDisk(string projectFilePath);

        void RenameModule(string currentName);

        void RenameFolder(string parentSubPath, string folderName, string newFolderName);

        void AddFolder(string parentSubPath, string newFolderName);

        void AddPage(string parentSubPath, string pageName);

        void AddWorkflow(string parentSubPath, string workflowName);

        void DeleteDocument(string parentSubPath, ProjectItemType type, string documentName);
    }

    public class ProjectItemChangedArgs
    {
        public ProjectItemType ItemType { get; set; }
        public ProjectItemChange Change { get; set; }
        public string ItemName { get; set; }
        public string ItemNameOld { get; set; }

        public string ParentSubPath { get; set; }
    }

    public enum ProjectItemChange
    {
        Create,
        Rename,
        Delete
    }
}
