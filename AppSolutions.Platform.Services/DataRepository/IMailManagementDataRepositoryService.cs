using AppSolutions.Platform.Models.MailManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppSolutions.Platform.Services.DataRepository
{
    public interface IMailManagementDataRepositoryService
    {
        #region MailTemplateHeader

        void CreateMailTemplateHeader(MailTemplateHeader header);

        void UpdateMailTemplateHeader(MailTemplateHeader header);

        MailTemplateHeader GetMailTemplateById(Guid mailTemplateHeaderId);

        MailTemplateHeader GetMailTemplateByName(string registeredClientId, string customer, string name);

        ICollection<MailTemplateHeader> GetMailTemplatesOfClient(string registeredClientId);

        #endregion MailTemplateHeader

        #region MailTemplateDetail

        void CreateMailTemplateDetail(MailTemplateDetail detail);

        void UpdateMailTemplateDetail(MailTemplateDetail detail);

        #endregion MailTemplateDetail
    }
}
