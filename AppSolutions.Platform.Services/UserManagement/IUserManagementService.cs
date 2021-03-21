using AppSolutions.Platform.Models.Common;
using AppSolutions.Platform.Models.UserManagement;
using System;

namespace AppSolutions.Platform.Services.UserManagement
{
    public interface IUserManagementService
    {
        string CreateClient(RegisteredClient client);

        User CreateUser(WorkContext workContext, User user);
    }
}
