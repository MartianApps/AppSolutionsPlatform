using AppSolutions.Platform.Models.Common;
using AppSolutions.Platform.Models.UserManagement;
using AppSolutions.Platform.Services.DataRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSolutions.Platform.Services.UserManagement.Impl
{
    public class UserManagementServiceImpl : IUserManagementService
    {
        private IUserManagementDataRepositoryService _dataService;

        public UserManagementServiceImpl(IUserManagementDataRepositoryService dataService)
        {
            _dataService = dataService;
        }

        #region IUserManagementService

        public string CreateClient(RegisteredClient client)
        {
            var rand = new Random();
            
            client.RegisteredClientId = GenerateClientId(rand.Next(0, 1000000));
            while (_dataService.GetRegisteredClient(client.RegisteredClientId) != null)
            {
                client.RegisteredClientId = GenerateClientId(rand.Next(0, 1000000));
            }

            _dataService.CreateRegisteredClient(client);

            return client.RegisteredClientId;
        }

        public User CreateUser(WorkContext workContext, User user)
        {
            ArgumentCheck.IsNotNull(workContext, nameof(workContext));
            ArgumentCheck.IsNotNullOrEmpty(workContext.LoginName, nameof(workContext.LoginName));
            ArgumentCheck.IsNotNull(user, nameof(user));
            ArgumentCheck.IsNotNullOrEmpty(user.RegisteredClientId, nameof(user.RegisteredClientId));
            ArgumentCheck.IsTrue(_dataService.GetRegisteredClient(user.RegisteredClientId) != null, $"referenced client id '{user.RegisteredClientId}' does not exist");
            ArgumentCheck.IsNotNullOrEmpty(user.LoginName, nameof(user.LoginName));
            ArgumentCheck.IsTrue(_dataService.GetUserByLoginName(user.RegisteredClientId, user.LoginName) == null, $"user with login name '{user.LoginName}' already exists for client '{user.RegisteredClientId}'");
            ArgumentCheck.IsNotNullOrEmpty(user.Password, nameof(user.Password));

            user.UserId = Guid.NewGuid();
            user.CreationDate = DateTime.Now;
            user.CreationUser = workContext.LoginName;
            user.UpdateDate = null;
            user.UpdateUser = null;

            _dataService.CreateUser(user);

            return user;
        }

        #endregion IUserManagementService

        #region private Methoden

        private static readonly char[] Values = {
            'B', 'C', 'D', 'F', 'G', 'H', 'J', 'K', 'L', 'M', 'N', 'P', 'Q', 'R', 'S', 'T', 'V', 'W', 'X', 'Y', 'Z', '2',
            '3', '4', '5', '6', '7', '8', '9'
        };

        private static readonly int ValueCount = Values.Length;

        private string GenerateClientId(int num)
        {
            return FormatNumber(num);
        }

        private static string FormatNumber(int value)
        {

            var digits = new[] { Values[0], Values[0], Values[0], Values[0], Values[0], Values[0], Values[0], Values[0], Values[0], Values[0] };
            var slotCount = digits.Length;

            var current = value;
            var count = 0;
            while (current > 0)
            {
                int rem;
                current = Math.DivRem(current, ValueCount, out rem);
                digits[slotCount - ++count] = Values[rem];
            }

            return new string(digits);
        }

        #endregion private Methoden
    }
}
