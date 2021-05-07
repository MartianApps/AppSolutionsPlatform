using AppSolutions.Platform.Models.Common;
using AppSolutions.Platform.Models.MailManagement;
using AppSolutions.Platform.Models.UserManagement;
using AppSolutions.Platform.Services.MailManagement;
using AppSolutions.Platform.Services.UserManagement;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppSolutions.TeamServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisteredClientsController
    {
        private Logger _logger = LogManager.GetCurrentClassLogger();
        private IUserManagementService _userService;
        private IMailManagementService _mailService;

        public RegisteredClientsController(IUserManagementService userService, IMailManagementService mailService)
        {
            _userService = userService;
            _mailService = mailService;
        }

        // POST api/RegisteredClients
        [HttpPost]
        public ClientRegistrationResponse Post([FromBody] ClientRegistrationRequest request)
        {
            try
            {
                _logger.Info("register new client: ");
                _logger.Info(JsonConvert.SerializeObject(request, Formatting.Indented));

                var loginName = $"{request.FirstName[0].ToString().ToLowerInvariant()}.{request.LastName.ToLowerInvariant()}";

                var clientId = _userService.CreateClient(new RegisteredClient 
                {
                    Country = request.Country,
                    State = RegisteredClientState.Pending,
                    CreationUser = loginName,
                    CreationDate = DateTime.Now,
                });

                var workContext = new WorkContext { UserId = Guid.Empty, LoginName = loginName };

                var user = _userService.CreateUser(workContext, new User 
                {
                    RegisteredClientId = clientId,
                    LoginName = loginName,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Email = request.Email,
                    EmailIsValidated = false,
                    Password = request.EncryptedPassword,
                    PasswordLastChangedDate = DateTime.Now,
                    IsActive = true,
                });

                var mailServerConfig = new MailServerConfiguration 
                { 
                    Hostname = "smtp.gmail.com",
                    Port = 587,
                    Username = "manuel.schlestein@gmail.com",
                    Password = "XXXXXXX",
                    From = "manuel.schlestein@gmail.com",
                    EnableSsl = true,
                    IsActive = true
                };

                var renderedMessage = _mailService.RenderMailContent("0000000000", "AppSolutions", "NewClientRegistrationUserMailAddressValidation", "EN", new
                {
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    MailAddress = request.Email,
                    VerificationLink = $"http://localhost:29328/User/EmailVerification/{clientId}/{user.UserId}"
                });

                var message = new MailMessage 
                {
                    From = new MailAddress { Address = mailServerConfig.From },
                    To = new List<MailAddress> { new MailAddress { Address = request.Email } },
                    IsBodyHtml = renderedMessage.IsBodyHtml,
                    Subject = renderedMessage.Subject,
                    Body = renderedMessage.Body,
                };

                _mailService.SendMail(mailServerConfig, message);

                return ClientRegistrationResponse.Success(clientId, request.Email);
            }
            catch (Exception e)
            {
                _logger.Error(e);
                return ClientRegistrationResponse.Error(e);
            }
        }
    }
}
