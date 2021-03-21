using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace AppSolutions.Platform.Models.MailManagement
{
    [DataContract]
    public class MailTemplateDetail
    {
        [DataMember]
        public Guid MailTemplateDetailId { get; set; }

        [DataMember]
        public Guid MailTemplateHeaderId { get; set; }

        [DataMember]
        public string Language { get; set; }

        [DataMember]
        public string Subject { get; set; }

        [DataMember]
        public string Body { get; set; }

        [DataMember]
        public bool IsBodyHtml { get; set; }

        [DataMember]
        public DateTime CreationDate { get; set; }

        [DataMember]
        public string CreationUser { get; set; }

        [DataMember]
        public long CreationTimestamp { get; set; }

        [DataMember]
        public DateTime? UpdateDate { get; set; }

        [DataMember]
        public string UpdateUser { get; set; }

        [DataMember]
        public long UpdateTimestamp { get; set; }
    }
}
