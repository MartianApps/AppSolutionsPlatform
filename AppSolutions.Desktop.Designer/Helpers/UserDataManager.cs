using AppSolutions.Desktop.Designer.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using Telerik.Windows.Controls;

namespace AppSolutions.Desktop.Designer.Helpers
{
    public class UserDataManager
    {
        private UserDataManager()
        {
            Directory.CreateDirectory(Folder);
            // Prüfen ob Datei vorhanden ist und erstellen falls nicht
            if (!File.Exists(FileName))
            {
                using (System.IO.StreamWriter file = new System.IO.StreamWriter(FileName))
                {
                    file.Write(JsonConvert.SerializeObject(_userData));
                }
            }

            _userData = (UserData)JsonConvert.DeserializeObject(System.IO.File.ReadAllText(FileName), typeof(UserData));
        }

        private void Save()
        {
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(FileName))
            {
                file.Write(JsonConvert.SerializeObject(_userData));
            }
        }

        private UserData _userData = new UserData();

        private string Folder
        {
            get
            {
                return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "MartianApps");
            }
        }

        private string FileName
        {
            get
            {
                return Path.Combine(Folder, "DesignerUserData.json");
            }
        }

        public static UserDataManager Instance { get; } = new UserDataManager();

        public string LastLoginName
        {
            get
            {
                return _userData.LastLoginName;
            }
            set
            {
                _userData.LastLoginName = value;
                Save();
            }
        }

        public void AddUsedProject(Guid id, string path, string name)
        {
            if (_userData.LastUsedProjects == null)
            {
                _userData.LastUsedProjects = new List<UsedProject>();
            }

            var p = _userData.LastUsedProjects.FirstOrDefault(o => o.Id == id);
            if (p == null)
            {
                _userData.LastUsedProjects.Add(new UsedProject
                {
                    Id = id,
                    LastUsedDate = DateTime.Now,
                    Name = name,
                    Path = path
                });
            }
            else
            {
                p.LastUsedDate = DateTime.Now;
            }

            Save();
        }

        public ICollection<UsedProject> LastUsedProjects
        {
            get
            {
                if (_userData.LastUsedProjects == null)
                {
                    return new List<UsedProject>();
                }
                else
                {
                    return _userData.LastUsedProjects;
                }
            }
        }
    }

    public class UserData : INotifyPropertyChanged
    {
        public ICollection<UsedProject> LastUsedProjects { get; set; }

        public string LastLoginName { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }

    public class UsedProject
    {
        public Guid Id { get; set; }

        public DateTime LastUsedDate { get; set; }

        public string Name { get; set; }

        public string Path { get; set; }
    }
}
