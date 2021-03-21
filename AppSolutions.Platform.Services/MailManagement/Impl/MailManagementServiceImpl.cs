using AppSolutions.Platform.Models.Common;
using AppSolutions.Platform.Models.MailManagement;
using AppSolutions.Platform.Services.DataRepository;
using NLog;
using Scriban;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using NetMail = System.Net.Mail;

namespace AppSolutions.Platform.Services.MailManagement.Impl
{
    public class MailManagementServiceImpl : IMailManagementService
    {
        private Logger _logger = LogManager.GetCurrentClassLogger();
        private IMailManagementDataRepositoryService _dataService;

        public MailManagementServiceImpl(IMailManagementDataRepositoryService dataService)
        {
            _dataService = dataService;
        }

        public MailServiceOperationResponse SendMail(MailServerConfiguration server, MailMessage message)
        {
            ArgumentCheck.IsNotNull(server, $"{nameof(server)} must not be null");
            ArgumentCheck.IsNotNullOrEmpty(server.Hostname, $"{nameof(server.Hostname)} must not be null or empty");
            ArgumentCheck.IsNotNullOrEmpty(server.From, $"{nameof(server.From)} must not be null or empty");

            ArgumentCheck.IsNotNull(message, $"{nameof(message)} must not be null");
            ArgumentCheck.IsNotNull(message.To, $"{nameof(message.To)} must not be null");
            ArgumentCheck.IsTrue(message.To.Count > 0, $"{nameof(message.To)} must contain at least 1 address");
            ArgumentCheck.IsFalse(message.To.Any(o => string.IsNullOrEmpty(o.Address)), $"{nameof(MailAddress.Address)} fields must not be null or empty");
            ArgumentCheck.IsNotNullOrEmpty(message.Body, $"{nameof(message.Body)}");
            ArgumentCheck.IsNotNullOrEmpty(message.Subject, $"{nameof(message.Subject)}");

            try
            {
                NetMail.MailMessage netMessage = new NetMail.MailMessage();

                // TO ##########################
                if (message.From == null)
                {
                    netMessage.From = new NetMail.MailAddress(message.From.Address);
                }
                else
                {
                    netMessage.From = new NetMail.MailAddress(server.From);
                }

                // TO ##########################
                foreach (var adr in message.To)
                {
                    if (string.IsNullOrEmpty(adr.Address))
                    {
                        continue;
                    }
                    NetMail.MailAddress address = new NetMail.MailAddress(adr.Address);
                    if (!string.IsNullOrEmpty(adr.DisplayName))
                    {
                        address = new NetMail.MailAddress(adr.Address, adr.DisplayName);
                    }
                    netMessage.To.Add(address);
                }

                // CC ##########################
                if (message.CC != null)
                {
                    foreach (var adr in message.CC)
                    {
                        if (string.IsNullOrEmpty(adr.Address))
                        {
                            continue;
                        }
                        NetMail.MailAddress address = new NetMail.MailAddress(adr.Address);
                        if (!string.IsNullOrEmpty(adr.DisplayName))
                        {
                            address = new NetMail.MailAddress(adr.Address, adr.DisplayName);
                        }
                        netMessage.CC.Add(address);
                    }
                }

                // BCC ##########################
                if (message.Bcc != null)
                {
                    foreach (var adr in message.Bcc)
                    {
                        if (string.IsNullOrEmpty(adr.Address))
                        {
                            continue;
                        }
                        NetMail.MailAddress address = new NetMail.MailAddress(adr.Address);
                        if (!string.IsNullOrEmpty(adr.DisplayName))
                        {
                            address = new NetMail.MailAddress(adr.Address, adr.DisplayName);
                        }
                        netMessage.Bcc.Add(address);
                    }
                }

                // ################################
                netMessage.Subject = message.Subject;
                netMessage.Body = message.Body;
                netMessage.IsBodyHtml = message.IsBodyHtml;

                // ################################
                using (NetMail.SmtpClient client = CreateClient(server))
                {
                    client.Send(netMessage);
                }
                _logger.Info($"Mail sent: '{message.Subject}' to '{string.Join(';', message.To.Select(o => o.Address).ToList())}'");
                return MailServiceOperationResponse.CreateSuccess();
            }
            catch (Exception e)
            {
                _logger.Error(e);
                return MailServiceOperationResponse.CreateInternalServerError(e);
            }
        }

        /// <summary>
        /// https://github.com/scriban/scriban
        /// </summary>
        /// <param name="registeredClientId"></param>
        /// <param name="customer"></param>
        /// <param name="name"></param>
        /// <param name="language"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public MailTemplateDetail RenderMailContent(string registeredClientId, string customer, string name, string language, object parameters)
        {
            ArgumentCheck.IsNotNullOrEmpty(registeredClientId, nameof(registeredClientId));
            ArgumentCheck.IsNotNullOrEmpty(customer, nameof(customer));
            ArgumentCheck.IsNotNullOrEmpty(name, nameof(name));
            ArgumentCheck.IsNotNullOrEmpty(language, nameof(language));

            if (parameters == null)
            {
                parameters = new object();
            }

            var header = _dataService.GetMailTemplateByName(registeredClientId, customer, name);
            ArgumentCheck.IsTrue(header != null, $"template header not found");

            var template = header.DetailTemplates.FirstOrDefault(o => o.Language.Equals(language, StringComparison.InvariantCultureIgnoreCase));
            ArgumentCheck.IsTrue(template != null, $"template not found");

            var subjectTemplate = Template.Parse(template.Subject);
            var bodyTemplate = Template.Parse(template.Body);

            return new MailTemplateDetail
            {
                MailTemplateHeaderId = template.MailTemplateHeaderId,
                MailTemplateDetailId = template.MailTemplateDetailId,
                Language = template.Language,
                IsBodyHtml = template.IsBodyHtml,
                Subject = subjectTemplate.Render(parameters),
                Body = bodyTemplate.Render(parameters)
            };
        }

        private NetMail.SmtpClient CreateClient(MailServerConfiguration config)
        {
            NetMail.SmtpClient client;
            if (config.Port > 0)
            {
                client = new NetMail.SmtpClient(config.Hostname, config.Port);
            }
            else
            {
                client = new NetMail.SmtpClient(config.Hostname);
            }

            if (!string.IsNullOrEmpty(config.Username) && !string.IsNullOrEmpty(config.Password))
            {
                client.Credentials = new NetworkCredential(config.Username, config.Password);
            }
            client.EnableSsl = config.EnableSsl;

            return client;
        }
    }
}
