using AppSolutions.Platform.Models.MailManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSolutions.Platform.Services.MailManagement
{
    public interface IMailManagementService
    {
        MailServiceOperationResponse SendMail(MailServerConfiguration server, MailMessage message);

        MailTemplateDetail RenderMailContent(string registeredClientId, string customer, string name, string language, object parameters);
    }
}
