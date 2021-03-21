using AppSolutions.Platform.Models.UserManagement;
using System;
using System.Collections.Generic;

namespace AppSolutions.Platform.Services.DataRepository
{
    public interface IUserManagementDataRepositoryService
    {
        #region RegisteredClient

        void CreateRegisteredClient(RegisteredClient o);

        void UpdateRegisteredClient(RegisteredClient o);

        RegisteredClient GetRegisteredClient(string registeredClientId);

        ICollection<RegisteredClient> GetRegisteredClients();

        #endregion RegisteredClient

        #region User

        void CreateUser(User o);

        void UpdateUser(User o);

        User GetUserById(Guid userId);

        User GetUserByLoginName(string registeredClientId, string loginName);

        ICollection<User> GetUsersOfClient(string registeredClientId);

        #endregion User
    }
}
