using AppSolutions.Platform.Models.UserManagement;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AppSolutions.Platform.Services.DataRepository.Impl
{
    public class UserManagementFileDataRepository : IUserManagementDataRepositoryService
    {
        private IList<RegisteredClient> _registeredClients = new List<RegisteredClient>() 
        {
            new RegisteredClient
            {
                RegisteredClientId = "0000000000",
                CompanyName = "AppSolutions",
                Country = "DE",
                State = RegisteredClientState.Active,
                CreationDate = DateTime.Now,
                CreationUser = "m.schlestein"
            }
        };
        private IList<User> _users = new List<User>()
        {
            new User
            {
                UserId = Guid.Parse("d1ebadfe-006b-4d23-ad5b-77ae165be439"),
                RegisteredClientId = "0000000000",
                LoginName = "m.schlestein",
                Email = "manuel.schlestein@gmail.com",
                EmailIsValidated = true,
                FirstName = "Manuel",
                LastName = "Schlestein",
                IsActive = true,
                CreationDate = DateTime.Now,
                CreationUser = "m.schlestein"
            }
        };

        public UserManagementFileDataRepository()
        {
            Directory.CreateDirectory(DataDirectory);

            if (!File.Exists(RegisteredClientsFileName) || !File.Exists(UsersFileName))
            {
                SaveData();
            }
            
            var content = File.ReadAllText(RegisteredClientsFileName);            
            _registeredClients = JsonConvert.DeserializeObject<IList<RegisteredClient>>(content);

            content = File.ReadAllText(UsersFileName);
            _users = JsonConvert.DeserializeObject<IList<User>>(content);
        }

        private string DataDirectory
        {
            get
            {
                return Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Data");
            }
        }

        private string RegisteredClientsFileName
        {
            get
            {
                return Path.Combine(DataDirectory, "RegisteredClients.json");
            }
        }

        private string UsersFileName
        {
            get
            {
                return Path.Combine(DataDirectory, "Users.json");
            }
        }

        private void SaveData()
        {
            File.WriteAllText(RegisteredClientsFileName, JsonConvert.SerializeObject(_registeredClients));
            File.WriteAllText(UsersFileName, JsonConvert.SerializeObject(_users));
        }

        public void CreateRegisteredClient(RegisteredClient o)
        {
            if (o == null)
                throw new ArgumentNullException(nameof(o));
            if (string.IsNullOrEmpty(o.RegisteredClientId))
                throw new ArgumentNullException(nameof(o.RegisteredClientId));
            if (string.IsNullOrEmpty(o.CreationUser))
                throw new ArgumentNullException(nameof(o.CreationUser));
            if (_registeredClients.Any(c => c.RegisteredClientId == o.RegisteredClientId))
                throw new ArgumentException($"registered client with ID '{o.RegisteredClientId}' already exists!");

            _registeredClients.Add(o);

            SaveData();
        }

        public RegisteredClient GetRegisteredClient(string registeredClientId)
        {
            return _registeredClients.FirstOrDefault(c => c.RegisteredClientId == registeredClientId);
        }

        public ICollection<RegisteredClient> GetRegisteredClients()
        {
            return _registeredClients.ToList();
        }

        public void UpdateRegisteredClient(RegisteredClient o)
        {
            if (o == null)
                throw new ArgumentNullException(nameof(o));
            if (string.IsNullOrEmpty(o.RegisteredClientId))
                throw new ArgumentNullException(nameof(o.RegisteredClientId));
            if (string.IsNullOrEmpty(o.UpdateUser))
                throw new ArgumentNullException(nameof(o.UpdateUser));
            if (o.UpdateDate == null)
                throw new ArgumentNullException(nameof(o.UpdateDate));

            var client = _registeredClients.FirstOrDefault(c => c.RegisteredClientId == o.RegisteredClientId);
            o.CreationDate = client.CreationDate;
            o.CreationUser = client.CreationUser;
            _registeredClients.Remove(client);
            _registeredClients.Add(o);

            SaveData();
        }

        #region User

        public void CreateUser(User o)
        {
            _users.Add(o);
            SaveData();
        }

        public void UpdateUser(User o)
        {
            var user = _users.FirstOrDefault(u => u.UserId == o.UserId);
            _users.Remove(user);
            _users.Add(o);
            SaveData();
        }

        public User GetUserById(Guid userId)
        {
            return _users.FirstOrDefault(u => u.UserId == userId);
        }

        public User GetUserByLoginName(string registeredClientId, string loginName)
        {
            return _users.FirstOrDefault(u => u.RegisteredClientId == registeredClientId && u.LoginName == loginName);
        }

        public ICollection<User> GetUsersOfClient(string registeredClientId)
        {
            return _users.Where(u => u.RegisteredClientId == registeredClientId).ToList();
        }

        #endregion User
    }
}
