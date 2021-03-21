using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace AppSolutions.Platform.Models.MailManagement
{
    [DataContract]
    public class MailTemplateHeader
    {
        [DataMember]
        public Guid MailTemplateHeaderId { get; set; }

        [DataMember]
        public string RegisteredClientId { get; set; }

        [DataMember]
        public string Customer { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public ICollection<MailTemplateDetail> DetailTemplates { get; set; }

        [DataMember]
        public bool IsLocked { get; set; }

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
